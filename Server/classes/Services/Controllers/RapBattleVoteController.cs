#region Using

using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Common.Models;
using Common.Types.Enums;
using FreestyleOnline.classes.Core;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class RapBattleVoteController : ApiController
    {
        #region API

        /// <summary>
        ///     Votes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="vote">The vote.</param>
        [HttpPut]
        [Authorize]
        public HttpResponseMessage Vote(int id, BattleVoteModel vote)
        {
            Contract.Requires(vote != null);
            var pageUserId = UserData.GetUserIdFromDisplayName(HttpContext.Current.User.Identity.Name);
            var vote1 = new RapBattleVote();
            var vote2 = new RapBattleVote();
            vote1.UserId = vote.UserId1;
            vote1.Flow = vote.User1Flow;
            vote1.Metaphores = vote.User1Metaphores;
            vote1.Multis = vote.User1Multis;
            vote1.PunchLines = vote.User1Punchlines;
            vote1.Wordplay = vote.User1Wordplay;
            vote2.UserId = vote.UserId2;
            vote2.Flow = vote.User2Flow;
            vote2.Metaphores = vote.User2Metaphores;
            vote2.Multis = vote.User2Multis;
            vote2.PunchLines = vote.User2Punchlines;
            vote2.Wordplay = vote.User2Wordplay;
            switch (vote.BattleType)
            {
                case RapBattleType.Written:
                {
                    var writtenBattle = new RapBattleWritten(pageUserId, id).GetSettings() as RapBattleWritten;
                    writtenBattle.Vote(vote1, vote2);
                }
                    break;
                case RapBattleType.Audio:
                    var audioBattle = new RapBattleAudio(pageUserId, id).GetSettings() as RapBattleAudio;
                    audioBattle.Vote(vote1, vote2);
                    break;
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        #endregion
    }
}