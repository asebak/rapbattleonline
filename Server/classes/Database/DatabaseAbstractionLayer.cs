#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using Common.Types.Enums;
using FreestyleOnline.classes.Types;
using YAF.Classes.Data;
using YAF.Types;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;
using YAF.Types.Interfaces.Data;

#endregion

namespace FreestyleOnline.classes.Database
{
    /// <summary>
    ///     Database Abstract Layer
    /// </summary>
    public static class Db
    {
        #region Database Provider Access

        /// <summary>
        ///     Gets the database access.
        /// </summary>
        /// <value>
        ///     The database access.
        /// </value>
        public static IDbAccess DbAccess
        {
            get { return ServiceLocatorAccess.CurrentServiceProvider.Get<IDbAccess>(); }
        }

        #endregion

        #region Music

        /// <summary>
        ///     Selects All Music in the rap_Music table
        /// </summary>
        /// <returns></returns>
        public static DataSet get_music()
        {
            using (var cmd = DbHelpers.GetCommand("get_music"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }


        /// <summary>
        ///     Selects all music for a specific user from rap_Music
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="pageUserId">The page user identifier.</param>
        /// <returns></returns>
        public static DataSet get_music([NotNull] int userId, [CanBeNull] int pageUserId)
        {
            using (var cmd = DbHelpers.GetCommand("get_music"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Gets all Music Tracks based on a similar title.
        /// </summary>
        /// <param name="title">The Music title.</param>
        /// <returns></returns>
        public static DataSet get_music_fromtitle([NotNull] string title)
        {
            using (var cmd = DbHelpers.GetCommand("get_music_fromtitle"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("Title", title);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Gets the users music.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public static DataTable get_users_music([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_users_music"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetData(cmd);
            }
        }


        /// <summary>
        ///     Gets the report messages from rap_MusicReports
        /// </summary>
        /// <returns></returns>
        public static DataTable get_all_musicreports()
        {
            using (var cmd = DbHelpers.GetCommand("get_all_musicreports"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetData(cmd);
            }
        }

        /// <summary>
        ///     Select all rap music tracks that are featured.
        /// </summary>
        /// <returns></returns>
        public static DataSet get_featured_music()
        {
            using (var cmd = DbHelpers.GetCommand("get_featured_music"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Inserts the track into the database table.
        /// </summary>
        /// <param name="fileNameMp3">The file name For the Song.</param>
        /// <param name="fileNamePng">The file name For the Picture.</param>
        /// <param name="title">The title of the song</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="canDownload"></param>
        public static void add_music([NotNull] string fileNameMp3, [CanBeNull] string fileNamePng,
            [NotNull] string title, [NotNull] int userId, [NotNull] bool canDownload)
        {
            int musicId;
            using (var cmd = DbHelpers.GetCommand("add_music"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("ID", userId);
                cmd.AddParam("Title", title);
                cmd.AddParam("Link", fileNameMp3);
                cmd.AddParam("Picture", fileNamePng);
                cmd.AddParam("Rating", 0);
                cmd.AddParam("CanDownload", canDownload ? 1 : 0);
                cmd.AddParam("DateAdded", DateTime.UtcNow);
                var paramOutput = new SqlParameter("MusicID", SqlDbType.Int) {Direction = ParameterDirection.Output};
                cmd.Parameters.Add(paramOutput);
                DbAccess.ExecuteNonQuery(cmd);
                musicId = Convert.ToInt32(paramOutput.Value);
            }
            add_musicrating(musicId, userId, 0);
        }


        /// <summary>
        ///     Deletes the music track.
        /// </summary>
        /// <param name="id">The music identifier.</param>
        public static void delete_music([NotNull] int id)
        {
            using (var cmd = DbHelpers.GetCommand("delete_music"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("MusicID", id);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        ///     Selects the picture and link urls from rap music table.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        /// <returns></returns>
        public static DataSet get_music_file([NotNull] int musicId)
        {
            using (var cmd = DbHelpers.GetCommand("get_music_file"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("MusicID", musicId);
                return DbAccess.GetDataset(cmd);
            }
        }


        /// <summary>
        ///     Adds a music track to the music featured table.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        /// <param name="untilDate">The until date. it expires.</param>
        public static void feature_music([NotNull] int musicId, [NotNull] DateTime untilDate)
        {
            using (var cmd = DbHelpers.GetCommand("feature_music"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("MusicID", musicId);
                cmd.AddParam("FeaturedUntil", untilDate);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Removes the featured track from the music featured table.
        /// </summary>
        /// <param name="musicId">The music identifier.</param>
        public static void delete_featuredmusic([NotNull] int musicId)
        {
            using (var cmd = DbHelpers.GetCommand("delete_featuredmusic"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("MusicID", musicId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Inserts a Report about a music track.
        /// </summary>
        /// <param name="id">The music identifier.</param>
        /// <param name="pageContextId">The page context user identifier.</param>
        /// <param name="content">The content.</param>
        public static void report_musictrack([NotNull] int id, [NotNull] int pageContextId,
            [CanBeNull] string content)
        {
            using (var cmd = DbHelpers.GetCommand("report_musictrack"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", pageContextId);
                cmd.AddParam("MusicID", id);
                cmd.AddParam("Content", content);
                cmd.AddParam("Confirmed", 0);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        ///     Deletes the music report message.
        /// </summary>
        /// <param name="id">The music identifier.</param>
        /// <param name="pageContextId">The page context user identifier.</param>
        public static void delete_musictrack_report([NotNull] int id, [NotNull] int pageContextId)
        {
            using (var cmd = DbHelpers.GetCommand("delete_musictrack_report"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", pageContextId);
                cmd.AddParam("MusicID", id);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Checks to see if the user can vote on a track.
        /// </summary>
        /// <param name="id">The music identifier.</param>
        /// <param name="pageContextId">The page context user identifier.</param>
        /// <returns></returns>
        public static object musictrack_ratingenabled([NotNull] int id, [NotNull] int pageContextId)
        {
            using (var cmd = DbHelpers.GetCommand("musictrack_ratingenabled"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", pageContextId);
                cmd.AddParam("MusicID", id);
                return DbAccess.ExecuteScalar(cmd);
            }
        }

        /// <summary>
        ///     Gets the rating for a music track.
        /// </summary>
        /// <param name="id">The music identifier.</param>
        /// <returns></returns>
        public static DataSet musictrack_ratingvalue([NotNull] int id)
        {
            using (var cmd = DbHelpers.GetCommand("musictrack_ratingvalue"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("MusicID", id);
                return DbAccess.GetDataset(cmd);
            }
        }


        /// <summary>
        ///     Sets the rating on a music track.
        /// </summary>
        /// <param name="id">The Music identifier.</param>
        /// <param name="pageContextId">The page context user identifier.</param>
        /// <param name="rating">The rating value.</param>
        public static void add_musicrating([NotNull] int id, [NotNull] int pageContextId,
            [CanBeNull] int rating)
        {
            using (var cmd = DbHelpers.GetCommand("add_musicrating"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", pageContextId);
                cmd.AddParam("MusicID", id);
                cmd.AddParam("RatingEnabled", 0);
                cmd.AddParam("RatingValue", rating);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region Contact

        /// <summary>
        ///     Get_all_contactmsgses this instance.
        /// </summary>
        /// <returns></returns>
        public static DataTable get_all_contactmsgs()
        {
            using (var cmd = DbHelpers.GetCommand("get_all_contactmsgs"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetData(cmd);
            }
        }


        /// <summary>
        ///     Submits the contact form information to be stored in contact table.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="titleHeader">The title of the form.</param>
        /// <param name="messageContent">Content of the the form.</param>
        public static void submit_contactmsg([NotNull] int userId, [CanBeNull] string titleHeader,
            [CanBeNull] string messageContent)
        {
            using (var cmd = DbHelpers.GetCommand("submit_contactmsg"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("Title", titleHeader);
                cmd.AddParam("Content", messageContent);
                cmd.AddParam("UserID", userId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        ///     deletes the contact form from the contact table.
        /// </summary>
        /// <param name="contactId">The contact identifier.</param>
        public static void delete_contactmsg([NotNull] int contactId)
        {
            using (var cmd = DbHelpers.GetCommand("delete_contactmsg"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("ContactID", contactId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region News

        /// <summary>
        ///     SQLs the select all rap news.
        /// </summary>
        /// <returns></returns>
        public static DataTable get_newsfeed()
        {
            using (var cmd = DbHelpers.GetCommand("get_newsfeed"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetData(cmd);
            }
        }

        /// <summary>
        ///     Posts the news into rap_NewsFeed
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="title">The title of news</param>
        /// <param name="content">The content of news</param>
        public static void post_newsfeed([NotNull] int userId, [CanBeNull] string title, [CanBeNull] string content)
        {
            using (var cmd = DbHelpers.GetCommand("post_newsfeed"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("Title", title);
                cmd.AddParam("DatePosted", DateTime.UtcNow);
                cmd.AddParam("Information", content);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        ///     Gets the news comments for a specific news id.
        /// </summary>
        /// <param name="newsId">The news identifier.</param>
        /// <returns></returns>
        public static DataTable get_newsfeed_comments([NotNull] int newsId)
        {
            using (var cmd = DbHelpers.GetCommand("get_newsfeed_comments"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("NewsID", newsId);
                return DbAccess.GetData(cmd);
            }
        }


        /// <summary>
        ///     Deletes the news based on the id.
        /// </summary>
        /// <param name="newsId">The news identifier.</param>
        public static void delete_news([NotNull] int newsId)
        {
            using (var cmd = DbHelpers.GetCommand("delete_news"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("NewsFeedID", newsId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Posts a comment in the news feed.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="newsId">The news identifier.</param>
        /// <param name="message">The message.</param>
        public static void post_newscomment([NotNull] int userId, [NotNull] int newsId,
            [CanBeNull] string message)
        {
            using (var cmd = DbHelpers.GetCommand("post_newscomment"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("NewsFeedID", newsId);
                cmd.AddParam("UserID", userId);
                cmd.AddParam("Comment", message);
                cmd.AddParam("DatePosted", DateTime.UtcNow);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region Membership

        /// <summary>
        ///     Selects all the rap profiles that are featured
        /// </summary>
        /// <returns></returns>
        public static DataSet get_featured_profiles()
        {
            using (var cmd = DbHelpers.GetCommand("get_featured_profile"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }


        /// <summary>
        ///     Gets random profiles from the user table.
        /// </summary>
        /// <returns></returns>
        public static DataSet get_randomprofiles()
        {
            using (var cmd = DbHelpers.GetCommand("get_randomprofiles"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }


        /// <summary>
        ///     Features a user for a given time period.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="untilDate">The until date is the date of expire.</param>
        public static void feature_profile([NotNull] int userId, [NotNull] DateTime untilDate)
        {
            using (var cmd = DbHelpers.GetCommand("feature_profile"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("FeaturedUntil", untilDate);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Removes the featured user from the featured profile table.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public static void delete_featuredprofile([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("delete_featuredprofile"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }


        /// <summary>
        ///     Gets the id from the display name. If Value of 1 is returned it corresponds to a Guest.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns></returns>
        public static DataSet userid_from_name([CanBeNull] string displayName)
        {
            using (var cmd = DbHelpers.GetCommand("userid_from_name"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("DisplayName", displayName);
                return DbAccess.GetDataset(cmd);
            }
        }

        #endregion

        #region Profile

        /// <summary>
        ///     Inserts a comment into the user comments table
        /// </summary>
        /// <param name="userId">The user identifier. of the page</param>
        /// <param name="pageUserId">The page user identifier.</param>
        /// <param name="comment">The comment.</param>
        public static void post_profilecomment([NotNull] int userId, [NotNull] int pageUserId,
            [NotNull] string comment)
        {
            using (var cmd = DbHelpers.GetCommand("post_profilecomment"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("CommenterID", pageUserId);
                cmd.AddParam("Comment", comment);
                cmd.AddParam("DatePosted", DateTime.UtcNow);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Selects the userId's profile comments
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static DataSet get_profilecomments([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_profilecomments"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select all rap users comments SQL ds.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static DataTable get_user_profilecomments([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_user_profilecomments"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetData(cmd);
            }
        }


        /// <summary>
        ///     SQLs the select all rap user p messages. Copied from YAF's implementation
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Int32 get_pm_count([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_pm_count"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return (int) DbAccess.ExecuteScalar(cmd);
            }
        }


        /// <summary>
        ///     SQLs the insert specific site profile header.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="file">The file.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public static void update_profileheader([NotNull] int userId, [NotNull] string file)
        {
            //might need to be optmized 
            using (var cmd = DbHelpers.GetCommand("update_profileheader"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("HeaderImage", file);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        public static void update_profilebio([NotNull] int userId, [NotNull] string bio)
        {
            //might need to be optmized 
            using (var cmd = DbHelpers.GetCommand("update_profilebio"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("Bio", bio);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select specific site profile header.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static DataSet get_user_siteprofile([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_user_siteprofile"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetDataset(cmd);
            }
        }

        #endregion

        #region Hoods

        /// <summary>
        ///     Insers a hood into the rap hood table.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="details">The details.</param>
        /// <param name="pic">The pic.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="privacy">if set to <c>true</c> [privacy].</param>
        public static void add_hood([NotNull] string name, [CanBeNull] string details,
            [CanBeNull] string pic, [NotNull] int userId, [NotNull] bool privacy)
        {
            int hoodId;
            using (var cmd = DbHelpers.GetCommand("add_hood"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("Name", name);
                cmd.AddParam("Picture", pic);
                cmd.AddParam("Details", details);
                cmd.AddParam("IsPublic", privacy ? 1 : 0);
                cmd.AddParam("DateCreated", DateTime.UtcNow);
                var paramOutput = new SqlParameter("HoodID", SqlDbType.Int) {Direction = ParameterDirection.Output};
                cmd.Parameters.Add(paramOutput);
                DbAccess.ExecuteNonQuery(cmd);
                hoodId = Convert.ToInt32(paramOutput.Value);
            }
            join_hood(userId, hoodId, true);
        }

        /// <summary>
        ///     Selects the name of a specific rap hood.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>If null then the hood can be added with the name specified</returns>
        public static object get_hoodname([NotNull] string name)
        {
            using (var cmd = DbHelpers.GetCommand("get_hoodname"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("Name", name);
                return DbAccess.ExecuteScalar(cmd);
            }
        }

        /// <summary>
        ///     Selects all Hood Details based on a hood id.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <returns></returns>
        public static DataSet get_hoodmembers([NotNull] int hoodId)
        {
            using (var cmd = DbHelpers.GetCommand("get_hoodmembers"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("HoodID", hoodId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        /// Get_hoodmembers_inviteds the specified hood identifier.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <returns></returns>
        public static DataSet get_hoodmembers_invited([NotNull] int hoodId)
        {
            using (var cmd = DbHelpers.GetCommand("get_hoodmembers_invited"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("HoodID", hoodId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Selects all hoods.
        /// </summary>
        /// <returns></returns>
        public static DataSet get_allhoods()
        {
            using (var cmd = DbHelpers.GetCommand("get_allhoods"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Selects random hoods
        /// </summary>
        /// <returns></returns>
        public static DataSet get_randomhoods()
        {
            using (var cmd = DbHelpers.GetCommand("get_randomhoods"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Selects all the hood ids based on a user id
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>hood id</returns>
        public static DataTable get_usershoods([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_usershoods"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetData(cmd);
            }
        }


        /// <summary>
        ///     Inserts a user into the rap hood table.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="hoodId">The hood identifier.</param>
        /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
        public static void join_hood([NotNull] int userId, [NotNull] int hoodId, [NotNull] bool isAdmin = false)
        {
            using (var cmd = DbHelpers.GetCommand("join_hood"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("HoodID", hoodId);
                cmd.AddParam("IsAdmin", isAdmin ? 1 : 0);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// Invite_hood_users the specified hood identifier.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public static void invite_hood_user([NotNull] int hoodId, [NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("invite_hood_user"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("HoodID", hoodId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Deletes the user from the rap hood table.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="hoodId">The hood identifier.</param>
        public static void remove_hoodmember([NotNull] int userId, [NotNull] int hoodId)
        {
            using (var cmd = DbHelpers.GetCommand("remove_hoodmember"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("HoodID", hoodId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Selects the first userid from a rap hood
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <returns></returns>
        public static object get_first_hoodmember([NotNull] int hoodId)
        {
            using (var cmd = DbHelpers.GetCommand("get_first_hoodmember"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("HoodID", hoodId);
                return DbAccess.ExecuteScalar(cmd);
            }
        }

        /// <summary>
        ///     Deletes the entire rap hood.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        public static void remove_hood([NotNull] int hoodId)
        {
            using (var cmd = DbHelpers.GetCommand("remove_hood"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("HoodID", hoodId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Updates a userId to admin in the hood users table.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="hoodId">The hood identifier.</param>
        public static void hoodmember_toadmin([NotNull] int userId, [NotNull] int hoodId)
        {
            using (var cmd = DbHelpers.GetCommand("hoodmember_toadmin"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("HoodID", hoodId);
                cmd.AddParam("IsAdmin", 1);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Updates specific hood details.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <param name="privacy">if set to <c>true</c> [privacy].</param>
        /// <param name="description">The description.</param>
        public static void update_hooddetails([NotNull] int hoodId, bool privacy, string description)
        {
            using (var cmd = DbHelpers.GetCommand("update_hooddetails"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("HoodID", hoodId);
                cmd.AddParam("IsPublic", privacy ? 1 : 0);
                cmd.AddParam("Description", description);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Selects hood comments based on the hood id.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <returns></returns>
        public static DataTable get_hood_comments([NotNull] int hoodId)
        {
            using (var cmd = DbHelpers.GetCommand("get_hood_comments"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("HoodID", hoodId);
                return DbAccess.GetData(cmd);
            }
        }

        /// <summary>
        ///     Inserts a comment into a hood based on their Id.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="contentHtml">The content HTML.</param>
        public static void post_hoodcomment([NotNull] int hoodId, [NotNull] int userId,
            [CanBeNull] string contentHtml)
        {
            using (var cmd = DbHelpers.GetCommand("post_hoodcomment"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("HoodID", hoodId);
                cmd.AddParam("UserID", userId);
                cmd.AddParam("Comment", contentHtml);
                cmd.AddParam("DatePosted", DateTime.UtcNow);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the delete all rap hood comments.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        public static void delete_hoodcomments([NotNull] int hoodId)
        {
            using (var cmd = DbHelpers.GetCommand("delete_hoodcomments"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("HoodID", hoodId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region WrittenBattles

        /// <summary>
        ///     Creates a rap written battle.
        /// </summary>
        /// <param name="userId1">The user id1.</param>
        /// <param name="userId2">The user id2.</param>
        /// <param name="isPublic">if set to <c>true</c> [is public].</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="numberOfBars">The number of bars.</param>
        public static Int32 add_writtenbattle([NotNull] int userId1, [CanBeNull] int? userId2,
            [NotNull] bool isPublic, [NotNull] DateTime endDate, [NotNull] int numberOfBars)
        {
            using (var cmd = DbHelpers.GetCommand("add_writtenbattle"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID1", userId1);
                cmd.AddParam("UserID2", userId2 ?? (object) DBNull.Value);
                cmd.AddParam("IsPublic", isPublic ? 1 : 0);
                cmd.AddParam("EndDate", endDate);
                cmd.AddParam("NumberOfBars", numberOfBars);
                var paramOutput = new SqlParameter("WrittenBattleID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramOutput);
                DbAccess.ExecuteNonQuery(cmd);
                return Convert.ToInt32(paramOutput.Value);
            }
        }

        /// <summary>
        ///     Select specific rap written battle based on battle Id.
        /// </summary>
        /// <param name="battleId">The battle identifier.</param>
        /// <returns></returns>
        public static DataSet get_writtenbattle([NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("get_writtenbattle"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("WrittenBattleID", battleId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Selects a specific users rap battles
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static DataSet get_users_writtenbattle([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_users_writtenbattles"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Selects most recent battles from the list
        /// </summary>
        /// <returns></returns>
        public static DataSet get_recent_writtenbattles()
        {
            using (var cmd = DbHelpers.GetCommand("get_recent_writtenbattles"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select all rap written battle.
        /// </summary>
        /// <returns></returns>
        public static DataSet get_all_writtenbattles()
        {
            using (var cmd = DbHelpers.GetCommand("get_all_writtenbattles"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Allows a user to join a  written rap battle
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="battleId"></param>
        public static void join_writtenbattle([NotNull] int userId, [NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("join_writtenbattle"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("BattleID", battleId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Update specific winner rap written battle.
        /// </summary>
        /// <param name="battleId">The Battle Id</param>
        /// <param name="winnerUserId">The winner user identifier.</param>
        /// <param name="user1Overall">The user1 overall.</param>
        /// <param name="user2Overall">The user2 overall.</param>
        public static void update_writtenbattle_winner([NotNull] int battleId, [CanBeNull] int? winnerUserId,
            [NotNull] float user1Overall,
            [NotNull] float user2Overall)
        {
            using (var cmd = DbHelpers.GetCommand("update_writtenbattle_winner"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("BattleID", battleId);
                cmd.AddParam("WinnerID", winnerUserId ?? (object) DBNull.Value);
                cmd.AddParam("User1Overall", user1Overall);
                cmd.AddParam("User2Overall", user2Overall);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Update User 1 Verse
        /// </summary>
        /// <param name="verse">The verse.</param>
        /// <param name="battleId">The battle identifier.</param>
        public static void update_writtenbattle_user1verse([CanBeNull] string verse, [NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("update_writtenbattle_user1verse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("BattleID", battleId);
                cmd.AddParam("Verse", verse);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Update User 2 Verse
        /// </summary>
        /// <param name="verse">The verse.</param>
        /// <param name="battleId">The battle identifier.</param>
        public static void update_writtenbattle_user2verse([CanBeNull] string verse, [NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("update_writtenbattle_user2verse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("BattleID", battleId);
                cmd.AddParam("Verse", verse);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Inserts A rating a written rap battle
        /// </summary>
        /// <param name="writtenBattleId">The written battle identifier.</param>
        /// <param name="pagerUserId">The pager user identifier.</param>
        /// <param name="user1Wordplay">The user1 wordplay.</param>
        /// <param name="user2Wordplay">The user2 wordplay.</param>
        /// <param name="user1Metaphores">The user1 metaphores.</param>
        /// <param name="user2Metaphores">The user2 metaphores.</param>
        /// <param name="user1Flow">The user1 flow.</param>
        /// <param name="user2Flow">The user2 flow.</param>
        /// <param name="user1Multis">The user1 multis.</param>
        /// <param name="user2Multis">The user2 multis.</param>
        /// <param name="user1Punchlines">The user1 punchlines.</param>
        /// <param name="user2Punchlines">The user2 punchlines.</param>
        public static void add_writtenbattle_rating([NotNull] int writtenBattleId,
            [NotNull] int pagerUserId, [CanBeNull] int user1Wordplay,
            [CanBeNull] int user2Wordplay, [CanBeNull] int user1Metaphores, [CanBeNull] int user2Metaphores,
            [CanBeNull] int user1Flow, [CanBeNull] int user2Flow, [CanBeNull] int user1Multis,
            [CanBeNull] int user2Multis, [CanBeNull] int user1Punchlines, [CanBeNull] int user2Punchlines)
        {
            using (var cmd = DbHelpers.GetCommand("add_writtenbattle_rating"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", pagerUserId);
                cmd.AddParam("WrittenBattleID", writtenBattleId);
                cmd.AddParam("RatingEnabled", 0);
                cmd.AddParam("User1Wordplay", user1Wordplay);
                cmd.AddParam("User1Metaphores", user1Metaphores);
                cmd.AddParam("User1Flow", user1Flow);
                cmd.AddParam("User1Multis", user1Multis);
                cmd.AddParam("User1Punchlines", user1Punchlines);
                cmd.AddParam("User2Wordplay", user2Wordplay);
                cmd.AddParam("User2Metaphores", user2Metaphores);
                cmd.AddParam("User2Flow", user2Flow);
                cmd.AddParam("User2Multis", user2Multis);
                cmd.AddParam("User2Punchlines", user2Punchlines);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Selects specific rating enabled rap written battle rating.
        /// </summary>
        /// <param name="battleId">The Battle Id</param>
        /// <param name="pageContextId">The page context identifier.</param>
        /// <returns></returns>
        public static object get_writtenbattle_ratingenabled([NotNull] int battleId,
            [NotNull] int pageContextId)
        {
            using (var cmd = DbHelpers.GetCommand("get_writtenbattle_ratingenabled"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", pageContextId);
                cmd.AddParam("ID", battleId);
                return DbAccess.ExecuteScalar(cmd);
            }
        }

        /// <summary>
        ///     Selects all rating rap written battle rating.
        /// </summary>
        /// <param name="battleId">The battle identifier.</param>
        /// <returns></returns>
        public static DataSet get_writtenbattle_votes([NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("get_writtenbattle_votes"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("ID", battleId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Selects all the ratings for a specific written battle
        /// </summary>
        /// <param name="battleId">The battle identifier.</param>
        /// <returns></returns>
        public static DataTable get_writtenbattle_ratings([NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("get_writtenbattle_ratings"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("WrittenBattleID", battleId);
                return DbAccess.GetData(cmd);
            }
        }


        /// <summary>
        ///     SQLs the select all rap written battle rating for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static DataSet get_writtenbattle_votesstatistics([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_writtenbattle_votesstatistics"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Inserts specific rap written battle comment.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="writtenBattleId">The written battle identifier.</param>
        /// <param name="message">The message.</param>
        public static void post_writtenbattlecomment([NotNull] int userId, [NotNull] int writtenBattleId,
            [CanBeNull] string message)
        {
            using (var cmd = DbHelpers.GetCommand("post_writtenbattlecomment"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("WrittenBattleID", writtenBattleId);
                cmd.AddParam("Comment", message);
                cmd.AddParam("DatePosted", DateTime.UtcNow);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Gets the Written Battle comments.
        /// </summary>
        /// <returns></returns>
        public static DataTable get_writtenbattle_comments([NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("get_writtenbattle_comments"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("WrittenBattleID", battleId);
                return DbAccess.GetData(cmd);
            }
        }

        #endregion

        #region AudioBattles

        /// <summary>
        ///     Inserts a Audio Battle Comement.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="audioBattleId"></param>
        /// <param name="message">The message.</param>
        public static void post_audiobattlecomment([NotNull] int userId, [NotNull] int audioBattleId,
            [CanBeNull] string message)
        {
            using (var cmd = DbHelpers.GetCommand("post_audiobattlecomment"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("AudioBattleID", audioBattleId);
                cmd.AddParam("Comment", message);
                cmd.AddParam("DatePosted", DateTime.UtcNow);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Selects a specific audio rap battle comments.
        /// </summary>
        /// <param name="battleId">The battle identifier.</param>
        /// <returns></returns>
        public static DataTable get_audiobattle_comments([NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("get_audiobattle_comments"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("AudioBattleID", battleId);
                return DbAccess.GetData(cmd);
            }
        }

        /// <summary>
        ///     Selects all ratings for a specific audio battle
        /// </summary>
        /// <param name="battleId">The battle identifier.</param>
        /// <returns></returns>
        public static DataTable get_audiobattle_ratings([NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("get_audiobattle_ratings"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("AudioBattleID", battleId);
                return DbAccess.GetData(cmd);
            }
        }


        /// <summary>
        ///     SQLs the select all rap audio battle rating ds.
        /// </summary>
        /// <param name="battleId">The battle identifier.</param>
        /// <returns></returns>
        public static DataSet get_audiobattle_votes([NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("get_audiobattle_votes"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("AudioBattleID", battleId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select all rap audio battle rating for user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static DataSet get_audiobattle_votesstatistics([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_audiobattle_votesstatistics"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Inserts an Audio Rap Battle
        /// </summary>
        /// <param name="userId1">The user id1.</param>
        /// <param name="userId2">The user id2.</param>
        /// <param name="isPublic">if set to <c>true</c> [is public].</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="audioLength">Length of the audio.</param>
        /// <returns></returns>
        public static Int32 add_audiobattle([NotNull] int userId1, [CanBeNull] int? userId2,
            [NotNull] bool isPublic, [NotNull] DateTime endDate, [NotNull] int audioLength)
        {
            using (var cmd = DbHelpers.GetCommand("add_audiobattle"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID1", userId1);
                cmd.AddParam("UserID2", userId2 ?? (object) DBNull.Value);
                cmd.AddParam("IsPublic", isPublic ? 1 : 0);
                cmd.AddParam("EndDate", endDate);
                cmd.AddParam("RecordingLength", audioLength);
                var paramOutput = new SqlParameter("AudioBattleID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramOutput);
                DbAccess.ExecuteNonQuery(cmd);
                return Convert.ToInt32(paramOutput.Value);
            }
        }

        /// <summary>
        ///     Selects all specific users rap battle audios.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static DataSet get_users_audiobattles([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_users_audiobattles"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     Selects a specific audio battle details.
        /// </summary>
        /// <param name="battleId">The battle identifier.</param>
        /// <returns></returns>
        public static DataSet get_audiobattle([NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("get_audiobattle"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("ID", battleId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     SQLs the update specific user join rap audio battle.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="battleId">The battle identifier.</param>
        public static void join_audiobattle([NotNull] int userId, [NotNull] int battleId)
        {
            using (var cmd = DbHelpers.GetCommand("join_audiobattle"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("BattleID", battleId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the update specific user recording rap audio battle.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="beatUsed">The beat used.</param>
        /// <param name="battleId">The battle identifier.</param>
        /// <param name="fileLocation">The file location.</param>
        public static void update_audiobattle_recording([NotNull] int userId, [CanBeNull] int beatUsed,
            [NotNull] int battleId,
            [CanBeNull] string fileLocation)
        {
            //do both commands, each command checks if the userid correponds to the page user id, and will return null if it doesnt match
            using (var cmd = DbHelpers.GetCommand("update_audiobattle_recording1"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("FileLocation", fileLocation);
                cmd.AddParam("Beat", beatUsed);
                cmd.AddParam("BattleID", battleId);
                cmd.AddParam("UserID", userId);
                DbAccess.ExecuteNonQuery(cmd);
            }

            using (var cmd = DbHelpers.GetCommand("update_audiobattle_recording2"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("FileLocation", fileLocation);
                cmd.AddParam("Beat", beatUsed);
                cmd.AddParam("BattleID", battleId);
                cmd.AddParam("UserID", userId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the update specific winner rap audio battle.
        /// </summary>
        /// <param name="battleId">The battle identifier.</param>
        /// <param name="winnerUserId">The winner user identifier.</param>
        /// <param name="user1Overall">The user1 overall.</param>
        /// <param name="user2Overall">The user2 overall.</param>
        public static void update_audiobattle_winner([NotNull] int battleId, [CanBeNull] int? winnerUserId,
            [NotNull] float user1Overall,
            [NotNull] float user2Overall)
        {
            using (var cmd = DbHelpers.GetCommand("update_audiobattle_winner"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("BattleID", battleId);
                cmd.AddParam("WinnerID", winnerUserId ?? (object) DBNull.Value);
                cmd.AddParam("User1Overall", user1Overall);
                cmd.AddParam("User2Overall", user2Overall);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the insert specific rap audio battle rating.
        /// </summary>
        /// <param name="audioBattleId">The audio battle identifier.</param>
        /// <param name="pagerUserId">The pager user identifier.</param>
        /// <param name="user1Wordplay">The user1 wordplay.</param>
        /// <param name="user2Wordplay">The user2 wordplay.</param>
        /// <param name="user1Metaphores">The user1 metaphores.</param>
        /// <param name="user2Metaphores">The user2 metaphores.</param>
        /// <param name="user1Flow">The user1 flow.</param>
        /// <param name="user2Flow">The user2 flow.</param>
        /// <param name="user1Multis">The user1 multis.</param>
        /// <param name="user2Multis">The user2 multis.</param>
        /// <param name="user1Punchlines">The user1 punchlines.</param>
        /// <param name="user2Punchlines">The user2 punchlines.</param>
        public static void add_audiobattle_rating([NotNull] int audioBattleId,
            [NotNull] int pagerUserId, [CanBeNull] int user1Wordplay,
            [CanBeNull] int user2Wordplay, [CanBeNull] int user1Metaphores, [CanBeNull] int user2Metaphores,
            [CanBeNull] int user1Flow, [CanBeNull] int user2Flow, [CanBeNull] int user1Multis,
            [CanBeNull] int user2Multis, [CanBeNull] int user1Punchlines, [CanBeNull] int user2Punchlines)
        {
            using (var cmd = DbHelpers.GetCommand("add_audiobattle_rating"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", pagerUserId);
                cmd.AddParam("AudioBattleID", audioBattleId);
                cmd.AddParam("RatingEnabled", 0);
                cmd.AddParam("User1Wordplay", user1Wordplay);
                cmd.AddParam("User1Metaphores", user1Metaphores);
                cmd.AddParam("User1Flow", user1Flow);
                cmd.AddParam("User1Multis", user1Multis);
                cmd.AddParam("User1Punchlines", user1Punchlines);
                cmd.AddParam("User2Wordplay", user2Wordplay);
                cmd.AddParam("User2Metaphores", user2Metaphores);
                cmd.AddParam("User2Flow", user2Flow);
                cmd.AddParam("User2Multis", user2Multis);
                cmd.AddParam("User2Punchlines", user2Punchlines);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select specific rating rap audio battle rating.
        /// </summary>
        /// <param name="battleId">The battle identifier.</param>
        /// <param name="pageContextId">The page context identifier.</param>
        /// <returns></returns>
        public static object get_audiobattle_ratingenabled([NotNull] int battleId,
            [NotNull] int pageContextId)
        {
            using (var cmd = DbHelpers.GetCommand("get_audiobattle_ratingenabled"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", pageContextId);
                cmd.AddParam("ID", battleId);
                return DbAccess.ExecuteScalar(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select all specific recent rap audio battle.
        /// </summary>
        /// <returns></returns>
        public static DataSet get_recent_audiobattles()
        {
            using (var cmd = DbHelpers.GetCommand("get_recent_audiobattles"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select all rap audio battle.
        /// </summary>
        /// <returns></returns>
        public static DataSet get_audiobattles()
        {
            using (var cmd = DbHelpers.GetCommand("get_audiobattles"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }

        #endregion

        #region Tournaments

        /// <summary>
        ///     SQLs the insert specific rap tournament.
        /// </summary>
        /// <param name="contestants">The contestants.</param>
        /// <param name="totalRounds">The total rounds.</param>
        /// <param name="date">The date.</param>
        /// <param name="tournamentStatus">The tournament status.</param>
        /// <param name="tournamentType">Type of the tournament.</param>
        public static void create_tournament([NotNull] int contestants, [NotNull] int totalRounds,
            [NotNull] DateTime date,
            [NotNull] int tournamentStatus, [NotNull] int tournamentType)
        {
            using (var cmd = DbHelpers.GetCommand("create_tournament"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("Challengers", contestants);
                cmd.AddParam("State", tournamentStatus);
                cmd.AddParam("Type", tournamentType);
                cmd.AddParam("Started", DateTime.UtcNow);
                cmd.AddParam("Rounds", totalRounds);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select specific rap tournament.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        public static DataSet get_tournament([NotNull] int tournamentId)
        {
            using (var cmd = DbHelpers.GetCommand("get_tournament"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("TournamentID", tournamentId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select specific rap tournament.
        /// </summary>
        public static DataSet get_all_tournaments()
        {
            using (var cmd = DbHelpers.GetCommand("get_all_tournaments"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     SQLs the insert specific rap tournament user.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static int add_tournament_user([NotNull] int tournamentId, [NotNull] int userId)
        {
            int entryNumber;
            using (var cmd = DbHelpers.GetCommand("get_last_tournamententry_number"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("TournamentID", tournamentId);
                entryNumber = (int) (DbAccess.ExecuteScalar(cmd) ?? 0);
            }
            entryNumber++;
            using (var cmd = DbHelpers.GetCommand("add_tournament_user"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("TournamentID", tournamentId);
                cmd.AddParam("EntryNumber", entryNumber);
                cmd.AddParam("UserID", userId);
                DbAccess.ExecuteNonQuery(cmd);
            }
            return entryNumber;
        }

        /// <summary>
        ///     SQLs the update specific rap tournament.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="tournamentState">State of the tournament.</param>
        public static void update_tournamentstate([NotNull] int tournamentId, [CanBeNull] int tournamentState)
        {
            using (var cmd = DbHelpers.GetCommand("update_tournamentstate"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("TournamentID", tournamentId);
                cmd.AddParam("TournamentState", tournamentState);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select all specific rap tournament users.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <returns></returns>
        public static DataSet get_tournament_users([NotNull] int tournamentId)
        {
            using (var cmd = DbHelpers.GetCommand("get_tournament_users"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("TournamentID", tournamentId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     SQLs the insert all preliminary rap tournament match.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="matchId">The match identifier.</param>
        /// <param name="matchRound">The match round.</param>
        /// <param name="battleType">Type of the battle.</param>
        /// <param name="userId1">The user id1.</param>
        /// <param name="userId2">The user id2.</param>
        public static void add_tournament_match([NotNull] int tournamentId, [NotNull] int matchId,
            [NotNull] int matchRound, [NotNull] RapBattleType battleType, [NotNull] int userId1, [NotNull] int userId2)
        {
            var battleId = 0;
            var tStored = "";
            switch (battleType)
            {
                case RapBattleType.Written:
                    battleId = add_writtenbattle(userId1, userId2, false, DateTime.Now.AddDays(14), 16);
                    tStored = "add_tournament_writtenmatch";
                    break;
                case RapBattleType.Audio:
                    battleId = add_audiobattle(userId1, userId2, false, DateTime.Now.AddDays(14), 60);
                    tStored = "add_tournament_audiomatch";
                    break;
                case RapBattleType.Video:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("battleType");
            }

            using (var cmd = DbHelpers.GetCommand(tStored))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("TournamentID", tournamentId);
                cmd.AddParam("MatchRound", matchRound);
                cmd.AddParam("MatchPosition", matchId);
                cmd.AddParam("BattleID", battleId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select all rap tournament matches.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="battleType">Type of the battle.</param>
        /// <returns></returns>
        public static DataSet get_tournamentmatches([NotNull] int tournamentId, [NotNull] RapBattleType battleType)
        {
            var tStored = "";
            switch (battleType)
            {
                case RapBattleType.Written:
                    tStored = "get_written_tournamentmatches";
                    break;
                case RapBattleType.Audio:
                    tStored = "get_audio_tournamentmatches";
                    break;
                case RapBattleType.Video:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("battleType");
            }
            using (var cmd = DbHelpers.GetCommand(tStored))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("TournamentID", tournamentId);
                return DbAccess.GetDataset(cmd);
            }
        }

        #endregion

        #region Verses

        /// <summary>
        ///     SQLs the insert specific verses audio.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="audioPath">The audio path.</param>
        public static void upload_audioverse([NotNull] int userId, [NotNull] string title,
            [NotNull] string audioPath)
        {
            using (var cmd = DbHelpers.GetCommand("upload_audioverse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("Title", title);
                cmd.AddParam("AudioPath", audioPath);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the insert specific verses written.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="writtenContent">Content of the written.</param>
        public static void upload_writtenverse([NotNull] int userId, [NotNull] string title,
            [NotNull] string writtenContent)
        {
            using (var cmd = DbHelpers.GetCommand("upload_writtenverse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("Title", title);
                cmd.AddParam("Verse", writtenContent);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// Update_writtenverses the specified verse identifier.
        /// </summary>
        /// <param name="verseId">The verse identifier.</param>
        /// <param name="content">The content.</param>
        /// <param name="userId">The user identifier.</param>
        public static void update_writtenverse([NotNull] int verseId,[NotNull] string content, [NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("update_writtenverse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                cmd.AddParam("VersesWrittenID", verseId);
                cmd.AddParam("Verse", content);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the delete specific verses written.
        /// </summary>
        /// <param name="verseId">The verse identifier.</param>
        public static void delete_writtenverse([NotNull] int verseId)
        {
            using (var cmd = DbHelpers.GetCommand("delete_writtenverse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("VersesWrittenID", verseId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the delete specific verses audio.
        /// </summary>
        /// <param name="verseId">The verse identifier.</param>
        public static void delete_audioverse([NotNull] int verseId)
        {
            using (var cmd = DbHelpers.GetCommand("delete_audioverse"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("VersesAudioID", verseId);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select all verses audio.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static DataSet get_user_audioverses([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_user_audioverses"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select all verses written.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static DataSet get_user_writtenverses([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_user_writtenverses"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetDataset(cmd);
            }
        }

        /// <summary>
        ///     SQLs the select specific verses audio.
        /// </summary>
        /// <param name="verseId">The verse identifier.</param>
        /// <returns></returns>
        public static DataSet get_audioverse_details([NotNull] int verseId)
        {
            using (var cmd = DbHelpers.GetCommand("get_audioverse_details"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("VersesAudioID", verseId);
                return DbAccess.GetDataset(cmd);
            }
        }

        #endregion

        #region Feed

        /// <summary>
        ///     Delete_feeditems the specified feed identifier.
        /// </summary>
        /// <param name="feedId">The feed identifier.</param>
        public static void delete_feeditem([NotNull] int feedId)
        {
            using (var cmd = DbHelpers.GetCommand("delete_feeditem"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("FeedID", feedId);
                cmd.AddParam("IsDeleted", 1);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Add_feeditems the specified to user identifier.
        /// </summary>
        /// <param name="toUserId">To user identifier.</param>
        /// <param name="fromUserId">From user identifier.</param>
        /// <param name="type">The type.</param>
        /// <param name="objectId">The object identifier.</param>
        public static void add_feeditem([NotNull] int toUserId, [NotNull] int fromUserId,
            [NotNull] RapSocialFeedType type, [NotNull] int objectId)
        {
            using (var cmd = DbHelpers.GetCommand("add_feeditem"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("ToID", toUserId);
                cmd.AddParam("FromID", fromUserId);
                cmd.AddParam("TypeID", type);
                cmd.AddParam("ObjectID", objectId);
                cmd.AddParam("Created", DateTime.UtcNow);
                cmd.AddParam("IsDeleted", 0);
                DbAccess.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        ///     Get_feeds the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static DataSet get_feed([NotNull] int userId)
        {
            using (var cmd = DbHelpers.GetCommand("get_feed"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.AddParam("UserID", userId);
                return DbAccess.GetDataset(cmd);
            }
        }

        #endregion
    }
}