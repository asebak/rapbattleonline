#region Using

using System;
using System.Linq;
using System.Web.UI.WebControls;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Factory;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.Controls.Generic;
using Microsoft.AspNet.FriendlyUrls;
using YAF.Types;

#endregion

namespace FreestyleOnline.Pages
{
    public partial class Hoods : RapPage
    {
        #region Members

        /// <summary>
        /// The post number
        /// </summary>
        [NotNull] protected int PostNumber = 0;

        #endregion


        #region Properties

        /// <summary>
        ///     Gets or sets the identifier of the hood.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; private set; }

        #endregion

        #region Methods
        protected override void OnInit(EventArgs e)
        {
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var query = this.GetService<UrlProvider>().GetQuery();
            this.Id = Convert.ToInt32(query[0]);
            var comments = new HoodComments(Convert.ToInt32(Id));
            var details = this.GetCore<HoodData>().GetHoodDetails(Convert.ToInt32(Id));
            this.CurrentHoodBox.Hood = details;
            this.CurrentHoodBox.BindDataSource();
            this.HoodPager.Repeater = this.HoodCommentsRepeater;
            this.HoodPager.Dt = comments.GetHoodComments();
            this.HoodPager.PerPage = 20;
            this.IntializeEditor(this.CurrentHoodBox.Hood);
            if (query.Count > 1)
            {
                if (query[1] == "join" && !details.IsPublic && details.Users.All(x => x.UserId != this.PageContext.PageUserID))
                {
                    if (this.GetCore<HoodData>()
                        .GetInvitedUsers(this.Id)
                        .Any(x => x.Equals(this.PageContext.PageUserID)))
                    {
                        //was able to join
                        this.GetCore<HoodData>().AddUserToHood(this.Id, this.PageContext.PageUserID);
                    }
                }
            }
        }

        /// <summary>
        ///     Intializes the text editor.
        /// </summary>
        /// <param name="data">The data.</param>
        private void IntializeEditor(HoodData data)
        {
            this.PostEdit.PostBtn.Click += PostComment_Click;
            if (this.PageContext.IsGuest || (!data.IsPublic &&
                                             data.Users.All(
                                                 x =>
                                                     x.UserId !=
                                                     this.PageContext.PageUserID)))
            {
                this.PostEdit.Visible = false;
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Click event of the PostComment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PostComment_Click(object sender, EventArgs e)
        {
            if (this.PostEdit.ValidPost())
            {
                var comments = new HoodComments(this.Id);
                comments.PostComment(this.PageContext.PageUserID, this.PostEdit.Editor.Text);
                this.AddLoadMessageSession(this.Text("COMMON", "COMMENT_POSTED"));
                this.GetService<UrlProvider>().RefreshPage();
            }
        }

        /// <summary>
        /// Handles the ItemDataBound event of the HoodCommentsRepeater control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void HoodCommentsRepeater_ItemDataBound([NotNull] object sender, [NotNull] RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var postItem = (DisplaySitePost)e.Item.FindControl("PostEditor");
                postItem.NotifyUpdate();
            }
        }

        #endregion

    }
}