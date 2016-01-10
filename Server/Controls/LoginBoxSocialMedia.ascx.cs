﻿#region Using

using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Classes;
using YAF.Classes.Data;
using YAF.Controls;
using YAF.Core;
using YAF.Core.Services;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.EventProxies;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;
using YAF.Utils;
using YAF.Utils.Helpers;

#endregion

namespace FreestyleOnline.Controls
{
    [Obsolete("Now Embedded Directly On HomePage")]
    public partial class LoginBoxSocialMedia : RapUserControl
    {
        #region Methods

        /// <summary>
        ///     Gets the valid username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///     The get valid login.
        /// </returns>
        protected virtual string GetValidUsername(string username, string password)
        {
            if (username.Contains("@") && this.Get<MembershipProvider>().RequiresUniqueEmail)
            {
                // attempt Email Login
                string realUsername = this.Get<MembershipProvider>().GetUserNameByEmail(username);

                if (realUsername.IsSet() && this.Get<MembershipProvider>().ValidateUser(realUsername, password))
                {
                    return realUsername;
                }
            }

            // Standard user name login
            if (this.Get<MembershipProvider>().ValidateUser(username, password))
            {
                return username;
            }

            // display name login...
            if (this.Get<YafBoardSettings>().EnableDisplayName)
            {
                // Display name login
                var id = this.Get<IUserDisplayName>().GetId(username);

                if (id.HasValue)
                {
                    // get the username associated with this id...
                    string realUsername = UserMembershipHelper.GetUserNameFromID(id.Value);

                    // validate again...
                    if (this.Get<MembershipProvider>().ValidateUser(realUsername, password))
                    {
                        return realUsername;
                    }
                }
            }

            // no valid login -- return null
            return null;
        }

        /// <summary>
        ///     The login 1_ authenticate.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        protected void Login1_Authenticate([NotNull] object sender, [NotNull] AuthenticateEventArgs e)
        {
            var username = this.Login1.UserName.Trim();
            var password = this.Login1.Password.Trim();

            e.Authenticated = false;

            var realUserName = this.GetValidUsername(username, password);

            if (realUserName.IsSet())
            {
                this.Login1.UserName = realUserName;
                e.Authenticated = true;
            }
        }

        /// <summary>
        ///     The Logged In Event
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        protected void Login1_LoggedIn([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Get<IRaiseEvent>().Raise(new SuccessfulUserLoginEvent(this.PageContext.PageUserID));

            LegacyDb.user_update_single_sign_on_status(this.PageContext.PageUserID, AuthService.none);
        }

        /// <summary>
        ///     The login 1_ login error.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        protected void Login1_LoginError([NotNull] object sender, [NotNull] EventArgs e)
        {
            bool emptyFields = false;

            var userName = this.Login1.FindControlAs<TextBox>("UserName");
            var password = this.Login1.FindControlAs<TextBox>("Password");

            if (userName.Text.Trim().Length == 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("REGISTER", "NEED_USERNAME"));
                emptyFields = true;
            }

            if (password.Text.Trim().Length == 0)
            {
                this.PageContext.AddLoadMessage(this.GetText("REGISTER", "NEED_PASSWORD"));
                emptyFields = true;
            }

            if (!emptyFields)
            {
                this.PageContext.AddLoadMessage(this.Login1.FailureText);
            }
        }

        /// <summary>
        ///     The page_ load.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            this.Login1.MembershipProvider = Config.MembershipProvider;

            // Login1.CreateUserText = "Sign up for a new account.";
            // Login1.CreateUserUrl = YafBuildLink.GetLink( ForumPages.register );
            this.Login1.PasswordRecoveryText = this.GetText("lostpassword");
            this.Login1.PasswordRecoveryUrl = YafBuildLink.GetLink(ForumPages.recoverpassword);
            this.Login1.FailureText = this.GetText("password_error");

            this.Login1.DestinationPageUrl =
                this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("ReturnUrl").IsSet()
                    ? this.Server.UrlDecode(this.Request.QueryString.GetFirstOrDefault("ReturnUrl"))
                    : YafBuildLink.GetLink(ForumPages.forum);

            // localize controls
            var rememberMe = this.Login1.FindControlAs<CheckBox>("RememberMe");
            var userName = this.Login1.FindControlAs<TextBox>("UserName");
            var password = this.Login1.FindControlAs<TextBox>("Password");
            var forumLogin = this.Login1.FindControlAs<Button>("LoginButton");
            var passwordRecovery = this.Login1.FindControlAs<Button>("PasswordRecovery");

