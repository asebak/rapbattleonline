#region Using

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.RealTime.Classes;

#endregion

namespace FreestyleOnline.classes.RealTime
{
    public class SocialFeedNotifier : RapSignalR
    {
        #region Members

        /// <summary>
        ///     The connected users on the site in real time.
        /// </summary>
        private static readonly List<UserConnection> ConnectedUsers = new List<UserConnection>();

        #endregion

        #region Methods

        /// <summary>
        ///     Called when user visits site.
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            var id = Context.ConnectionId;
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserConnection
                {
                    ConnectionId = id,
                    UserName = HttpContext.Current.User.Identity.Name
                });
            }
            return base.OnConnected();
        }

        /// <summary>
        ///     Gets the connected users.
        /// </summary>
        /// <returns></returns>
        public static List<UserConnection> GetConnectedUsers()
        {
            return ConnectedUsers;
        }

        #endregion
    }
}