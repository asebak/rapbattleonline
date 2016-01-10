#region Using

using System.Diagnostics.Contracts;
using System.IO;
using System.Web.UI;
using Ext.Net;
using FreestyleOnline.classes.Types;
using YAF.Core;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;
using YAF.Utils.Helpers;
using Yahoo.Yui.Compressor;

#endregion

namespace FreestyleOnline.classes.Providers
{
    /// <summary>
    ///     Class To provide easy access to controls to the client
    /// </summary>
    public class ClientProviders : BaseRapProvider
    {
        #region Methods

        /// <summary>
        ///     Registers a raw external css or js file.  This should be used if the file is already minimized
        /// </summary>
        /// <param name="webpage">The webpage.</param>
        /// <param name="scriptName">Name of the script.</param>
        /// <param name="rawScript">The raw script.</param>
        public void RegisterRawScript(Page webpage, string scriptName, string rawScript)
        {
            Contract.Requires(webpage != null);
            webpage.Page.ClientScript.RegisterClientScriptBlock(webpage.GetType(), scriptName,
                string.Format("{0}", rawScript));
        }

        /// <summary>
        ///     Registers a raw external css or js file.  This should be used if the file is already minimized
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="scriptName">Name of the script.</param>
        /// <param name="rawScript">The raw script.</param>
        public void RegisterRawScript(UserControl control, string scriptName, string rawScript)
        {
            Contract.Requires(control != null);
            control.Page.ClientScript.RegisterClientScriptBlock(control.GetType(), scriptName,
                string.Format("{0}", rawScript));
        }

        /// <summary>
        ///     Registers a script near the end of the form only allowed to register Js scripts.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="scriptName">Name of the script.</param>
        /// <param name="script">The script.</param>
        /// <param name="minimized">if set to <c>true</c> [minimized].</param>
        public void RegisterStartUpScript(UserControl control, string scriptName, string script, bool minimized = false)
        {
            Contract.Requires(control != null);
            if (minimized)
            {
                script = this.MinimizeJsCode(script);
            }
            control.Page.ClientScript.RegisterStartupScript(control.GetType(), scriptName,
                string.Format("<script>{0}</script>", script));
        }


        /// <summary>
        ///     Registers a script near the end of the form only allowed to register Js scripts.
        /// </summary>
        /// <param name="webPage">The web page.</param>
        /// <param name="scriptName">Name of the script.</param>
        /// <param name="script">The script.</param>
        /// <param name="minimized">if set to <c>true</c> [minimized].</param>
        public void RegisterStartUpScript(Page webPage, string scriptName, string script, bool minimized = false)
        {
            Contract.Requires(webPage != null);
            if (minimized)
            {
                script = this.MinimizeJsCode(script);
            }
            webPage.Page.ClientScript.RegisterStartupScript(webPage.GetType(), scriptName,
                string.Format("<script>{0}</script>", script));
        }

        /// <summary>
        ///     Registers a Js Script at the header of a page. Can use minimization and external files based on parameters passed
        ///     to function.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="scriptName">Name of the script.</param>
        /// <param name="script">The script.</param>
        /// <param name="minimized">if set to <c>true</c> [minimized].</param>
        /// <param name="isExternalFile">if set to <c>true</c> [is external file].</param>
        public void RegisterClientScriptBlock(UserControl control, string scriptName, string script,
            bool minimized = false, bool isExternalFile = false)
        {
            Contract.Requires(control != null);
            if (minimized)
            {
                script = this.MinimizeJsCode(script);
            }
            if (isExternalFile)
            {
                //TODO: verifiy map path
                script = File.ReadAllText(this.RapContext.MapPath(script));
            }
            control.Page.ClientScript.RegisterClientScriptBlock(control.GetType(), scriptName,
                string.Format("<script>{0}</script>", script));
        }


        /// <summary>
        ///     Registers a Js Script at the header of a page. Can use minimization and external files based on parameters passed
        ///     to function.
        /// </summary>
        /// <param name="webPage">The web page.</param>
        /// <param name="scriptName">Name of the script.</param>
        /// <param name="script">The script.</param>
        /// <param name="minimized">if set to <c>true</c> [minimized].</param>
        /// <param name="isExternalFile">if set to <c>true</c> [is external file].</param>
        public void RegisterClientScriptBlock(Page webPage, string scriptName, string script, bool minimized = false,
            bool isExternalFile = false)
        {
            Contract.Requires(webPage != null);
            if (minimized)
            {
                script = this.MinimizeJsCode(script);
            }
            if (isExternalFile)
            {
                script = File.ReadAllText(this.RapContext.MapPath(script));
            }
            webPage.Page.ClientScript.RegisterClientScriptBlock(webPage.GetType(), scriptName,
                string.Format("<script>{0}</script>", script));
        }


