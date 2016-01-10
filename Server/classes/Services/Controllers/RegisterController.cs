#region Using

using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Models;
using FreestyleOnline.classes.Secruity;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class RegisterController : ApiController
    {
        /// <summary>
        ///     Registers the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RegisterUser(RegisterModel user)
        {
            Contract.Requires(user != null);
            try
            {
                var registerationManager = new UserRegistration(user.Username, user.Password, user.Email);
                registerationManager.RegisterUser();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}