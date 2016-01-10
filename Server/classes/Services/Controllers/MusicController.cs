#region Using

using System.Net.Http;
using System.Web.Http;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class MusicController : ApiController
    {
        #region API

        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("FileStream")]
        public HttpResponseMessage GetAudioStream(int id)
        {
            //WebApiValidation.Validate(Request);
            var musicTrack = MusicData.GetMusicTrackDetailsFromId(id);
            var audioFilePath = new ResourceProvider().GetPath(RapResource.MusicTracks, musicTrack.LinkLocation);
            return RapFileStreamPlayer.Get(audioFilePath);
        }

        #endregion
    }
}