namespace Common.Models
{
    public class RapMatchModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the match identifier.
        /// </summary>
        /// <value>
        /// The match identifier.
        /// </value>
        public int MatchId { get; set; }
        /// <summary>
        /// Gets or sets the battle identifier.
        /// </summary>
        /// <value>
        /// The battle identifier.
        /// </value>
        public int BattleId { get; set; }
        /// <summary>
        /// Gets or sets the user id1.
        /// </summary>
        /// <value>
        /// The user id1.
        /// </value>
        public int? UserId1 { get; set; }
        /// <summary>
        /// Gets or sets the user id2.
        /// </summary>
        /// <value>
        /// The user id2.
        /// </value>
        public int? UserId2 { get; set; }
        /// <summary>
        /// Gets or sets the winner identifier.
        /// </summary>
        /// <value>
        /// The winner identifier.
        /// </value>
        public int? WinnerId { get; set; }
        /// <summary>
        /// Gets or sets the round number.
        /// </summary>
        /// <value>
        /// The round number.
        /// </value>
        public int RoundNumber { get; set; }

        #endregion
    }
}