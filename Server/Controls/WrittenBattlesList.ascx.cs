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
    public partial class WrittenBattlesList : RapUserControl
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
        /// Gets or sets the written battles count.
        /// </summary>
        /// <value>
        /// The written battles count.
        /// </value>
        [CanBeNull]
        public int WrittenBattlesCount { get; private set; }
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
            this.WrittenBattlesPager.PerPage = 10;
            this.WrittenBattlesPager.GridView = this.MyWrittenBattles;
            var writtenBattles = userData.GetUsersWrittenBattles().Cast<object>().ToList();
            this.WrittenBattlesPager.ListDs = writtenBattles;
            this.WrittenBattlesCount = writtenBattles.Count;
            if (this.WrittenBattlesCount <= 0)
            {
                var noBattles = this.GetCore<CalloutBox>()
                    .Create(BootstrapElementType.Info, this.Text("BATTLES", "NO_WRITTEN"));
                this.NoWritten.Controls.Add(noBattles);
            }
        }

        #endregion
    }
}