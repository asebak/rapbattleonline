﻿#region Using

using System;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Types;

#endregion

namespace FreestyleOnline.Pages
{
    public partial class AudioLeaderboards : RapPage
    {
        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.AudioLeader.BattleRankings = this.GetCore<AudioBattleLeaderboards>().GetRankings();
        }

        #endregion
    }
}