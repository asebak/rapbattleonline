#region Using

using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class LogoutController : ApiController
    {
        /// <summary>
        ///     Logouts this instance.
        /// </summary>
        [HttpPost]
        public HttpResponseMessage Logout()
        {
            var httpContext = HttpContext.Current;
            if (httpContext.Session != null)
            {
                httpContext.Session.Clear();
                httpContext.Session.Abandon();
            }
            FormsAuthentication.SignOut();
            httpContext.Response.Cache.SetExpires(DateTime.Now);
            httpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            httpContext.Response.Cache.SetNoStore();
            httpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            httpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            httpContext.Response.Cache.SetNoStore();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}