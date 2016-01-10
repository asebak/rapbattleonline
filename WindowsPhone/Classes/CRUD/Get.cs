#region Using

using System;
using System.Net;
using Common.Types;

#endregion

namespace FreestyleOnline___WP.Classes.CRUD
{
    public class Get
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Get" /> class.
        /// </summary>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="e">The e.</param>
        /// <param name="methodUrl">The method URL.</param>
        public Get(string controllerName, DownloadStringCompletedEventHandler e = null, string methodUrl = "")
        {
            var api = new WebApi(controllerName);
            var webClient = new RapWebClient {CookieContainer = RapClientCookie.Current};
            webClient.DownloadStringCompleted += e;
            webClient.DownloadStringAsync(string.IsNullOrEmpty(methodUrl)
                ? new Uri(api.Get())
                : new Uri(api.GetByAction(methodUrl)));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Get" /> class.
        /// </summary>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="e">The e.</param>
        /// <param name="methodUrl">The method URL.</param>
        public Get(string controllerName, int id, DownloadStringCompletedEventHandler e = null, string methodUrl = "")
        {
            var api = new WebApi(controllerName);
            var webClient = new RapWebClient {CookieContainer = RapClientCookie.Current};
            webClient.DownloadStringCompleted += e;
            webClient.DownloadStringAsync(string.IsNullOrEmpty(methodUrl)
                ? new Uri(api.Get(id))
                : new Uri(api.GetByAction(id, methodUrl)));
        }
    }
}