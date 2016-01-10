#region Using

using System;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using YAF.Types;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Controls.Verses
{
    public partial class AddWrittenVerse : RapUserControl
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
            this.WrittenVerseBtn.OnClientClick += "return handlepostback({0});".FormatWith(this.WrittenVerseBtn.ClientID);
            base.OnInit(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Visible = (this.PageContext.PageUserID == UserId);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Click event of the WrittenVerse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void WrittenVerse_Click([NotNull] object sender, EventArgs eventArgs)
        {
            if (this.VerseTitle.Value.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.Text("VERSES", "NO_TITLE"));
                return;
            }
            if (this.VerseContent.Value.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.Text("VERSES", "NO_CONTENT"));
                return;
            }
            var v = new RapWrittenVerses(this.PageContext.PageUserID);
            v.AddVerse(this.VerseTitle.Value, this.VerseContent.Value);
            this.AddLoadMessageSession(this.Text("VERSES", "SUCCESS"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        #endregion
    }
}