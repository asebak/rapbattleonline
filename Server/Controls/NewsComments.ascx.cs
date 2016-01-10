#region Using

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Factory;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.Controls.Generic;
using Microsoft.AspNet.FriendlyUrls;
using YAF.Types;
using YAF.Types.Constants;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     News Comments Box
    /// </summary>
    public partial class NewsComments : RapUserControl
    {
        #region Members

        /// <summary>
        /// The _news comments
        /// </summary>
        private NewsCommentsList _newsComments;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public string Id { get;private set; }

        /// <summary>
        /// The post number
        /// </summary>
        public int PostNumber = 0;

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Id = this.GetService<UrlProvider>().GetQuery()[0];
            if (this.PageContext.IsGuest)
            {
                this.GetService<UrlProvider>().Redirect("~/");
            }
            this._newsComments = new NewsCommentsList(Convert.ToInt32(Id));
            this.NewsCommentsPager.Dt = _newsComments.GetDataSource();
            this.NewsCommentsPager.Repeater = this.NewsFeedID;
            this.NewsCommentsPager.PerPage = 10;
            this.PostEdit.PostBtn.Click += PostCommentNews_Click;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Click event of the PostCommentNews control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PostCommentNews_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.PostEdit.ValidPost())
            {
                this._newsComments.PostComment(this.PageContext.PageUserID, this.PostEdit.Editor.Text);
                this.AddLoadMessageSession(this.Text("COMMON", "COMMENT_POSTED"));
                this.GetService<UrlProvider>().RefreshPage();
            }
        }

        /// <summary>
        /// Handles the ItemDataBound event of the NewsFeedID control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void NewsFeedID_ItemDataBound(object sender, RepeaterItemEventArgs e)
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