using System;
using System.Linq;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Types;

namespace FreestyleOnline.Controls.Featured
{
    [Obsolete("No Need For Control")]
    public partial class VoteInProgressAudioBattles : RapUserControl
    {
        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var voteinProgressAudioBattles = new RapBattleAudio();
            var dataSource = voteinProgressAudioBattles.GetVotingInProgressBattles().Cast<RapBattleAudio>().ToList();
            this.VotingProgressAudioBattlesRepeater.DataSource = dataSource;
            this.VotingProgressAudioBattlesRepeater.DataBind();
        }

        #endregion

    }
}