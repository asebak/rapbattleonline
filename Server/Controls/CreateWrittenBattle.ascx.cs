#region Using

using System;
using Ext.Net;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using Microsoft.AspNet.FriendlyUrls;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class CreateWrittenBattle : RapUserControl
    {
        #region Methods
        protected override void OnInit(EventArgs e)
        {
            this.CreateBattleWritten.OnClientClick +=
                "return handlepostback({0});".FormatWith(this.CreateBattleWritten.ClientID);
            base.OnInit(e);
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.HideComboBox();
        }
        #endregion

        #region Direct Methods
        /// <summary>
        ///     Hides the text box on the client side.
        /// </summary>
        [DirectMethod]
        public void HideComboBox()
        {
            var combobox = (ComboBox) this.WrittenBattleUser.FindControl("UserComboxBoxList");
            combobox.Hidden = this.PrivacyGroup.CheckedItems[0].InputValue == "1";
        }

        /// <summary>
        ///     Handles the date on the client side.
        /// </summary>
        [DirectMethod]
        public void HandleDate()
        {
            //TODO: Maybe use alertify instead of ext.net window
            HideComboBox();
            var date = DateTime.Parse(this.DateBox.Text);
            if (this.WrittenBattleUser.UserNameSelected == "" && this.PrivacyGroup.CheckedItems[0].InputValue == "0")
            {
                this.GetService<ClientProviders>()
                    .DisplayRealTimeError(350, 100, true, this.Text("BATTLES", "BATTLE_NOUSER"), this.DateBox);
            }
            if (date <= DateTime.Now)
            {
                this.GetService<ClientProviders>()
                    .DisplayRealTimeError(350, 100, true, this.Text("BATTLES", "BATTLE_DATEFUTURE"), this.DateBox);
                this.DateBox.Text = "";
            }
            else if (date > DateTime.Now.AddDays(30))
            {
                this.GetService<ClientProviders>()
                    .DisplayRealTimeError(350, 100, true, this.Text("BATTLES", "BATTLE_DATETOFAR"), this.DateBox);
                this.DateBox.Text = "";
            }
        }
        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Click event of the CreateBattleWritten control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void CreateBattleWritten_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var userName = this.WrittenBattleUser.UserNameSelected;
            var isPublic = this.PrivacyGroup.CheckedItems[0].InputValue == "1";
            var numOfBars = Convert.ToInt32(this.BarsGroup.CheckedItems[0].BoxLabel);
            var endDate = DateTime.Parse(this.DateBox.Text);
            var isValidBattle = this.GetCore<RapBattleWritten>()
                .IsValidRapBattle(endDate, isPublic, this.PageContext.PageUserID,
                    UserData.GetUserIdFromDisplayName(userName));
            if (isValidBattle != null)
            {
                this.PageContext.AddLoadMessage(isValidBattle, MessageTypes.Error);
                return;
            }
            var battle = isPublic
                ? new RapBattleWritten(this.PageContext.PageUserID, null, endDate, numOfBars, true)
                : new RapBattleWritten(this.PageContext.PageUserID, UserData.GetUserIdFromDisplayName(userName), endDate,
                    numOfBars, false);

            var writtenBattleId = battle.CreateBattle();
            this.GetService<UrlProvider>().Redirect("~/Pages/WrittenBattles", writtenBattleId);
        }

        #endregion
    }
}