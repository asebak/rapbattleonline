#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Models;
using Common.Types.Enums;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Classes;
using YAF.Core;
using YAF.Core.Services;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class UserData : RapClass
    {
        #region Members

        /// <summary>
        ///     The _music list
        /// </summary>
        private readonly MusicData _musicList = new MusicData();

        /// <summary>
        ///     The page context user identifier
        /// </summary>
        protected int PageContextUserId;

        /// <summary>
        ///     The user identifier
        /// </summary>
        protected int UserId;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserData" /> class.
        /// </summary>
        public UserData()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserData" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public UserData(int userId)
        {
            UserId = userId;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserData" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="pageContextUserId">The page context user identifier.</param>
        public UserData(int userId, int pageContextUserId)
        {
            UserId = userId;
            PageContextUserId = pageContextUserId;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The users the music list.
        /// </summary>
        /// <returns></returns>
        public List<MusicTrack> UsersMusicList()
        {
            var musicDs = Db.get_music(UserId, PageContextUserId);
            var musicTracks = (from r in musicDs.Tables[0].AsEnumerable()
                select new MusicTrack
                {
                    MusicId = r.Field<int>("MusicID"),
                    UserId = r.Field<int>("UserID"),
                    SongName = r.Field<string>("Title"),
                    LinkLocation =
                        this.GetService<ResourceProvider>().GetPath(RapResource.MusicTracks) + r.Field<string>("Link"),
                    PictureLocation =
                        this.GetService<ResourceProvider>().GetPath(RapResource.MusicTracksPictures) +
                        r.Field<string>("Picture"),
                    Ranking = _musicList.GetRankingOfTrack(r.Field<int>("MusicID")),
                    TotalVotes = _musicList.GetTotalVotesForMusicTrack(r.Field<int>("MusicID")),
                    Rating = _musicList.GetRatingForMusicTrack(r.Field<int>("MusicID")),
                    RatingEnabled = _musicList.TrackRatingEnabled(UserId, PageContextUserId, r.Field<int>("MusicID")),
                    CanDownload = r.Field<bool>("CanDownload"),
                    DateAdded = r.Field<DateTime>("DateAdded")
                }).ToList();
            return musicTracks;
        }

        /// <summary>
        ///     Userses the music list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="pageUserId">The page user identifier.</param>
        /// <returns></returns>
        public List<MusicTrack> UsersMusicList(int userId, int pageUserId)
        {
            var musicDs = Db.get_music(userId, pageUserId);
            var musicTracks = (from r in musicDs.Tables[0].AsEnumerable()
                select new MusicTrack
                {
                    MusicId = r.Field<int>("MusicID"),
                    UserId = r.Field<int>("UserID"),
                    SongName = r.Field<string>("Title"),
                    LinkLocation =
                        this.GetService<ResourceProvider>().GetPath(RapResource.MusicTracks) + r.Field<string>("Link"),
                    PictureLocation =
                        this.GetService<ResourceProvider>().GetPath(RapResource.MusicTracksPictures) +
                        r.Field<string>("Picture"),
                    Ranking = _musicList.GetRankingOfTrack(r.Field<int>("MusicID")),
                    TotalVotes = _musicList.GetTotalVotesForMusicTrack(r.Field<int>("MusicID")),
                    Rating = _musicList.GetRatingForMusicTrack(r.Field<int>("MusicID")),
                    RatingEnabled = _musicList.TrackRatingEnabled(userId, pageUserId, r.Field<int>("MusicID"))
                }).ToList();
            return musicTracks;
        }

        /// <summary>
        ///     Gets the display name of the user identifier from.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns></returns>
        public static int GetUserIdFromDisplayName(string displayName)
        {
            try
            {
                var userIdDs = Db.userid_from_name(displayName);
                return Convert.ToInt32(userIdDs.Tables[0].Rows[0]["UserID"]);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///     Gets the profile context.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static ProfileModel GetProfileContext(int userId)
        {
            var userData = new CombinedUserDataHelper(userId).Profile;
            return new ProfileModel
            {
                UserId = userId,
                UserName = UserMembershipHelper.GetDisplayNameFromID(userId),
                Avatar = new YafAvatars(YafContext.Current.BoardSettings).GetAvatarUrlForUser(userId),
                AIM = userData.AIM,
                City = userData.City,
                Country = userData.Country,
                Facebook = userData.Facebook,
                FacebookId = userData.FacebookId,
                Homepage = userData.Homepage,
                MSN = userData.MSN,
                Twitter = userData.Twitter,
                TwitterId = userData.TwitterId,
                Interests = userData.Interests,
                Skype = userData.Skype,
                Occupation = userData.Occupation,
                UnreadMessages = Message.GetTotalUnreadInboxMessages(userId)
            };
        }

        /// <summary>
        ///     Gets the users music data source.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public DataTable GetUsersMusicDataSource(int userId)
        {
            return Db.get_users_music(userId);
        }

        /// <summary>
        ///     Gets the users hoods.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<HoodData> GetUsersHoods(int userId)
        {
            //TODO: Maybe find a more efficient way to query rather than make 2 db calls
            var hoodIds =
                (IEnumerable<int>) Db.get_usershoods(userId).AsEnumerable().Select(h => h.Field<int>("HoodID"));
            return
                hoodIds.Select(Db.get_hoodmembers)
                    .Select(dataDs => HoodData.ConstructHoodObjectList(dataDs).First())
                    .ToList();
        }

        /// <summary>
        ///     Gets the users written battles.
        /// </summary>
        /// <returns></returns>
        public List<RapBattleWritten> GetUsersWrittenBattles()
        {
            var usersWrittenBattlesDs = Db.get_users_writtenbattle(UserId);
            return new RapBattleWritten().ConstructRapBattleObject(usersWrittenBattlesDs,
                RapBattleType.Written).Cast<RapBattleWritten>().ToList();
        }

        /// <summary>
        ///     Gets the users audio battles.
        /// </summary>
        /// <returns></returns>
        public List<RapBattleAudio> GetUsersAudioBattles()
        {
            var usersAudioBattlesDs = Db.get_users_audiobattles(UserId);
            return new RapBattleAudio().ConstructRapBattleObject(usersAudioBattlesDs,
                RapBattleType.Audio).Cast<RapBattleAudio>().ToList();
        }

        /// <summary>
        ///     Gets the users profile comments.
        /// </summary>
        /// <param name="boardSettings">The board settings.</param>
        /// <returns></returns>
        public List<UserComments> GetUsersProfileComments(YafBoardSettings boardSettings)
        {
            var commentsDs = Db.get_profilecomments(UserId);
            var avatar = new YafAvatars(boardSettings);

            var userComments =
                (from r in commentsDs.Tables[0].AsEnumerable()
                    select new UserComments
                    {
                        DisplayName = UserMembershipHelper.GetDisplayNameFromID(r.Field<int>("CommenterID")),
                        HyperLink =
                            this.GetService<UrlProvider>().GetUrl("/Pages/Profile/{0}", (r.Field<int>("CommenterID"))),
                        Avatar = avatar.GetAvatarUrlForUser(r.Field<int>("CommenterID")),
                        Comment = r.Field<string>("Comment"),
                        DatePosted = r.Field<DateTime>("DatePosted").ToString("MM/dd/yyyy")
                    }).ToList();

            return userComments;
        }

        /// <summary>
        ///     Gets the users profile comments.
        /// </summary>
        /// <returns></returns>
        public DataTable GetUsersProfileComments()
        {
            return Db.get_user_profilecomments(UserId);
        }

        #endregion
    }
}