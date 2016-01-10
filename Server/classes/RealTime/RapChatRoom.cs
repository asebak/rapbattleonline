#region Using

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.RealTime.Classes;

#endregion

namespace FreestyleOnline.classes.RealTime
{
    // Client API:
    // 
    // onNewUserConnected(int, string)
    // messageReceived(string,string)
    // onUserDisconnected(int, string)
    public class RapChatRoom : RapSignalR
    {
        #region Members

        /// <summary>
        ///     The connected users
        /// </summary>
        private static readonly List<UserConnection> ConnectedUsers = new List<UserConnection>();

        /// <summary>
        ///     The current message
        /// </summary>
        private static readonly List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        #endregion

        #region Methods

        /// <summary>
        ///     Connects the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="isGuest">if set to <c>true</c> [is guest].</param>
        /// <param name="image">The image.</param>
        public void Connect(string userName, bool isGuest, string image)
        {
            var id = Context.ConnectionId;
            if (isGuest)
            {
                userName += " <i>(Guest)</i>" + image;
            }
            else
            {
                userName += image;
            }
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserConnection {ConnectionId = id, UserName = userName});
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        /// <summary>
        ///     Sends the message to all.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="message">The message.</param>
        public void SendMessageToAll(string userName, string message)
        {
            AddMessageinCache(userName, message);
            Clients.All.messageReceived(userName, message);
        }


        /// <summary>
        ///     Called when disconnected.
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected()
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);
            }

            return base.OnDisconnected();
        }

        /// <summary>
        ///     Adds the messagein cache.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="message">The message.</param>
        private void AddMessageinCache(string userName, string message)
        {
            CurrentMessage.Add(new MessageDetail {UserName = userName, Message = message});

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }

        #endregion
    }
}