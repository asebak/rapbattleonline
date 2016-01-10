using System;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Types;

namespace FreestyleOnline.Controls.Featured
{
    [Obsolete("No need for control")]
    public partial class NewTournaments : RapUserControl
    {
        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.TournamentsRepeater.DataSource = this.GetCore<RapTournament>().GetActiveTournaments();
            this.TournamentsRepeater.DataBind();
        }

        #endregion

    }
}