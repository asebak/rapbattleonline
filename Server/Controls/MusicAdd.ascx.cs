#region Using

using System;
using System.IO;
using System.Threading;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Utils.Helpers;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     Allows a User to Add Music
    /// </summary>
    public partial class MusicAdd : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        [NotNull]
        public int UserId { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.UploadMusic.OnClientClick += "return handlepostback({0})".FormatWith(this.UploadMusic.ClientID);
            base.OnInit(e);
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Visible = (this.PageContext.PageUserID == UserId);
            if (this.IsMobile)
            {
                this.Visible = false;
            }
        }

        /// <summary>
        ///     Inserts the Song into the rap_Music Table
        /// </summary>
        private void InsertSongThread([NotNull] string filenamemusic,[NotNull] string filenamepicture)
        {
            //TODO verifiy if it strips title textbox text
            this.GetCore<MusicData>()
                .InsertTrack(filenamemusic, filenamepicture, BBCodeHelper.StripBBCode(
                    HtmlHelper.StripHtml(HtmlHelper.CleanHtmlString(this.TitleBoxMusic.Text))).RemoveMultipleWhitespace(), this.UserId,
                    this.RadioGroupDownloadable.CheckedItems[0].InputValue == "1");
        }


        /// <summary>
        ///     Saves song to server
        /// </summary>
        private void SaveTrackToServerThread([NotNull] string file)
        {
            this.MusicFile.PostedFile.SaveAs(this.GetService<ResourceProvider>().GetPath(RapResource.MusicTracks,file));
        }

        /// <summary>
        ///     Saves picture to server
        /// </summary>
        private void SavePictureToServerThread([NotNull] string file)
        {
            this.PictureFile.PostedFile.SaveAs(
                this.GetService<ResourceProvider>().GetPath(RapResource.MusicTracksPictures, file));
        }
        #endregion

        #region Event Handler
        /// <summary>
        ///     onClick of the fileupload button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Upload_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.TitleBoxMusic.Text.IsNotSet())
            {
                if (this.MusicFile.HasFile && this.PictureFile.HasFile)
                {
                    if (this.MusicFile.PostedFile.ContentType.Contains("audio") &&
                        this.PictureFile.PostedFile.ContentType.Contains("image"))
                    {
                        if (this.MusicFile.PostedFile.ContentLength < 5242880)
                        {
                            var fileNameMusic = Guid.NewGuid() + Path.GetExtension(this.MusicFile.FileName);
                            var fileNamePicture = Guid.NewGuid() + Path.GetExtension(this.PictureFile.FileName);
                            this.InsertSongThread(fileNameMusic, fileNamePicture);
                            this.SaveTrackToServerThread(fileNameMusic);
                            this.SavePictureToServerThread(fileNamePicture);
                            this.AddLoadMessageSession(this.Text("MUSIC", "MUSIC_UPLOADED"));
                            this.GetService<UrlProvider>().RefreshPage();
                        }
                        else
                        {
                            this.PageContext.AddLoadMessage(this.Text("COMMON", "COMMON_MUSICEERROR")
                                .FormatWith(this.Text("MUSIC", "ERROR_SIZE").FormatWith("5")), MessageTypes.Error);
                        }
                    }
                    else
                    {
                        this.PageContext.AddLoadMessage(this.Text("COMMON", "COMMON_MUSICEERROR")
                            .FormatWith(this.Text("MUSIC", "ERROR_FORMAT").FormatWith("mp3")), MessageTypes.Error);
                    }
                }
                else
                {
                    this.PageContext.AddLoadMessage(this.Text("COMMON", "COMMON_MUSICEERROR")
                        .FormatWith(this.Text("MUSIC", "ERROR_MISSING")));
                }
            }
            else
            {
                this.PageContext.AddLoadMessage(this.Text("COMMON", "COMMON_MUSICEERROR")
                    .FormatWith(this.Text("MUSIC", "ERROR_MISSING")));
            }
        }
        #endregion
    }
}