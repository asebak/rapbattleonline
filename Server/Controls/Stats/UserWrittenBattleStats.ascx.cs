#region Using

using System;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Stats
{
    public partial class UserWrittenBattleStats : RapUserControl
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
            var s = new WrittenBattleStatistics(this.UserId);
            var wBs = s.GetStats();
            if (Math.Abs(wBs.Flow) < 0.1 && Math.Abs(wBs.Metaphores) < 0.1 && Math.Abs(wBs.Multis) < 0.1 &&
                Math.Abs(wBs.PunchLines) < 0.1 && Math.Abs(wBs.Wordplay) < 0.1)
            {
                this.Visible = false;
                return;
            }
            BattleStatsChartAspNet.CreateStatsProgressBar(wBs.Wordplay, this.wordplay);
            BattleStatsChartAspNet.CreateStatsProgressBar(wBs.Flow, this.flow);
            BattleStatsChartAspNet.CreateStatsProgressBar(wBs.Multis, this.multis);
            BattleStatsChartAspNet.CreateStatsProgressBar(wBs.PunchLines, this.punchlines);
            BattleStatsChartAspNet.CreateStatsProgressBar(wBs.Metaphores, this.metaphores);
            this.wordplay.InnerText = this.Text("BATTLES", "BATTLE_WORDPLAY");
            this.metaphores.InnerText = this.Text("BATTLES", "BATTLE_METAPHORES");
            this.multis.InnerText = this.Text("BATTLES", "BATTLE_MULTIS");
            this.punchlines.InnerText = this.Text("BATTLES", "BATTLE_PUNCHLINES");
            this.flow.InnerText = this.Text("BATTLES", "BATTLE_FLOW");
        }

        #endregion
    }
}