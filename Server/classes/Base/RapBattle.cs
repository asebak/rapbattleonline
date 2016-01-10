#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Types.Enums;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Providers;
using YAF.Types;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.classes.Base
{
    public abstract class RapBattle : RapClass
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the battle identifier.
        /// </summary>
        /// <value>
        ///     The battle identifier.
        /// </value>
        public virtual int BattleId { get; set; }

        /// <summary>
        ///     Gets or sets the page user identifier.
        /// </summary>
        /// <value>
        ///     The page user identifier.
        /// </value>
        public virtual int PageUserId { get; set; }

        /// <summary>
        ///     Gets or sets the user id1.
        /// </summary>
        /// <value>
        ///     The user id1.
        /// </value>
        public virtual int UserId1 { get; set; }

        /// <summary>
        ///     Gets or sets the user id2.
        /// </summary>
        /// <value>
        ///     The user id2.
        /// </value>
        public virtual int? UserId2 { get; set; }

        /// <summary>
        ///     Gets or sets the end date.
        /// </summary>
        /// <value>
        ///     The end date.
        /// </value>
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        ///     Gets or sets the length.
        /// </summary>
        /// <value>
        ///     The length.
        /// </value>
        public virtual int Length { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [is public].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [is public]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsPublic { get; set; }

        /// <summary>
        ///     Gets or sets the winner identifier.
        /// </summary>
        /// <value>
        ///     The winner identifier.
        /// </value>
        public virtual int? WinnerId { get; set; }

        /// <summary>
        ///     Gets or sets the user1 overall.
        /// </summary>
        /// <value>
        ///     The user1 overall.
        /// </value>
        public virtual float? User1Overall { get; set; }

        /// <summary>
        ///     Gets or sets the user2 overall.
        /// </summary>
        /// <value>
        ///     The user2 overall.
        /// </value>
        public virtual float? User2Overall { get; set; }

        public double VoteDays { get; set; }

        #endregion

        #region Methods

        public abstract DataTable GetBattleComments();
        public abstract DataTable GetVotes();
        public abstract List<RapBattleVote> GetVotesUser1();
        public abstract List<RapBattleVote> GetVotesUser2();
        public abstract void UpdateBattle();

        public List<RapBattle> ConstructRapBattleObject([CanBeNull] DataSet data,[NotNull] RapBattleType battleType)
        {
            //TODO: Verify if rapcontext is working was using httpcontext before
            var pageUserId = this.RapContext.GetUserId();
            switch (battleType)
            {
                case RapBattleType.Written:
                    var writtenBattles = (from r in data.Tables[0].AsEnumerable()
                        select new RapBattleWritten
                        {
                            BattleId = r.Field<int>("WrittenBattleID"),
                            EndDate = r.Field<DateTime>("EndDate"),
                            User1Overall =
                                r.IsNull("User1Overall") ? (float?) null : (float) r.Field<double>("User1Overall"),
                            User2Overall =
                                r.IsNull("User2Overall") ? (float?) null : (float) r.Field<double>("User2Overall"),
                            UserId1 = r.Field<int>("UserID1"),
                            //might need HttpUtility.decode on the verse strings
                            User1Verse = r.IsNull("User1Verse") ? null : r.Field<string>("User1Verse"),
                            User2Verse = r.IsNull("User2Verse") ? null : r.Field<string>("User2Verse"),
                            UserId2 = r.IsNull("UserID2") ? (int?) null : r.Field<int>("UserID2"),
                            WinnerId = r.IsNull("WinnerID") ? (int?) null : r.Field<int>("WinnerID"),
                            Length = r.Field<int>("NumberOfBars"),
                            IsPublic = r.Field<bool>("IsPublic"),
                            PageUserId = pageUserId
                        }).ToList();
                    writtenBattles.ForEach(w => w.UpdateBattle());
                    return writtenBattles.Cast<RapBattle>().ToList();
                case RapBattleType.Audio:
                    var audioBattles = (from r in data.Tables[0].AsEnumerable()
                        select new RapBattleAudio
                        {
                            BattleId = r.Field<int>("AudioBattleID"),
                            EndDate = r.Field<DateTime>("EndDate"),
                            User1Overall =
                                r.IsNull("User1Overall") ? (float?) null : (float) r.Field<double>("User1Overall"),
                            User2Overall =
                                r.IsNull("User2Overall") ? (float?) null : (float) r.Field<double>("User2Overall"),
                            UserId1 = r.Field<int>("UserID1"),
                            User1Audio = r.IsNull("User1Recording") ? null : r.Field<string>("User1Recording"),
                            User2Audio = r.IsNull("User2Recording") ? null : r.Field<string>("User2Recording"),
                            UserId2 = r.IsNull("UserID2") ? (int?) null : r.Field<int>("UserID2"),
                            WinnerId = r.IsNull("WinnerID") ? (int?) null : r.Field<int>("WinnerID"),
                            PageUserId = pageUserId,
                            IsPublic = r.Field<bool>("IsPublic"),
                            Length = r.Field<int>("RecordingLength"),
                            Beat = r.IsNull("Beat") ? (int?) null : Convert.ToInt32(r.Field<string>("Beat"))
                        }).ToList();
                    audioBattles.ForEach(a => a.UpdateBattle());
                    return audioBattles.Cast<RapBattle>().ToList();
                default:
                    throw new ArgumentOutOfRangeException("battleType");
            }
        }

        public List<RapBattle> GetAllBattles([NotNull] RapBattleType battleType)
        {
            var dataSource = new DataSet();
            switch (battleType)
            {
                case RapBattleType.Written:
                    dataSource = Db.get_all_writtenbattles();
                    break;
                case RapBattleType.Audio:
                    dataSource = Db.get_audiobattles();
                    break;
                case RapBattleType.Video:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("battleType");
            }
            return this.ConstructRapBattleObject(dataSource, battleType);
        }

        public string IsValidRapBattle([NotNull] DateTime battleEnd, [NotNull] bool isPublic,[CanBeNull] int battleUserId,[NotNull] int pageUserId)
        {
            if (battleUserId == pageUserId)
            {
                return this.Text("BATTLES", "BATTLE_SELF");
            }
            if (battleUserId <= 1 && !isPublic)
            {
                return this.Text("BATTLES", "BATTLE_NOUSER");
            }
            if (battleEnd.ToString().IsNotSet() || DateTime.Now > battleEnd)
            {
                return this.Text("COMMON", "COMMON_ERRORDATE");
            }
            return null;
        }
        #endregion
    }
}