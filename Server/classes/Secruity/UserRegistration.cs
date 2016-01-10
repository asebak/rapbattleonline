#region Using

using System.Web.Security;
using YAF.Classes;
using YAF.Classes.Data;
using YAF.Core;
using YAF.Types.Constants;
using YAF.Types.Interfaces;
using YAF.Utils;

#endregion

namespace FreestyleOnline.classes.Secruity
{
    public class UserRegistration
    {
        #region Members

        private readonly YafContext _context;
        private readonly string _email;
        private readonly string _password;
        private readonly string _username;

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserRegistration" /> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="passWord">The pass word.</param>
        /// <param name="email">The email.</param>
        public UserRegistration(string userName, string passWord, string email)
        {
            _username = userName;
            _email = email;
            _password = passWord;
            _context = YafContext.Current;
        }

        /// <summary>
        ///     Registers the user.
        /// </summary>
        public void RegisterUser()
        {
            MembershipCreateStatus status;
            var user = _context.Get<MembershipProvider>().CreateUser(
                _username,
                _password,
                _email,
                "noquestion",
                "noawnser",
                true,
                null,
                out status);
            RoleMembershipHelper.SetupUserRoles(YafContext.Current.PageBoardID, _username);
            var userIdRole = RoleMembershipHelper.CreateForumUser(user, YafContext.Current.PageBoardID);
            var userProfile = YafUserProfile.GetProfile(_username);
            userProfile.Location = "";
            userProfile.Homepage = "";
            userProfile.Save();
            LegacyDb.user_save(
                UserMembershipHelper.GetUserIDFromProviderUserKey(user.ProviderUserKey),
                _context.PageBoardID,
                null,
                null,
                null,
                -720,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            var autoWatchTopicsEnabled =
                _context.Get<YafBoardSettings>().DefaultNotificationSetting.Equals(
                    UserNotificationSetting.TopicsIPostToOrSubscribeTo);
            LegacyDb.user_savenotification(
                UserMembershipHelper.GetUserIDFromProviderUserKey(user.ProviderUserKey),
                true,
                autoWatchTopicsEnabled,
                _context.Get<YafBoardSettings>().DefaultNotificationSetting,
                _context.Get<YafBoardSettings>().DefaultSendDigestEmail);
        }

        #endregion
    }
}