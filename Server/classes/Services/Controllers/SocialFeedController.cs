using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types;
using Newtonsoft.Json;

namespace FreestyleOnline.classes.Services.Controllers
{
    public class SocialFeedController : RapApiController
    {
        // GET api/socialfeed/
        [Authorize]
        [HttpGet]
        [ActionName("getfeed")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                this.GetCore<RapSocialFeed>().Get(RapContextFacade.Current.GetUserId()));
        }

        // DELETE api/socialfeed/feedid
        public void Delete(int id)
        {
        }
    }
}