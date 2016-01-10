#region Using

using System;
using System.Diagnostics.Contracts;
using Common.Types.Enums;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types.Helpers;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Controls.Admin
{
    public partial class TournamentManager : RapUserControl
    {
        #region Methods
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.CreateTournament.OnClientClick +=
                "return handlepostback({0});".FormatWith(this.CreateTournament.ClientID);
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

        /// <summary>
        ///     Handles the Click event of the CreateTournament control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void CreateTournament_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.ContestantsField.Text.IsNotSet() || this.BattleTypeField.Text.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.Text("ADMIN", "TOURNAMENT_INVALID"));
                return;
            }
            var contestants = Convert.ToInt32(this.ContestantsField.Text);
            var rounds = this.GetCore<RapTournamentHelpers>().CalculateTotalRounds(contestants);
            RapBattleType battleType;
            var battleTypeText = this.BattleTypeField.Text;
            if (battleTypeText == this.Text("BATTLES", "AUDIO"))
            {
                battleType = RapBattleType.Audio;

            }
            else if (battleTypeText == this.Text("BATTLES", "WRITTEN"))
            {
                battleType = RapBattleType.Written;
            }
            else
            {
                battleType = RapBattleType.Video;
            }
            this.GetCore<RapTournament>().CreateTournament(contestants, rounds, battleType);
            this.PageContext.AddLoadMessage(this.Text("ADMIN", "TOURNAMENT_ADDED"), MessageTypes.Success);
        }

        #endregion
    }
}