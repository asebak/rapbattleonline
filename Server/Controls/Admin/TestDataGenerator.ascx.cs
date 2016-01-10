#region Using

using System;
using System.IO;
using System.Linq;
using System.Web.Security;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Classes;
using YAF.Classes.Data;
using YAF.Core;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;
using YAF.Utils;

#endregion

namespace FreestyleOnline.Controls.Admin
{
    public partial class TestDataGenerator : RapUserControl
    {
        #region Methods

        protected override void OnInit([NotNull] EventArgs e)
        {
            this.UserCreator.OnClientClick += "return handlepostback({0});".FormatWith(this.UserCreator.ClientID);
            this.HoodCreator.OnClientClick += "return handlepostback({0});".FormatWith(this.HoodCreator.ClientID);
            base.OnInit(e);
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Click event of the UserCreator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void UserCreator_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var baseUserName = this.BaseUserName.Text;
            var baseEmail = this.BaseEmail.Text;
            var password = this.PasswordTextField.Text;
            var totalUsers = this.TotalUsersCreation.Number;
            if (baseUserName.IsNotSet() || baseEmail.IsNotSet() || password.IsNotSet() || totalUsers < 1)
            {
                this.PageContext.AddLoadMessage(this.Text("ADMIN", "TEST_INVALIDPARAMETERS"), MessageTypes.Error);
                return;
            }
            for (var i = 0; i < totalUsers; i++)
            {
                var newUserName = string.Format("{0}{1}", baseUserName, i);
                MembershipCreateStatus status;
                var user = this.Get<MembershipProvider>().CreateUser(
                    newUserName,
                    password,
                    string.Format("{0}{1}", i, baseEmail),
                    "testingquestion",
                    "testingawnser",
                    true,
                    null,
                    out status);

                if (status != MembershipCreateStatus.Success)
                {
                    continue;
                }

                RoleMembershipHelper.SetupUserRoles(YafContext.Current.PageBoardID, newUserName);
                var userIdRole = RoleMembershipHelper.CreateForumUser(user, YafContext.Current.PageBoardID);
                var userProfile = YafUserProfile.GetProfile(newUserName);
                userProfile.Location = "";
                userProfile.Homepage = "";
                userProfile.Save();
                LegacyDb.user_save(
                    UserMembershipHelper.GetUserIDFromProviderUserKey(user.ProviderUserKey),
                    this.PageContext.PageBoardID,
                    null,
                    null,
                    null,
                    -720,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null);

                var autoWatchTopicsEnabled =
                    this.Get<YafBoardSettings>().DefaultNotificationSetting.Equals(
                        UserNotificationSetting.TopicsIPostToOrSubscribeTo);
                LegacyDb.user_savenotification(
                    UserMembershipHelper.GetUserIDFromProviderUserKey(user.ProviderUserKey),
                    true,
                    autoWatchTopicsEnabled,
                    this.Get<YafBoardSettings>().DefaultNotificationSetting,
                    this.Get<YafBoardSettings>().DefaultSendDigestEmail);
            }
            this.PageContext.AddLoadMessage(this.Text("ADMIN", "TESTDATA_DONE"), MessageTypes.Success);
        }

        /// <summary>
        ///     Handles the Click event of the HoodCreator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void HoodCreator_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            var baseHoodName = this.BaseHood.Text;
            var totalHoods = this.TotalHoodsCreation.Number;
            var hoodList = this.GetCore<HoodData>();
            if (baseHoodName.IsNotSet() || totalHoods < 1 || !this.HoodPicture.HasFile)
            {
                this.PageContext.AddLoadMessage(this.Text("ADMIN", "TEST_INVALIDPARAMETERS"), MessageTypes.Error);
                return;
            }
            if (this.HoodPicture.HasFile)
            {
                if (this.HoodPicture.PostedFile.ContentType.Contains("image"))
                {
                    this.HoodPicture.PostedFile.SaveAs(
                        this.GetService<ResourceProvider>().GetPath(RapResource.HoodPictures, "TestData" + Path.GetFileName(this.HoodPicture.FileName)));
                }
            }
            for (var i = 0; i < totalHoods; i++)
            {
                var newHoodName = string.Format("{0}{1}", baseHoodName, i);
                var newHoodDiscription = this.Text("ADMIN", "TESTDATA_HOODDETAILS");
                var randomPrivacy = new Random();
                var newHoodData = new HoodData
                {
                    Name = newHoodName,
                    Picture = "TestData" + Path.GetFileName(this.HoodPicture.FileName),
                    IsPublic = randomPrivacy.Next(0, 2) == 1,
                    Details = newHoodDiscription
                };
                newHoodData.AddHood(newHoodData, this.PageContext.PageUserID);
            }
            var listOfUsers =
                UserMembershipHelper.GetAllUsers()
                    .Cast<MembershipUser>()
                    .Select(x => UserData.GetUserIdFromDisplayName(x.UserName))
                    .ToList();
            var listOfHoods = this.GetCore<HoodData>().GetAllHoods();
            foreach (var hd in listOfHoods)
            {
                foreach (var u in listOfUsers)
                {
                    hoodList.AddUserToHood(hd.HoodId, u);
                }
            }
            this.PageContext.AddLoadMessage(this.Text("ADMIN", "TESTDATA_DONE"), MessageTypes.Success);
        }

        #endregion
    }
}