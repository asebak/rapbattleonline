#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Providers;
using YAF.Classes;
using YAF.Core;
using YAF.Core.Services;

#endregion

namespace FreestyleOnline.classes.Core
{
    [Obsolete("Not Being Used")]
    public class UserDataRandom : RapClass
    {
        #region Methods

        /// <summary>
        ///     Gets the random users.
        /// </summary>
        /// <param name="boardSettings">The board settings.</param>
        /// <returns></returns>
        public List<CarouselData> GetRandomUsers(YafBoardSettings boardSettings)
        {
            var randomDs = Db.get_randomprofiles();
            var avatar = new YafAvatars(boardSettings);

            var randomProfileList =
                (from r in randomDs.Tables[0].AsEnumerable().Where(r => r.Field<int>("UserID") != 1)
                    select new CarouselData
                    {
                        UserId = r.Field<int>("UserID"),
                        CaptionText =
                            string.Format("View {0}'s profile",
                                UserMembershipHelper.GetDisplayNameFromID(r.Field<int>("UserID"))),
                        HyperLink =
                            this.GetService<UrlProvider>().GetUrl("/Pages/Profile/{0}", (r.Field<int>("UserID"))),
                        ImagePath = avatar.GetAvatarUrlForUser(r.Field<int>("UserID"))
                    }).Take(30).ToList();


            return randomProfileList;
        }

        #endregion
    }
}