using System;
using System.Collections.Specialized;
using System.Web;
using FreestyleOnline.classes.Core;
using Intelligencia.UrlRewriter;
using Intelligencia.UrlRewriter.Utilities;
using YAF.Classes.Pattern;

namespace FreestyleOnline.classes.Types
{
    public class RapContextFacade : IContextFacade
    {
        public RapContextFacade()
		{
			_mapPath = InternalMapPath;
		}

		public MapPath MapPath
		{
			get
			{
				return _mapPath;
			}
		}

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public static RapContextFacade Current
        {
            get { return PageSingleton<RapContextFacade>.Instance; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is mobile.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is mobile; otherwise, <c>false</c>.
        /// </value>
        public bool IsMobile
        {
            get { return System.Web.HttpContext.Current.Request.Browser.IsMobileDevice; }
        }


        /// <summary>
		/// Retrieves the application path.
		/// </summary>
		/// <returns>The application path.</returns>
		public string GetApplicationPath()
		{
            return System.Web.HttpContext.Current.Request.ApplicationPath;
		}

        /// <summary>
        /// Gets the user identity.
        /// </summary>
        /// <returns></returns>
        public string GetUserIdentity()
        {
            return System.Web.HttpContext.Current.User.Identity.Name;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is guest.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is guest; otherwise, <c>false</c>.
        /// </value>
        public bool IsGuest
        {
            get { return !System.Web.HttpContext.Current.User.Identity.IsAuthenticated; }
        }

        public int GetUserId()
        {
            return UserData.GetUserIdFromDisplayName(System.Web.HttpContext.Current.User.Identity.Name);
        }

		/// <summary>
		/// Retrieves the raw url.
		/// </summary>
		/// <returns>The raw url.</returns>
		public string GetRawUrl()
		{
            return System.Web.HttpContext.Current.Request.RawUrl;
		}

		/// <summary>
		/// Retrieves the current request url.
		/// </summary>
		/// <returns>The request url.</returns>
		public Uri GetRequestUrl()
		{
            return System.Web.HttpContext.Current.Request.Url;
		}

		/// <summary>
		/// Maps the url to the local file path.
		/// </summary>
		/// <param name="url">The url to map.</param>
		/// <returns>The local file path.</returns>
		private string InternalMapPath(string url)
		{
            return System.Web.HttpContext.Current.Server.MapPath(url);
		}

		/// <summary>
		/// Sets the status code for the response.
		/// </summary>
		/// <param name="code">The status code.</param>
		public void SetStatusCode(int code)
		{
            System.Web.HttpContext.Current.Response.StatusCode = code;
		}

		/// <summary>
		/// Rewrites the request to the new url.
		/// </summary>
		/// <param name="url">The new url to rewrite to.</param>
		public void RewritePath(string url)
		{
            System.Web.HttpContext.Current.RewritePath(url, false);
		}

		/// <summary>
		/// Sets the redirection location to the given url.
		/// </summary>
		/// <param name="url">The url of the redirection location.</param>
		public void SetRedirectLocation(string url)
		{
            System.Web.HttpContext.Current.Response.RedirectLocation = url;
		}

		/// <summary>
		/// Appends a header to the response.
		/// </summary>
		/// <param name="name">The header name.</param>
		/// <param name="value">The header value.</param>
		public void AppendHeader(string name, string value)
		{
            System.Web.HttpContext.Current.Response.AppendHeader(name, value);
		}

		/// <summary>
		/// Adds a cookie to the response.
		/// </summary>
		/// <param name="cookie">The cookie to add.</param>
		public void AppendCookie(HttpCookie cookie)
		{
            System.Web.HttpContext.Current.Response.AppendCookie(cookie);
		}

		/// <summary>
		/// Handles an error with the error handler.
		/// </summary>
		/// <param name="handler">The error handler to use.</param>
		public void HandleError(IRewriteErrorHandler handler)
		{
            handler.HandleError(System.Web.HttpContext.Current);
		}

		/// <summary>
		/// Sets a context item.
		/// </summary>
		/// <param name="item">The item key</param>
		/// <param name="value">The item value</param>
		public void SetItem(object item, object value)
		{
            System.Web.HttpContext.Current.Items[item] = value;
		}

		/// <summary>
		/// Retrieves a context item.
		/// </summary>
		/// <param name="item">The item key.</param>
		/// <returns>The item value.</returns>
		public object GetItem(object item)
		{
            return System.Web.HttpContext.Current.Items[item];
		}

		/// <summary>
		/// Retrieves the HTTP method used by the request.
		/// </summary>
		/// <returns>The HTTP method.</returns>
		public string GetHttpMethod()
		{
            return System.Web.HttpContext.Current.Request.HttpMethod;
		}

		/// <summary>
		/// Gets a collection of server variables.
		/// </summary>
		/// <returns></returns>
		public NameValueCollection GetServerVariables()
		{
            return System.Web.HttpContext.Current.Request.ServerVariables;
		}

		/// <summary>
		/// Gets a collection of headers.
		/// </summary>
		/// <returns></returns>
		public NameValueCollection GetHeaders()
		{
            return System.Web.HttpContext.Current.Request.Headers;
		}

		/// <summary>
		/// Gets a collection of cookies.
		/// </summary>
		/// <returns></returns>
		public HttpCookieCollection GetCookies()
		{
            return System.Web.HttpContext.Current.Request.Cookies;
		}

		private readonly MapPath _mapPath;
	}
}