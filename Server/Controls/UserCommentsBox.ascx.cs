#region Using

using System;
using System.Web.UI.WebControls;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.Controls.Generic;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class UserComments : RapUserControl
    {
        #region Members
        [NotNull]
        protected int PostNumber = 0;
        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        [NotNull] public int UserId { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var ud = new UserData(UserId, this.PageContext.PageUserID);
            this.ProfileCommentsPager.Repeater = this.CommentsFeed;
            this.ProfileCommentsPager.Dt = ud.GetUsersProfileComments();
            this.ProfileCommentsPager.PerPage = 20;
            this.PostEdit.PostBtn.Click += PostComment_Click;
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Click event of the PostComment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PostComment_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.PostEdit.ValidPost())
            {
                var userComment = new classes.Core.UserComments(this.PageContext.PageUserID);
                userComment.PostComment(this.UserId, this.PostEdit.Editor.Text);
                this.AddLoadMessageSession(this.Text("COMMON", "COMMON_POSTSUCCESS"));
                this.GetService<UrlProvider>().RefreshPage();
            }
        }

        #endregion

        /// <summary>
        /// Handles the ItemDataBound event of the CommentsFeed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void CommentsFeed_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var postItem = (DisplaySitePost)e.Item.FindControl("PostEditor");
                postItem.NotifyUpdate();
            }
        }
    }
}