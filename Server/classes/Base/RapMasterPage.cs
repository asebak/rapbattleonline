#region Using

using System.Web.UI;
using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Core;
using YAF.Types.Interfaces;

#endregion

namespace FreestyleOnline.classes.Base
{
    public class RapMasterPage : MasterPage, IRapTextElement, IRapServiceProvider
    {
        #region Properties

        /// <summary>
        ///     Gets the page context.
        /// </summary>
        /// <value>
        ///     The page context.
        /// </value>
        public YafContext PageContext
        {
            get { return YafContext.Current; }
        }

        #endregion

        /// <summary>
        ///     Determines whether this instance is mobile.
        /// </summary>
        /// <returns></returns>
        public bool IsMobile
        {
            get { return this.Request.Browser.IsMobileDevice; }
        }

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
        public string Text(string page, string textTagElement)
        {
            var text = YafContext.Current.Get<ILocalization>()
                .GetText(page, textTagElement, this.GetService<ResourceProvider>().GetPath(RapResource.Languages));
            return text;
        }

        #endregion
    }
}