using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Types;

namespace FreestyleOnline.Controls.Featured
{
    [Obsolete("No Need For Control")]
    public partial class NewAudioBattles : RapUserControl
    {
        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var newAudioBattles = new RapBattleAudio();
            var dataSource = newAudioBattles.GetLatestBattles().Cast<RapBattleAudio>().ToList();
            this.NewAudioBattlesRepeater.DataSource = dataSource;
            this.NewAudioBattlesRepeater.DataBind();
        }

        #endregion

    }
}