        /// <summary>
        ///     Displays the real time notification.
        /// </summary>
        /// <param name="title">The title of notification.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="autoHide">if set to <c>true</c> [automatic hide].</param>
        /// <param name="content">The content in the notification.</param>
        /// <param name="extComponent">The ext component.</param>
        public void DisplayRealTimeNotification(string title, Icon icon, int width, int height, bool autoHide,
            string content, AbstractComponent extComponent = null)
        {
            if (extComponent == null)
            {
                Notification.Show(new NotificationConfig
                {
                    Title = title,
                    Icon = icon,
                    Width = width,
                    Height = height,
                    AutoHide = autoHide,
                    Html = content,
                    AutoScroll = true
                });
            }
            else
            {
                Notification.Show(new NotificationConfig
                {
                    Title = title,
                    AlignCfg = new NotificationAlignConfig
                    {
                        ElementAnchor = AnchorPoint.Left,
                        TargetAnchor = AnchorPoint.Right,
                        OffsetX = 2,
                        OffsetY = 0,
                        El = extComponent.ConfigID
                    },
                    Icon = Icon.Error,
                    Width = width,
                    Height = height,
                    AutoHide = autoHide,
                    Html = content,
                    AutoScroll = true
                });
            }
        }

        /// <summary>
        /// Displays the real time notification.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="content">The content.</param>
        public void DisplayRealTimeNotification(UserControl control, string content)
        {
            control.Page.ClientScript.RegisterStartupScript(control.GetType(), "alertifyNotification",
                "alertify.log('{0}')".FormatWith(content), true);

        }

        /// <summary>
        ///     Displays the real time error.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="autoHide">if set to <c>true</c> [automatic hide].</param>
        /// <param name="content">The content.</param>
        /// <param name="extComponent">The ext component.</param>
        public void DisplayRealTimeError(int width, int height, bool autoHide, string content,
            AbstractComponent extComponent = null)
        {
            if (extComponent == null)
            {
                Notification.Show(new NotificationConfig
                {
                    Title =
                        YafContext.Current.Get<ILocalization>()
                            .GetText("COMMON", "COMMON_ERROR", new ResourceProvider().GetPath(RapResource.Languages)),
                    Icon = Icon.Error,
                    Width = width,
                    Height = height,
                    AutoHide = autoHide,
                    Html = content,
                    AutoScroll = true
                });
            }
            else
            {
                Notification.Show(new NotificationConfig
                {
                    Title =
                        YafContext.Current.Get<ILocalization>()
                            .GetText("COMMON", "COMMON_ERROR", new ResourceProvider().GetPath(RapResource.Languages)),
                    AlignCfg = new NotificationAlignConfig
                    {
                        ElementAnchor = AnchorPoint.Left,
                        TargetAnchor = AnchorPoint.Right,
                        OffsetX = 2,
                        OffsetY = 0,
                        El = extComponent.ConfigID
                    },
                    Icon = Icon.Error,
                    Width = width,
                    Height = height,
                    AutoHide = autoHide,
                    Html = content,
                    AutoScroll = true
                });
            }
        }

        /// <summary>
        /// Displays the real time error.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="content">The content.</param>
        public void DisplayRealTimeError(UserControl control, string content)
        {
            control.Page.ClientScript.RegisterStartupScript(control.GetType(), "alertifyError",
                "alertify.error('"+ content + "')", true);
        }

        /// <summary>
        /// Displays the real time success.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="content">The content.</param>
        public void DisplayRealTimeSuccess(UserControl control, string content)
        {
            control.Page.ClientScript.RegisterStartupScript(control.GetType(), "alertifySuccess",
                "alertify.success('{0}')".FormatWith(content), true);
        }

        /// <summary>
        ///     Minimizes the js code.
        /// </summary>
        /// <param name="inputJsFile">The input js file.</param>
        /// <returns></returns>
        public string MinimizeJsCode(string inputJsFile)
        {
            var jsContents = File.ReadAllText(this.RapContext.MapPath(inputJsFile));
            var jsCompressor = new JavaScriptCompressor();
            return jsCompressor.Compress(jsContents);
        }

        /// <summary>
        ///     Minimizes the CSS code.
        /// </summary>
        /// <param name="inputCssFile">The input CSS file.</param>
        /// <returns></returns>
        public string MinimizeCssCode(string inputCssFile)
        {
            var cssContents = File.ReadAllText(this.RapContext.MapPath(inputCssFile));
            var cssCompress = new CssCompressor();
            return cssCompress.Compress(cssContents);
        }

        #endregion
    }
}