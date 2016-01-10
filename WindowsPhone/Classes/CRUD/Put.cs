#region Using

using System;
using System.Net;
using Common.Types;
using Newtonsoft.Json;

#endregion

namespace FreestyleOnline___WP.Classes.CRUD
{
    public class Put
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Put" /> class.
        /// </summary>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="payLoad">The pay load.</param>
        /// <param name="e">The e.</param>
        /// <param name="methodUrl">The method URL.</param>
        public Put(string controllerName, int id, object payLoad, UploadStringCompletedEventHandler e = null,
            string actionName = "")
        {
            var api = new WebApi(controllerName);
            var webClient = new RapWebClient {CookieContainer = RapClientCookie.Current};
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.UploadStringCompleted += e;
            webClient.UploadStringAsync(string.IsNullOrEmpty(actionName) ?
                new Uri(api.Put(id)) :
                new Uri(api.PutByAction(id, actionName)), "PUT", JsonConvert.SerializeObject(payLoad));
        }
    }
}