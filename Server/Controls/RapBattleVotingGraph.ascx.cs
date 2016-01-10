#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class RapBattleVotingGraph : RapUserControl
    {
        #region Properites
        /// <summary>
        /// Gets or sets the user1 votes.
        /// </summary>
        /// <value>
        /// The user1 votes.
        /// </value>
        [NotNull]
        public List<RapBattleVote> UserVotes { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (UserVotes != null && UserVotes.Count != 0)
            {
                var wordplayAvg = (float) UserVotes.Select(x => x.Wordplay).Average();
                var flowAvg = (float) UserVotes.Select(x => x.Flow).Average();
                var multisAvg = (float) UserVotes.Select(x => x.Multis).Average();
                var punchAvg = (float) UserVotes.Select(x => x.PunchLines).Average();
                var metaphoresAvg = (float) UserVotes.Select(x => x.Metaphores).Average();
                BattleStatsChartAspNet.CreateStatsProgressBar(wordplayAvg, this.wordplay);
                BattleStatsChartAspNet.CreateStatsProgressBar(flowAvg, this.flow);
                BattleStatsChartAspNet.CreateStatsProgressBar(multisAvg, this.multis);
                BattleStatsChartAspNet.CreateStatsProgressBar(punchAvg, this.punchlines);
                BattleStatsChartAspNet.CreateStatsProgressBar(metaphoresAvg, this.metaphores);
                this.wordplay.InnerText = this.Text("BATTLES", "BATTLE_WORDPLAY");
                this.metaphores.InnerText = this.Text("BATTLES", "BATTLE_METAPHORES");
                this.multis.InnerText = this.Text("BATTLES", "BATTLE_MULTIS");
                this.punchlines.InnerText = this.Text("BATTLES", "BATTLE_PUNCHLINES");
                this.flow.InnerText = this.Text("BATTLES", "BATTLE_FLOW");
            }
            else
            {
                this.Visible = false;
            }
        }

        #endregion

    }
}