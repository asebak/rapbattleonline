#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Classes.Data;

#endregion

namespace FreestyleOnline.classes
{
    public class UserFriend: RapClass
    {
        #region Members

        private int _buddiesForUserId;
        private UserData _usersData;

        #endregion

        #region Properties

        public int UserId { get; set; }
        public string UserName { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the buddies.
        /// </summary>
        /// <returns></returns>
        public List<UserFriend> GetBuddies(int userId)
        {
            this._buddiesForUserId = userId;
            this._usersData = new UserData(userId);

            //validate if working changed the where implementation
            var buddiesList = LegacyDb.buddy_list(this._buddiesForUserId);
            return (from DataRow r in buddiesList.Rows
                select new UserFriend
                {
                    UserId = Convert.ToInt32(r["UserID"]),
                    UserName = r["Name"].ToString(),
                }).Where(x => this.IsBuddy(buddiesList, x.UserId)).ToList();
        }

        /// <summary>
        ///     Gets the hood buddies.
        /// </summary>
        /// <returns></returns>
        public List<UserFriend> GetHoodBuddies()
        {
            //TODO: Get hood buddies should not be duplicate user ids as well as enhance performance
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Determines whether the specified source is buddy.
        /// </summary>
        /// <param name="userBuddyList">The user buddy list.</param>
        /// <param name="buddyUserId">The buddy user identifier.</param>
        /// <returns></returns>
        private bool IsBuddy(DataTable userBuddyList, int buddyUserId)
        {
            if (buddyUserId == this._buddiesForUserId)
            {
                return true;
            }
            return (userBuddyList != null) && (userBuddyList.Rows.Count > 0);
        }

        #endregion
    }
}