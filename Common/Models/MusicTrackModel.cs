#region Using

using System;

#endregion

namespace Common.Models
{
    public class MusicTrackModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the song.
        /// </summary>
        /// <value>
        ///     The name of the song.
        /// </value>
        public string SongName { get; set; }

        /// <summary>
        ///     Gets or sets the link location.
        /// </summary>
        /// <value>
        ///     The link location.
        /// </value>
        public string LinkLocation { get; set; }

        /// <summary>
        ///     Gets or sets the picture location.
        /// </summary>
        /// <value>
        ///     The picture location.
        /// </value>
        public string PictureLocation { get; set; }

        /// <summary>
        ///     Gets or sets the total votes.
        /// </summary>
        /// <value>
        ///     The total votes.
        /// </value>
        public int TotalVotes { get; set; }

        /// <summary>
        ///     Gets or sets the rating.
        /// </summary>
        /// <value>
        ///     The rating.
        /// </value>
        public double Rating { get; set; }

        /// <summary>
        ///     Gets or sets the music identifier.
        /// </summary>
        /// <value>
        ///     The music identifier.
        /// </value>
        public int MusicId { get; set; }

        /// <summary>
        ///     Gets or sets the ranking.
        /// </summary>
        /// <value>
        ///     The ranking.
        /// </value>
        public int Ranking { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [rating enabled].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [rating enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool RatingEnabled { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [can download].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [can download]; otherwise, <c>false</c>.
        /// </value>
        public bool CanDownload { get; set; }

        /// <summary>
        ///     Gets or sets the date added.
        /// </summary>
        /// <value>
        ///     The date added.
        /// </value>
        public DateTime DateAdded { get; set; }

        #endregion
    }
}