#region Using

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Interfaces;

#endregion

namespace FreestyleOnline.classes.Base
{
    public abstract class BaseBattleLeaderboards : RapClass, IRapLeaderboard<BattleRanking>
    {
        public int RankPosition = 0;

        #region IRapLeaderboard<BattleRanking> Members

        public abstract List<BattleRanking> GetRankings();

        #endregion

        /// <summary>
        ///     Sorts the battles according to a list of rap battles.
        /// </summary>
        /// <param name="allBattles">All battles.</param>
        /// <returns></returns>
        protected Dictionary<int, int> SortBattles(List<RapBattle> allBattles)
        {
            Contract.Requires(allBattles != null);
            Contract.Ensures(Contract.Result<Dictionary<int, int>>() != null);
            var dict = new Dictionary<int, int>();
            foreach (var b in allBattles.Where(b => b.WinnerId != null))
            {
                if (dict.ContainsKey((int) b.WinnerId))
                {
                    dict[(int) b.WinnerId]++;
                }
                else
                {
                    dict.Add((int) b.WinnerId, 1);
                }
            }
            return dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        ///     Gets the top rankings.
        /// </summary>
        /// <param name="battlesDict">The battles dictionary.</param>
        /// <returns></returns>
        protected List<BattleRanking> GetTopRankings(Dictionary<int, int> battlesDict)
        {
            this.RankPosition = 0;
            return
                battlesDict.Select(
                    pair => new BattleRanking {Id = pair.Key, Wins = pair.Value, Position = ++this.RankPosition})
                    .ToList();
        }
    }
}