#region Using

using System;
using Common.Types.Enums;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class RapBattleBox : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the battle identifier.
        /// </summary>
        /// <value>
        ///     The battle identifier.
        /// </value>
        public int BattleId { get; set; }

        /// <summary>
        ///     Gets or sets the end date.
        /// </summary>
        /// <value>
        ///     The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     Gets or sets the user id1.
        /// </summary>
        /// <value>
        ///     The user id1.
        /// </value>
        public int? UserId1 { get; set; }

        /// <summary>
        ///     Gets or sets the user id2.
        /// </summary>
        /// <value>
        ///     The user id2.
        /// </value>
        public int? UserId2 { get; set; }

        /// <summary>
        ///     Gets or sets the battle URL.
        /// </summary>
        /// <value>
        ///     The battle URL.
        /// </value>
        public string BattleUrl { get; set; }

        /// <summary>
        ///     Gets or sets the user1 overall.
        /// </summary>
        /// <value>
        ///     The user1 overall.
        /// </value>
        public float? User1Overall { get; set; }

        /// <summary>
        ///     Gets or sets the user2 overall.
        /// </summary>
        /// <value>
        ///     The user2 overall.
        /// </value>
        public float? User2Overall { get; set; }

        /// <summary>
        ///     Gets or sets the winner identifier.
        /// </summary>
        /// <value>
        ///     The winner identifier.
        /// </value>
        public int? WinnerId { get; set; }

        /// <summary>
        ///     Gets or sets the user1 verse.
        /// </summary>
        /// <value>
        ///     The user1 verse.
        /// </value>
        public string User1Verse { get; set; }

        /// <summary>
        ///     Gets or sets the user2 verse.
        /// </summary>
        /// <value>
        ///     The user2 verse.
        /// </value>
        public string User2Verse { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public RapBattleType Type { get; set; }
        protected double VoteDays { get; private set; }
        #endregion

        #region Methods
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.VoteDays = Convert.ToDouble(this.GetService<ApplicationProvider>().GetApplicationSettings("RAP.VoteDays"));
            base.OnInit(e);
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.RegisterJsScripts();
        }

        /// <summary>
        /// Registers the js scripts.
        /// </summary>
        private void RegisterJsScripts()
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "rapBattleRating", "~/js/RapBattleRating.js", true);
        }

        #endregion
    }
}