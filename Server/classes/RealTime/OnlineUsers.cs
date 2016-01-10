#region Using

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.RealTime.Classes;
using FreestyleOnline.classes.Types;

#endregion

namespace FreestyleOnline.classes.RealTime
{
    /// <summary>
    ///     Client API
    ///     totalOnlineUsers(int)
    /// </summary>
    public class OnlineUsers : RapSignalR
    {
        #region Members

        /// <summary>
        ///     The connected users on the site in real time.
        /// </summary>
        private static readonly List<UserConnectionID> ConnectedUsers = new List<UserConnectionID>();

        public static List<UserConnectionID> Connected
        {
            get { return ConnectedUsers; }
        }

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
                ConnectedUsers.Add(new UserConnectionID {ConnectionId = id, UserId = RapContextFacade.Current.GetUserId()});
            }
            Clients.All.totalOnlineUsers(ConnectedUsers.Count);
            return base.OnConnected();
        }

        /// <summary>
        ///     Called when user is reconnected.
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            var id = Context.ConnectionId;
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserConnectionID { ConnectionId = id, UserId = RapContextFacade.Current.GetUserId() });
            }

            Clients.All.totalOnlineUsers(ConnectedUsers.Count);

            return base.OnReconnected();
        }

        /// <summary>
        ///     Called when user is disconnected.
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected()
        {
            var id = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionId == id) > 0)
            {
                ConnectedUsers.RemoveAll(y => y.ConnectionId == id);
            }

            Clients.All.totalOnlineUsers(ConnectedUsers.Count);

            return base.OnDisconnected();
        }

        #endregion
    }
}