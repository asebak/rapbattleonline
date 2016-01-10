#region Using

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Core;
using YAF.Core.Services;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Utils;
using YAF.Utils.Helpers;
using HyperLink = System.Web.UI.WebControls.HyperLink;
using ImageButton = Ext.Net.ImageButton;
using Label = Ext.Net.Label;

#endregion

namespace FreestyleOnline
{
    /// <summary>
    ///     Class SiteMaster
    /// </summary>
    public partial class SiteMaster : RapMasterPage
    {

        #region Methods

        /// <summary>
        ///     Gets the dialog.
        /// </summary>
        /// <value>
        ///     The dialog.
        /// </value>
        /// <summary>
        ///     The get return url.
        /// </summary>
        /// <returns>
        ///     The url.
        /// </returns>
        protected string GetReturnUrl()
        {
            return
                HttpContext.Current.Server.UrlEncode(
                    HttpContext.Current.Request.QueryString.GetFirstOrDefault("ReturnUrl").IsSet()
                        ? General.GetSafeRawUrl(HttpContext.Current.Request.QueryString.GetFirstOrDefault("ReturnUrl"))
                        : General.GetSafeRawUrl());
        }

        protected override void OnInit([NotNull] EventArgs e)
        {
            if (this.IsPostBack || !this.PageContext.IsGuest)
            {
                return;
            }
            var facebookHolder = this.HeadLoginView.FindControlAs<PlaceHolder>("FacebookHolder");
            var facebookLogin = this.HeadLoginView.FindControlAs<HtmlAnchor>("FacebookLogin");

            var twitterHolder = this.HeadLoginView.FindControlAs<PlaceHolder>("TwitterHolder");
            var twitterLogin = this.HeadLoginView.FindControlAs<HtmlAnchor>("TwitterLogin");

            var googleHolder = this.HeadLoginView.FindControlAs<PlaceHolder>("GoogleHolder");
            var googleLogin = this.HeadLoginView.FindControlAs<HtmlAnchor>("GoogleLogin");
            var facebookEnabled = YAF.Classes.Config.FacebookAPIKey.IsSet() && YAF.Classes.Config.FacebookSecretKey.IsSet();
            var twitterEnabled = YAF.Classes.Config.TwitterConsumerKey.IsSet() && YAF.Classes.Config.TwitterConsumerSecret.IsSet();
            var googleEnabled = YAF.Classes.Config.GoogleClientID.IsSet() && YAF.Classes.Config.GoogleClientSecret.IsSet();

            twitterHolder.Visible = twitterEnabled;
            facebookHolder.Visible = facebookEnabled;
            googleHolder.Visible = googleEnabled;
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
                    twitterHolder.Visible = false;
                }
            }
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
                    facebookHolder.Visible = false;
                }
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
                        googleHolder.Visible = false;
                    }
                }
            }

            base.OnInit(e);
        }

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.RegisterjQueryUi();
            this.RegisterSapUi5();
            this.RegisterBootStrap();
            this.RegisterGlobalCss();
            this.RegisterAudioJs();
            this.HandleButtonAutoPostback();
            this.RegisterAlertify();
            this.RegisterSignalR();
            this.RegisterExtraJsFiles();
            if (!YafContext.Current.IsGuest)
            {
                if (this.HeadLoginView.FindControlAs<Label>("InboxSymbol") != null)
                {
                    this.HeadLoginView.FindControlAs<Label>("InboxSymbol").Text =
                        YafContext.Current.UnreadPrivate.ToString();
                    this.HeadLoginView.FindControlAs<Label>("InboxSymbol").Visible = YafContext.Current.UnreadPrivate !=
                                                                                     0;
                    if (this.HeadLoginView.FindControlAs<ImageButton>("AdminLink") != null)
                    {
                        this.HeadLoginView.FindControlAs<ImageButton>("AdminLink").Visible =
                            YafContext.Current.IsAdmin;
                    }
                }
            }
            var loginLink = this.HeadLoginView.FindControlAs<HyperLink>("LoginLink");
            if (loginLink != null)
            {
                loginLink.NavigateUrl = "~/forum/forum.aspx?g=login&ReturnUrl={0}".FormatWith(this.GetReturnUrl());
            }
        }

        /// <summary>
        /// Registers the alertify.
        /// </summary>
        private void RegisterAlertify()
        {
            this.GetService<ClientProviders>().RegisterRawScript(this.Page, "alertify",
                "<script type='text/javascript'" +
                "src='/js/scripts/alertify.min.js'>" +
                "</script>");
        }

        /// <summary>
        /// Handles the button automatic postback.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        private void HandleButtonAutoPostback()
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "buttonAutoPostback",
                    string.Format("function handlepostback(id){{id.disable();id.setText('{0}')}}",
                        this.Text("COMMON", "COMMON_PROCESSING")));
        }

        /// <summary>
        ///     Registerjs the query UI.
        /// </summary>
        private void RegisterjQueryUi()
        {
            this.GetService<ClientProviders>().RegisterRawScript(this.Page, "jQueryUI",
                "<script type='text/javascript'" +
                "src='http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js'>" +
                "</script>");
        }

        /// <summary>
        ///     Registers SAP UI5 Globally
        /// </summary>
        private void RegisterSapUi5()
        {
            this.GetService<ClientProviders>().RegisterRawScript(this.Page, "SapUI5",
                "<script id='sap-ui-bootstrap'" +
                "type='text/javascript'" +
                "src='https://sapui5.hana.ondemand.com/resources/sap-ui-core-nojQuery.js'" +
                "data-sap-ui-theme='sap_platinum'" +
                "data-sap-ui-libs='sap.ui.commons'>" +
                "</script>");
        }

        /// <summary>
        ///     Registers Bootstrap CSS and JS Globally
        /// </summary>
        private void RegisterBootStrap()
        {
            this.GetService<ClientProviders>().RegisterRawScript(this.Page, "BootStrapCSS",
                "<link rel='stylesheet' href='//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css' />");
            this.GetService<ClientProviders>().RegisterRawScript(this.Page, "FontAwsomeCSS",
                "<link rel='stylesheet' href='//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css' />");
            this.GetService<ClientProviders>().RegisterRawScript(this.Page, "BootStrapJS",
                "<script type='text/javascript' src='//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js'></script>");
            this.GetService<ClientProviders>().RegisterRawScript(this.Page, "bootstrapeditablecss",
                "<link rel='stylesheet' href='//cdnjs.cloudflare.com/ajax/libs/x-editable/1.5.0/bootstrap3-editable/css/bootstrap-editable.css' />");
            this.GetService<ClientProviders>().RegisterRawScript(this.Page, "bootstrapeditable",
               "<script type='text/javascript' src='//cdnjs.cloudflare.com/ajax/libs/x-editable/1.5.0/bootstrap3-editable/js/bootstrap-editable.min.js'></script>");
        }

        /// <summary>
        ///     Registers SignalR Scripts
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        private void RegisterSignalR()
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "signalR", "~/js/Scripts/jquery.signalR-1.1.3.min.js", false, true);
        }

        /// <summary>
        ///     Registers Audio Enhancement Control
        /// </summary>
        private void RegisterAudioJs()
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "audioPlayerScript", "~/js/Scripts/audioplayer.js", true);
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "audioPlayer", "$(function() { $('audio').audioPlayer(); });");
        }

        /// <summary>
        /// Registers the extra js files.
        /// </summary>
        private void RegisterExtraJsFiles()
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "jQueryNotification", "~/forum/resources/js/jquery.notification.js",
                    true);
            this.Page.Header.Controls.Add(
                new LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" +
                                   ResolveUrl("~/forum/resources/css/jquery.notification.css") + "\" />"));
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "yafmodal", "~/forum/resources/js/jquery.yafmodaldialog.js",
                    true);
            this.Page.Header.Controls.Add(
                new LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" +
                                   ResolveUrl("~/forum/resources/css/jquery.yafmodaldialog.css") + "\" />"));
        }

        /// <summary>
        ///     Registers Rapbattleonline.css minimized version
        /// </summary>
        private void RegisterGlobalCss()
        {
            this.Page.Header.Controls.Add(
                new LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" +
                                   ResolveUrl("~/Styles/rapbattleonline.css") + "\" />"));
        }

        #endregion

        #region Client Handlers
        /// <summary>
        ///     Handles the DirectClick event of the InboxLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Ext.Net.DirectEventArgs" /> instance containing the event data.</param>
        protected void InboxLink_DirectClick([NotNull] object sender, [NotNull] DirectEventArgs e)
        {
            this.GetService<UrlProvider>().Redirect("~/Forum/cp_pm");
        }

        /// <summary>
        ///     Handles the DirectClick event of the AdminLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Ext.Net.DirectEventArgs" /> instance containing the event data.</param>
        protected void AdminLink_DirectClick([NotNull] object sender, [NotNull] DirectEventArgs e)
        {
            this.GetService<UrlProvider>().Redirect("~/Pages/Admin");
        }

        /// <summary>
        /// Handles the DirectClick event of the FriendsLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectEventArgs"/> instance containing the event data.</param>
        protected void FriendsLink_DirectClick([NotNull]object sender, [NotNull] DirectEventArgs e)
        {
            this.Friends.FriendWindow.Show();
        }

        /// <summary>
        /// Handles the DirectClick event of the BugReportLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectEventArgs"/> instance containing the event data.</param>
        protected void BugReportLink_DirectClick([NotNull] object sender, [NotNull] DirectEventArgs e)
        {
            this.WorkItem.GetWindow.Show();
        }

        /// <summary>
        /// Handles the DirectClick event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectEventArgs"/> instance containing the event data.</param>
        protected void HelpLink_DirectClick([NotNull] object sender, [NotNull] DirectEventArgs e)
        {
            this.GetService<UrlProvider>().Redirect("~/Pages/Help");

        }

        /// <summary>
        /// Handles the DirectClick event of the CpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectEventArgs"/> instance containing the event data.</param>
        protected void CpLink_DirectClick([NotNull] object sender, [NotNull] DirectEventArgs e)
        {
            this.GetService<UrlProvider>().Redirect("~/forum/cp_profile");
        }
        #endregion
    }
}