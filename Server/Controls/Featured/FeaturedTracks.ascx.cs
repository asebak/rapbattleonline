#region Using

using System;
using System.Linq;
using System.Web.UI.WebControls;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Featured
{
    /// <summary>
    ///     Creates a Carousel of Featured Music Items
    /// </summary>
    public partial class FeaturedTracks : RapUserControl
    {
        #region Methods
        protected override void OnInit(EventArgs e)
        {
                        var Data = this.GetCore<MusicData>();
            var featuredMusicList = Data.GetFeaturedTracks(this.PageContext.PageUserID);
            featuredMusicList.Shuffle();
            if (featuredMusicList.Any())
            {
                this.MusicFeatured.MusicId = featuredMusicList[0].MusicId;
                this.MusicFeatured.UserId = featuredMusicList[0].UserId;
                this.MusicFeatured.Title = featuredMusicList[0].SongName;
                this.MusicFeatured.ImageUrl = featuredMusicList[0].PictureLocation;
                this.MusicFeatured.Date = featuredMusicList[0].DateAdded;
                this.MusicFeatured.CanDownload = featuredMusicList[0].CanDownload;
                this.MusicFeatured.UserId = featuredMusicList[0].UserId;
                this.DataBind();
            }
            else
            {
                var noData = new Label
                {
                    Text = this.Text("DEFAULT", "NOFEATURED_MUSIC")
                };
                this.MusicFeatured.Visible = false;
                this.NoFeaturedMusic.Controls.Add(noData);
            }
            base.OnInit(e);
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {

        }

        #endregion
    }
}