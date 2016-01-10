#region Using

using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNet.FriendlyUrls;

#endregion

namespace FreestyleOnline.classes.Providers
{
    public class UrlProvider : BaseRapProvider
    {
        #region Methods

        /// <summary>
        ///     Sets the URL.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="query">The query.</param>
        public string GetUrl(string path, params object[] query)
        {
            return FriendlyUrl.Href(path, query);
        }

        /// <summary>
        ///     Redirects the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Redirect(string path)
        {
            HttpContext.Current.Response.Redirect(GetUrl(path));
        }

        /// <summary>
        ///     Redirects the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="query">The query.</param>
        public void Redirect(string path, params object[] query)
        {
            HttpContext.Current.Response.Redirect(GetUrl(path, query));
        }

        /// <summary>
        ///     Refreshes the page.
        /// </summary>
        public void RefreshPage()
        {
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
        }

        /// <summary>
        ///     Gets the query.
        /// </summary>
        /// <returns></returns>
        public IList<string> GetQuery()
        {
            return HttpContext.Current.Request.GetFriendlyUrlSegments();
        }

        /// <summary>
        /// Gets the first query as int.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Web.HttpParseException"></exception>
        public int GetFirstQueryAsInt()
        {
            return HttpContext.Current.Request.GetFriendlyUrlSegments().Count > 0
                ? Convert.ToInt32(HttpContext.Current.Request.GetFriendlyUrlSegments()[0])
                : 0;
            //todo go to custom query string error page
            //throw new HttpParseException();
        }

        /// <summary>
        /// Gets the first query as string.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Web.HttpParseException"></exception>
        public string GetFirstQueryAsString()
        {
            if (HttpContext.Current.Request.GetFriendlyUrlSegments().Count > 0)
            {
                return HttpContext.Current.Request.GetFriendlyUrlSegments()[0];
            }
            //todo go to custom query string error page
            throw new HttpParseException();
        }

        #endregion
    }
}