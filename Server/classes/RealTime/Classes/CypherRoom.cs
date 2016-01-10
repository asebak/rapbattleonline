#region Using

using System.Collections.Generic;
using YAF.Types;

#endregion

namespace FreestyleOnline.classes.RealTime.Classes
{
    public class CypherRoom
    {
        #region Members

        /// <summary>
        ///     The cypher users
        /// </summary>
        [CanBeNull] public List<CypherUserConnection> CypherUsers;

        #endregion
    }
}