            var userNameRow = this.Login1.FindControlAs<HtmlTableRow>("UserNameRow");
            var passwordRow = this.Login1.FindControlAs<HtmlTableRow>("PasswordRow");

            var singleSignOnOptionsRow = this.Login1.FindControlAs<HtmlTableRow>("SingleSignOnOptionsRow");
            var singleSignOnOptions = this.Login1.FindControlAs<RadioButtonList>("SingleSignOnOptions");

            var registerLink = this.Login1.FindControlAs<LinkButton>("RegisterLink");
            var registerLinkPlaceHolder = this.Login1.FindControlAs<PlaceHolder>("RegisterLinkPlaceHolder");

            var singleSignOnRow = this.Login1.FindControlAs<HtmlTableRow>("SingleSignOnRow");

            var facebookHolder = this.Login1.FindControlAs<PlaceHolder>("FacebookHolder");
            var facebookLogin = this.Login1.FindControlAs<HtmlAnchor>("FacebookLogin");

            var twitterHolder = this.Login1.FindControlAs<PlaceHolder>("TwitterHolder");
            var twitterLogin = this.Login1.FindControlAs<HtmlAnchor>("TwitterLogin");

            var googleHolder = this.Login1.FindControlAs<PlaceHolder>("GoogleHolder");
            var googleLogin = this.Login1.FindControlAs<HtmlAnchor>("GoogleLogin");

            var facebookRegister = this.Login1.FindControlAs<LinkButton>("FacebookRegister");
            var twitterRegister = this.Login1.FindControlAs<LinkButton>("TwitterRegister");
            var googleRegister = this.Login1.FindControlAs<LinkButton>("GoogleRegister");

            userName.Focus();

            /*
                RequiredFieldValidator usernameRequired = ( RequiredFieldValidator ) Login1.FindControl( "UsernameRequired" );
                RequiredFieldValidator passwordRequired = ( RequiredFieldValidator ) Login1.FindControl( "PasswordRequired" );

                usernameRequired.ToolTip = usernameRequired.ErrorMessage = GetText( "REGISTER", "NEED_USERNAME" );
                passwordRequired.ToolTip = passwordRequired.ErrorMessage = GetText( "REGISTER", "NEED_PASSWORD" );
                */
            if (rememberMe != null)
            {
                rememberMe.Text = this.GetText("auto");
            }

            if (forumLogin != null)
            {
                forumLogin.Text = this.GetText("FORUM_LOGIN");
            }

            if (passwordRecovery != null)
            {
                passwordRecovery.Text = this.GetText("LOSTPASSWORD");
            }

            if (password != null && forumLogin != null)
            {
                password.Attributes.Add(
                    "onkeydown",
                    "if(event.which || event.keyCode){{if ((event.which == 13) || (event.keyCode == 13)) {{document.getElementById('{0}').click();return false;}}}} else {{return true}}; "
                        .FormatWith(forumLogin.ClientID));
            }

            if (registerLinkPlaceHolder != null && this.PageContext.IsGuest
                && !this.Get<YafBoardSettings>().DisableRegistrations && !Config.IsAnyPortal)
            {
                registerLinkPlaceHolder.Visible = true;

                registerLink.Text = this.GetText("REGISTER_INSTEAD");
            }

