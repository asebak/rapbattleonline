#region Using

using System;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using YAF.Classes;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     Creates a Contact Form
    /// </summary>
    public partial class ContactForm : RapUserControl
    {
        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
        }
        /// <summary>
        /// Determines whether [is post reply delay].
        /// </summary>
        /// <returns></returns>
        private bool IsPostReplyDelay()
        {
            if (this.Get<YafBoardSettings>().PostFloodDelay <= 0)
            {
                return false;
            }
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
        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Click event of the SubmitBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SubmitBtn_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (TitleTxtBox.Value.IsNotSet() || ContentTxtBox.Value.IsNotSet())
            {
                this.PageContext.AddLoadMessage(
                    this.Text("COMMON", "COMMON_MISSING").FormatWith(this.Text("COMMON", "COMMON_CONTENT")),
                    MessageTypes.Error);
                return;
            }
            if (this.IsPostReplyDelay())
            {
                return;
            }
            this.GetCore<ContactMessage>().SubmitContactForm(this.PageContext.PageUserID, TitleTxtBox.Value, ContentTxtBox.Value);
            this.PageContext.AddLoadMessage(this.Text("COMMON", "COMMON_CONTACTUS"), MessageTypes.Success);
        }
    

        #endregion
    }
}