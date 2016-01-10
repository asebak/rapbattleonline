#region Using

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Secruity;
using FreestyleOnline.classes.Types;
using YAF.Core;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class DownloadController : ApiController
    {
        #region API

        /// <summary>
        ///     Gets the music.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Music")]
        public HttpResponseMessage Music(int id)
        {
            WebApiValidation.Validate(Request);
            var musicTrack = MusicData.GetMusicTrackDetailsFromId(id);
            var localFilePath = new ResourceProvider().GetPath(RapResource.MusicTracks) +
                                musicTrack.LinkLocation;
            if (String.IsNullOrEmpty(musicTrack.LinkLocation) || !musicTrack.CanDownload)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName =
                        string.Format("{0} - {1}{2}",
                            UserMembershipHelper.GetDisplayNameFromID(musicTrack.UserId),
                            musicTrack.SongName, Path.GetExtension(musicTrack.LinkLocation))
                };
            return result;
        }

        #endregion
    }
}