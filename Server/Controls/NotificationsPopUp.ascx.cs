#region Using

using System;
using System.Web;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Classes;
using YAF.Controls;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class RapPopUp : RapUserControl
    {
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.RegisterPopUpScripts();
            this.GeneratePopUp();
        }

        /// <summary>
        /// Registers the pop up scripts.
        /// </summary>
        private void RegisterPopUpScripts()
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "yafModalScript", "~/forum/resources/js/jquery.yafmodaldialog.js", true);
            this.GetService<ClientProviders>()
                .RegisterRawScript(this, "yafModelCss",
                    "<link href='/forum/resources/css/jquery.yafmodaldialog.css' rel='stylesheet' type='text/css' />");
        }

        /// <summary>
        ///     Displays the pm popup.
        /// </summary>
        /// <returns></returns>
        protected bool DisplayPmPopup()
        {
            return (this.PageContext.UnreadPrivate > 0)
                   && (this.PageContext.LastUnreadPm > this.Get<IYafSession>().LastPm);
        }

        /// <summary>
        ///     Displays the pending buddies.
        /// </summary>
        /// <returns></returns>
        protected bool DisplayPendingBuddies()
        {
            return (this.PageContext.PendingBuddies > 0)
                   && (this.PageContext.LastPendingBuddies > this.Get<IYafSession>().LastPendingBuddies);
        }

        /// <summary>
        ///     Generates the pop up.
        /// </summary>
        private void GeneratePopUp()
        {
            var notification = this.YafDialogBox;

            // This happens when user logs in
            if (this.DisplayPmPopup()
                &&
                (!this.PageContext.ForumPageType.Equals(ForumPages.cp_pm)
                 || !this.PageContext.ForumPageType.Equals(ForumPages.cp_editbuddies)))
            {
                if (!(this.Get<YafBoardSettings>().NotifcationNativeOnMobile
                      && this.Get<HttpRequestBase>().Browser.IsMobileDevice))
                {
                    notification.Show(
                        this.GetText("COMMON", "UNREAD_MSG2").FormatWith(this.PageContext.UnreadPrivate),
                        this.GetText("COMMON", "UNREAD_MSG_TITLE"),
                        DialogBox.DialogIcon.Mail,
                        new DialogBox.DialogButton
                        {
                            Text = this.GetText("COMMON", "YES"),
                            CssClass = "StandardButton OkButton",
                            ForumPageLink = new DialogBox.ForumLink {ForumPage = ForumPages.cp_pm}
                        },
                        new DialogBox.DialogButton
                        {
                            Text = this.GetText("COMMON", "NO"),
                            CssClass = "StandardButton CancelButton"
                        });
                }
                else
                {
                    this.PageContext.AddLoadMessage(
                        this.GetText("COMMON", "UNREAD_MSG").FormatWith(this.PageContext.UnreadPrivate));
                }

                this.Get<IYafSession>().LastPm = this.PageContext.LastUnreadPm;
                // Avoid Showing Both Popups
                return;
            }

            if (!this.DisplayPendingBuddies()
                ||
                (this.PageContext.ForumPageType.Equals(ForumPages.cp_editbuddies)
                 || this.PageContext.ForumPageType.Equals(ForumPages.cp_pm)))
            {
                return;
            }

            if (!(this.Get<YafBoardSettings>().NotifcationNativeOnMobile
                  && this.Get<HttpRequestBase>().Browser.IsMobileDevice))
            {
                notification.Show(
                    this.GetText("BUDDY", "PENDINGBUDDIES2").FormatWith(this.PageContext.PendingBuddies),
                    this.GetText("BUDDY", "PENDINGBUDDIES_TITLE"),
                    DialogBox.DialogIcon.Info,
                    new DialogBox.DialogButton
                    {
                        Text = this.GetText("COMMON", "YES"),
                        CssClass = "StandardButton OkButton",
                        ForumPageLink = new DialogBox.ForumLink {ForumPage = ForumPages.cp_editbuddies}
                    },
                    new DialogBox.DialogButton
                    {
                        Text = this.GetText("COMMON", "NO"),
                        CssClass = "StandardButton CancelButton",
                    });
            }
            else
            {
                this.PageContext.AddLoadMessage(
                    this.GetText("BUDDY", "PENDINGBUDDIES2").FormatWith(this.PageContext.PendingBuddies));
            }

            this.Get<IYafSession>().LastPendingBuddies = this.PageContext.LastPendingBuddies;
        }
    }
}