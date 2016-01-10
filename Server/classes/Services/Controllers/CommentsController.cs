#region Using

using System;
using System.Net.Http;
using System.Web.Http;
using Common.Models;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class CommentsController : ApiController
    {
        [HttpPost]
        [ActionName("News")]
        public HttpResponseMessage News(CommentModel cm)
        {
            throw new NotImplementedException();
        }
    }
}