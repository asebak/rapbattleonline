#region Using

using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ext.Net;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types.UI;
using Microsoft.AspNet.FriendlyUrls;
using YAF.Core;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using Button = Ext.Net.Button;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class MyHood : RapUserControl
    {
        //TODO: FIx Autopostback on direct method
        //TODO: create another db table that handle all invites
        #region Properties

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets the hood count.
        /// </summary>
        /// <value>
        /// The hood count.
        /// </value>
        public int HoodCount { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.PageContext.IsGuest)
            {
                this.GetService<UrlProvider>().Redirect("~/");
            }
            var usersHoods = this.GetCore<UserData>().GetUsersHoods(this.UserId);
            this.HoodCount = usersHoods.Count;
            if (this.HoodCount <= 0)
            {
                this.MyHoods.Visible = false;
                var noHoods = this.GetCore<CalloutBox>()
                    .Create(BootstrapElementType.Info, this.Text("HOOD", "NO_HOODS"));
                this.NoHoods.Controls.Add(noHoods);
                return;
            }
            //TODO: Post back might cause issues
            if (!this.Page.IsPostBack)
            {
                this.MyHoodGridView.DataSource = usersHoods;
                this.MyHoodGridView.DataBind();
            }
        }

        /// <summary>
        ///     Handles the RowDataBound event of the MyHoodGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs" /> instance containing the event data.</param>
        protected void MyHoodGridView_RowDataBound([NotNull] object sender, [NotNull] GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }

            var hoodBox = (HoodBox)e.Row.FindControl("MyHoodBox");
            var div = (HtmlGenericControl) e.Row.FindControl("MyHoodAdmin");
            //var descriptionTxtBox = (TextArea) e.Row.FindControl("DescriptionMyHood");
            var submitButton = (Button) e.Row.FindControl("SubmitButton");
            var hoodUsersStore = (Store) e.Row.FindControl("HoodMembersStore");
            var inviteUsersStore = (Store) e.Row.FindControl("InviteMembersStore");

            var currentRow = (HoodData) e.Row.DataItem;

            hoodBox.Hood = currentRow;
            hoodBox.BindDataSource();

            div.Visible = currentRow.Users.Any(x => x.UserId == this.PageContext.PageUserID && x.IsAdmin);
            hoodUsersStore.DataSource =
                currentRow.Users.ConvertAll(
                    x =>
                        new object[]
                        {
                            UserMembershipHelper.GetDisplayNameFromID(x.UserId),
                            string.Format("{0};{1};{2}", currentRow.HoodId, x.UserId, e.Row.DataItemIndex)
                        }).ToArray();
            hoodUsersStore.DataBind();

            inviteUsersStore.DataSource = UserMembershipHelper.GetAllUsers()
                .Cast<MembershipUser>()
                .ToList()
                .ConvertAll(x => new object[] {x.UserName, string.Format("{0};{1}", currentRow.HoodId, x.UserName)})
                .ToArray();
            inviteUsersStore.DataBind();
            submitButton.OnClientClick += "return handlepostback({0})".FormatWith(submitButton.ClientID);
            submitButton.CommandArgument = currentRow.HoodId + ";" + e.Row.RowIndex;
        }


        /// <summary>
        ///     Handles the Command event of the SubmitButton control updates.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void SubmitButton_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var commandEventArgs = e.CommandArgument.ToString().Split(';');
            Contract.Assert(commandEventArgs.Length == 2);
            var hoodId = Convert.ToInt32(commandEventArgs[0]);
            var rowIndex = Convert.ToInt32(commandEventArgs[1]);
            var radioList = (RadioGroup) this.MyHoodGridView.Rows[rowIndex].FindControl("Privacy");
            var descriptionTxtBox = (TextArea) this.MyHoodGridView.Rows[rowIndex].FindControl("DescriptionMyHood");
            var privacy = radioList.CheckedItems[0].InputValue == "1";
            this.GetCore<HoodData>().UpdateHood(hoodId, privacy, descriptionTxtBox.Text);
            this.AddLoadMessageSession(this.Text("HOOD", "HOOD_UPDATED"));
            this.GetService<UrlProvider>().RefreshPage();
        }
        #endregion

        #region Client Event Handlers

        /// <summary>
        ///     On Trigger From The ComboBox
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="sender">The sender.</param>
        [DirectMethod]
        public void HoodMemberTrigger([NotNull]int index, [CanBeNull] object sender)
        {
            var comboBox = (ComboBox) this.MyHoodGridView.Rows[index].FindControl("HoodMembers");
            //TODO: Fix Autopostback happening
            if (sender == null)
            {
                this.GetService<ClientProviders>()
                    .DisplayRealTimeError(100, 150, true, this.Text("COMMON", "COMMON_NOUSER"), comboBox);
                return;
            }
            var commandEventArgs = sender.ToString().Split(';');
            Contract.Assert(commandEventArgs.Length == 3);
            var hoodId = Convert.ToInt32(commandEventArgs[0]);
            var userId = Convert.ToInt32(commandEventArgs[1]);
            var data = this.GetCore<HoodData>();
            switch (index)
            {
                    //promote to admin
                case 0:
                    data.UpdateHoodUserToAdmin(hoodId, userId);
                    //might not work
                    this.GetService<ClientProviders>()
                        .DisplayRealTimeNotification(this.Text("COMMON", "COMMON_SUCCESS"),
                            Icon.None, 300, 125, true,
                            this.Text("HOOD", "HOOD_PROMOTE"), comboBox);
                    break;
                    //remove user from hood
                case 1:
                    data.RemoveUserFromHood(hoodId, userId);
                    //might not work
                    this.GetService<ClientProviders>()
                        .DisplayRealTimeNotification(this.Text("COMMON", "COMMON_SUCCESS"),
                            Icon.None, 300, 125, true,
                            this.Text("HOOD", "HOOD_REMOVEMEMBER"), comboBox);
                    break;
            }
        }

        /// <summary>
        /// Invites the member to the hood that activates the trigger.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="sender">The sender.</param>
        [DirectMethod]
        public void InviteMemberTrigger([NotNull] int index, [CanBeNull] object sender)
        {
            var comboBox = (ComboBox)this.MyHoodGridView.Rows[index].FindControl("HoodMembers");
            //TODO: Fix Autopostback happening
            if (sender == null)
            {
                this.GetService<ClientProviders>()
                    .DisplayRealTimeError(100, 150, true, this.Text("COMMON", "COMMON_NOUSER"), comboBox);
                return;
            }
            var commandEventArgs = sender.ToString().Split(';');
            Contract.Assert(commandEventArgs.Length == 3);
            var hoodId = Convert.ToInt32(commandEventArgs[0]);
            var displayName = Convert.ToString(commandEventArgs[1]);
            var userId = UserData.GetUserIdFromDisplayName(displayName);
            var data = this.GetCore<HoodData>().GetHoodDetails(hoodId);
            if (data.IsPublic)
            {
                Message.SendPmMessage(this.PageContext.PageUserID, userId, this.Text("HOOD", "HOOD_INVITEMEMBERHEADER"),
                    this.Text("HOOD", "HOOD_INVITEMEMBERCONTENT")
                        .FormatWith(data.Name, FriendlyUrl.Href("~/Pages/Hoods", data.HoodId)));
            }
            else
            {
                this.GetCore<HoodData>().PrivateInvitation(userId, hoodId);
                //private invitation
                Message.SendPmMessage(this.PageContext.PageUserID, userId, this.Text("HOOD", "HOOD_INVITEMEMBERHEADER"),
                    this.Text("HOOD", "HOOD_INVITEMEMBERCONTENT")
                        .FormatWith(data.Name, FriendlyUrl.Href("~/Pages/Hoods", data.HoodId, "join")));
            }
            this.GetService<ClientProviders>()
                .DisplayRealTimeNotification(this.Text("HOOD", "HOOD_INVITEMEMBER"), Icon.None,
                    300, 125, true,
                    this.Text("HOOD", "HOOD_INVITEMEMBE2").FormatWith(displayName), comboBox);
        }

        #endregion
    }
}