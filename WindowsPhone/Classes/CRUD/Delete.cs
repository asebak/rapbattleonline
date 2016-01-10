using System;
using System.Net;
using Common.Types;
using Newtonsoft.Json;

namespace FreestyleOnline___WP.Classes.CRUD
{
    public class Delete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Post" /> class.
        /// </summary>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="e">The e.</param>
        /// <param name="actionName">Name of the action.</param>
        public Delete(string controllerName, int id, UploadStringCompletedEventHandler e = null, string actionName = "")
        {
            var api = new WebApi(controllerName);
            var webClient = new RapWebClient {CookieContainer = RapClientCookie.Current};
            webClient.UploadStringCompleted += e;
            webClient.UploadStringAsync(string.IsNullOrEmpty(actionName) ? 
                new Uri(api.Delete(id)) : 
                new Uri(api.DeleteByAction(id, actionName)), "DELETE", "");
        }
    }
}