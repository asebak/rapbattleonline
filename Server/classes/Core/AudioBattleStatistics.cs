#region Using

using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Interfaces;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class AudioBattleStatistics : BattleStatistics, IRapBattleStatistic<BattleStatistics>
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="AudioBattleStatistics" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public AudioBattleStatistics(int userId)
            : base(userId)
        {
        }

        #endregion

        #region IRapBattleStatistic<BattleStatistics> Members

        /// <summary>
        ///     Gets the stats.
        /// </summary>
        /// <returns></returns>
        public BattleStatistics GetStats()
        {
            var usersDs = Db.get_audiobattle_votesstatistics(this.UserId);
            return BuildStats(usersDs);
        }

        #endregion
    }
}