#region Using

using System;
using System.Net;
using Common.Types;
using Newtonsoft.Json;

#endregion

namespace FreestyleOnline___WP.Classes.CRUD
{
    public class Post
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Post" /> class.
        /// </summary>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="payLoad">The pay load.</param>
        /// <param name="e">The e.</param>
        /// <param name="methodUrl">The method URL after the controller name from web api.</param>
        public Post(string controllerName, object payLoad, UploadStringCompletedEventHandler e = null,
            string methodUrl = "")
        {
            var api = new WebApi(controllerName);
            var url = new Uri(api.Post() + methodUrl);
            var webClient = new RapWebClient {CookieContainer = RapClientCookie.Current};
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.UploadStringCompleted += e;
            webClient.UploadStringAsync(url, "POST", JsonConvert.SerializeObject(payLoad));
        }
    }
}