            if (this.Get<YafBoardSettings>().AllowSingleSignOn
                && (Config.FacebookAPIKey.IsSet() || Config.TwitterConsumerKey.IsSet() || Config.GoogleClientID.IsSet()))
            {
                singleSignOnRow.Visible = true;

                var facebookEnabled = Config.FacebookAPIKey.IsSet() && Config.FacebookSecretKey.IsSet();
                var twitterEnabled = Config.TwitterConsumerKey.IsSet() && Config.TwitterConsumerSecret.IsSet();
                var googleEnabled = Config.GoogleClientID.IsSet() && Config.GoogleClientSecret.IsSet();

                string loginAuth = this.GetService<UrlProvider>().GetFirstQueryAsString();

                if (loginAuth.IsNotSet())
                {
                    if (facebookEnabled)
                    {
                        facebookRegister.Visible = true;
                        facebookRegister.Text = this.GetTextFormatted("AUTH_CONNECT", "Facebook");
                        facebookRegister.ToolTip = this.GetTextFormatted("AUTH_CONNECT_HELP", "Facebook");
                    }

                    if (twitterEnabled)
                    {
                        twitterRegister.Visible = true;
                        twitterRegister.Text = this.GetTextFormatted("AUTH_CONNECT", "Twitter");
                        twitterRegister.ToolTip = this.GetTextFormatted("AUTH_CONNECT_HELP", "Twitter");
                    }

                    if (googleEnabled)
                    {
                        googleRegister.Visible = true;
                        googleRegister.Text = this.GetTextFormatted("AUTH_CONNECT", "Google");
                        googleRegister.ToolTip = this.GetTextFormatted("AUTH_CONNECT_HELP", "Google");
                    }
                }
                else
                {
                    singleSignOnOptionsRow.Visible = true;

                    facebookRegister.Visible = false;
                    twitterRegister.Visible = false;
                    googleRegister.Visible = false;

                    userNameRow.Visible = false;
                    passwordRow.Visible = false;
                    registerLinkPlaceHolder.Visible = false;
                    passwordRecovery.Visible = false;
                    forumLogin.Visible = false;
                    rememberMe.Visible = false;
                    switch ((AuthService) Enum.Parse(typeof (AuthService), loginAuth, true))
                    {
                        case AuthService.twitter:
                        {
                            twitterHolder.Visible = twitterEnabled;

                            singleSignOnOptions.Items.Clear();

                            singleSignOnOptions.Items.Add(
                                new ListItem
                                {
                                    Value = "login",
                                    Text = this.GetTextFormatted("AUTH_LOGIN_EXISTING", "Twitter"),
                                    Selected = true
                                });
                            singleSignOnOptions.Items.Add(
                                new ListItem
                                {
                                    Value = "connect",
                                    Text =
                                        this.GetTextFormatted(
                                            "AUTH_CONNECT_ACCOUNT",
                                            "Twitter",
                                            this.GetText("AUTH_CONNECT_TWITTER"))
                                });

                            if (twitterEnabled)
                            {
                                try
                                {
                                    var twitterLoginUrl = YafSingleSignOnUser.GenerateLoginUrl(AuthService.twitter, true);

                                    // Redirect the user to Twitter for authorization.
                                    twitterLogin.Attributes.Add("onclick", twitterLoginUrl);
                                }
                                catch (Exception exception)
                                {
                                    this.Logger.Warn(
                                        exception,
                                        "YAF encountered an error when loading the Twitter Login Link");

                                    twitterHolder.Visible = false;
                                }
                            }
                        }

                            break;
                        case AuthService.facebook:
                        {
                            facebookHolder.Visible = facebookEnabled;

                            singleSignOnOptions.Items.Clear();

                            singleSignOnOptions.Items.Add(
                                new ListItem
                                {
                                    Value = "login",
                                    Text = this.GetTextFormatted("AUTH_LOGIN_EXISTING", "Facebook"),
                                    Selected = true
                                });
                            singleSignOnOptions.Items.Add(
                                new ListItem
                                {
                                    Value = "connect",
                                    Text =
                                        this.GetTextFormatted(
                                            "AUTH_CONNECT_ACCOUNT",
                                            "Facebook",
                                            this.GetText("AUTH_CONNECT_FACEBOOK"))
                                });

                            if (facebookEnabled)
                            {
                                try
                                {
                                    var facebookLoginUrl = YafSingleSignOnUser.GenerateLoginUrl(AuthService.facebook,
                                        true);

                                    // Redirect the user to Twitter for authorization.
                                    facebookLogin.Attributes.Add(
                                        "onclick",
                                        "location.href='{0}'".FormatWith(facebookLoginUrl));
                                }
                                catch (Exception exception)
                                {
                                    this.Logger.Warn(
                                        exception,
                                        "YAF encountered an error when loading the facebook Login Link");

                                    facebookHolder.Visible = false;
                                }
                            }
                        }

                            break;
                        case AuthService.google:
                        {
                            googleHolder.Visible = googleEnabled;

                            singleSignOnOptions.Items.Clear();

                            singleSignOnOptions.Items.Add(
                                new ListItem
                                {
                                    Value = "login",
                                    Text = this.GetTextFormatted("AUTH_LOGIN_EXISTING", "Google"),
                                    Selected = true
                                });
                            singleSignOnOptions.Items.Add(
                                new ListItem
                                {
                                    Value = "connect",
                                    Text =
                                        this.GetTextFormatted(
                                            "AUTH_CONNECT_ACCOUNT",
                                            "Facebook",
                                            this.GetText("AUTH_CONNECT_GOOGLE"))
                                });

                            if (googleEnabled)
                            {
                                try
                                {
                                    var googleLoginUrl = YafSingleSignOnUser.GenerateLoginUrl(AuthService.google, true);

                                    // Redirect the user to Twitter for authorization.
                                    googleLogin.Attributes.Add(
                                        "onclick",
                                        "location.href='{0}'".FormatWith(googleLoginUrl));
                                }
                                catch (Exception exception)
                                {
                                    this.Logger.Warn(
                                        exception,
                                        "YAF encountered an error when loading the Google Login Link");

                                    googleHolder.Visible = false;
                                }
                            }
                        }

                            break;
                    }
                }
            }

