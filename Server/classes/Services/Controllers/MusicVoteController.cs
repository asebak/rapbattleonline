#region Using

using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Common.Models;
using FreestyleOnline.classes.Core;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class MusicVoteController : ApiController
    {
        #region Members

        private MusicData _music;

        #endregion

        #region API

        /// <summary>
        ///     Determines whether [is rating enabled] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("IsRatingEnabled")]
        public HttpResponseMessage IsRatingEnabled([FromUri] MusicVoteModel id)
        {
            Contract.Requires(id != null);
            var pageUserId = UserData.GetUserIdFromDisplayName(HttpContext.Current.User.Identity.Name);
            if (pageUserId != id.PageUserId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, false);
            }
            _music = new MusicData();
            var enabled = _music.TrackRatingEnabled(id.UserId, pageUserId, id.MusicId);
            return Request.CreateResponse(HttpStatusCode.OK, enabled);
        }

        /// <summary>
        ///     Gets the total votes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTotalVotes")]
        public HttpResponseMessage GetTotalVotes(int id)
        {
            _music = new MusicData();
            return Request.CreateResponse(HttpStatusCode.OK, _music.GetTotalVotesForMusicTrack(id));
        }

        /// <summary>
        ///     Gets the rating value.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRatingValue")]
        public HttpResponseMessage GetRatingValue(int id)
        {
            _music = new MusicData();
            return Request.CreateResponse(HttpStatusCode.OK, _music.GetRatingForMusicTrack(id));
        }

        /// <summary>
        ///     /api/MusicVote/{id}
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="vote">The vote.</param>
        [HttpPut]
        [Authorize]
        public HttpResponseMessage SetRatingValue(int id, MusicVoteModel vote)
        {
            Contract.Requires(vote != null);
            _music = new MusicData();
            _music.SetRatingOnMusicTrack(id, vote.PageUserId, vote.RatingValue);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        #endregion
    }
}