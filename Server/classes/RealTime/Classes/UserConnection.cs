namespace FreestyleOnline.classes.RealTime.Classes
{
    /// <summary>
    ///     User Context when Connected to Hub
    /// </summary>
    public class UserConnection
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the connection identifier.
        /// </summary>
        /// <value>
        ///     The connection identifier.
        /// </value>
        public string ConnectionId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        public string UserName { get; set; }

        #endregion
    }
}