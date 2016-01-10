#region Using

using System;
using System.Net.Http;
using System.Web.Http;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class NewsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}