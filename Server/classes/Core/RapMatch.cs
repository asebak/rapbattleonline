#region Using

using Common.Models;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class RapMatch : RapMatchModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the battle identifier.
        /// </summary>
        /// <value>
        ///     The battle identifier.
        /// </value>
        public new int? BattleId { get; set; }

        #endregion
    }
}