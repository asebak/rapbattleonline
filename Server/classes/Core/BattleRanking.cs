#region Using

using FreestyleOnline.classes.Base;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class BattleRanking : BaseRanking
    {
        #region Properties

        public int Wins { get; set; }
        public int Losses { get; set; }

        #endregion
    }
}