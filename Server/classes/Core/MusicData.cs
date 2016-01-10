#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Web;
using Common.Types;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Core;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class MusicData : RapClass
    {
        #region Properties

        /// <summary>
        ///     The rating values
        /// </summary>
        public readonly List<Rating> RatingValues = new List<Rating>();

        /// <summary>
        ///     The music track data
        /// </summary>
        public MusicTrack MusicTrackData = new MusicTrack();

        #endregion

        #region Methods

        /// <summary>
        ///     Constructs the music data object.
        /// </summary>
        /// <returns></returns>
        /// <summary>
        ///     Organizes the tracks In a Logical View.
        /// </summary>
        /// <returns></returns>
        public List<MusicData> OrganizeTracks()
        {
            var listMusicData = new List<MusicData>();
            var rating = new Rating();
            var listOfSongsDs = Db.get_music();

            foreach (DataRow r in listOfSongsDs.Tables[0].Rows)
            {
                var musicData = new MusicTrack
                {
                    MusicId = Convert.ToInt32(r["MusicID"]),
                    UserId = Convert.ToInt32(r["UserID"]),
                    SongName = r["Title"].ToString(),
                    PictureLocation = r["Picture"].ToString(),
                    LinkLocation = r["Link"].ToString(),
                    CanDownload = Convert.ToBoolean(r["CanDownload"]),
                    DateAdded = Convert.ToDateTime(r["DateAdded"]),
                    Rating = 0,
                    TotalVotes = 0,
                };

                if (!r.IsNull("RatingValue"))
                {
                    rating.Value = Convert.ToInt32(r["RatingValue"]);
                }

                var found = listMusicData.Any(x => x.MusicTrackData.MusicId == musicData.MusicId);

                if (found) //if else statement to remove rating of 0 that is added in db
                {
                    listMusicData.Find(x => x.MusicTrackData.MusicId == musicData.MusicId).RatingValues.Add(rating);
                }
                else
                {
                    listMusicData.Add(new MusicData {MusicTrackData = musicData});
                }
            }


            return listMusicData;
        }

        /// <summary>
        ///     Gets The Ranking of the Track
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        /// <returns></returns>
        public int GetRankingOfTrack(int musicId)
        {
            Contract.Ensures(Contract.Result<int>() != 0);
            if (this.GetTotalVotesForMusicTrack(musicId) == 0)
            {
                return 0;
            }
            var musicList = OrganizeTracks();
            foreach (var ml in musicList.Where(ml => ml.RatingValues.Count != 0))
            {
                ml.MusicTrackData.TotalVotes = ml.RatingValues.Count;
                ml.MusicTrackData.Rating = ml.RatingValues.Average(x => x.Value);
            }

            musicList =
                musicList.OrderByDescending(x => x.MusicTrackData.Rating)
                    .ThenByDescending(x => x.MusicTrackData.TotalVotes)
                    .ToList();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return
                musicList.Where(ml => ml.MusicTrackData.Rating != 0)
                    .TakeWhile(ml => musicId != ml.MusicTrackData.MusicId)
                    .Count() + 1;
        }

        /// <summary>
        ///     Gets all the Top Tracks, No Limits
        /// </summary>
        /// <returns></returns>
        public List<MusicTrack> GetTopTracks()
        {
            var musicTracks = new List<MusicTrack>();
            var ranking = 1;
            var musicList = OrganizeTracks();
            foreach (var ml in musicList.Where(ml => ml.RatingValues.Count != 0))
            {
                ml.MusicTrackData.TotalVotes = ml.RatingValues.Count;
                ml.MusicTrackData.Rating = ml.RatingValues.Average(x => x.Value);
            }

            musicList =
                musicList.OrderByDescending(x => x.MusicTrackData.Rating)
                    .ThenByDescending(x => x.MusicTrackData.TotalVotes)
                    .ToList();

            foreach (var ml in musicList)
            {
                var mt = new MusicTrack();

                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (ml.MusicTrackData.Rating != 0)
                {
                    mt.MusicId = ml.MusicTrackData.MusicId;
                    mt.UserId = ml.MusicTrackData.UserId;
                    mt.TotalVotes = ml.MusicTrackData.TotalVotes;
                    mt.SongName = ml.MusicTrackData.SongName;
                    mt.Rating = ml.MusicTrackData.Rating;
                    mt.LinkLocation = ml.MusicTrackData.LinkLocation;
                    mt.PictureLocation = ml.MusicTrackData.PictureLocation;
                    mt.DateAdded = ml.MusicTrackData.DateAdded;
                    mt.CanDownload = ml.MusicTrackData.CanDownload;
                    mt.Ranking = ranking;
                    musicTracks.Add(mt);
                    ranking++;
                }
            }

            return musicTracks;
        }


        /// <summary>
        ///     If a user is allowed to vote
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="pageUserId">The page user identifier.</param>
        /// <param name="musicId">The music identifier.</param>
        /// <returns></returns>
        public bool TrackRatingEnabled(int userId, int pageUserId, int musicId)
        {
            if ((userId == pageUserId) || !HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var query = Db.musictrack_ratingenabled(musicId, pageUserId);
            return query == null;
        }

        /// <summary>
        ///     Sets the rating on music track.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        /// <param name="pageContextUserId">The page context user identifier.</param>
        /// <param name="ratingValue">The rating value.</param>
        public void SetRatingOnMusicTrack(int musicId, int pageContextUserId, int ratingValue)
        {
            Contract.Requires(pageContextUserId > 1);
            var query = Db.musictrack_ratingenabled(musicId, pageContextUserId);
            if (query == null)
            {
                Db.add_musicrating(musicId, pageContextUserId, ratingValue);
            }
        }

        /// <summary>
        ///     Sets the featured track.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        /// <param name="dateTime">The date time.</param>
        public void SetFeaturedTrack(int musicId, DateTime dateTime)
        {
            Db.feature_music(musicId, dateTime);
        }

        /// <summary>
        ///     Gets the featured tracks.
        /// </summary>
        /// <param name="profileContextId">The profile context identifier.</param>
        /// <returns></returns>
        public List<MusicTrack> GetFeaturedTracks(int profileContextId)
        {
            var musicFeaturedDs = Db.get_featured_music();
            var musicFeatureList = (from r in musicFeaturedDs.Tables[0].AsEnumerable()
                .Where(r => RapGlobalHelpers.IsDateExpired(r.Field<DateTime>("FeaturedUntil")) == false)
                select new MusicTrack
                {
                    MusicId = r.Field<int>("MusicID"),
                    UserId = r.Field<int>("UserID"),
                    CanDownload = r.Field<bool>("CanDownload"),
                    DateAdded = r.Field<DateTime>("DateAdded"),
                    PictureLocation = r.Field<string>("Picture"),
                    LinkLocation = r.Field<string>("Link"),
                    SongName = r.Field<string>("Title"),
                    Rating = this.GetRatingForMusicTrack(r.Field<int>("MusicID")),
                    TotalVotes = this.GetTotalVotesForMusicTrack(r.Field<int>("MusicID"))
                }).ToList();
            return musicFeatureList;
        }

        /// <summary>
        ///     Deletes the music track.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        public void DeleteMusicTrack(int musicId)
        {
            var toDelete = GetPictureAndSongFromId(musicId);
            var musicTracksPath = this.GetService<ResourceProvider>().GetPath(RapResource.MusicTracks);
            var musicPicsPath = this.GetService<ResourceProvider>().GetPath(RapResource.MusicTracksPictures);
            foreach (var s in toDelete)
            {
                if (File.Exists(musicTracksPath + s))
                {
                    File.Delete(musicTracksPath + s);
                }
                if (File.Exists(musicPicsPath + s))
                {
                    File.Delete(musicPicsPath + s);
                }
            }
            Db.delete_music(musicId);
        }

        /// <summary>
        ///     Removes the featured track.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        [Obsolete]
        private void RemoveFeaturedTrack(int musicId)
        {
            Db.delete_featuredmusic(musicId);
        }

        /// <summary>
        ///     Inserts the track into the database table abstraction layer.
        /// </summary>
        /// <param name="fileNameMp3">The file name MP3.</param>
        /// <param name="fileNamePng">The file name PNG.</param>
        /// <param name="title">The title.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="canDownload"></param>
        public void InsertTrack(string fileNameMp3, string fileNamePng, string title, int userId, bool canDownload)
        {
            Contract.Requires(!string.IsNullOrEmpty(fileNameMp3));
            Contract.Requires(!string.IsNullOrEmpty(fileNamePng));
            Contract.Requires(!string.IsNullOrEmpty(title));
            Db.add_music(fileNameMp3, fileNamePng, title, userId, canDownload);
        }

        /// <summary>
        ///     Gets the total votes for music track.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        /// <returns></returns>
        public int GetTotalVotesForMusicTrack(int musicId)
        {
            var totalVotes = Db.musictrack_ratingvalue(musicId);
            return (totalVotes.Tables[0].Rows.Count - 1);
        }

        /// <summary>
        ///     Gets the rating for music track.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        /// <returns></returns>
        public double GetRatingForMusicTrack(int musicId)
        {
            var musicRatingsDs = Db.musictrack_ratingvalue(musicId);
            var musicRatings = musicRatingsDs.Tables[0].AsEnumerable()
                .Select(r => r.Field<int>("RatingValue"))
                .ToList();
            musicRatings.Remove(0);
            return musicRatings.Count == 0 ? 0 : musicRatings.Average();
        }

        /// <summary>
        ///     Gets the picture and song from identifier.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        /// <returns></returns>
        private static IEnumerable<string> GetPictureAndSongFromId(int musicId)
        {
            Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
            var picturesAndSongsDs = Db.get_music_file(musicId);
            var musicPictures = picturesAndSongsDs.Tables[0].AsEnumerable()
                .Select(r => r.Field<string>("Picture"))
                .ToList();
            var musicLinks = picturesAndSongsDs.Tables[0].AsEnumerable()
                .Select(r => r.Field<string>("Link"))
                .ToList();
            return musicPictures.Concat(musicLinks).ToList();
        }

        /// <summary>
        ///     Gets the music track details from identifier.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        /// <returns></returns>
        public static MusicTrack GetMusicTrackDetailsFromId(int musicId)
        {
            Contract.Ensures(Contract.Result<MusicTrack>() != null);
            var songDs = Db.get_music_file(musicId);
            var musicTrack = (from r in songDs.Tables[0].AsEnumerable()
                select new MusicTrack
                {
                    UserId = r.Field<int>("UserID"),
                    SongName = r.Field<string>("Title"),
                    LinkLocation = r.Field<string>("Link"),
                    CanDownload = r.Field<bool>("CanDownload")
                });
            return musicTrack.Any() ? musicTrack.First() : null;
        }

        /// <summary>
        ///     Gets the music from title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public DataSet GetMusicFromTitle(string title)
        {
            Contract.Requires(!string.IsNullOrEmpty(title));
            return Db.get_music_fromtitle(title);
        }

        #endregion
    }
}