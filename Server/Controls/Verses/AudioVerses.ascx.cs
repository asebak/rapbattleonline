#region Using

using System;
using System.Linq;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Verses
{
    public partial class AudioVerses : RapUserControl
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

        /// <summary>
        ///     Gets or sets the audio verses count.
        /// </summary>
        /// <value>
        ///     The audio verses count.
        /// </value>
        [NotNull]
        public int AudioVersesCount { get;private set; }

        #endregion

        #region Members

        [NotNull] private RapAudioVerses _verse;

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this._verse = this.GetCore<RapAudioVerses>();
            this._verse.UserId = this.UserId;
            this.AudioVersesPager.PerPage = 6;
            this.AudioVersesPager.GridView = this.AudioVersesGV;
            var usersVerses = this._verse.GetVerses(this.UserId);
            this.AudioVersesPager.ListDs = usersVerses.Cast<object>().ToList();
            this.AudioVersesCount = usersVerses.Count;
            if (this.AudioVersesCount <= 0)
            {
                var noVerses = this.GetCore<CalloutBox>()
                    .Create(BootstrapElementType.Info, this.Text("VERSES", "NO_AUDIO"));
                this.NoAudioVerses.Controls.Add(noVerses);
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Command event of the DeleteVerse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
        protected void DeleteVerse_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var verseId = Convert.ToInt32(e.CommandArgument.ToString());
            this._verse.DeleteVerse(verseId);
            this.AddLoadMessageSession(this.Text("VERSES", "DELETE_SUCCESS"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        #endregion
    }
}