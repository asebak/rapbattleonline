#region Using

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.RealTime.Classes;

#endregion

namespace FreestyleOnline.classes.RealTime
{
    /// <summary>
    ///     Client API
    ///     notifyGlobal(string, string)
    ///     notifyPM(int userId, string message)
    /// </summary>
    public class GlobalNotifications : RapSignalR
    {
        #region Members

        /// <summary>
        ///     The connected users on the site in real time.
        /// </summary>
        private static readonly List<UserConnection> ConnectedUsers = new List<UserConnection>();

        #endregion

        #region Methods

        /// <summary>
        ///     Connects the specified username.
        /// </summary>
        /// <param name="userId"></param>
        public void Connect(int userId)
        {
            ConnectedUsers.Add(new UserConnectionID
            {
                UserId = userId,
                ConnectionId = Context.ConnectionId
            });
        }
        /// <summary>
        /// Called when [disconnected].
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected()
        {
            ConnectedUsers.RemoveAll(u => u.ConnectionId == Context.ConnectionId);
            return base.OnDisconnected();
        }
        /// <summary>
        ///     Notifies the Client of a notification message.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void Notify(string title, string message)
        {
            //var listofUsers = this.GetService<RapProviders>().GetAllRegisteredUsers();
            //foreach (var s in listofUsers)
            //{
            //    Message.SendPmMessage(UserData.GetUserIdFromDisplayName(HttpContext.Current.User.Identity.Name),
            //        UserData.GetUserIdFromDisplayName(s), title,
            //        message);
            //}
            Clients.Others.notifyGlobal(title, message);
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