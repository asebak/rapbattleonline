#region Using

using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types;
using YAF.Core;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class ProfileController : ApiController
    {
        #region API

        /// <summary>
        ///     Gets all profiles.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getallprofiles")]
        public HttpResponseMessage GetAllProfiles()
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                UserMembershipHelper.GetAllUsers().Cast<MembershipUser>().Select(x => x.UserName).ToList());
        }

        /// <summary>
        ///     Gets the user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getuserid")]
        public HttpResponseMessage GetUserId([FromUri] string id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, UserData.GetUserIdFromDisplayName(id));
        }

        /// <summary>
        ///     Gets the authentication status.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getauthenticationstatus")]
        public HttpResponseMessage GetAuthenticationStatus()
        {
            return Request.CreateResponse(HttpStatusCode.OK, HttpContext.Current.User.Identity.IsAuthenticated);
        }

        /// <summary>
        ///     Gets the profile context of the logged in user.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [ActionName("getprofilecontext")]
        public HttpResponseMessage GetProfileContext()
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                UserData.GetProfileContext(UserData.GetUserIdFromDisplayName(HttpContext.Current.User.Identity.Name)));
        }

        /// <summary>
        ///     Gets the profile context.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getprofilecontext")]
        public HttpResponseMessage GetProfileContext(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, UserData.GetProfileContext(id));
        }

        /// <summary>
        /// Creates the bio.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ActionName("createbio")]
        public HttpResponseMessage CreateBio(EditableField e)
        {
            Contract.Requires(e != null, "Cannot Leave an Empty field");
            if (e.Pk == RapContextFacade.Current.GetUserId())
            {
                new UserProfile(e.Pk).UpdateBio(e.Value);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }


        #endregion
    }
}