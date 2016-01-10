#region Using

using System;
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

namespace FreestyleOnline.Controls
{
    public partial class WrittenBattleComments : RapUserControl
    {
        #region Members

        [NotNull] private RapBattleWritten _battle;
        [NotNull] protected int PostNumber = 0;
        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var query = this.GetService<UrlProvider>().GetQuery();
            this._battle = new RapBattleWritten(this.PageContext.PageUserID, Convert.ToInt32(query[0]));
            this.WrittenBattleCommentsPager.Dt = this._battle.GetBattleComments();
            this.WrittenBattleCommentsPager.Repeater = this.WrittenBattleFeed;
            this.WrittenBattleCommentsPager.PerPage = 10;
            this.PostEdit.PostBtn.Click += PostComment_Click;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Click event of the PostComment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void PostComment_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.PostEdit.ValidPost())
            {
                this._battle.PostComment(this.PageContext.PageUserID, this.PostEdit.Editor.Text);
                this.AddLoadMessageSession(this.Text("COMMON", "COMMON_POSTSUCCESS"));
                this.GetService<UrlProvider>().RefreshPage();
            }
        }

        /// <summary>
        /// Handles the ItemDataBound event of the WrittenBattleFeed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void WrittenBattleFeed_ItemDataBound([NotNull] object sender,[NotNull] RepeaterItemEventArgs e)
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