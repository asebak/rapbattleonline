#region Using

using System;
using System.Web.UI;
using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Classes;
using YAF.Core;
using YAF.Core.Services;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Interfaces;
using YAF.Utilities;
using YAF.Utils;

#endregion

namespace FreestyleOnline.classes.Base
{
    public class RapPage : Page, IRapTextElement, IRapServiceProvider, IRapCore, IRapFactory
    {
        #region Properties

        /// <summary>
        ///     Gets a value indicating whether [is mobile].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [is mobile]; otherwise, <c>false</c>.
        /// </value>
        public bool IsMobile
        {
            get { return Request.Browser.IsMobileDevice; }
        }

        /// <summary>
        ///     Gets PageContext.
        /// </summary>
        public YafContext PageContext
        {
            get { return YafContext.Current; }
        }

        /// <summary>
        ///     Gets ServiceLocator.
        /// </summary>
        public IServiceLocator ServiceLocator
        {
            get { return this.PageContext.ServiceLocator; }
        }

        #endregion

        #region IRapCore Members

        private LoadMessage _loadMessage;

        /// <summary>
        ///     Gets the core.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetCore<T>() where T : RapClass, new()
        {
            return new T();
        }
        /// <summary>
        ///     Gets the load message.
        /// </summary>
        /// <value>
        ///     The load message.
        /// </value>
        public LoadMessage LoadMessage
        {
            get { return this._loadMessage ?? (this._loadMessage = new LoadMessage()); }
        }

        /// <summary>
        ///     Adds the load message session.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageType">Type of the message.</param>
        public void AddLoadMessageSession([NotNull] string message, MessageTypes messageType = MessageTypes.Success)
        {
            this.LoadMessage.AddSession(message, messageType);
        }
        #endregion

        #region IRapServiceProvider Members

        /// <summary>
        ///     Gets the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>() where T : BaseRapProvider, new()
        {
            return new T();
        }

        #endregion

        #region IRapTextElement Members

        /// <summary>
        ///     Texts the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="textTagElement">The text tag element.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string Text(string page, string textTagElement)
        {
            return YafContext.Current.Get<ILocalization>()
                .GetText(page, textTagElement, this.GetService<ResourceProvider>().GetPath(RapResource.Languages));
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        ///     Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                var csType = typeof (Page);

                var uRlToResource = Config.JQueryFile;

                if (!uRlToResource.StartsWith("http"))
                {
                    uRlToResource = YafForumInfo.GetURLToResource(Config.JQueryFile);
                }

                ScriptManager.RegisterClientScriptInclude(this, csType, "JQuery", uRlToResource);

                ScriptManager.RegisterClientScriptInclude(
                    this, csType, "jqueryTimeagoscript", YafForumInfo.GetURLToResource("js/jquery.timeago.js"));

                ScriptManager.RegisterStartupScript(this, csType, "timeagoloadjs", JavaScriptBlocks.TimeagoLoadJs,
                    true);
            }
            catch (Exception)
            {
                this.Response.Redirect("~/forum/install/default.aspx");
            }
            base.OnLoad(e);
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Web.UI.Page.PreInit" /> event at the beginning of page initialization.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            //if (this.IsMobile)
            //{
            //    //MasterPageFile = "~/Mobile/Mobile.Master";
            //}
            //base.OnPreInit(e);
        }

        #endregion

        public T GetFactory<T>() where T : BaseFactory, new()
        {
            return new T();
        }
    }
}