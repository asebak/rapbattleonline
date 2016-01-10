#region Using

using System.Data;
using System.Linq;
using FreestyleOnline.classes.Database;
using YAF.Classes.Data;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class UserProfile : UserData
    {
        private readonly int _boardId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserProfile" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public UserProfile(int userId)
        {
            this.UserId = userId;
        }

        public UserProfile(int boardId, int userId)
        {
            this._boardId = boardId;
            this.UserId = userId;
        }

        /// <summary>
        ///     Uploads the header image.
        /// </summary>
        /// <param name="file">The file.</param>
        public void UploadHeaderImage(string file)
        {
            Db.update_profileheader(this.UserId, file);
        }

        /// <summary>
        /// Updates the bio.
        /// </summary>
        /// <param name="htmlcontent">The htmlcontent.</param>
        public void UpdateBio(string htmlcontent)
        {
            Db.update_profilebio(this.UserId, htmlcontent);
        }

        /// <summary>
        /// Gets the bio.
        /// </summary>
        /// <returns></returns>
        public string GetBio()
        {
            var bio = Db.get_user_siteprofile(this.UserId);
            var bioLast = bio.Tables[0].AsEnumerable()
                .Select(r => r.Field<string>("Bio"))
                .ToList();
            return bioLast.Any() ? bioLast.Last() : null;
        }

        /// <summary>
        ///     Gets the header image.
        /// </summary>
        /// <returns></returns>
        public string GetHeaderImage()
        {
            var headerImages = Db.get_user_siteprofile(this.UserId);
            var headers = headerImages.Tables[0].AsEnumerable()
                .Select(r => r.Field<string>("HeaderImage"))
                .ToList();
            return headers.Any() ? headers.Last() : null;
        }

        /// <summary>
        ///     Gets the last visit.
        /// </summary>
        /// <returns></returns>
        public object GetLastVisit()
        {
            using (var dt = LegacyDb.user_list(this._boardId, this.UserId, null))
            {
                var row = dt.Rows[0];
                return row["LastVisit"];
            }
        }
    }
}