#region Using

using System;
using System.Net;

#endregion

namespace FreestyleOnline___WP.Classes.CRUD
{
    public class RapWebClient : WebClient
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RapWebClient" /> class.
        /// </summary>
        public RapWebClient()
        {
            CookieContainer = new CookieContainer();
        }

        /// <summary>
        ///     Gets or sets the cookie container.
        /// </summary>
        /// <value>
        ///     The cookie container.
        /// </value>
        public CookieContainer CookieContainer { get; set; }

        /// <summary>
        ///     Returns a <see cref="T:System.Net.WebRequest" /> object for the specified resource.
        /// </summary>
        /// <param name="address">A <see cref="T:System.Uri" /> that identifies the resource to request.</param>
        /// <returns>
        ///     A new <see cref="T:System.Net.WebRequest" /> object for the specified resource.
        /// </returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest) base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }
    }
}