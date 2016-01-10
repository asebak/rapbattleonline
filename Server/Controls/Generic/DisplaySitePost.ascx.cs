#region Using

using System;
using System.Text;
using System.Web;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Classes;
using YAF.Classes.Data;
using YAF.Controls;
using YAF.Core;
using YAF.Core.Services.Auth;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;
using YAF.Utilities;
using YAF.Utils;
using YAF.Utils.Helpers;

#endregion

namespace FreestyleOnline.Controls.Generic
{
    public partial class DisplaySitePost : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int UserId { get; set; } // this is the poster

        /// <summary>
        ///     Gets or sets the date posted.
        /// </summary>
        /// <value>
        ///     The date posted.
        /// </value>
        public DateTime DatePosted { get; set; }

        /// <summary>
        ///     Gets or sets the post information.
        /// </summary>
        /// <value>
        ///     The post information.
        /// </value>
        public string PostInformation { get; set; }

        /// <summary>
        ///     Gets or sets the _user data.
        /// </summary>
        /// <value>
        ///     The _user data.
        /// </value>
        private IUserData _userData { get; set; }

        /// <summary>
        ///     Gets or sets the post identifier.
        /// </summary>
        /// <value>
        ///     The post identifier.
        /// </value>
        public int PostId { get; set; }

        /// <summary>
        ///     Gets or sets the message identifier.
        /// </summary>
        /// <value>
        ///     The message identifier.
        /// </value>
        public int MessageId { get; set; }

        #endregion

        #region Events

