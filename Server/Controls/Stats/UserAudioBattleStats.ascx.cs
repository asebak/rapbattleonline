#region Using

using System;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Stats
{
    public partial class UserAudioBattleStats : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int UserId { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var s = new AudioBattleStatistics(this.UserId);
            var aBs = s.GetStats();
            if (Math.Abs(aBs.Flow) < 0.1 && Math.Abs(aBs.Metaphores) < 0.1 && Math.Abs(aBs.Multis) < 0.1 &&
                Math.Abs(aBs.PunchLines) < 0.1 && Math.Abs(aBs.Wordplay) < 0.1)
            {
                this.Visible = false;
                return;
            }
            BattleStatsChartAspNet.CreateStatsProgressBar(aBs.Wordplay, this.wordplay);
            BattleStatsChartAspNet.CreateStatsProgressBar(aBs.Flow, this.flow);
            BattleStatsChartAspNet.CreateStatsProgressBar(aBs.Multis, this.multis);
            BattleStatsChartAspNet.CreateStatsProgressBar(aBs.PunchLines, this.punchlines);
            BattleStatsChartAspNet.CreateStatsProgressBar(aBs.Metaphores, this.metaphores);
            this.wordplay.InnerText = this.Text("BATTLES", "BATTLE_WORDPLAY");
            this.metaphores.InnerText = this.Text("BATTLES", "BATTLE_METAPHORES");
            this.multis.InnerText = this.Text("BATTLES", "BATTLE_MULTIS");
            this.punchlines.InnerText = this.Text("BATTLES", "BATTLE_PUNCHLINES");
            this.flow.InnerText = this.Text("BATTLES", "BATTLE_FLOW");
        }

        #endregion
    }
}