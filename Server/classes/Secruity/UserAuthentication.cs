#region Using

using System.Web.Security;
using YAF.Core;
using YAF.Types.Interfaces;

#endregion

namespace FreestyleOnline.classes.Secruity
{
    public class UserAuthentication
    {
        #region Members

        private readonly YafContext _context;
        private readonly string _passWord;
        private readonly string _userName;

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserAuthentication" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public UserAuthentication(string username, string password)
        {
            this._userName = username;
            this._passWord = password;
            this._context = YafContext.Current;
        }

        /// <summary>
        ///     Determines whether this instance is authenticated.
        /// </summary>
        /// <returns></returns>
        public string IsAuthenticated()
        {
            var realUserName = this.GetValidUsername(this._userName, this._passWord);
            return realUserName;
        }

        /// <summary>
        ///     Gets the valid username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        protected virtual string GetValidUsername(string username, string password)
        {
            if (username.Contains("@") && _context.Get<MembershipProvider>().RequiresUniqueEmail)
            {
                // attempt Email Login
                var realUsername = _context.Get<MembershipProvider>().GetUserNameByEmail(username);

                if (realUsername != null && _context.Get<MembershipProvider>().ValidateUser(realUsername, password))
                {
                    return realUsername;
                }
            }

            // Standard user name login
            if (_context.Get<MembershipProvider>().ValidateUser(username, password))
            {
                return username;
            }

            // Display name login
            var id = _context.Get<IUserDisplayName>().GetId(username);

            if (id.HasValue)
            {
                // get the username associated with this id...
                var realUsername = UserMembershipHelper.GetUserNameFromID(id.Value);

                // validate again...
                if (_context.Get<MembershipProvider>().ValidateUser(realUsername, password))
                {
                    return realUsername;
                }
            }

            // no valid login -- return null
            return null;
        }

        #endregion
    }
}