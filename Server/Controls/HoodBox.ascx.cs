#region Using

using System;
using System.Linq;
using System.Web.UI.WebControls;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using Microsoft.AspNet.FriendlyUrls;
using YAF.Types;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class HoodBox : RapUserControl
    {
        #region Properties
        /// <summary>
        /// Gets or sets the hood.
        /// </summary>
        /// <value>
        /// The hood.
        /// </value>
        [NotNull]
        public HoodData Hood { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.JoinButton.OnClientClick += "return handlepostback({0})".FormatWith(this.JoinButton.ClientID);
            this.LeaveButton.OnClientClick += "return handlepostback({0})".FormatWith(this.LeaveButton.ClientID);
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

        /// <summary>
        ///     Binds the data source.
        /// </summary>
        public void BindDataSource()
        {
            this.HoodUsersRepeater.DataSource = this.Hood.Users.OrderByDescending(x => x.UserId).Take(10);
            this.HoodUsersRepeater.DataBind();
            this.HoodItemImage.Src = this.GetService<ResourceProvider>()
                .GetClientPath(RapResource.HoodPictures, this.Hood.Picture);

            this.HoodTotalMembers.Html = this.Text("HOOD", "HOOD_TOTALMEMBERS").FormatWith(this.Hood.Users.Count);

            this.HoodHyperLink.NavigateUrl = this.GetService<UrlProvider>().GetUrl("~/Pages/Hoods", this.Hood.HoodId);
            this.HoodHyperLink.Text = this.Hood.Name;

            this.HoodAbout.InnerText = this.Text("HOOD", "HOOD_ABOUT").FormatWith(this.Hood.Details);
            this.DateCreated.DateTime = this.Hood.DateCreated;

            this.HandleJoinButtonVisibility();

            this.LeaveButton.Visible = this.Hood.Users.Any(x => x.UserId == this.PageContext.PageUserID);
            this.LeaveButton.CommandArgument = this.Hood.HoodId.ToString();
        }

        /// <summary>
        /// Handles the join button visibility.
        /// </summary>
        private void HandleJoinButtonVisibility()
        {
            this.JoinButton.CommandArgument = Hood.HoodId.ToString();

            if (this.Hood.IsPublic && this.Hood.Users.Any(x => x.UserId == this.PageContext.PageUserID))
            {
                this.JoinButton.Visible = false;
            }

            else if (this.Hood.IsPublic && this.Hood.Users.All(x => x.UserId != this.PageContext.PageUserID))
            {
                this.JoinButton.Visible = true;
            }

            else
            {
                this.JoinButton.Visible = false;
            }

            if (this.PageContext.IsGuest)
            {
                this.JoinButton.Visible = false;
                this.LeaveButton.Visible = false;
            }
        }

        /// <summary>
        ///     Handles the Command event of the JoinButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void JoinButton_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var hoodId = Convert.ToInt32(e.CommandArgument);
            this.GetCore<HoodData>().AddUserToHood(hoodId, this.PageContext.PageUserID);
            this.AddLoadMessageSession(this.Text("HOOD", "HOOD_JOINED"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        /// <summary>
        ///     Handles the Command event of the LeaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void LeaveButton_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var hoodId = Convert.ToInt32(e.CommandArgument);
            this.GetCore<HoodData>().RemoveUserFromHood(hoodId, this.PageContext.PageUserID);
            this.AddLoadMessageSession(this.Text("HOOD", "HOOD_LEFT2"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        #endregion
    }
}