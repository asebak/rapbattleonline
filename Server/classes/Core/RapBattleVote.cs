#region Using

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Types.Enums;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class RapBattleVote : RapClass
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the wordplay.
        /// </summary>
        /// <value>
        ///     The wordplay.
        /// </value>
        public int Wordplay { get; set; }

        /// <summary>
        ///     Gets or sets the metaphores.
        /// </summary>
        /// <value>
        ///     The metaphores.
        /// </value>
        public int Metaphores { get; set; }

        /// <summary>
        ///     Gets or sets the flow.
        /// </summary>
        /// <value>
        ///     The flow.
        /// </value>
        public int Flow { get; set; }

        /// <summary>
        ///     Gets or sets the multis.
        /// </summary>
        /// <value>
        ///     The multis.
        /// </value>
        public int Multis { get; set; }

        /// <summary>
        ///     Gets or sets the punch lines.
        /// </summary>
        /// <value>
        ///     The punch lines.
        /// </value>
        public int PunchLines { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Constructs the battle vote object.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="forUser1">if set to <c>true</c> [for user1].</param>
        /// <returns></returns>
        public static List<RapBattleVote> ConstructBattleVoteObject(DataSet data, bool forUser1)
        {
            if (forUser1)
            {
                return (from r in data.Tables[0].AsEnumerable()
                    select new RapBattleVote
                    {
                        Flow = r.Field<int>("User1Flow"),
                        Metaphores = r.Field<int>("User1Metaphores"),
                        Multis = r.Field<int>("User1Multis"),
                        PunchLines = r.Field<int>("User1Punchlines"),
                        Wordplay = r.Field<int>("User1Wordplay")
                    }).ToList();
            }
            return (from r in data.Tables[0].AsEnumerable()
                select new RapBattleVote
                {
                    Flow = r.Field<int>("User2Flow"),
                    Metaphores = r.Field<int>("User2Metaphores"),
                    Multis = r.Field<int>("User2Multis"),
                    PunchLines = r.Field<int>("User2Punchlines"),
                    Wordplay = r.Field<int>("User2Wordplay")
                }).ToList();
        }

        /// <summary>
        ///     Declares the rap battle winner.
        /// </summary>
        /// <param name="user1VotesList">The user1 votes list.</param>
        /// <param name="user2VotesList">The user2 votes list.</param>
        /// <param name="battleType">Type of the battle.</param>
        /// <param name="battleId">The battle identifier.</param>
        /// <param name="user1Id">The user1 identifier.</param>
        /// <param name="user2Id">The user2 identifier.</param>
        /// <returns></returns>
        public static UpdatedRapBattleVote DeclareRapBattleWinner(List<RapBattleVote> user1VotesList,
            List<RapBattleVote> user2VotesList, RapBattleType battleType, int battleId, int user1Id, int user2Id)
        {
            var winnerObject = new UpdatedRapBattleVote();
            if (user1VotesList.Any())
            {
                winnerObject.User1Overall = (float)
                    (user1VotesList.Average(x => x.Wordplay) + user1VotesList.Average(x => x.Flow) +
                     user1VotesList.Average(x => x.Metaphores) + user1VotesList.Average(x => x.Multis) +
                     user1VotesList.Average(x => x.PunchLines))/5;
            }
            else
            {
                winnerObject.User1Overall = 0f;
            }
            if (user2VotesList.Any())
            {
                winnerObject.User2Overall = (float)
                    (user2VotesList.Average(x => x.Wordplay) + user2VotesList.Average(x => x.Flow) +
                     user2VotesList.Average(x => x.Metaphores) + user2VotesList.Average(x => x.Multis) +
                     user2VotesList.Average(x => x.PunchLines))/5;
            }
            else
            {
                winnerObject.User2Overall = 0f;
            }
            if (winnerObject.User1Overall > winnerObject.User2Overall)
            {
                winnerObject.WinnerId = user1Id;
                if (battleType == RapBattleType.Written)
                {
                    Db.update_writtenbattle_winner(battleId, user1Id, (float) winnerObject.User1Overall,
                        (float) winnerObject.User2Overall);
                }
                else
                {
                    Db.update_audiobattle_winner(battleId, user1Id, (float) winnerObject.User1Overall,
                        (float) winnerObject.User2Overall);
                }
            }
            else if (winnerObject.User2Overall > winnerObject.User1Overall)
            {
                winnerObject.WinnerId = user2Id;
                if (battleType == RapBattleType.Written)
                {
                    Db.update_writtenbattle_winner(battleId, user2Id, (float) winnerObject.User1Overall,
                        (float) winnerObject.User2Overall);
                }
                else
                {
                    Db.update_audiobattle_winner(battleId, user2Id, (float) winnerObject.User1Overall,
                        (float) winnerObject.User2Overall);
                }
            }
            else //draw
            {
                if (battleType == RapBattleType.Written)
                {
                    Db.update_writtenbattle_winner(battleId, null, (float) winnerObject.User1Overall,
                        (float) winnerObject.User2Overall);
                }
                else
                {
                    Db.update_audiobattle_winner(battleId, null, (float) winnerObject.User1Overall,
                        (float) winnerObject.User2Overall);
                }
            }
            return winnerObject;
        }

        #endregion
    }
}