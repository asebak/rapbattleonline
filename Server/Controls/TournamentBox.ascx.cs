#region Using

using System;
using Common.Types.Enums;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Types;
using YAF.Types;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class TournamentBox : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int TournamentId { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        public RapTournamentStatus Status { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public RapBattleType Type { get; set; }

        /// <summary>
        /// Gets or sets the total challengers.
        /// </summary>
        /// <value>
        /// The total challengers.
        /// </value>
        public int TotalChallengers { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// </exception>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.ChallengersLbl.Text = this.Text("TOURNAMENTS", "CONTESTANTS_2").FormatWith(this.TotalChallengers);
            switch (this.Status)
            {
                case RapTournamentStatus.NotStarted:
                    this.tournamentheader.InnerHtml = this.Text("TOURNAMENTS", "NOTSTARTED");
                    break;
                case RapTournamentStatus.InProgress:
                    this.tournamentheader.InnerHtml = this.Text("TOURNAMENTS", "INPROGRESS");
                    break;
                case RapTournamentStatus.Over:
                    this.tournamentheader.InnerHtml = this.Text("TOURNAMENTS", "OVER");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            switch (this.Type)
            {
                case RapBattleType.Written:
                    this.TournamentType.Text = this.Text("TOURNAMENTS", "WRITTEN");
                    this.TournamentType.Icon = Icon.Pencil;
                    break;
                case RapBattleType.Audio:
                    this.TournamentType.Text = this.Text("TOURNAMENTS", "AUDIO");
                    this.TournamentType.Icon = Icon.Music;
                    break;
                case RapBattleType.Video:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

    }
}