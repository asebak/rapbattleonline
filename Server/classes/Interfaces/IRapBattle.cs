#region Using

using System.Collections.Generic;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;

#endregion

namespace FreestyleOnline.classes.Interfaces
{
    /// <summary>
    ///     Interface for a rap battle
    /// </summary>
    internal interface IRapBattle
    {
        #region Methods

        int CreateBattle();
        void JoinBattle(int userId);
        void Submit(int userId, object content);
        void Vote(RapBattleVote user1, RapBattleVote user2);
        RapBattle GetSettings();
        bool IsUserAllowedToVote(bool isGuest);
        List<RapBattle> GetLatestBattles();
        List<RapBattle> GetVotingInProgressBattles();

        #endregion
    }
}