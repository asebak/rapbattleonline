#region Using

using System;
using System.Linq;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class AudioBattlesList : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        [NotNull]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the audio battles count.
        /// </summary>
        /// <value>
        /// The audio battles count.
        /// </value>
        [CanBeNull]
        public int AudioBattlesCount { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var userData = new UserData(UserId);
            this.AudioBattlesPager.PerPage = 10;
            this.AudioBattlesPager.GridView = this.MyAudioBattles;
            var audioBattles = userData.GetUsersAudioBattles().Cast<object>().ToList();
            this.AudioBattlesPager.ListDs = audioBattles;
            this.AudioBattlesCount = audioBattles.Count;
            if (this.AudioBattlesCount <= 0)
            {
                var noData = this.GetCore<CalloutBox>()
                    .Create(BootstrapElementType.Info, this.Text("BATTLES", "NO_AUDIO"));
                this.NoAudioBattles.Controls.Add(noData);
            }
        }

        #endregion
    }
}