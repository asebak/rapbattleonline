#region Using

using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Core;
using YAF.Core.Services;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Interfaces;

#endregion

namespace FreestyleOnline.classes.Base
{
    public class RapUserControl : BaseUserControl, IRapTextElement, IRapServiceProvider, IRapCore, IRapFactory
    {
        private LoadMessage _loadMessage;

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

        #region IRapCore Members

        /// <summary>
        ///     Gets the core.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetCore<T>() where T : RapClass, new()
        {
            return new T();
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
        ///     Gets the text for Rap Battle Online.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="textTagElement">The text tag element.</param>
        /// <returns></returns>
        public string Text([NotNull] string page, [NotNull] string textTagElement)
        {
            return this.Get<ILocalization>()
                .GetText(page, textTagElement, this.GetService<ResourceProvider>().GetPath(RapResource.Languages));
        }

        #endregion

        public T GetFactory<T>() where T : BaseFactory, new()
        {
            return new T();
        }
    }
}