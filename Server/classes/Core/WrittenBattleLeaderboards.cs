#region Using

using System.Collections.Generic;
using Common.Types.Enums;
using FreestyleOnline.classes.Base;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class WrittenBattleLeaderboards : BaseBattleLeaderboards
    {
        /// <summary>
        ///     Gets the rankings.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<BattleRanking> GetRankings()
        {
            var allWrittenBattles = new RapBattleWritten().GetAllBattles(RapBattleType.Written);
            var sortedBattles = this.SortBattles(allWrittenBattles);
            return this.GetTopRankings(sortedBattles);
        }
    }
}