            this.DataBind();
        }

        /// <summary>
        ///     Called when Password Recovery is Clicked
        /// </summary>
        /// <param name="sender">
        ///     standard event object sender
        /// </param>
        /// <param name="e">
        ///     event args
        /// </param>
        protected void PasswordRecovery_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.recoverpassword);
        }

        /// <summary>
        ///     Show the Facebook Login/Register Form
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void FacebookFormClick(object sender, EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.login, "auth={0}", "facebook");
        }

        /// <summary>
        ///     Show the Twitter Login/Register Form
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void TwitterFormClick(object sender, EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.login, "auth={0}", "twitter");
        }

        /// <summary>
        ///     Show the Google Login/Register Form
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void GoogleFormClick(object sender, EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.login, "auth={0}", "google");
        }

        /// <summary>
        ///     Redirects to the Register Page
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void RegisterLinkClick(object sender, EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.register);
        }

        /// <summary>
        ///     Check if we need to display the Login form
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void SingleSignOnOptionsChanged(object sender, EventArgs e)
        {
            var singleSignOnOptions = this.Login1.FindControlAs<RadioButtonList>("SingleSignOnOptions");

            var userNameRow = this.Login1.FindControlAs<HtmlTableRow>("UserNameRow");
            var passwordRow = this.Login1.FindControlAs<HtmlTableRow>("PasswordRow");
            var forumLogin = this.Login1.FindControlAs<Button>("LoginButton");

            var facebookHolder = this.Login1.FindControlAs<PlaceHolder>("FacebookHolder");
            var twitterHolder = this.Login1.FindControlAs<PlaceHolder>("TwitterHolder");
            var googleHolder = this.Login1.FindControlAs<PlaceHolder>("GoogleHolder");

            var loginAuth =
                (AuthService)
                    Enum.Parse(typeof (AuthService),
                        this.GetService<UrlProvider>().GetFirstQueryAsString(), true);

            switch (singleSignOnOptions.SelectedValue)
            {
                case "connect":
                {
                    userNameRow.Visible = true;
                    passwordRow.Visible = true;
                    forumLogin.Visible = true;

                    facebookHolder.Visible = false;
                    twitterHolder.Visible = false;
                    googleHolder.Visible = false;

                    switch (loginAuth)
                    {
                        case AuthService.twitter:
                        {
                            this.Login1.DestinationPageUrl = YafSingleSignOnUser.GenerateLoginUrl(
                                AuthService.twitter,
                                false,
                                true);
                        }

                            break;
                        case AuthService.facebook:
                        {
                            this.Login1.DestinationPageUrl = YafSingleSignOnUser.GenerateLoginUrl(
                                AuthService.facebook,
                                false,
                                true);
                        }

                            break;
                        case AuthService.google:
                        {
                            this.Login1.DestinationPageUrl = YafSingleSignOnUser.GenerateLoginUrl(
                                AuthService.google,
                                false,
                                true);
                        }

                            break;
                    }
                }

                    break;
                default:
                {
                    userNameRow.Visible = false;
                    passwordRow.Visible = false;
                    forumLogin.Visible = false;

                    switch (loginAuth)
                    {
                        case AuthService.twitter:
                        {
                            twitterHolder.Visible = true;
                        }

                            break;
                        case AuthService.facebook:
                        {
                            facebookHolder.Visible = true;
                        }

                            break;
                        case AuthService.google:
                        {
                            googleHolder.Visible = true;
                        }

                            break;
                    }
                }

                    break;
            }
        }

        #endregion
    }
}