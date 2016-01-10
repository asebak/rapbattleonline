using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Ext.Net;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Classes;
using YAF.Core;
using YAF.Core.Services;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Interfaces;
using MyUserData = FreestyleOnline.classes.Core.UserData;

namespace FreestyleOnline.Controls
{
    public partial class FriendsList : RapUserControl
    {
        #region Properties

        public Window FriendWindow { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.PageContext.IsGuest)
            {
                return;
            }
            this.ConstructOnlineUsersTree();
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Constructs the online users tree.
        /// </summary>
        private void ConstructOnlineUsersTree()
        {
            var activeOnlineUsers = this.GetActiveUsers();
            var friendsList = this.GetBuddiesList();
            var tree = new TreePanel
            {
                RootVisible = false,
                ID = "OnlineUsersTree",
                ContextMenuID = "OnlineUsersMenu",
            };
            var root = new Node {NodeID = "root", Expanded = true};
            tree.Root.Add(root);
            var friendsNode = new Node { Text = this.Text("FRIENDS", "LIST"), Expanded = true };
            foreach (var friend in friendsList)
            {
                friendsNode.Children.Add(new Node()
                {
                    Text = friend.UserName,
                    Icon =
                        (activeOnlineUsers.Exists(x => x.UserId == friend.UserId))
                            ? Icon.StatusOnline
                            : Icon.StatusOffline,
                    Leaf = true
                });
            }
            if (!friendsList.Any())
            {
                friendsNode.Children.Add(new Node()
                {
                    Text = this.Text("FRIENDS", "NONE"),
                    Icon = Icon.None,
                    Leaf = true,
                });
                this.OnlineUsersMenu.Visible = false;
            }
            root.Children.Add(friendsNode);
            //TODO: Optimize friends list due to performance issue
            //var usersHood = this.GetCore<classes.Core.UserData>().GetUsersHoods(this.PageContext.PageUserID);
            //foreach (var h in usersHood)
            //{
            //    var hoodNode = new Node { Text = h.Name, Expanded = false };
            //    foreach (var u in h.Users.Where(x => x.UserId != this.PageContext.PageUserID))
            //    {
            //        hoodNode.Children.Add(new Node()
            //        {
            //            Text = UserMembershipHelper.GetDisplayNameFromID(u.UserId),
            //            Icon =
            //                (activeOnlineUsers.Exists(x => x.UserId == u.UserId))
            //                    ? Icon.StatusOnline
            //                    : Icon.StatusOffline,
            //            Leaf = true
            //        });
            //    }
            //    root.Children.Add(hoodNode);
            //}
            FriendWindow = new Window
            {
                ID = "friendsWindow",
                AutoRender = false,
                Hidden = true,
                Width = Unit.Pixel(250),
                Height = Unit.Pixel(400),
                Maximizable = false,
                BodyBorder = 0,
                Layout = "Accordion"
            };

            this.FriendWindow.Items.Add(tree);
            this.FriendsAspPlaceHolder.Controls.Add(this.FriendWindow);
        }

        /// <summary>
        /// Gets the buddies list.
        /// </summary>
        /// <returns></returns>
        private List<UserFriend> GetBuddiesList()
        {
            return this.GetCore<UserFriend>().GetBuddies(this.PageContext.PageUserID);
        }

        /// <summary>
        /// Gets the active users.
        /// </summary>
        /// <returns></returns>
        private List<ActiveOnlineUsers> GetActiveUsers()
        {
            return new ActiveOnlineUsers().Get(this.Get<IDataCache>().GetOrSet(
                Constants.Cache.UsersOnlineStatus,
                () =>
                    this.Get<YafDbBroker>().GetActiveList(false, this.Get<YafBoardSettings>().ShowCrawlersInActiveList),
                TimeSpan.FromMilliseconds(this.Get<YafBoardSettings>().OnlineStatusCacheTimeout)));
        }

        #endregion

        #region Client Handlers

        /// <summary>
        /// Handles the Click event of the EmailMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectEventArgs"/> instance containing the event data.</param>
        protected void EmailMenuItem_Click(object sender, DirectEventArgs e)
        {
            var node = JSON.Deserialize<List<Node>>(e.ExtraParams["userNode"]);
            this.PrivateMsg.To.Text = node[0].Text;
            this.PrivateMsg.To.Disabled = true;
            this.PrivateMsg.GetWindow.Show();
        }

        /// <summary>
        /// Handles the Click event of the ProfileMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectEventArgs"/> instance containing the event data.</param>
        protected void ProfileMenuItem_Click(object sender, DirectEventArgs e)
        {
            var node = JSON.Deserialize<List<Node>>(e.ExtraParams["userNode"]);
            var userId = MyUserData.GetUserIdFromDisplayName(node[0].Text);
            this.GetService<UrlProvider>().Redirect("~/Pages/Profile", userId);
        }

        #endregion
    }

}