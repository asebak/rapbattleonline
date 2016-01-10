#region Using

using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class VersesController : RapApiController
    {
        #region API

        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getaudioverse")]
        public HttpResponseMessage GetAudioVerse(int id)
        {
            var file = new RapAudioVerses().GetAudioVersePath(id);
            var path = HttpContext.Current.Server.MapPath("~/Uploads/AudioVerses/" + file);
            return RapFileStreamPlayer.Get(path);
        }

        /// <summary>
        /// Updates the written verse.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("updatewrittenverse")]
        [Authorize]
        public HttpResponseMessage UpdateWrittenVerse(EditableField e)
        {
            var v = this.GetCore<RapWrittenVerses>();
            v.UserId = RapContextFacade.Current.GetUserId();
            v.UpdateVerse(e.Pk, e.Value);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        #endregion

    }
}