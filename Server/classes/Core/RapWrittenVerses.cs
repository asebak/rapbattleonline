#region Using

using System.Collections.Generic;
using System.Data;
using System.Linq;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Interfaces;

#endregion

namespace FreestyleOnline.classes.Core
{
    public sealed class RapWrittenVerses : BaseRapVerses, IRapVerse<List<RapWrittenVerses>>
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapWrittenVerses" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public RapWrittenVerses(int userId)
        {
            this.UserId = userId;
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="RapWrittenVerses" /> class from being created.
        /// </summary>
        public RapWrittenVerses()
        {
        }

        #endregion

        #region IRapVerse<List<RapWrittenVerses>> Members

        /// <summary>
        ///     Deletes the verse.
        /// </summary>
        /// <param name="verseId">The verse identifier.</param>
        public void DeleteVerse(int verseId)
        {
            Db.delete_writtenverse(verseId);
        }

        /// <summary>
        ///     Adds the verse.
        /// </summary>
        /// <param name="verseTitle">The verse title.</param>
        /// <param name="verseContent">Content of the verse.</param>
        public void AddVerse(string verseTitle, string verseContent)
        {
            Db.upload_writtenverse(this.UserId, verseTitle, verseContent);
        }

        /// <summary>
        /// Updates the verse.
        /// </summary>
        /// <param name="verseId">The verse identifier.</param>
        /// <param name="verseContent">Content of the verse.</param>
        public void UpdateVerse(int verseId, string verseContent)
        {
            Db.update_writtenverse(verseId, verseContent, this.UserId);
        }

        /// <summary>
        ///     Gets the verses.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<RapWrittenVerses> GetVerses(int userId)
        {
            var usersVerses = Db.get_user_writtenverses(userId);
            return (from r in usersVerses.Tables[0].AsEnumerable()
                select new RapWrittenVerses
                {
                    UserId = r.Field<int>("UserID"),
                    VerseId = r.Field<int>("VersesWrittenID"),
                    Content = r.Field<string>("Verse"),
                    Title = r.Field<string>("Title")
                }).ToList();
        }

        #endregion
    }
}