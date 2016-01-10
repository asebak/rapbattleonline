#region Using

using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Core;
using YAF.Types.Interfaces;

#endregion

namespace FreestyleOnline.classes.Base
{
    /// <summary>
    ///     Base Class For All Classes
    /// </summary>
    public class RapClass : IRapServiceProvider, IRapTextElement
    {
        /// <summary>
        /// Gets the rap context.
        /// </summary>
        /// <value>
        /// The rap context.
        /// </value>
        public RapContextFacade RapContext
        {
            get { return RapContextFacade.Current; }
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
            return YafContext.Current.Get<ILocalization>()
                .GetText(page, textTagElement, this.GetService<ResourceProvider>().GetPath(RapResource.Languages));
        }

        /// <summary>
        /// Posts the social feed.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <param name="t">The t.</param>
        public void PostSocialFeed(int userId, int objectId, RapSocialFeedType t)
        {
            new RapSocialFeed().Submit(userId, objectId, t);
        }


        #endregion
    }
}