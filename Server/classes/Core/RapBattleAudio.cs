#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Common.Types;
using Common.Types.Enums;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Providers;
using YAF.Core;

#endregion

namespace FreestyleOnline.classes.Core
{
    public sealed class RapBattleAudio : RapBattle, IRapBattle, IRapComment
    {
        #region Properties

        public string User1Audio { get; set; }
        public string User2Audio { get; set; }
        public int? Beat { get; set; }

        #endregion

        #region Constructor

        public RapBattleAudio()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapBattleAudio" /> class.
        /// </summary>
        /// <param name="userId1">The user id1.</param>
        /// <param name="userId2">The user id2.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="length">The length.</param>
        /// <param name="isPublic">if set to <c>true</c> [is public].</param>
        public RapBattleAudio(int userId1, int? userId2, DateTime endDate, int length, bool isPublic)
        {
            this.UserId1 = userId1;
            this.UserId2 = userId2;
            this.EndDate = endDate;
            this.Length = length;
            this.IsPublic = isPublic;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapBattleAudio" /> class.
        /// </summary>
        /// <param name="pageUserId">The page user identifier.</param>
        /// <param name="battleId"></param>
        public RapBattleAudio(int pageUserId, int battleId)
        {
            this.PageUserId = pageUserId;
            this.BattleId = battleId;
        }

        #endregion

        #region Methods

        #region IRapBattle Members

        /// <summary>
        ///     Creates the battle.
        /// </summary>
        public Int32 CreateBattle()
        {
            var battleId = Db.add_audiobattle(UserId1, UserId2, IsPublic, EndDate, Length);
            if (!IsPublic)
            {
                Message.SendPmMessage(UserId1, UserId2, "You Have Been Invited To A Battle",
                    string.Format(
                        "{0} has challenged you to a audio battle located at the following <a href=\'{1}' target='_blank'>link</a>",
                        UserMembershipHelper.GetDisplayNameFromID(UserId1),
                        this.GetService<UrlProvider>().GetUrl("~/Pages/AudioBattles", battleId)));
            }
            return battleId;
        }


        /// <summary>
        ///     Votes the specified user.
        /// </summary>
        /// <param name="user1">The user1.</param>
        /// <param name="user2">The user2.</param>
        public void Vote(RapBattleVote user1, RapBattleVote user2)
        {
            if (PageUserId == user1.UserId || PageUserId == user2.UserId)
            {
                return;
            }
            if (this.IsUserAllowedToVote(!HttpContext.Current.User.Identity.IsAuthenticated))
            {
                Db.add_audiobattle_rating(this.BattleId, this.PageUserId, user1.Wordplay, user2.Wordplay,
                    user1.Metaphores, user2.Metaphores, user1.Flow, user2.Flow, user1.Multis, user2.Multis,
                    user1.PunchLines,
                    user2.PunchLines);
            }
        }

        /// <summary>
        ///     Joins the battle.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void JoinBattle(int userId)
        {
            Db.join_audiobattle(userId, this.BattleId);
        }

        /// <summary>
        ///     Submits the specified content.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="content">The content.</param>
        public void Submit(int userId, object content)
        {
            Db.update_audiobattle_recording(userId, (int) Beat, this.BattleId, content.ToString());
        }

        /// <summary>
        ///     Gets the settings.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public RapBattle GetSettings()
        {
            var details = Db.get_audiobattle(this.BattleId);
            var currentBattleContext =
                this.ConstructRapBattleObject(details, RapBattleType.Audio).Cast<RapBattleAudio>().First();
            return currentBattleContext;
        }

        /// <summary>
        ///     Determines whether [is user allowed to vote].
        /// </summary>
        /// <returns></returns>
        public bool IsUserAllowedToVote(bool isGuest)
        {
            var query = Db.get_audiobattle_ratingenabled(this.BattleId, PageUserId);
            return !isGuest && PageUserId != UserId1 && PageUserId != UserId2 && UserId2 != null && (query == null);
        }

        /// <summary>
        ///     Gets the latest battles.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<RapBattle> GetLatestBattles()
        {
            var audioBattlesDs = Db.get_recent_audiobattles();
            return this.ConstructRapBattleObject(audioBattlesDs, RapBattleType.Audio);
        }

        /// <summary>
        ///     Gets the voting in progress battles.
        /// </summary>
        /// <returns></returns>
        public List<RapBattle> GetVotingInProgressBattles()
        {
            var votingDays =
                Convert.ToDouble(this.GetService<ApplicationProvider>().GetApplicationSettings("RAP.VoteDays"));
            return
                this.GetAllBattles(RapBattleType.Audio)
                    .Where(w => !RapGlobalHelpers.IsDateExpired(w.EndDate.AddDays(votingDays))
                                && RapGlobalHelpers.IsDateExpired(w.EndDate) && w.UserId2 != null).ToList();
        }

        #endregion

        #region IRapComment Members

        /// <summary>
        ///     Posts the comment.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="comment">The comment.</param>
        public void PostComment(int userId, string comment)
        {
            Db.post_audiobattlecomment(userId, this.BattleId, comment);
        }

        /// <summary>
        ///     Deletes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        public void DeleteComment(int commentId)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        ///     Gets the votes user1.
        /// </summary>
        /// <returns></returns>
        public override List<RapBattleVote> GetVotesUser1()
        {
            var user1VotesDs = Db.get_audiobattle_votes(BattleId);
            return RapBattleVote.ConstructBattleVoteObject(user1VotesDs, true);
        }

        /// <summary>
        ///     Gets the votes user2.
        /// </summary>
        /// <returns></returns>
        public override List<RapBattleVote> GetVotesUser2()
        {
            var user2VotesDs = Db.get_audiobattle_votes(BattleId);
            return RapBattleVote.ConstructBattleVoteObject(user2VotesDs, false);
        }

        /// <summary>
        ///     Updates the battle.
        /// </summary>
        public override void UpdateBattle()
        {
            //TODO check if a battle is a tournament, if a tournament match draws cannot happen and therefore has to be a random winner
            if (User1Overall != null && User2Overall != null)
            {
                return;
            }
            //removed checking if User1Audio != null && User2Audio != null since someone might submit content and the other will not, so if both
            //do not and people vote then someone could potentially win
            //update the battle winner and overrall rating for each user
            if (RapGlobalHelpers.IsDateExpired
                (EndDate.AddDays(
                    Convert.ToDouble(this.GetService<ApplicationProvider>().GetApplicationSettings("RAP.VoteDays")))) &&
                UserId2 != null)
            {
                var allRatings = Db.get_audiobattle_votes(BattleId);
                var user1VotesList = RapBattleVote.ConstructBattleVoteObject(allRatings, true);
                var user2VotesList = RapBattleVote.ConstructBattleVoteObject(allRatings, false);
                var voteOutcome = RapBattleVote.DeclareRapBattleWinner(user1VotesList, user2VotesList,
                    RapBattleType.Audio,
                    this.BattleId, this.UserId1, (int) this.UserId2);
                this.WinnerId = voteOutcome.WinnerId;
                this.User1Overall = voteOutcome.User1Overall;
                this.User2Overall = voteOutcome.User2Overall;
            }
        }

        /// <summary>
        ///     Gets the battle comments.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override DataTable GetBattleComments()
        {
            return Db.get_audiobattle_comments(this.BattleId);
        }

        /// <summary>
        ///     Gets the votes.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override DataTable GetVotes()
        {
            return Db.get_audiobattle_ratings(this.BattleId);
        }

        #endregion
    }
}