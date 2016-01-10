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
    public partial class WrittenVerses : RapUserControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [NotNull]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the written verses count.
        /// </summary>
        /// <value>
        /// The written verses count.
        /// </value>
        [NotNull]
        public int WrittenVersesCount { get; private set; }

        #endregion

        #region Methods
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.UserId == this.PageContext.PageUserID)
            {
                this.GetService<ClientProviders>()
                    .RegisterClientScriptBlock(this, "editableverses", "~/js/WrittenVerses.js", true);
            }
            this.WrittenVersesPager.PerPage = 6;
            this.WrittenVersesPager.GridView = this.WrittenVersesGV;
            var usersVerses = new RapWrittenVerses().GetVerses(this.UserId);
            this.WrittenVersesPager.ListDs = usersVerses.Cast<object>().ToList();
            this.WrittenVersesCount = usersVerses.Count;
            if (this.WrittenVersesCount <= 0)
            {
                var noVerses = this.GetCore<CalloutBox>()
                    .Create(BootstrapElementType.Info, this.Text("VERSES", "NO_WRITTEN"));
                this.NoWrittenVerses.Controls.Add(noVerses);
            }
        }

        #endregion

        #region Event Handler

        /// <summary>
        /// Handles the Command event of the DeleteVerseWritten control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        protected void DeleteVerseWritten_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var verse = new RapWrittenVerses(this.UserId);
            verse.DeleteVerse(Convert.ToInt32(e.CommandArgument.ToString()));
            this.AddLoadMessageSession(this.Text("VERSES", "DELETE_SUCCESS"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        #endregion
    }
}