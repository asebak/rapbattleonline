#region Using

using System;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Types;
using YAF.Types.Constants;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     Creates a Music Track Box
    /// </summary>
    public partial class MusicTrackBox : RapUserControl
    {
        #region Properties
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public object Date { get; set; }
        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        /// <value>
        ///     The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [rating enabled].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [rating enabled]; otherwise, <c>false</c>.
        /// </value>
        protected bool RatingEnabled
        {
            get
            {//TODO: Optimize in DB Call
                return this.GetCore<MusicData>()
                    .TrackRatingEnabled(this.UserId, this.PageContext.PageUserID, this.MusicId);
            }
        }

        /// <summary>
        ///     Gets or sets the total votes.
        /// </summary>
        /// <value>
        ///     The total votes.
        /// </value>
        protected int TotalVotes
        {//TODO: Optimize in DB Call
            get { return this.GetCore<MusicData>().GetTotalVotesForMusicTrack(this.MusicId); }
        }

        /// <summary>
        ///     Gets or sets the rating.
        /// </summary>
        /// <value>
        ///     The rating.
        /// </value>
        protected double Rating
        {//TODO: Optimize in DB Call
            get { return this.GetCore<MusicData>().GetRatingForMusicTrack(this.MusicId); }
        }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the music identifier.
        /// </summary>
        /// <value>
        ///     The music identifier.
        /// </value>
        public int MusicId { get; set; }

        /// <summary>
        ///     Gets or sets the image URL.
        /// </summary>
        /// <value>
        ///     The image URL.
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can download.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can download; otherwise, <c>false</c>.
        /// </value>
        public bool CanDownload { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "musicList", "~/js/MusicList.js", true);
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "downloadMusicFile", "~/js/Download.js", true);
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "musicListRating", "~/js/MusicRating.js", true);
            //this.MusicImage.ImageUrl = this.GetService<ResourceProvider>().GetClientPath(RapResource.MusicTracksPictures, this.ImageUrl);
            //this.DisplayDate.DateTime = this.Date;
        }

        #endregion
        /// <summary>
        ///     Handles the Command event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void DeleteButton_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var musicId = Convert.ToInt32(e.CommandArgument);
            this.GetCore<MusicData>().DeleteMusicTrack(musicId);
            this.AddLoadMessageSession(this.Text("MUSIC", "DELETE"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        /// <summary>
        ///     Handles the Command event of the ReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void ReportButton_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var musicId = Convert.ToInt32(e.CommandArgument.ToString());
            this.GetCore<ReportMessage>()
                .SubmitReportMessage(musicId, this.PageContext.PageUserID, this.Text("MUSIC", "MUSIC_REPORT"));
            this.PageContext.AddLoadMessage(this.Text("MUSIC", "REPORT"), MessageTypes.Success);
        }
    }
}