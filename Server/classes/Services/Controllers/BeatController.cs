#region Using

using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    //TODO FIx using RapApiController
    public class BeatController : RapApiController
    {
        #region API

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getallbeats")]
        public HttpResponseMessage Get()
        {
            //if (this.Validate(this.Request))
            //{
                return Request.CreateResponse(HttpStatusCode.OK, this.GetCore<RapBeats>().Get());
            //}
            //throw new HttpRequestException();
        }

        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getbeat")]
        public HttpResponseMessage Get(int id)
        {
            Contract.Requires(id >= 0);
            //if (this.Validate(this.Request))
            //{
                var listOfBeats = this.GetCore<RapBeats>().Get();
                return (id > listOfBeats.Count
                    ? Request.CreateResponse(HttpStatusCode.BadRequest)
                    : Request.CreateResponse(HttpStatusCode.OK, listOfBeats[id]));
            //}
            //throw new HttpRequestException();
        }

        /// <summary>
        ///     Gets the audio stream.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("beatstream")]
        public HttpResponseMessage GetAudioStream(int id)
        {
            Contract.Requires(id >= 0);
            //if (this.Validate(this.Request))
            //{
                var listOfBeats = this.GetCore<RapBeats>().Get();
                if (id > listOfBeats.Count)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                var beatsFilePath = this.GetService<ResourceProvider>().GetPath(RapResource.Beats, listOfBeats[id].Name);
                return RapFileStreamPlayer.Get(beatsFilePath);
            //}
            //throw new HttpRequestException();
        }

        #endregion
    }
}