#region Using

using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using Microsoft.AspNet.SignalR;
using YAF.Core;
using YAF.Types.Interfaces;

#endregion

namespace FreestyleOnline.classes.Base
{
    public class RapSignalR : Hub, IRapServiceProvider, IRapTextElement
    {
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
            return YafContext.Current.Get<ILocalization>()
                .GetText(page, textTagElement, this.GetService<ResourceProvider>().GetPath(RapResource.Languages));
        }

        #endregion
    }
}