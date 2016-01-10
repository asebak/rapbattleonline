#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using Common.Types;
using Common.Types.Enums;
using Common.Types.Exceptions;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Core;

#endregion

namespace FreestyleOnline.classes.Core
{
    public sealed class RapBattleWritten : RapBattle, IRapBattle, IRapComment
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the user1 verse.
        /// </summary>
        /// <value>
        ///     The user1 verse.
        /// </value>
        public string User1Verse { get; set; }

        /// <summary>
        ///     Gets or sets the user2 verse.
        /// </summary>
        /// <value>
        ///     The user2 verse.
        /// </value>
        public string User2Verse { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapBattleWritten" /> class. Use this constructor for creating a battle
        /// </summary>
        /// <param name="userId1">The user id1.</param>
        /// <param name="userId2">The user id2.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="length">The length.</param>
        /// <param name="isPublic">if set to <c>true</c> [is public].</param>
        public RapBattleWritten(int userId1, int? userId2, DateTime endDate, int length, bool isPublic)
        {
            this.UserId1 = userId1;
            this.UserId2 = userId2;
            this.EndDate = endDate;
            this.Length = length;
            this.IsPublic = isPublic;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapBattleWritten" /> class. Using this constructor for posting a
        ///     comment or voting, or viewing the battle
        /// </summary>
        /// <param name="pageUserId">The page user identifier.</param>
        /// <param name="battleId"></param>
        public RapBattleWritten(int pageUserId, int battleId)
        {
            this.PageUserId = pageUserId;
            this.BattleId = battleId;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapBattleWritten" /> class.
        /// </summary>
        public RapBattleWritten()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapBattleWritten" /> class. Use this constructor to get users battles
        /// </summary>
        /// <param name="pageUserId">The page user identifier.</param>
        public RapBattleWritten(int pageUserId)
        {
            this.PageUserId = pageUserId;
        }

        #endregion

        #region Methods

        #region IRapBattle Members

        /// <summary>
        ///     Creates the battle.
        /// </summary>
        public Int32 CreateBattle()
        {
            this.BattleId = Db.add_writtenbattle(this.UserId1, this.UserId2, this.IsPublic, this.EndDate, this.Length);
            if (!IsPublic)
            {
                Message.SendPmMessage(this.UserId1, this.UserId2, "You Have Been Invited To A Battle",
                    string.Format(
                        "{0} has challenged you to a written battle located at the following <a href=\'{1}' target='_blank'>link</a>",
                        UserMembershipHelper.GetDisplayNameFromID(UserId1),
                        this.GetService<UrlProvider>().GetUrl("~/Pages/WrittenBattles", this.BattleId)));
            }
            this.PostSocialFeed(this.UserId1, this.BattleId, RapSocialFeedType.WrittenBattleStart);
            return this.BattleId;
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
                Db.add_writtenbattle_rating(this.BattleId, this.PageUserId, user1.Wordplay, user2.Wordplay,
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
            //TODO: Validation Here that a user actually doesn't battle himself
            Db.join_writtenbattle(userId, BattleId);
        }

        /// <summary>
        ///     Submits the specified content.
        /// </summary>
        /// <param name="userId">The User ID</param>
        /// <param name="content">The content.</param>
        public void Submit(int userId, object content)
        {
            if (UserId1 == userId)
            {
                Db.update_writtenbattle_user1verse((string) content, BattleId);
            }
            else if (UserId2 == userId)
            {
                Db.update_writtenbattle_user2verse((string) content, BattleId);
            }
            else
            {
                throw new UnauthorizedRapBattleUserException("You Are Forbidden To Submit to this rap battle.");
            }
        }

        /// <summary>
        ///     Gets the settings.
        /// </summary>
        /// <returns></returns>
        public RapBattle GetSettings()
        {
            var details = Db.get_writtenbattle(BattleId);
            var currentBattleContext =
                this.ConstructRapBattleObject(details, RapBattleType.Written).Cast<RapBattleWritten>().First();
            return currentBattleContext;
        }

        /// <summary>
        ///     Determines whether [is user allowed to vote].
        /// </summary>
        /// <returns></returns>
        public bool IsUserAllowedToVote(bool isGuest)
        {
            var query = Db.get_writtenbattle_ratingenabled(this.BattleId, this.PageUserId);
            return !isGuest && PageUserId != UserId1 && PageUserId != UserId2 && UserId2 != null && (query == null);
        }

        /// <summary>
        ///     Gets the latest battles.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<RapBattle> GetLatestBattles()
        {
            var writtenBattlesDs = Db.get_recent_writtenbattles();
            var latestWrittenBattles = this.ConstructRapBattleObject(writtenBattlesDs, RapBattleType.Written);
            return latestWrittenBattles;
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
                this.GetAllBattles(RapBattleType.Written)
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
            Db.post_writtenbattlecomment(userId, this.BattleId, comment);
        }

        /// <summary>
        ///     Deletes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        public void DeleteComment(int commentId)
        {
            throw new NotImplementedException("Not Created Yet!");
        }

        #endregion

        /// <summary>
        ///     Gets the battle comments.
        /// </summary>
        /// <returns></returns>
        public override DataTable GetBattleComments()
        {
            return Db.get_writtenbattle_comments(this.BattleId);
        }

        /// <summary>
        ///     Gets the votes.
        /// </summary>
        /// <returns></returns>
        public override DataTable GetVotes()
        {
            return Db.get_writtenbattle_ratings(this.BattleId);
        }

        /// <summary>
        ///     Gets the votes user1.
        /// </summary>
        /// <returns></returns>
        public override List<RapBattleVote> GetVotesUser1()
        {
            var user1VotesDs = Db.get_writtenbattle_votes(BattleId);
            return RapBattleVote.ConstructBattleVoteObject(user1VotesDs, true);
        }

        /// <summary>
        ///     Gets the votes user2.
        /// </summary>
        /// <returns></returns>
        public override List<RapBattleVote> GetVotesUser2()
        {
            var user2VotesDs = Db.get_writtenbattle_votes(this.BattleId);
            return RapBattleVote.ConstructBattleVoteObject(user2VotesDs, false);
        }

        /// <summary>
        ///     Updates the finished battle.
        /// </summary>
        public override void UpdateBattle()
        {
            //TODO check if a battle is a tournament, if a tournament match draws cannot happen and therefore has to be a random winner
            if (User1Overall != null && User2Overall != null)
            {
                return;
            }
            //removed checking for User1Verse != null && User2Verse != null
            //update the battle winner and overrall rating for each user, 
            //optimize getting applicationsettings
            //added userid2 not being null
            if (RapGlobalHelpers.IsDateExpired
                (EndDate.AddDays(
                    Convert.ToDouble(this.GetService<ApplicationProvider>().GetApplicationSettings("RAP.VoteDays")))) &&
                UserId2 != null)
            {
                var allRatings = Db.get_writtenbattle_votes(this.BattleId);
                var user1VotesList = RapBattleVote.ConstructBattleVoteObject(allRatings, true);
                var user2VotesList = RapBattleVote.ConstructBattleVoteObject(allRatings, false);
                var voteOutcome = RapBattleVote.DeclareRapBattleWinner(user1VotesList, user2VotesList,
                    RapBattleType.Written,
                    this.BattleId, this.UserId1, (int) this.UserId2);
                this.WinnerId = voteOutcome.WinnerId;
                this.User1Overall = voteOutcome.User1Overall;
                this.User2Overall = voteOutcome.User2Overall;
            }
        }

        #endregion
    }
}