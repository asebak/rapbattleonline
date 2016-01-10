#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Types;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Providers;
using YAF.Classes;
using YAF.Core;
using YAF.Core.Services;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class UserDataFeatured : UserData
    {
        #region Methods

        /// <summary>
        ///     Sets the featured users.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="date">The date.</param>
        public void SetFeaturedUsers(int userId, DateTime date)
        {
            Db.feature_profile(userId, date);
        }


        /// <summary>
        ///     Gets the featured users.
        /// </summary>
        /// <param name="boardSettings">The board settings.</param>
        /// <returns></returns>
        public List<CarouselData> GetFeaturedUsers(YafBoardSettings boardSettings)
        {
            var avatar = new YafAvatars(boardSettings); // not needed
            var userDs = Db.get_featured_profiles();

            var userFeaturedList = (from r in userDs.Tables[0].AsEnumerable()
                .Where(r => RapGlobalHelpers.IsDateExpired(r.Field<DateTime>("FeaturedUntil")) == false)
                select new CarouselData
                {
                    UserId = r.Field<int>("UserID"),
                    CaptionText = // not needed
                        String.Format("View {0}'s Profile",
                            UserMembershipHelper.GetDisplayNameFromID(r.Field<int>("UserID"))),// not needed
                    HyperLink = this.GetService<UrlProvider>().GetUrl("/Pages/Profile/{0}", (r.Field<int>("UserID"))),// not needed
                    ImagePath = avatar.GetAvatarUrlForUser(r.Field<int>("UserID")),
                    ExpiryDate = r.Field<DateTime>("FeaturedUntil") // not needed
                }).ToList();

            return userFeaturedList;
        }

        /// <summary>
        ///     Removes the featured profile.
        /// </summary>
        private void RemoveFeaturedProfile()
        {
            Db.delete_featuredprofile(UserId);
        }

        #endregion
    }
}