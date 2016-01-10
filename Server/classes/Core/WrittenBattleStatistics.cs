#region Using

using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Interfaces;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class WrittenBattleStatistics : BattleStatistics, IRapBattleStatistic<BattleStatistics>
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="WrittenBattleStatistics" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public WrittenBattleStatistics(int userId)
            : base(userId)
        {
        }

        #endregion

        #region IRapBattleStatistic<BattleStatistics> Members

        /// <summary>
        ///     Gets the stats.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public BattleStatistics GetStats()
        {
            var userDs = Db.get_writtenbattle_votesstatistics(this.UserId);
            return BuildStats(userDs);
        }

        #endregion
    }
}