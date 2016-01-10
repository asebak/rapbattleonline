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
    // Client API:
    // 
    // updateCypherList(List<User> users, int cypherId)
    // updateUserList(List<User> userList)
    // callAccepted(User acceptingUser)
    // callDeclined(User decliningUser, string reason)
    // incomingCall(User callingUser)
    // receiveSignal(User signalingUser, string signal)
    // joinCypher(int cypherId)
    public class RapCypher : RapSignalR
    {
        #region Members

        private static readonly List<CypherUserConnection> ConnectedUsers = new List<CypherUserConnection>();
        private static readonly List<CypherUsers> UserCalls = new List<CypherUsers>();
        private static readonly List<CallOffer> CallOffers = new List<CallOffer>();

        private static readonly Dictionary<int, List<CypherUserConnection>> CypherRooms =
            new Dictionary<int, List<CypherUserConnection>>();

        private static string _userName;

        #endregion

        #region Methods

        /// <summary>
        ///     Connects the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        public void Connect(string username)
        {
            _userName = username;
            ConnectedUsers.Add(new CypherUserConnection
            {
                UserName = username,
                ConnectionId = Context.ConnectionId
            });
            SendUserListUpdate();
            SendCyperListUpdate();
        }

        /// <summary>
        ///     Called when [disconnected].
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected()
        {
            HangUp();
            ConnectedUsers.RemoveAll(u => u.ConnectionId == Context.ConnectionId);
            foreach (var cypher in CypherRooms)
            {
                cypher.Value.RemoveAll(u => u.ConnectionId == Context.ConnectionId);
            }
            SendUserListUpdate();
            SendCyperListUpdate();
            return base.OnDisconnected();
        }

        /// <summary>
        ///     Calls the user.
        /// </summary>
        /// <param name="targetConnectionId">The target connection identifier.</param>
        public void CallUser(string targetConnectionId)
        {
            //instead of calling a user should automatically join a cypher group
            var callingUser = ConnectedUsers.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = ConnectedUsers.SingleOrDefault(u => u.ConnectionId == targetConnectionId);

            // Make sure the person we are trying to call is still here
            if (targetUser == null)
            {
                // If not, let the caller know
                Clients.Caller.callDeclined(targetConnectionId, "The user you called has left.");
                return;
            }

            // And that they aren't already in a call
            if (GetUserCall(targetUser.ConnectionId) != null)
            {
                Clients.Caller.callDeclined(targetConnectionId,
                    string.Format("{0} is already in a call.", targetUser.UserName));
                return;
            }

            // They are here, so tell them someone wants to talk
            Clients.Client(targetConnectionId).incomingCall(callingUser);

            // Create an offer
            CallOffers.Add(new CallOffer
            {
                Caller = callingUser,
                Callee = targetUser
            });
        }

        /// <summary>
        ///     Answers the call.
        /// </summary>
        /// <param name="acceptCall">if set to <c>true</c> [accept call].</param>
        /// <param name="targetConnectionId">The target connection identifier.</param>
        public void AnswerCall(bool acceptCall, string targetConnectionId)
        {
            var callingUser = ConnectedUsers.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = ConnectedUsers.SingleOrDefault(u => u.ConnectionId == targetConnectionId);

            // This can only happen if the server-side came down and clients were cleared, while the user
            // still held their browser session.
            if (callingUser == null)
            {
                return;
            }

            // Make sure the original caller has not left the page yet
            if (targetUser == null)
            {
                Clients.Caller.callEnded(targetConnectionId, "The other user in your call has left.");
                return;
            }

            // Send a decline message if the callee said no
            if (acceptCall == false)
            {
                Clients.Client(targetConnectionId)
                    .callDeclined(callingUser, string.Format("{0} did not accept your call.", callingUser.UserName));
                return;
            }

            // Make sure there is still an active offer.  If there isn't, then the other use hung up before the Callee answered.
            var offerCount = CallOffers.RemoveAll(c => c.Callee.ConnectionId == callingUser.ConnectionId
                                                       && c.Caller.ConnectionId == targetUser.ConnectionId);
            if (offerCount < 1)
            {
                Clients.Caller.callEnded(targetConnectionId,
                    string.Format("{0} has already hung up.", targetUser.UserName));
                return;
            }

            // And finally... make sure the user hasn't accepted another call already
            if (GetUserCall(targetUser.ConnectionId) != null)
            {
                // And that they aren't already in a call
                Clients.Caller.callDeclined(targetConnectionId,
                    string.Format("{0} chose to accept someone elses call instead of yours :(", targetUser.UserName));
                return;
            }

            // Remove all the other offers for the call initiator, in case they have multiple calls out
            CallOffers.RemoveAll(c => c.Caller.ConnectionId == targetUser.ConnectionId);

            // Create a new call to match these folks up
            UserCalls.Add(new CypherUsers
            {
                Users = new List<CypherUserConnection> {callingUser, targetUser}
            });

            // Tell the original caller that the call was accepted
            Clients.Client(targetConnectionId).callAccepted(callingUser);

            // Update the user list, since thes two are now in a call
            SendUserListUpdate();
        }

        /// <summary>
        ///     Hangs up.
        /// </summary>
        public void HangUp()
        {
            var callingUser = ConnectedUsers.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);

            if (callingUser == null)
            {
                return;
            }

            var currentCall = GetUserCall(callingUser.ConnectionId);

            // Send a hang up message to each user in the call, if there is one
            if (currentCall != null)
            {
                foreach (var user in currentCall.Users.Where(u => u.ConnectionId != callingUser.ConnectionId))
                {
                    Clients.Client(user.ConnectionId)
                        .callEnded(callingUser.ConnectionId, string.Format("{0} has hung up.", callingUser.UserName));
                }

                // Remove the call from the list if there is only one (or none) person left.  This should
                // always trigger now, but will be useful when we implement conferencing.
                currentCall.Users.RemoveAll(u => u.ConnectionId == callingUser.ConnectionId);
                if (currentCall.Users.Count < 2)
                {
                    UserCalls.Remove(currentCall);
                }
            }

            // Remove all offers initiating from the caller
            CallOffers.RemoveAll(c => c.Caller.ConnectionId == callingUser.ConnectionId);

            SendUserListUpdate();
        }

        // WebRTC Signal Handler
        /// <summary>
        ///     Sends the signal.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <param name="targetConnectionId">The target connection identifier.</param>
        public void SendSignal(string signal, string targetConnectionId)
        {
            var callingUser = ConnectedUsers.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = ConnectedUsers.SingleOrDefault(u => u.ConnectionId == targetConnectionId);

            // Make sure both users are valid
            if (callingUser == null || targetUser == null)
            {
                return;
            }

            // Make sure that the person sending the signal is in a call
            var userCall = GetUserCall(callingUser.ConnectionId);

            // ...and that the target is the one they are in a call with
            if (userCall != null && userCall.Users.Exists(u => u.ConnectionId == targetUser.ConnectionId))
            {
                // These folks are in a call together, let's let em talk WebRTC
                Clients.Client(targetConnectionId).receiveSignal(callingUser, signal);
            }
        }

        /// <summary>
        ///     Joins the cypher.
        /// </summary>
        /// <param name="cypherId">The cypher identifier.</param>
        public void JoinCypher(int cypherId)
        {
            if (CypherRooms.Any(cypher => cypher.Value.Count(x => x.ConnectionId == Context.ConnectionId) != 0))
            {
                Clients.Client(Context.ConnectionId)
                    .sendNotification(string.Format("You can only join one cypher at a time."), RapAlert.Error);
                return;
            }
            Groups.Add(Context.ConnectionId, cypherId.ToString());
            var cypherUsers = new List<CypherUserConnection>();
            if (CypherRooms.ContainsKey(cypherId))
            {
                cypherUsers = CypherRooms[cypherId];
            }
            if (cypherUsers.Count(x => x.ConnectionId == Context.ConnectionId) == 0)
            {
                Clients.OthersInGroup(cypherId.ToString())
                    .sendNotification(string.Format("{0} joined the cypher", _userName), RapAlert.Normal);
                cypherUsers.Add(new CypherUserConnection
                {
                    UserName = _userName,
                    ConnectionId = Context.ConnectionId,
                    InCypher = true
                });
            }
            CypherRooms[cypherId] = cypherUsers;
            SendCyperListUpdate();
        }

        /// <summary>
        ///     Leaves the cypher.
        /// </summary>
        /// <param name="cypherId">The cypher identifier.</param>
        public void LeaveCypher(int cypherId)
        {
            CypherRooms[cypherId].Find(x => x.ConnectionId == Context.ConnectionId).InCypher = false;
            CypherRooms[cypherId].RemoveAll(u => u.ConnectionId == Context.ConnectionId);
            Groups.Remove(Context.ConnectionId, cypherId.ToString());
            SendCyperListUpdate();
            Clients.OthersInGroup(cypherId.ToString())
                .sendNotification(string.Format("{0} left the cypher", _userName), RapAlert.Error);
        }

        /// <summary>
        ///     Sends the cyper list update.
        /// </summary>
        private void SendCyperListUpdate()
        {
            foreach (var cypher in CypherRooms)
            {
                Clients.All.updateCypherList(cypher.Value, cypher.Key);
            }
        }

        /// <summary>
        ///     Sends the user list update.
        /// </summary>
        private void SendUserListUpdate()
        {
            ConnectedUsers.ForEach(u => u.InCypher = (GetUserCall(u.ConnectionId) != null));
            Clients.All.updateUserList(ConnectedUsers);
        }

        /// <summary>
        ///     Gets the user call.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <returns></returns>
        private CypherUsers GetUserCall(string connectionId)
        {
            var matchingCall =
                UserCalls.SingleOrDefault(uc => uc.Users.SingleOrDefault(u => u.ConnectionId == connectionId) != null);
            return matchingCall;
        }

        #endregion
    }
}