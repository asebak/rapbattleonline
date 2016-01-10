#region Using

using System;
using System.Linq;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Featured
{
    [Obsolete("No Need For Control")]
    public partial class VoteInProgressWrittenBattles : RapUserControl
    {
        #region Members

        public bool IsNotVisible { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            IsNotVisible = false;
            var newWrittenBattles = new RapBattleWritten();
            var dataSource = newWrittenBattles.GetVotingInProgressBattles().Cast<RapBattleWritten>().ToList();
            this.VotingProgressWrittenBattlesRepeater.DataSource = dataSource;
            this.VotingProgressWrittenBattlesRepeater.DataBind();
            if (dataSource.Count < 1)
            {
                IsNotVisible = true;
            }
        }

        #endregion

    }
}