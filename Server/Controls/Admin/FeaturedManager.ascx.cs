#region Using

using System;
using System.Diagnostics.Contracts;
using Common.Types;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using YAF.Core;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;

#endregion

namespace FreestyleOnline.Controls.Admin
{
    /// <summary>
    ///     Features either a User Profile or a Music Track on the Main Site
    /// </summary>
    public partial class FeaturedManager : RapUserControl
    {
        #region Methods
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.FeaturedProfile.OnClientClick += "return handlepostback({0});".FormatWith(this.FeaturedProfile.ClientID);
            this.FeaturedTrack.OnClientClick += "return handlepostback({0});".FormatWith(this.FeaturedTrack.ClientID);
            base.OnInit(e);
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.PageContext.IsAdmin)
            {
                return;
            }
            this.BindFeaturedMusic();
        }

        /// <summary>
        /// Binds the featured music.
        /// </summary>
        private void BindFeaturedMusic()
        {
            this.FeatureMusicStore.DataSource =
                this.GetCore<MusicData>().OrganizeTracks()
                    .ConvertAll(
                        x =>
                            new object[]
                            {
                                x.MusicTrackData.SongName,
                                UserMembershipHelper.GetDisplayNameFromID(x.MusicTrackData.UserId),
                                string.Format("{0};{1}", x.MusicTrackData.MusicId, x.MusicTrackData.UserId)
                            }).ToArray();
            this.FeatureMusicStore.DataBind();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        ///     Features a profile on click
        /// </summary>
        protected void FeaturedProfile_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var userId = this.FeaturedUsers.UserIdSelected;
            var enteredDate = Convert.ToDateTime(this.DateForProfileFeature.Text);
            if (this.FeaturedUsers.UserNameSelected.IsNotSet() || RapGlobalHelpers.IsDateExpired(enteredDate))
            {
                this.PageContext.AddLoadMessage(this.Text("ADMIN", "FEATURE_USERNOTSET"));
                return;
            }
            this.GetCore<UserDataFeatured>().SetFeaturedUsers(userId, enteredDate);
            Message.SendPmMessage(this.PageContext.PageUserID, Convert.ToInt32(userId),
                this.Text("ADMIN", "FEATUREDMANAGER_PROFILESUCCESS"),
                this.Text("ADMIN", "FEATUREDMANAGER_PROFILE").FormatWith(enteredDate));
            this.PageContext.AddLoadMessage(this.Text("ADMIN", "FEATURE_USERSET").FormatWith(this.FeaturedUsers.UserNameSelected), MessageTypes.Success);
        }

        /// <summary>
        ///     Handles the Click event of the FeaturedTrack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void FeaturedTrack_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var enteredDate = Convert.ToDateTime(this.DateForMusicFeature.Text);
            if (this.DateForMusicFeature.Text.IsNotSet() || this.MusicTextbox.Text.IsNotSet() || RapGlobalHelpers.IsDateExpired(enteredDate))
            {
                this.PageContext.AddLoadMessage(this.Text("ADMIN", "FEATURE_MUSICNOTSET"));
                return;
            }
            var inputValue = this.MusicTextbox.Value.ToString().Split(';');
            Contract.Assert(inputValue.Length == 2);
            var musicId = Convert.ToInt32(inputValue[0]);
            var userId = Convert.ToInt32(inputValue[1]);
            this.GetCore<MusicData>().SetFeaturedTrack(musicId, enteredDate);
            Message.SendPmMessage(YafContext.Current.PageUserID, userId,
                this.Text("ADMIN", "FEATUREDMANAGER_MUSICSUCCESS"),
                this.Text("ADMIN", "FEATUREDMANAGER_MUSIC")
                    .FormatWith(this.MusicTextbox.Text, this.DateForMusicFeature.Text));
            this.PageContext.AddLoadMessage(this.Text("ADMIN", "FEATURE_MUSICSET").FormatWith(this.MusicTextbox.Text), MessageTypes.Success);
        }

        #endregion
    }
}