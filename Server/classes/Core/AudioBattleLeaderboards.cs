#region Using

using System.Collections.Generic;
using Common.Types.Enums;
using FreestyleOnline.classes.Base;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class AudioBattleLeaderboards : BaseBattleLeaderboards
    {
        /// <summary>
        ///     Gets the rankings.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override List<BattleRanking> GetRankings()
        {
            var allAudioBattles = new RapBattleAudio().GetAllBattles(RapBattleType.Audio);
            var sortedBattles = this.SortBattles(allAudioBattles);
            return this.GetTopRankings(sortedBattles);
        }
    }
}