#region Using

using System;
using System.Collections.Generic;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Core;
using YAF.Core.Services;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Leaderboards
{
    public partial class BattleLeaderboard : RapUserControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets the battle rankings.
        /// </summary>
        /// <value>
        /// The battle rankings.
        /// </value>
        [CanBeNull]
        public List<BattleRanking> BattleRankings { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.BattleRankings != null && this.BattleRankings.Count > 0)
            {
                this.usersrankings.DataSource = this.BattleRankings;
                this.usersrankings.DataBind();
            }
            else
            {
                this.noleaderboardstats.Visible = true;
            }
        }

        #endregion

    }
}