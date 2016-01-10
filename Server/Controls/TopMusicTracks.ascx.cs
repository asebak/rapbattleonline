#region Using

using System;
using System.Linq;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     Creates a table for the Top Music Tracks
    /// </summary>
    public partial class TopMusicTracks : RapUserControl
    {
        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.TopMusicTracksPager.PerPage = 15;
            this.TopMusicTracksPager.GridView = this.TopTracksGrid;
            var topTracks = this.GetCore<MusicData>().GetTopTracks().Cast<object>().ToList();
            this.TopMusicTracksPager.ListDs = topTracks;
            if (topTracks.Count <= 0)
            {
                var noTopTracks = this.GetCore<CalloutBox>()
                    .Create(BootstrapElementType.Info, this.Text("MUSIC", "NO_TOP"));
                this.NoTopMusic.Controls.Add(noTopTracks);
            }
        }

        #endregion
    }
}