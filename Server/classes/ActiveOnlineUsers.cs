#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;

#endregion

namespace FreestyleOnline.classes
{
    public class ActiveOnlineUsers
    {
        #region Properties

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserDisplayName { get; set; }
        public bool IsHidden { get; set; }
        public DateTime LastActive { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the specified active users.
        /// </summary>
        /// <param name="activeUsers">The active users.</param>
        /// <returns></returns>
        public List<ActiveOnlineUsers> Get(DataTable activeUsers)
        {
            Contract.Requires(activeUsers != null);
            return (from DataRow r in activeUsers.Rows
                select new ActiveOnlineUsers
                {
                    UserId = Convert.ToInt32(r["UserID"]),
                    UserName = r["UserName"].ToString(),
                    UserDisplayName = r["UserDisplayName"].ToString(),
                    IsHidden = Convert.ToBoolean(r["IsHidden"]),
                    LastActive = Convert.ToDateTime(r["LastActive"])
                }).ToList();
        }

        #endregion
    }
}