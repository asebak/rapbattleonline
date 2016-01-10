#region Using

using FreestyleOnline.classes.Base;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class HoodUser : RapClass
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [is admin].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [is admin]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdmin { get; set; }

        #endregion
    }
}