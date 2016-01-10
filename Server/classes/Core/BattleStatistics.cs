#region Using

using System.Data;
using System.Linq;
using FreestyleOnline.classes.Base;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class BattleStatistics : BaseStatistics
    {
        #region Properties

        public double Wordplay { get; set; }
        public double Metaphores { get; set; }
        public double Flow { get; set; }
        public double PunchLines { get; set; }
        public double Multis { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="BattleStatistics" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public BattleStatistics(int userId)
        {
            this.UserId = userId;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BattleStatistics" /> class.
        /// </summary>
        public BattleStatistics()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Builds the stats.
        /// </summary>
        /// <param name="StatsDataset">The stats dataset.</param>
        /// <returns></returns>
        protected BattleStatistics BuildStats(DataSet StatsDataset)
        {
            var userAsUserId1 =
                StatsDataset.Tables[0].AsEnumerable()
                    .Where(r => r.Field<int>("UserID1") == this.UserId)
                    .Select(r => new BattleStatistics
                    {
                        Flow = r.Field<int>("User1Flow"),
                        Metaphores = r.Field<int>("User1Metaphores"),
                        Multis = r.Field<int>("User1Multis"),
                        PunchLines = r.Field<int>("User1Punchlines"),
                        Wordplay = r.Field<int>("User1Wordplay")
                    }).ToList();

            var userAsUserId2 =
                StatsDataset.Tables[0].AsEnumerable()
                    .Where(r => r.Field<int>("UserID2") == this.UserId)
                    .Select(r => new BattleStatistics
                    {
                        Flow = r.Field<int>("User2Flow"),
                        Metaphores = r.Field<int>("User2Metaphores"),
                        Multis = r.Field<int>("User2Multis"),
                        PunchLines = r.Field<int>("User2Punchlines"),
                        Wordplay = r.Field<int>("User2Wordplay")
                    }).ToList();

            var usersStatisticsAudio = userAsUserId1.Union(userAsUserId2).ToList();
            if (usersStatisticsAudio.Any())
            {
                return new BattleStatistics
                {
                    Flow = usersStatisticsAudio.Average(x => x.Flow),
                    Metaphores = usersStatisticsAudio.Average(x => x.Metaphores),
                    Multis = usersStatisticsAudio.Average(x => x.Multis),
                    PunchLines = usersStatisticsAudio.Average(x => x.PunchLines),
                    Wordplay = usersStatisticsAudio.Average(x => x.Wordplay)
                };
            }
            return new BattleStatistics
            {
                Flow = 0,
                Metaphores = 0,
                Multis = 0,
                PunchLines = 0,
                Wordplay = 0
            };
        }

        #endregion
    }
}