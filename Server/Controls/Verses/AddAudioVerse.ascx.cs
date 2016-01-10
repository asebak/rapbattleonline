#region Using

using System;
using System.IO;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Controls.Verses
{
    public partial class AddAudioVerse : RapUserControl
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
        ///     Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.AudioVerseBtn.OnClientClick += "return handlepostback({0});".FormatWith(this.AudioVerseBtn.ClientID);
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
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Click event of the SubmitAudioVerse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void SubmitAudioVerse_Click([NotNull] object sender,[NotNull] EventArgs eventArgs)
        {
            if (this.VerseTitleAudio.Value.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.Text("VERSES", "NO_TITLE"));
                return;
            }
            if (this.VerseUploader.PostedFile.FileName.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.Text("VERSES", "NO_CONTENT"));
                return;
            }
            if (this.VerseUploader.PostedFile.ContentType.Contains("audio"))
            {
                if (this.VerseUploader.PostedFile.ContentLength < 3242880)
                {
                    var fileAudioVerse = Guid.NewGuid() + Path.GetExtension(this.VerseUploader.FileName);
                    var path = this.GetService<ResourceProvider>().GetPath(RapResource.AudioVerses, fileAudioVerse);
                    this.VerseUploader.PostedFile.SaveAs(path);
                    var a = new RapAudioVerses(this.PageContext.PageUserID);
                    a.AddVerse(this.VerseTitleAudio.Value, fileAudioVerse);
                    this.AddLoadMessageSession(this.Text("VERSES", "SUCCESS"));
                    this.GetService<UrlProvider>().RefreshPage();
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

        #endregion

    }
}