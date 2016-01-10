#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.RealTime;
using FreestyleOnline.classes.Types;
using Microsoft.AspNet.SignalR;
using YAF.Core;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class RapSocialFeed : RapClass, IRapSocialFeed<List<RapSocialFeedItem>>
    {
        #region Members

        private readonly RapSocialFeedType _type;
        //private IHubContext _socialContext;

        #endregion

        #region Constructor
        public RapSocialFeed(){}
        public RapSocialFeed(RapSocialFeedType type)
        {
            this._type = type;
            //this._socialContext = GlobalHost.ConnectionManager.GetHubContext<SocialFeedNotifier>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<RapSocialFeedItem> Get(int userId)
        {
            if (userId <= 1)
            {
                return null;
            }
            var feedDs = Db.get_feed(userId);
            var feed = (from r in feedDs.Tables[0].AsEnumerable()
                select new RapSocialFeedItem
                {
                    FeedId = r.Field<int>("FeedID"),
                    From = UserMembershipHelper.GetDisplayNameFromID(r.Field<int>("FromID")),
                    To = UserMembershipHelper.GetDisplayNameFromID(r.Field<int>("ToID")),
                    ObjectId = r.Field<int>("ObjectID"),
                    Type = r.Field<RapSocialFeedType>("TypeID"),
                    IsDeleted = r.Field<bool>("IsDeleted"),
                    Time = r.Field<DateTime>("Created")
                }).ToList();
            return feed;
        }

        /// <summary>
        /// Submits the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="objectId">The object identifier.</param>
        /// <param name="type">The type.</param>
        public void Submit(int from, int objectId, RapSocialFeedType type)
        {           
            //TODO: do signalR Notification
            var friends = new UserFriend().GetBuddies(from);
            //var hoodUserList = new UserFriend().GetHoodBuddies();
            foreach (var f in friends)
            {
                Db.add_feeditem(f.UserId, from, type, objectId);
            }
        }

        /// <summary>
        ///     Deletes the specified social feed identifier.
        /// </summary>
        /// <param name="socialFeedId">The social feed identifier.</param>
        public void Delete(int socialFeedId)
        {
            //validation through web api
            Db.delete_feeditem(socialFeedId);
        }

        #endregion
    }
}