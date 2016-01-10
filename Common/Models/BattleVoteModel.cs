#region Using

using Common.Types.Enums;

#endregion

namespace Common.Models
{
    public class BattleVoteModel
    {
        #region Properties

        public RapBattleType BattleType { get; set; }
        public int BattleId { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public int User1Wordplay { get; set; }
        public int User1Metaphores { get; set; }
        public int User1Flow { get; set; }
        public int User1Multis { get; set; }
        public int User1Punchlines { get; set; }
        public int User2Wordplay { get; set; }
        public int User2Metaphores { get; set; }
        public int User2Flow { get; set; }
        public int User2Multis { get; set; }
        public int User2Punchlines { get; set; }

        #endregion
    }
}