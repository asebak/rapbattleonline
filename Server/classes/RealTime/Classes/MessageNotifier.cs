#region Using

using System.Linq;
using FreestyleOnline.classes.Base;
using Microsoft.AspNet.SignalR;

#endregion

namespace FreestyleOnline.classes.RealTime.Classes
{
    public class MessageNotifier : RapClass
    {
        /// <summary>
        ///     Notifies the private message.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="messageContent">Content of the message.</param>
        public void NotifyPrivateMessage(int to, int from, string messageContent)
        {
            //not working properly the clients.client for some reason
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<GlobalNotifications>();
            var connectedUsers = OnlineUsers.Connected;
            if (connectedUsers.Any())
            {
                //TODO: Fix Signal R Communication with private messages and RT
                context.Clients.Client(connectedUsers.Find(x => x.UserId == to).ConnectionId)
                    .notifyGlobal(from, messageContent);
            }
        }
    }
}