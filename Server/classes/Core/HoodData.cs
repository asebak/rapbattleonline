#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Types;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class HoodData : RapClass
    {
        #region Properties

        [NotNull]
        public int HoodId { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Picture { get; set; }

        [NotNull]
        public DateTime DateCreated { get; set; }

        [CanBeNull]
        public string Details { get; set; }

        [NotNull]
        public bool IsPublic { get; set; }

        [CanBeNull]
        public List<HoodUser> Users { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Valididates if a user can add the hood
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public bool ValidHood(HoodData data)
        {
            Contract.Requires(data != null);
            return !string.IsNullOrEmpty(data.Name) && Db.get_hoodname(data.Name) == null;
        }

        /// <summary>
        ///     Adds the hood after verification has been done.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="userId">The user identifier.</param>
        public void AddHood(HoodData data, int userId)
        {
            Contract.Requires(data != null);
            Contract.Requires(!string.IsNullOrEmpty(data.Name));
            Db.add_hood(data.Name, data.Details, data.Picture, userId, data.IsPublic);
        }

        /// <summary>
        ///     Constructs the hood object list.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static List<HoodData> ConstructHoodObjectList(DataSet data)
        {
            Contract.Requires(data != null);
            var hoodList = new List<HoodData>();
            foreach (DataRow r in data.Tables[0].Rows)
            {
                var hoodData = new HoodData
                {
                    DateCreated = DateTime.Parse(r["DateCreated"].ToString()),
                    Details = r["Details"].ToString(),
                    HoodId = Convert.ToInt32(r["HoodID"]),
                    IsPublic = Convert.ToBoolean(r["IsPublic"]),
                    Name = r["Name"].ToString(),
                    Picture = r["Picture"].ToString(),
                    Users = new List<HoodUser>()
                };
                var hoodUser = new HoodUser
                {
                    UserId = Convert.ToInt32(r["UserID"]),
                    IsAdmin = Convert.ToBoolean(r["IsAdmin"])
                };
                //check if hood exists
                var found = hoodList.Any(x => x.HoodId == hoodData.HoodId);
                //if found append user to list
                if (found)
                {
                    hoodList.Find(x => x.HoodId == hoodData.HoodId).Users.Add(hoodUser);
                }
                    //else add the new hood with that user
                else
                {
                    //user id of 0 or 1 is bad, cannot have guests joining hoods
                    hoodData.Users.Add(hoodUser);
                    hoodList.Add(hoodData);
                }
            }
            return hoodList;
        }


        /// <summary>
        ///     Gets all hoods and organizes the data in a logical list.
        /// </summary>
        /// <returns></returns>
        public List<HoodData> GetAllHoods()
        {
            var dataDs = Db.get_allhoods();
            var allHoods = ConstructHoodObjectList(dataDs);
            return allHoods;
        }

        /// <summary>
        /// Gets the invited users.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <returns></returns>
        public List<int> GetInvitedUsers(int hoodId)
        {
            var invitedDs = Db.get_hoodmembers_invited(hoodId);
            return (from DataRow row in invitedDs.Tables[0].Rows select (int) row[0]).ToList();
        }

        /// <summary>
        /// Privates the invitation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="hoodId">The hood identifier.</param>
        public void PrivateInvitation(int userId, int hoodId)
        {
            Db.invite_hood_user(hoodId, userId);
        }

        /// <summary>
        ///     Gets the hood details.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <returns></returns>
        public HoodData GetHoodDetails(int hoodId)
        {
            Contract.Ensures(Contract.Result<HoodData>() != null);
            var dataDs = Db.get_hoodmembers(hoodId);
            var specificHood = ConstructHoodObjectList(dataDs);
            return specificHood[0];
        }

        /// <summary>
        ///     Gets the random hoods.
        /// </summary>
        /// <returns></returns>
        public List<HoodData> GetRandomHoods()
        {
            var randomDs = Db.get_randomhoods();
            var randomHoods = ConstructHoodObjectList(randomDs);
            return randomHoods.Take(30).ToList();
        }

        /// <summary>
        ///     Adds the user to hood.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void AddUserToHood(int hoodId, int userId)
        {
            Contract.Requires(hoodId > 0);
            Contract.Requires(userId > 1);
            Db.join_hood(userId, hoodId);
        }

        /// <summary>
        ///     Removes the user from hood.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string[] RemoveUserFromHood(int hoodId, int userId)
        {
            Contract.Requires(hoodId > 0);
            Contract.Requires(userId > 1);
            Db.remove_hoodmember(userId, hoodId);
            var nextuserId = Db.get_first_hoodmember(hoodId);
            if (nextuserId == null)
            {
                Db.delete_hoodcomments(hoodId);
                Db.remove_hood(hoodId);
                return new[] {"HOOD", "HOOD_NOMEMBERS!"};
            }
            return new[] {"HOOD", "HOOD_LEFT2!"};
        }

        /// <summary>
        ///     Updates the hood user to admin.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateHoodUserToAdmin(int hoodId, int userId)
        {
            Contract.Requires(hoodId > 0);
            Contract.Requires(userId > 1);
            Db.hoodmember_toadmin(userId, hoodId);
        }

        /// <summary>
        ///     Updates the hood's description and privacy setting.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <param name="isPublic">if set to <c>true</c> [is public].</param>
        /// <param name="description">The description.</param>
        public void UpdateHood(int hoodId, bool isPublic, string description)
        {
            Contract.Requires(hoodId > 0);
            Db.update_hooddetails(hoodId, isPublic, description);
        }

        #endregion
    }
}