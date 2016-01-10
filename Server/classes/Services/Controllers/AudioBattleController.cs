#region Using

using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Common.Models;
using Common.Types.Enums;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Factory;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using FreestyleOnline.classes.Types.Helpers;
using YAF.Core;
using YAF.Core.Services;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class AudioBattleController : ApiController
    {
        #region Members

        /// <summary>
        ///     The _audio battle
        /// </summary>
        private RapBattleAudio _audioBattle;

        #endregion

        #region API

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        [HttpGet]
        [ActionName("getallbattles")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new RapBattleAudio().GetAllBattles(RapBattleType.Audio));
        }

        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getbattle")]
        public HttpResponseMessage Get(int id)
        {
            var pageUserId = RapContextFacade.Current.GetUserId();
            this._audioBattle =
                new RapBattleAudio(pageUserId, id).GetSettings() as RapBattleAudio;
            var builtObj = new AudioBattleFactory().Construct(this._audioBattle);
            return Request.CreateResponse(HttpStatusCode.OK, builtObj);
        }

        ///// <summary>
        ///// Gets the audio stream.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        [HttpGet]
        [ActionName("audiobattlestream")]
        public HttpResponseMessage GetAudioStream(string id)
        {
            var audioBattleFilePath = new ResourceProvider().GetPath(RapResource.RapBattleAudio, id + ".wav");
            return RapFileStreamPlayer.Get(audioBattleFilePath);
        }

        /// <summary>
        ///     Joins the battle.
        /// </summary>
        /// <param name="value">The value.</param>
        [HttpPost]
        [Authorize]
        public HttpResponseMessage Join(AudioBattleModel value)
        {
            //TODO more validation if its a valid battle that a user can join such as end date
            Contract.Requires(value != null);
            var pageUserId = value.PageUserId;
            this._audioBattle = new RapBattleAudio(value.PageUserId, value.BattleId).GetSettings() as RapBattleAudio;

            if (this._audioBattle.UserId1 == pageUserId || this._audioBattle.UserId2 == pageUserId ||
                (RapContextFacade.Current.GetUserId() != pageUserId))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            this._audioBattle.JoinBattle(value.PageUserId);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        /// <summary>
        ///     Submits the specified value.
        /// </summary>
        /// <param name="recordedAudio">The recorded audio.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ActionName("submit")]
        public HttpResponseMessage Submit(AudioBattleDspModel recordedAudio)
        {
            //TODO more validation here
            Contract.Requires(recordedAudio != null);
            this._audioBattle =
                new RapBattleAudio(recordedAudio.PageUserId, recordedAudio.BattleId).GetSettings() as RapBattleAudio;
            if (this._audioBattle.Beat == null)
            {
                this._audioBattle.Beat = recordedAudio.Beat;
            }
            if (this._audioBattle.PageUserId != RapContextFacade.Current.GetUserId() 
                && this._audioBattle.UserId1 != this._audioBattle.PageUserId &&
                this._audioBattle.UserId1 != this._audioBattle.PageUserId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var signal = new RapSignalProcessing(recordedAudio.RecordedFileLocation, (int) recordedAudio.Beat);
            this._audioBattle.Submit(recordedAudio.PageUserId,
                recordedAudio.RequiresDspEngineering ? signal.CombineAudioSignal() : recordedAudio.RecordedFileLocation);
            //TODO might not be neccessary to get settings again
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        /// <summary>
        ///     Creates the battle.
        /// </summary>
        /// <param name="battle">The battle.</param>
        [HttpPost]
        [Authorize]
        [ActionName("CreateBattle")]
        public HttpResponseMessage CreateBattle(AudioBattleModel battle)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}