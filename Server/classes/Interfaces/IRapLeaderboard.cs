#region Using

using System.Collections.Generic;

#endregion

namespace FreestyleOnline.classes.Interfaces
{
    internal interface IRapLeaderboard<T>
    {
        List<T> GetRankings();
    }
}