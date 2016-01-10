#region Using

using System.Collections.Generic;

#endregion

namespace FreestyleOnline.classes.RealTime.Classes
{
    public class CypherUsers
    {
        #region Members

        /// <summary>
        ///     The users
        /// </summary>
        public List<CypherUserConnection> Users;

        #endregion
    }

    public class CallOffer
    {
        #region Members

        /// <summary>
        ///     The callee
        /// </summary>
        public CypherUserConnection Callee;

        /// <summary>
        ///     The caller
        /// </summary>
        public CypherUserConnection Caller;

        #endregion
    }
}