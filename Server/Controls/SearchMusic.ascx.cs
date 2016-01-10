#region Using

using System;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class SearchMusic : RapUserControl
    {
        //TODO: Fix Music Player Style after postback seems like it doesn't get register the js for audio player
        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SearchMusicGrid.Visible = false;
        }

        /// <summary>
        ///     Binds the data after user search.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        private void BindDataAfterUserSearch([CanBeNull] string userName)
        {
            var userId = UserData.GetUserIdFromDisplayName(userName);
            if (userId > 1)
            {
                this.SearchMusicGrid.DataSource = this.GetCore<UserData>().GetUsersMusicDataSource(userId);
                this.SearchMusicGrid.DataBind();
            }
            if (SearchMusicGrid.Rows.Count == 0 || userId <= 1)
            {
                this.SearchMusicGrid.Visible = false;
                this.PageContext.AddLoadMessage(this.Text("COMMON", "NORESULTS"), MessageTypes.Warning);
            }
            else
            {
                this.SearchMusicGrid.Visible = true;
            }
        }

        /// <summary>
        ///     Binds the data after title search.
        /// </summary>
        /// <param name="title">The title.</param>
        private void BindDataAfterTitleSearch([CanBeNull] string title)
        {
            if (title.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.Text("MUSIC", "NO_TITLE"), MessageTypes.Error);
                return;
            }
            this.SearchMusicGrid.DataSource = this.GetCore<MusicData>().GetMusicFromTitle(title);
            this.SearchMusicGrid.DataBind();

            if (SearchMusicGrid.Rows.Count == 0)
            {
                this.SearchMusicGrid.Visible = false;
                this.PageContext.AddLoadMessage(this.Text("COMMON", "NORESULTS"), MessageTypes.Warning);
                return;
            }
            this.SearchMusicGrid.Visible = true;
        }
        #endregion

        #region Event Handlers

        /// <summary>
        ///     Called when [search button_ click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void OnSearchButton_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            switch (this.SelectionList.CheckedItems[0].InputValue)
            {
                case "0":
                    this.BindDataAfterUserSearch(this.SearchParametersText.Value);
                    break;
                case "1":
                    this.BindDataAfterTitleSearch(this.SearchParametersText.Value);
                    break;
                default:
                    this.PageContext.AddLoadMessage(this.Text("COMMON", "NORESULTS"), MessageTypes.Warning);
                    break;
            }
        }

        #endregion
    }
}