#region Using

using System;
using Common.Types.Enums;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     This is used for when a user wants to vote on a battle
    /// </summary>
    public partial class BattleVote : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the type of the battle.
        /// </summary>
        /// <value>
        ///     The type of the battle.
        /// </value>
        [NotNull]
        public RapBattleType BattleType { get; set; }

        /// <summary>
        ///     Gets or sets the battle identifier.
        /// </summary>
        /// <value>
        ///     The battle identifier.
        /// </value>
        [NotNull]
        public int BattleId { get; set; }

        /// <summary>
        ///     Gets or sets the display name1.
        /// </summary>
        /// <value>
        ///     The display name1.
        /// </value>
        [NotNull]
        public string DisplayName1 { get; set; }

        /// <summary>
        ///     Gets or sets the display name2.
        /// </summary>
        /// <value>
        ///     The display name2.
        /// </value>
        [NotNull]
        public string DisplayName2 { get; set; }

        /// <summary>
        ///     Gets or sets the user id1.
        /// </summary>
        /// <value>
        ///     The user id1.
        /// </value>
        [NotNull]
        public int UserId1 { get; set; }

        /// <summary>
        ///     Gets or sets the user id2.
        /// </summary>
        /// <value>
        ///     The user id2.
        /// </value>
       [NotNull] public int UserId2 { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.BattleId = Convert.ToInt32(this.GetService<UrlProvider>().GetQuery()[0]);
            this.RegisterVotingScript();
        }

        /// <summary>
        /// Registers the voting script.
        /// </summary>
        private void RegisterVotingScript()
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "battleVoting", "~/js/BattleVoting.js", true);
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "battleVotingLayout",
                    string.Format(
                        "$(function() {{CreateBattleVotingLayout('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')}});",
                        this.DisplayName1, this.BattleId, this.DisplayName2, this.BattleType, this.UserId1, this.UserId2));
        }

        #endregion
    }
}