#region Using

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Classes;
using YAF.Controls;
using YAF.Core;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;
using YAF.Utilities;
using YAF.Utils;
using YAF.Utils.Helpers;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     The profile context of a user
    /// </summary>
    public partial class Profile : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     User's profile
        /// </summary>
       [NotNull] public int UserId { get; set; }

        /// <summary>
        ///     When implemented, gets a collection of information that can be accessed by a control designer.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IDictionary" /> containing information about the control.</returns>
        [CanBeNull] public IUserData UserData { get; set; }

        /// <summary>
        ///     Gets or sets the page user identifier.
        /// </summary>
        /// <value>
        ///     The page user identifier.
        /// </value>
        [NotNull] public int PageUserId { get; set; }

        [CanBeNull] protected string Bio;
        #endregion

        #region Methods
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.UploadBtn.OnClientClick += "return handlepostback({0})".FormatWith(this.UploadBtn.ClientID);
            base.OnInit(e);
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.UserId = this.GetService<UrlProvider>().GetFirstQueryAsInt();
            this.PageUserId = this.PageContext.PageUserID;
            if (this.PageUserId == this.UserId)
            {
                this.GetService<ClientProviders>().RegisterClientScriptBlock(this, "editablebio", "~/js/Bio.js", true);
            }
            this.UserData = new CombinedUserDataHelper(this.UserId);
            this.CheckIfRealProfile();
            this.InitializeUsersProfile();
            this.SetupUserLinks(UserData, UserMembershipHelper.GetDisplayNameFromID(UserId));
            this.SetupBuddyList(UserId, new CombinedUserDataHelper(this.UserId));
        }

        /// <summary>
        /// Initializes the users profile.
        /// </summary>
        private void InitializeUsersProfile()
        {
            this.ProfileMusicList.UserId = this.UserId;
            this.WrittenBattlesList.UserId = this.UserId;
            this.AudioBattlesList.UserId = this.UserId;
            this.ProfileHood.UserId = this.UserId;
            this.ProfileAlbumList.UserID = this.UserId;
            this.ProfileMusicAdd.UserId = this.UserId;
            this.ProfileMusicList.UserId = this.UserId;
            this.ProfileUserComments.UserId = this.UserId;
            this.UsersWrittenVerses.UserId = this.UserId;
            this.UsersAudioVerses.UserId = this.UserId;
            this.AddVerseAudio.UserId = this.UserId;
            this.AddVerseWritten.UserId = this.UserId;
            this.UserOverviewStats.UserId = this.UserId;
            this.UserOverviewStats.UserData = this.UserData;
            this.UserAudioStats.UserId = this.UserId;
            this.UserWrittenStats.UserId = this.UserId;
            this.UserProfileLink.UserID = this.UserId;
            this.ProfileImage.ImageUrl = this.Get<IAvatars>().GetAvatarUrlForUser(UserId);
            var headerImage = new UserProfile(this.UserId).GetHeaderImage();
            this.Bio = new UserProfile(this.UserId).GetBio();
            if (this.Bio.IsNotSet() && this.UserId == this.PageUserId)
            {
                this.Bio = this.Text("PROFILE", "EDIT_BIO");
            }else if (this.Bio.IsNotSet())
            {
                this.Bio = this.Text("PROFILE", "NO_BIO");
            }
            if (headerImage != null)
            {
                this.HeaderImage.ImageUrl = this.GetService<ResourceProvider>()
                    .GetClientPath(RapResource.HeaderPictures, headerImage);
                if (this.IsMobile)
                {
                    this.HeaderImage.Height = Unit.Pixel(200);
                    this.HeaderImage.Width = Unit.Pixel(275);
                }
                else
                {
                    this.HeaderImage.Height = Unit.Pixel(200);
                    this.HeaderImage.Width = Unit.Pixel(450);
                }
            }
            else
            {
                this.HeaderImage.Hidden = true;
            }
        }


        /// <summary>
        ///     Checks if real profile.
        /// </summary>
        /// <exception cref="System.Web.HttpException">404;Invalid UserID Entered</exception>
        private void CheckIfRealProfile()
        {
            if (UserData.DisplayName == null)
            {
                throw new HttpException(404, "Invalid UserID Entered");
            }
        }

        /// <summary>
        ///     The setup theme button with link.
        /// </summary>
        /// <param name="thisButton">
        ///     The this button.
        /// </param>
        /// <param name="linkUrl">
        ///     The link url.
        /// </param>
        protected void SetupThemeButtonWithLink([NotNull] ThemeButton thisButton, [NotNull] string linkUrl)
        {
            if (linkUrl.IsSet())
            {
                var link = linkUrl.Replace("\"", string.Empty);
                if (!link.ToLower().StartsWith("http"))
                {
                    link = "http://" + link;
                }

                thisButton.NavigateUrl = link;
                thisButton.Attributes.Add("target", "_blank");
                if (this.Get<YafBoardSettings>().UseNoFollowLinks)
                {
                    thisButton.Attributes.Add("rel", "nofollow");
                }
            }
            else
            {
                thisButton.NavigateUrl = string.Empty;
            }
        }

        /// <summary>
        ///     The setup user links.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <param name="userName">Name of the user.</param>
        private void SetupUserLinks([NotNull] IUserData userData, string userName)
        {
            // homepage link
            this.Home.Visible = userData.Profile.Homepage.IsSet();
            this.SetupThemeButtonWithLink(this.Home, userData.Profile.Homepage);
            this.Home.ParamTitle0 = userName;

            // blog link
            this.Blog.Visible = userData.Profile.Blog.IsSet();
            this.SetupThemeButtonWithLink(this.Blog, userData.Profile.Blog);
            this.Blog.ParamTitle0 = userName;

            this.Google.Visible = userData.Profile.Google.IsSet();
            this.Google.NavigateUrl = userData.Profile.Google;
            this.Google.ParamTitle0 = userName;

            var loadHoverCardJs = false;

            // Facebook
            if (userData.Profile.Facebook.IsSet())
            {
                this.Facebook.Visible = userData.Profile.Facebook.IsSet();

                if (userData.Profile.Facebook.IsSet())
                {
                    this.Facebook.NavigateUrl =
                        ValidationHelper.IsNumeric(userData.Profile.Facebook)
                                                ? "https://www.facebook.com/profile.php?id={0}".FormatWith(
                                                    userData.Profile.Facebook)
                                                : userData.Profile.Facebook;
                }

                this.Facebook.ParamTitle0 = userName;

                if (this.Get<YafBoardSettings>().EnableUserInfoHoverCards)
                {
                    this.Facebook.Attributes.Add("data-hovercard", userData.Profile.Facebook);
                    this.Facebook.CssClass += " Facebook-HoverCard";

                    loadHoverCardJs = true;
                }
            }

            // Twitter
            if (userData.Profile.Twitter.IsSet())
            {
                this.Twitter.Visible = userData.Profile.Twitter.IsSet();
                this.Twitter.NavigateUrl = "http://twitter.com/{0}".FormatWith(userData.Profile.Twitter);
                this.Twitter.ParamTitle0 = userName;

                if (this.Get<YafBoardSettings>().EnableUserInfoHoverCards)
                {
                    this.Twitter.Attributes.Add("data-hovercard", userData.Profile.Twitter);
                    this.Twitter.CssClass += " Twitter-HoverCard";

                    loadHoverCardJs = true;
                }
            }

            if (userData.UserID == this.PageContext.PageUserID)
            {
                return;
            }

            this.PM.Visible = !userData.IsGuest
                              && this.Get<YafBoardSettings>().AllowPrivateMessages;
            this.PM.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.pmessage, "u={0}", userData.UserID);
            this.PM.ParamTitle0 = userName;

            // email link
            this.Email.Visible = !userData.IsGuest
                                 && this.Get<YafBoardSettings>().AllowEmailSending;
            this.Email.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_email, "u={0}", userData.UserID);
            if (this.PageContext.IsAdmin)
            {
                this.Email.TitleNonLocalized = userData.Membership.Email;
            }

            this.Email.ParamTitle0 = userName;

            this.MSN.Visible = userData.Profile.MSN.IsSet();
            this.MSN.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_msn, "u={0}", userData.UserID);
            this.MSN.ParamTitle0 = userName;

            this.YIM.Visible = userData.Profile.YIM.IsSet();
            this.YIM.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_yim, "u={0}", userData.UserID);
            this.YIM.ParamTitle0 = userName;

            this.AIM.Visible = userData.Profile.AIM.IsSet();
            this.AIM.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_aim, "u={0}", userData.UserID);
            this.AIM.ParamTitle0 = userName;

            this.ICQ.Visible = userData.Profile.ICQ.IsSet();
            this.ICQ.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_icq, "u={0}", userData.UserID);
            this.ICQ.ParamTitle0 = userName;

            this.XMPP.Visible = userData.Profile.XMPP.IsSet();
            this.XMPP.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_xmpp, "u={0}", userData.UserID);
            this.XMPP.ParamTitle0 = userName;

            this.Skype.Visible = userData.Profile.Skype.IsSet();
            this.Skype.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_skype, "u={0}", userData.UserID);
            this.Skype.ParamTitle0 = userName;

            if (!loadHoverCardJs || !this.Get<YafBoardSettings>().EnableUserInfoHoverCards)
            {
                return;
            }

            var hoverCardLoadJs = new StringBuilder();

            if (this.Facebook.Visible)
            {
                hoverCardLoadJs.Append(
                    JavaScriptBlocks.HoverCardLoadJs(
                        ".Facebook-HoverCard",
                        "Facebook",
                        this.GetText("DEFAULT", "LOADING_FB_HOVERCARD"),
                        this.GetText("DEFAULT", "ERROR_FB_HOVERCARD")));
            }

            if (this.Twitter.Visible)
            {
                hoverCardLoadJs.Append(
                    JavaScriptBlocks.HoverCardLoadJs(
                        ".Twitter-HoverCard",
                        "Twitter",
                        this.GetText("DEFAULT", "LOADING_TWIT_HOVERCARD"),
                        this.GetText("DEFAULT", "ERROR_TWIT_HOVERCARD")));
            }

            // Setup Hover Card JS
            this.GetService<ClientProviders>().RegisterRawScript(this.Page, "hovercardjs",
                "<script type='text/javascript' src='/forum/resources/js/jquery.hovercard.js'></script>");
            this.GetService<ClientProviders>().RegisterRawScript(this.Page, "hovercardcss",
             "<link rel='stylesheet' href='/forum/resources/css/jquery.hovercard.css' />");
            this.GetService<ClientProviders>()
                .RegisterStartUpScript(this, "hovercardssocialmedia", hoverCardLoadJs.ToString());
        }

        /// <summary>
        ///     The setup buddy list.
        /// </summary>
        /// <param name="userID">
        ///     The user id.
        /// </param>
        /// <param name="userData">
        ///     The user data.
        /// </param>
        private void SetupBuddyList(int userID, [NotNull] CombinedUserDataHelper userData)
        {
            if (userID == this.PageContext.PageUserID)
            {
                this.lnkBuddy.Visible = false;
            }
            else if (this.Get<IBuddy>().IsBuddy((int)userData.DBRow["userID"], true) && !this.PageContext.IsGuest)
            {
                this.lnkBuddy.Visible = true;
                this.lnkBuddy.Text = "({0})".FormatWith(this.GetText("BUDDY", "REMOVEBUDDY"));
                this.lnkBuddy.CommandArgument = "removebuddy";
                this.lnkBuddy.Attributes["onclick"] =
                    "return confirm('{0}')".FormatWith(this.GetText("CP_EDITBUDDIES", "NOTIFICATION_REMOVE"));
            }
            else if (this.Get<IBuddy>().IsBuddy((int)userData.DBRow["userID"], false))
            {
                this.lnkBuddy.Visible = false;
                this.ltrApproval.Visible = true;
            }
            else
            {
                if (!this.PageContext.IsGuest)
                {
                    this.lnkBuddy.Visible = true;
                    this.lnkBuddy.Text = "({0})".FormatWith(this.GetText("BUDDY", "ADDBUDDY"));
                    this.lnkBuddy.CommandArgument = "addbuddy";
                    this.lnkBuddy.Attributes["onclick"] = string.Empty;
                }
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Command event of the lnkBuddy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
        protected void lnkBuddy_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "addbuddy")
            {
                var strBuddyRequest = this.Get<IBuddy>().AddRequest(this.UserId);

                this.lnkBuddy.Visible = false;

                if (Convert.ToBoolean(strBuddyRequest[1]))
                {
                    this.PageContext.AddLoadMessage(
                        this.GetText("NOTIFICATION_BUDDYAPPROVED_MUTUAL").FormatWith(strBuddyRequest[0]),
                        MessageTypes.Success);
                }
                else
                {
                    this.ltrApproval.Visible = true;
                    this.PageContext.AddLoadMessage(this.GetText("NOTIFICATION_BUDDYREQUEST"), MessageTypes.Success);
                }
            }
            else
            {
                this.PageContext.AddLoadMessage(
                    this.GetText("REMOVEBUDDY_NOTIFICATION").FormatWith(this.Get<IBuddy>().Remove(this.UserId)),
                    MessageTypes.Success);
            }
            this.GetService<UrlProvider>().RefreshPage();
        }

        /// <summary>
        /// Handles the OnClick event of the HeaderUpload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectEventArgs" /> instance containing the event data.</param>
        protected void HeaderUpload_OnClick([NotNull] object sender,[NotNull] EventArgs e)
        {
            if (this.HeaderUploader.PostedFile.ContentType.Contains("image"))
            {
                var file = Guid.NewGuid() + Path.GetExtension(this.HeaderUploader.FileName);
                this.HeaderUploader.PostedFile.SaveAs(
                    this.GetService<ResourceProvider>().GetPath(RapResource.HeaderPictures, file));
                var up = new UserProfile(this.UserId);
                up.UploadHeaderImage(file);
                this.AddLoadMessageSession(this.Text("PROFILE", "HEADERIMG"));
                this.GetService<UrlProvider>().RefreshPage();
            }
            else
            {
                this.PageContext.AddLoadMessage(this.Text("COMMON", "INVALID_IMAGE"), MessageTypes.Error);
            }
        }

        #endregion
    }
}