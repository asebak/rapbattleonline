#region Using

using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Factory;
using FreestyleOnline.classes.Providers;
using YAF.Classes;
using YAF.Classes.Data;
using YAF.Core;
using YAF.Core.Services;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Flags;
using YAF.Types.Interfaces;
using YAF.Types.Objects;
using YAF.Utilities;
using YAF.Utils;
using YAF.Utils.Helpers;

#endregion

namespace FreestyleOnline.Controls.Generic
{
    public partial class PostEditor : RapUserControl
    {
        #region Constants and Fields

        public Ext.Net.Button PostBtn { get { return this.PostButton; } }
        public CKEditor.NET.CKEditorControl Editor {get { return this.CkEditor; }}
        #endregion
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.PostBtn.OnClientClick += "return handlepostback({0});".FormatWith(this.PostBtn.ClientID);
            base.OnInit(e);
        }
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.LocalizedLblMaxNumberOfPost.Param0 =
                 this.Get<YafBoardSettings>().MaxPostSize.ToString(CultureInfo.InvariantCulture);
            this.CkEditor = this.GetFactory<TextEditorFactory>().Build(this.CkEditor);
            if (this.IsPostBack && this.Get<HttpSessionStateBase>()["quotedmessage"] != null)
            {
                this.CkEditor.Text += this.Get<HttpSessionStateBase>()["quotedmessage"];
                this.Get<HttpSessionStateBase>().Remove("quotedmessage");
            }

        }
        /// <summary>
        ///     Verifies the user isn't posting too quickly, if so, tells them to wait.
        /// </summary>
        /// <returns>
        ///     True if there is a delay in effect.
        /// </returns>
        private bool IsPostReplyDelay()
        {
            // see if there is a post delay
            if (this.PageContext.IsAdmin || this.PageContext.ForumModeratorAccess
                || this.Get<YafBoardSettings>().PostFloodDelay <= 0)
            {
                return false;
            }
            // see if they've past that delay point
            if (this.Get<IYafSession>().LastPost
                <= DateTime.UtcNow.AddSeconds(-this.Get<YafBoardSettings>().PostFloodDelay))
            {
                return false;
            }

            this.PageContext.AddLoadMessage(
                this.GetTextFormatted(
                    "wait",
                    (this.Get<IYafSession>().LastPost
                     - DateTime.UtcNow.AddSeconds(-this.Get<YafBoardSettings>().PostFloodDelay)).Seconds),
                MessageTypes.Warning);
            return true;
        }

        /// <summary>
        ///     Handles verification of the PostReply. Adds java script message if there is a problem.
        /// </summary>
        /// <returns>
        ///     true if everything is verified
        /// </returns>
        private bool IsPostReplyVerified()
        {
            // To avoid posting whitespace(s) or empty messages
            string postedMessage = this.CkEditor.Text.Trim();

            if (postedMessage.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.GetText("ISEMPTY"), MessageTypes.Warning);
                return false;
            }

            // No need to check whitespace if they are actually posting something
            if (this.Get<YafBoardSettings>().MaxPostSize > 0
                && this.CkEditor.Text.Length >= this.Get<YafBoardSettings>().MaxPostSize)
            {
                this.PageContext.AddLoadMessage(this.GetText("ISEXCEEDED"), MessageTypes.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the post.
        /// </summary>
        public bool ValidPost()
        {
            if (!this.IsPostReplyVerified())
            {
                return false;
            }

            if (this.IsPostReplyDelay())
            {
                return false;
            }

            // Check for SPAM
            if (!this.PageContext.IsAdmin && !this.PageContext.ForumModeratorAccess
                && !this.Get<YafBoardSettings>().SpamServiceType.Equals(0))
            {
                var spamChecker = new YafSpamCheck();
                string spamResult;

                // Check content for spam
                if (
                    spamChecker.CheckPostForSpam(this.PageContext.PageUserName,
                        YafContext.Current.Get<HttpRequestBase>().GetUserRealIPAddress(),
                        BBCodeHelper.StripBBCode(
                            HtmlHelper.StripHtml(HtmlHelper.CleanHtmlString(this.CkEditor.Text)))
                            .RemoveMultipleWhitespace(), this.PageContext.User.Email,
                        out spamResult))
                {
                    if (this.Get<YafBoardSettings>().SpamMessageHandling.Equals(1))
                    {
                        this.Get<ILogger>()
                            .Info(
                                "Spam Check detected possible SPAM ({2}) posted by User: {0}, it was flagged as unapproved post. Content was: {1}",
                                this.PageContext.PageUserName,
                                this.CkEditor.Text,
                                spamResult);
                    }
                    else if (this.Get<YafBoardSettings>().SpamMessageHandling.Equals(2))
                    {
                        this.Get<ILogger>()
                            .Info(
                                "Spam Check detected possible SPAM ({2}) posted by User: {0}, post was rejected. Content was: {1}",
                                this.PageContext.PageUserName,
                                this.CkEditor.Text,
                                spamResult);

                        this.PageContext.AddLoadMessage(this.GetText("SPAM_MESSAGE"), MessageTypes.Error);

                        return false;
                    }
                }
            }
            this.Get<IYafSession>().LastPost = DateTime.UtcNow.AddSeconds(30);
            return true;
        }
    }
}