#region Using

using System;
using System.Linq;
using Ext.Net;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;

#endregion

namespace FreestyleOnline.Pages
{
    public partial class Tournament : RapPage
    {
        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var allTournaments = this.GetCore<RapTournament>().GetAllTournaments();
            this.TournamentsGV.DataSource = allTournaments;
            this.TournamentsGV.DataBind();
            if (!allTournaments.Any())
            {
                var noTournaments = this.GetCore<CalloutBox>()
                    .Create(BootstrapElementType.Info, this.Text("TOURNAMENTS", "NONE"));
                this.TournamentsPH.Controls.Add(noTournaments);
            }
        }

        #endregion
    }
}