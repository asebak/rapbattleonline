#region Using

using System;
using Ext.Net;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class CreateAudioBattle : RapUserControl
    {
        #region Methods
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.CreateBattleAudio.OnClientClick +=
                "return handlepostback({0});".FormatWith(this.CreateBattleAudio.ClientID);
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

        #region DirectMethods

        /// <summary>
        ///     Hides the ComboBox.
        /// </summary>
        [DirectMethod]
        public void HideComboBox()
        {
            var combobox = (ComboBox) this.AudioBattleUser.FindControl("UserComboxBoxList");
            combobox.Hidden = this.PrivacyGroup.CheckedItems[0].InputValue == "1";

        }

        /// <summary>
        ///     Handles the date.
        /// </summary>
        [DirectMethod]
        public void HandleDate()
        {
            this.HideComboBox();
            var date = DateTime.Parse(this.DateBox.Text);
            if (this.AudioBattleUser.UserNameSelected.IsNotSet() && this.PrivacyGroup.CheckedItems[0].InputValue == "0")
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
        ///     Handles the Click event of the CreateBattleAudio control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void CreateBattleAudio_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var userName = this.AudioBattleUser.UserNameSelected;
            var isPublic = this.PrivacyGroup.CheckedItems[0].InputValue == "1";
            var audioRecordingLength = Convert.ToInt32(this.AudioLengthGroup.CheckedItems[0].InputValue);
            var endDate = DateTime.Parse(this.DateBox.Text);
            var isValidBattle = this.GetCore<RapBattleAudio>()
                .IsValidRapBattle(endDate, isPublic, this.PageContext.PageUserID,
                    UserData.GetUserIdFromDisplayName(userName));
            if (isValidBattle != null)
            {
                this.PageContext.AddLoadMessage(isValidBattle, MessageTypes.Error);
                return;
            }

            var battle = isPublic
                ? new RapBattleAudio(this.PageContext.PageUserID, null, endDate, audioRecordingLength, true)
                : new RapBattleAudio(this.PageContext.PageUserID, UserData.GetUserIdFromDisplayName(userName), endDate,
                    audioRecordingLength, false);
            var audioBattleId = battle.CreateBattle();
            this.GetService<UrlProvider>().Redirect("~/Pages/AudioBattles", audioBattleId);
        }

        #endregion
    }
}