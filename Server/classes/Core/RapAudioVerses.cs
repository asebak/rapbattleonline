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
    public sealed class RapAudioVerses : BaseRapVerses, IRapVerse<List<RapAudioVerses>>
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapAudioVerses" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public RapAudioVerses(int userId)
        {
            this.UserId = userId;
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="RapAudioVerses" /> class from being created.
        /// </summary>
        public RapAudioVerses()
        {
        }

        #endregion

        #region IRapVerse<List<RapAudioVerses>> Members

        /// <summary>
        ///     Deletes the verse.
        /// </summary>
        /// <param name="verseId">The verse identifier.</param>
        public void DeleteVerse(int verseId)
        {
            //TODO Delete File From Server
            Db.delete_audioverse(verseId);
        }

        /// <summary>
        ///     Adds the verse.
        /// </summary>
        /// <param name="verseTitle">The verse title.</param>
        /// <param name="versePath">The verse path.</param>
        public void AddVerse(string verseTitle, string versePath)
        {
            Db.upload_audioverse(this.UserId, verseTitle, versePath);
        }

        /// <summary>
        ///     Gets the verses.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<RapAudioVerses> GetVerses(int userId)
        {
            var usersVerses = Db.get_user_audioverses(userId);
            return (from r in usersVerses.Tables[0].AsEnumerable()
                select new RapAudioVerses
                {
                    UserId = r.Field<int>("UserID"),
                    VerseId = r.Field<int>("VersesAudioID"),
                    Content = r.Field<string>("AudioPath"),
                    Title = r.Field<string>("Title")
                }).ToList();
        }

        #endregion

        /// <summary>
        ///     Gets the audio verse path.
        /// </summary>
        /// <param name="verseId">The verse identifier.</param>
        /// <returns></returns>
        public string GetAudioVersePath(int verseId)
        {
            var usersVerses = Db.get_audioverse_details(verseId);
            return usersVerses.Tables[0].AsEnumerable()
                .Select(r => r.Field<string>("AudioPath")).First();
        }
    }
}