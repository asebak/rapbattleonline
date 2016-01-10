#region Using

using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Common.Models;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Secruity;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class LoginController : ApiController
    {
        #region API

        /// <summary>
        ///     Logins the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Login(LoginModel userData)
        {
            Contract.Requires(userData != null);
            var userToAuthenticate = new UserAuthentication(userData.Username, userData.Password);
            var realUserName = userToAuthenticate.IsAuthenticated();
            if (!string.IsNullOrEmpty(realUserName))
            {
                var ticket = new FormsAuthenticationTicket(1, userData.Username, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, "", "/");
                var strEncTicket = FormsAuthentication.Encrypt(ticket);
                var authCookie = new HttpCookie(".YAFNET_Authentication", strEncTicket) {Path = "/"};
                HttpContext.Current.Response.Cookies.Add(authCookie);
                var usersProfile = UserData.GetProfileContext(UserData.GetUserIdFromDisplayName(realUserName));
                return Request.CreateResponse(HttpStatusCode.Created, usersProfile);
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        #endregion
    }
}