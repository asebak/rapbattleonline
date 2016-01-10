#region Using

using System.Linq;
using System.Web.Security;
using YAF.Core;

#endregion

namespace FreestyleOnline.classes.Providers
{
    /// <summary>
    ///     Provides a Singleton to provide global services throughout the Application
    /// </summary>
    public class RapProviders : BaseRapProvider
    {
        #region Methods    

        /// <summary>
        ///     Gets all registered users.
        /// </summary>
        /// <returns></returns>
        public string[] GetAllRegisteredUsers()
        {
            return UserMembershipHelper.GetAllUsers().Cast<MembershipUser>()
                .Select(x => x.UserName).ToArray();
        }

        #endregion
    }
}