        //  /// <summary>
        //  /// Handles the Load event of the Page control.
        //  /// </summary>
        //  /// <param name="sender">The source of the event.</param>
        //  /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "yafSitePost", "~/forum/resources/js/yaf.js", true);
        }

        /// <summary>
        /// Notifies the update.
        /// </summary>
        public void NotifyUpdate()
        {
            this._userData = new CombinedUserDataHelper(this.UserId);
            this.PreRender += this.DisplaySitePostFooter_PreRender;
            this.PreRender += this.DisplaySitePost_PreRender;

            this.PopMenu1.Visible = true;
            this.SetupPopupMenu();
        }

        #endregion

        #region Methods

        //  /// <summary>
        //  /// The setup theme button with link.
        //  /// </summary>
        //  /// <param name="thisButton">
        //  /// The this button.
        //  /// </param>
        //  /// <param name="linkUrl">
        //  /// The link url.
        //  /// </param>
        protected void SetupThemeButtonWithLink([NotNull] ThemeButton thisButton, [NotNull] string linkUrl)
        {
            if (linkUrl.IsSet())
            {
                var link = linkUrl.Replace("\"", string.Empty);
                if (!link.ToLower().StartsWith("http"))
                {
                    link = "http://{0}".FormatWith(link);
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
                thisButton.Visible = false;
            }
        }

        //  /// <summary>
        //  /// The display post footer_ pre render.
        //  /// </summary>
        //  /// <param name="sender">The source of the event.</param>
        //  /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DisplaySitePostFooter_PreRender([NotNull] object sender, [NotNull] EventArgs e)
        {
            var userName = UserMembershipHelper.GetDisplayNameFromID(this.UserId);
            // albums link
            if (this._userData.UserID != this.PageContext.PageUserID && this.PageContext.User != null &&
                this.Get<YafBoardSettings>().EnableAlbum)
            {
                var numAlbums =
                    this.Get<IDataCache>().GetOrSet<int?>(
                        Constants.Cache.AlbumCountUser.FormatWith(this._userData.UserID),
                        () =>
                        {
                            var usrAlbumsData = LegacyDb.user_getalbumsdata(
                                this._userData.UserID, YafContext.Current.PageBoardID);
                            return usrAlbumsData.GetFirstRowColumnAsValue<int?>("NumAlbums", null);
                        },
                        TimeSpan.FromMinutes(5));

                this.Albums.Visible = numAlbums.HasValue && numAlbums > 0;
                this.Albums.NavigateUrl = YafBuildLink.GetLinkNotEscaped(
                    ForumPages.albums, "u={0}", this._userData.UserID);
                this.Albums.ParamTitle0 = userName;
            }
            // private messages
            this.Pm.Visible = this._userData.UserID != this.PageContext.PageUserID && this.PageContext.User != null
                              && this.Get<YafBoardSettings>().AllowPrivateMessages;
            this.Pm.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.pmessage, "u={0}", this._userData.UserID);
            this.Pm.ParamTitle0 = userName;

            // emailing
            this.Email.Visible = this._userData.UserID != this.PageContext.PageUserID && this.PageContext.User != null
                                 && this.Get<YafBoardSettings>().AllowEmailSending;
            this.Email.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_email, "u={0}", this._userData.UserID);
            this.Email.ParamTitle0 = userName;

            // home page
            this.SetupThemeButtonWithLink(this.Home, this._userData.Profile.Homepage);
            this.Home.ParamTitle0 = userName;

            // blog page
            this.SetupThemeButtonWithLink(this.Blog, this._userData.Profile.Blog);
            this.Blog.ParamTitle0 = userName;

            if (this.PageContext.User != null && (this._userData.UserID != this.PageContext.PageUserID))
            {
                // MSN
                this.Msn.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_msn, "u={0}", this._userData.UserID);
                this.Msn.ParamTitle0 = userName;

                // Yahoo IM
                this.Yim.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_yim, "u={0}", this._userData.UserID);
                this.Yim.ParamTitle0 = userName;

                // AOL IM
                this.Aim.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_aim, "u={0}", this._userData.UserID);
                this.Aim.ParamTitle0 = userName;

                // ICQ
                this.Icq.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.im_icq, "u={0}", this._userData.UserID);
                this.Icq.ParamTitle0 = userName;

                // XMPP
                this.Xmpp.NavigateUrl = YafBuildLink.GetLinkNotEscaped(
                    ForumPages.im_xmpp, "u={0}", this._userData.UserID);
                this.Xmpp.ParamTitle0 = userName;

                // Skype
                this.Skype.NavigateUrl = YafBuildLink.GetLinkNotEscaped(
                    ForumPages.im_skype, "u={0}", this._userData.UserID);
                this.Skype.ParamTitle0 = userName;
            }

            var loadHoverCardJs = false;

            // Facebook
            if (this._userData.Profile.Facebook.IsSet())
            {
                this.Facebook.Visible = this._userData.Profile.Facebook.IsSet();

                if (this._userData.Profile.Facebook.IsSet())
                {
                    this.Facebook.NavigateUrl =
                        ValidationHelper.IsNumeric(this._userData.Profile.Facebook)
                                                ? "https://www.facebook.com/profile.php?id={0}".FormatWith(
                                                    this._userData.Profile.Facebook)
                                                : this._userData.Profile.Facebook;
                }

                this.Facebook.ParamTitle0 = userName;

                if (this.Get<YafBoardSettings>().EnableUserInfoHoverCards)
                {
                    this.Facebook.Attributes.Add("data-hovercard", this._userData.Profile.Facebook);
                    this.Facebook.CssClass += " Facebook-HoverCard";

                    loadHoverCardJs = true;
                }
            }

            // Twitter
            if (this._userData.Profile.Twitter.IsSet())
            {
                this.Twitter.Visible = this._userData.Profile.Twitter.IsSet();
                this.Twitter.NavigateUrl = "http://twitter.com/{0}".FormatWith(this._userData.Profile.Twitter);
                this.Twitter.ParamTitle0 = userName;

                if (this.Get<YafBoardSettings>().EnableUserInfoHoverCards)
                {
                    this.Twitter.Attributes.Add("data-hovercard", this._userData.Profile.Twitter);
                    this.Twitter.CssClass += " Twitter-HoverCard";

                    loadHoverCardJs = true;
                }
            }

            // Google+
            if (this._userData.Profile.Google.IsSet())
            {
                this.Google.Visible = this._userData.Profile.Google.IsSet();
                this.Google.NavigateUrl = this._userData.Profile.Google;
                this.Google.ParamTitle0 = userName;
            }

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
                .RegisterStartUpScript(this, "hovercardssocialmedia",hoverCardLoadJs.ToString());
        }

        //  #endregion

        //  #region Methods

        //  /// <summary>
        //  /// Formats the dvThanksInfo section.
        //  /// </summary>
        //  /// <param name="rawStr">
        //  /// The raw Str. 
        //  /// </param>
        //  /// <returns>
        //  /// The format thanks info. 
        //  /// </returns>
        [NotNull]
        protected string FormatThanksInfo([NotNull] string rawStr)
        {
            var sb = new StringBuilder();

            bool showDate = this.Get<YafBoardSettings>().ShowThanksDate;

            // Extract all user IDs, usernames and (If enabled thanks dates) related to this message.
            foreach (var chunk in rawStr.Split(','))
            {
                var subChunks = chunk.Split('|');

                int userId = int.Parse(subChunks[0]);
                DateTime thanksDate = DateTime.Parse(subChunks[1]);

                if (sb.Length > 0)
                {
                    sb.Append(",&nbsp;");
                }

                // Get the username related to this User ID
                string displayName = this.Get<IUserDisplayName>().GetName(userId);

                sb.AppendFormat(
                    @"<a id=""{0}"" href=""{1}""><u>{2}</u></a>",
                    userId,
                    YafBuildLink.GetLink(ForumPages.profile, "u={0}", userId),
                    this.Get<HttpServerUtilityBase>().HtmlEncode(displayName));

                // If showing thanks date is enabled, add it to the formatted string.
                if (showDate)
                {
                    sb.AppendFormat(
                        @" {0}",
                        this.GetText("DEFAULT", "ONDATE").FormatWith(this.Get<IDateTime>().FormatDateShort(thanksDate)));
                }
            }

            return sb.ToString();
        }

        //  /// <summary>
        //  /// Retweets Message thru the Twitter API
        //  /// </summary>
        //  /// <param name="sender">
        //  /// The source of the event. 
        //  /// </param>
        //  /// <param name="e">
        //  /// The <see cref="System.EventArgs"/> instance containing the event data. 
        //  /// </param>
        protected void Retweet_Click(object sender, EventArgs e)
        {
            var twitterName = this.Get<YafBoardSettings>().TwitterUserName.IsSet()
                ? "@{0} ".FormatWith(this.Get<YafBoardSettings>().TwitterUserName)
                : string.Empty;
            //breaks here
            var twitterMsg =
                BBCodeHelper.StripBBCode(
                    HtmlHelper.StripHtml(HtmlHelper.CleanHtmlString(this.PostInformation))).RemoveMultipleWhitespace();
            if (Config.TwitterConsumerKey.IsSet() && Config.TwitterConsumerSecret.IsSet() &&
                this.Get<IYafSession>().TwitterToken.IsSet() && this.Get<IYafSession>().TwitterTokenSecret.IsSet() &&
                this.Get<IYafSession>().TwitterTokenSecret.IsSet() && this.PageContext.IsTwitterUser)
            {
                var oAuth = new OAuthTwitter
                {
                    ConsumerKey = Config.TwitterConsumerKey,
                    ConsumerSecret = Config.TwitterConsumerSecret,
                    Token = this.Get<IYafSession>().TwitterToken,
                    TokenSecret = this.Get<IYafSession>().TwitterTokenSecret
                };

                var tweets = new TweetAPI(oAuth);

                tweets.UpdateStatus(
                    TweetAPI.ResponseFormat.json,
                    this.Server.UrlEncode("RT {1}: {0} {2}".FormatWith(twitterMsg.Truncate(100), twitterName,
                        HttpContext.Current.Request.Url.AbsoluteUri)),
                    string.Empty);
            }
            else
            {
                this.Get<HttpResponseBase>().Redirect(
                    "http://twitter.com/share?url={0}&text={1}".FormatWith(
                        this.Server.UrlEncode(this.Get<HttpRequestBase>().Url.ToString()),
                        this.Server.UrlEncode(
                            "RT {1}: {0} {2}".FormatWith(twitterMsg.Truncate(100), twitterName,
                                HttpContext.Current.Request.Url.AbsoluteUri))));
            }
        }

        /// <summary>
        ///     Shows the ip information.
        /// </summary>
        private void ShowIPInfo()
        {
            // TODO: Make DB Tables for comments support ipaddress
            if (this.PageContext.IsAdmin ||
                (this.Get<YafBoardSettings>().AllowModeratorsViewIPs && this.PageContext.ForumModeratorAccess))
            {
                // We should show IP
                //this.IPSpan1.Visible = true;
                //var ip = IPHelper.GetIp4Address(this._userData.DataRow["IP"].ToString());
                //this.IPLink1.HRef = this.Get<YafBoardSettings>().IPInfoPageURL.FormatWith(ip);
                //this.IPLink1.Title = this.GetText("COMMON", "TT_IPDETAILS");
                //this.IPLink1.InnerText = this.HtmlEncode(ip);
            }
        }

        //  /// <summary>
        //  /// Setup the popup menu.
        //  /// </summary>
        private void SetupPopupMenu()
        {
            this.PopMenu1.ItemClick += this.PopMenu1_ItemClick;
            this.PopMenu1.AddPostBackItem("userprofile", this.GetText("POSTS", "USERPROFILE"));

            this.PopMenu1.AddPostBackItem("lastposts", this.GetText("PROFILE", "SEARCHUSER"));

            if (this.Get<YafBoardSettings>().EnableThanksMod)
            {
                this.PopMenu1.AddPostBackItem("viewthanks", this.GetText("VIEWTHANKS", "TITLE"));
            }

            if (this.PageContext.IsAdmin)
            {
                this.PopMenu1.AddPostBackItem("edituser", this.GetText("POSTS", "EDITUSER"));
            }

            if (!this.PageContext.IsGuest)
            {
                if (this.Get<IUserIgnored>().IsIgnored(this._userData.UserID))
                {
                    this.PopMenu1.AddPostBackItem("toggleuserposts_show", this.GetText("POSTS", "TOGGLEUSERPOSTS_SHOW"));
                }
                else
                {
                    this.PopMenu1.AddPostBackItem("toggleuserposts_hide", this.GetText("POSTS", "TOGGLEUSERPOSTS_HIDE"));
                }
            }

            if (this.Get<YafBoardSettings>().EnableBuddyList &&
                this.PageContext.PageUserID != this.UserId)
            {
                // Should we add the "Add Buddy" item?
                if (!this.Get<IBuddy>().IsBuddy(this.UserId, false) && !this.PageContext.IsGuest)
                {
                    this.PopMenu1.AddPostBackItem("addbuddy", this.GetText("BUDDY", "ADDBUDDY"));
                }
                else if (this.Get<IBuddy>().IsBuddy(this.UserId, true) && !this.PageContext.IsGuest)
                {
                    // Are the users approved buddies? Add the "Remove buddy" item.
                    this.PopMenu1.AddClientScriptItemWithPostback(
                        this.GetText("BUDDY", "REMOVEBUDDY"),
                        "removebuddy",
                        "if (confirm('{0}')) {1}".FormatWith(
                            this.GetText("CP_EDITBUDDIES", "NOTIFICATION_REMOVE"), "{postbackcode}"));
                }
            }
            //this.PopMenu1.Attach(this.UserProfileLink);
        }

        //  /// <summary>
        //  /// The display post_ pre render.
        //  /// </summary>
        //  /// <param name="sender">
        //  /// The sender. 
        //  /// </param>
        //  /// <param name="e">
        //  /// The e. 
        //  /// </param>
        private void DisplaySitePost_PreRender([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Retweet.Visible = this.Get<IPermissions>().Check(this.Get<YafBoardSettings>().ShowRetweetMessageTo);
            //var userProfileId = this.GetService<UrlProvider>().GetFirstQueryAsInt();
            //this.Delete.Visible = (this.PageContext.PageUserID == this.UserId || userProfileId == this.PageContext.PageUserID);
            this.Delete.Visible = false;
            this.Quote.Visible = false;
        }

        //  /// <summary>
        //  /// Add Reputation Controls to the User PopMenu
        //  /// </summary>
        private void AddReputationControls()
        {
            if (this.PageContext.PageUserID != this._userData.UserID &&
                this.Get<YafBoardSettings>().EnableUserReputation && !this.PageContext.IsGuest)
            {
                // Check if the User matches minimal requirements for voting up
                if (this.PageContext.Reputation >= this.Get<YafBoardSettings>().ReputationMinUpVoting)
                {
                    //this.AddReputation.Visible = true;
                }

                // Check if the User matches minimal requirements for voting down
                if (this.PageContext.Reputation >= this.Get<YafBoardSettings>().ReputationMinDownVoting)
                {
                    // Check if the Value is 0 or Bellow
                    if (!this.Get<YafBoardSettings>().ReputationAllowNegative &&
                        this._userData.Points.ToType<int>() > 0 ||
                        this.Get<YafBoardSettings>().ReputationAllowNegative)
                    {
                        //this.RemoveReputation.Visible = true;
                    }
                }
            }
        }


        //  /// <summary>
        //  /// The pop menu 1_ item click.
        //  /// </summary>
        //  /// <param name="sender">The source of the event.</param>
        //  /// <param name="e">The <see cref="YAF.Controls.PopEventArgs"/> instance containing the event data.</param>
        private void PopMenu1_ItemClick([NotNull] object sender, [NotNull] PopEventArgs e)
        {
            switch (e.Item)
            {
                case "userprofile":
                    YafBuildLink.Redirect(ForumPages.profile, "u={0}", this._userData.UserID);
                    break;
                case "lastposts":
                    YafBuildLink.Redirect(
                        ForumPages.search,
                        "postedby={0}",
                        this.Get<YafBoardSettings>().EnableDisplayName
                            ? this._userData.DisplayName
                            : this._userData.UserName);
                    break;
                case "addbuddy":
                    this.PopMenu1.RemovePostBackItem("addbuddy");
                    string[] strBuddyRequest = this.Get<IBuddy>().AddRequest(this._userData.UserID);
                    if (Convert.ToBoolean(strBuddyRequest[1]))
                    {
                        this.PageContext.AddLoadMessage(
                            this.GetTextFormatted("NOTIFICATION_BUDDYAPPROVED_MUTUAL", strBuddyRequest[0]),
                            MessageTypes.Success);

                        this.PopMenu1.AddClientScriptItemWithPostback(
                            this.GetText("BUDDY", "REMOVEBUDDY"),
                            "removebuddy",
                            "if (confirm('{0}')) {1}".FormatWith(
                                this.GetText("CP_EDITBUDDIES", "NOTIFICATION_REMOVE"), "{postbackcode}"));
                    }
                    else
                    {
                        this.PageContext.AddLoadMessage(this.GetText("NOTIFICATION_BUDDYREQUEST"));
                    }

                    break;
                case "removebuddy":
                {
                    this.PopMenu1.RemovePostBackItem("removebuddy");
                    this.PopMenu1.AddPostBackItem("addbuddy", this.GetText("BUDDY", "ADDBUDDY"));
                    this.PageContext.AddLoadMessage(
                        this.GetTextFormatted(
                            "REMOVEBUDDY_NOTIFICATION", this.Get<IBuddy>().Remove(this._userData.UserID)),
                        MessageTypes.Success);
                    break;
                }

                case "edituser":
                    YafBuildLink.Redirect(ForumPages.admin_edituser, "u={0}", this._userData.UserID);
                    break;
                case "toggleuserposts_show":
                    this.Get<IUserIgnored>().RemoveIgnored(this._userData.UserID);
                    this.Response.Redirect(this.Request.RawUrl);
                    break;
                case "toggleuserposts_hide":
                    this.Get<IUserIgnored>().AddIgnored(this._userData.UserID);
                    this.Response.Redirect(this.Request.RawUrl);
                    break;
                case "viewthanks":
                    YafBuildLink.Redirect(ForumPages.viewthanks, "u={0}", this._userData.UserID);
                    break;
            }
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the Quote control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Quote_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Get<HttpSessionStateBase>().Add("quotedmessage", this.PostInformation);
        }
    }
}