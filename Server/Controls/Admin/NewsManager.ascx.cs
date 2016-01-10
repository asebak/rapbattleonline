#region Using

using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Ext.Net;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Factory;
using FreestyleOnline.classes.Providers;
using Microsoft.AspNet.FriendlyUrls;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;

#endregion

namespace FreestyleOnline.Controls.Admin
{
    public partial class EditNewsFeed : RapUserControl
    {

        #region Methods
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.btnDelete.OnClientClick += "return handlepostback({0});".FormatWith(this.btnDelete.ClientID);
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
                this.GetService<UrlProvider>().Redirect("~/");
            }
            this.BindNews();
        }

        /// <summary>
        /// Binds the news.
        /// </summary>
        private void BindNews()
        {
            this.NewsPoster.PostBtn.Click += PostBtn_Click;
            var newsList = this.GetCore<News>().GetDataSource().AsEnumerable().ToList();
            if (!newsList.Any())
            {
                this.btnDelete.Visible = false;
            }
            this.NewsStore.DataSource = newsList.ConvertAll(x =>
                new object[]
                {
                    x.Field<int>("NewsFeedID"), x.Field<string>("Information"), x.Field<string>("Title")
                }).ToArray();
            this.NewsStore.DataBind();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Click event of the PostBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PostBtn_Click([NotNull] object sender,[NotNull] EventArgs e)
        {
            if (this.NewsPoster.ValidPost() && !this.NewsTitle.Value.IsNotSet())
            {
                this.GetCore<News>().PostNews(this.PageContext.PageUserID, this.NewsTitle.Value, this.NewsPoster.Editor.Text);
                this.AddLoadMessageSession(this.Text("ADMIN", "NEWSMANAGER_POSTED"), MessageTypes.Success);
                this.GetService<UrlProvider>().RefreshPage();
            }
        }
        /// <summary>
        ///     Handles the deletion.
        /// </summary>
        [DirectMethod]
        public void HandleDeletion()
        {
            var selectedModel = this.NewsGrid.GetSelectionModel() as RowSelectionModel;
            if (selectedModel.SelectedRow != null)
            {
               this.GetCore<News>().DeleteNews(Convert.ToInt32(selectedModel.SelectedRow.RecordID));
               this.AddLoadMessageSession(this.Text("ADMIN", "NEWSMANAGER_DELETE"), MessageTypes.Success);
               this.GetService<UrlProvider>().RefreshPage();
            }
            this.AddLoadMessageSession(this.Text("ADMIN", "NEWSMANAGER_NODELETE"), MessageTypes.Warning);
            this.GetService<UrlProvider>().RefreshPage();
        }

        #endregion
    }
}