#region Using

using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Types;
using YAF.Utils;

#endregion

namespace FreestyleOnline.classes.Secruity
{
    public class WebApiValidation
    {
        /// <summary>
        ///     Validates Web Api Requests from caller used to make sure people dont call api from browser
        /// </summary>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        public static bool Validate(HttpRequestMessage request)
        {
            if (request.Headers.Referrer != null)
            {
                //TODO: make sure this works
                //make sure the call comes from localhost, or the computer hosting the site or the main domain
                var urlCaller = request.Headers.Referrer.ToString();
                if ((urlCaller.Contains("localhost") ||
                     urlCaller.Contains(RapGlobalHelpers.Address) || urlCaller.Contains("rapbattleonline")) && request.Headers.Referrer.Host == request.RequestUri.Host)
                {
                    return true;
                }
            }
            throw new HttpResponseException(HttpStatusCode.Forbidden);
        }
    }
}