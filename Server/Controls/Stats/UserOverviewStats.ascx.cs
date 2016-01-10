#region Using

using System;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using YAF.Classes;
using YAF.Classes.Data;
using YAF.Core;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;
using YAF.Utils;

#endregion

namespace FreestyleOnline.Controls.Stats
{
    public partial class UserOverviewStats : RapUserControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// When implemented, gets a collection of information that can be accessed by a control designer.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IDictionary" /> containing information about the control.</returns>
        public IUserData UserData { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Joined.Text =
                "{0}".FormatWith(this.Get<IDateTime>().FormatDateLong(Convert.ToDateTime(UserData.Joined)));
            if (!this.PageContext.IsAdmin && Convert.ToBoolean(UserData.DBRow["IsActiveExcluded"]))
            {
                this.LastVisit.Text = this.GetText("COMMON", "HIDDEN");
                this.LastVisit.Visible = true;
            }
            else
            {
                this.LastVisitDateTime.DateTime = UserData.LastVisit;
                this.LastVisitDateTime.Visible = true;
            }
            this.lnkThanks.Text = "({0})".FormatWith(this.GetText("VIEWTHANKS", "TITLE"));
            this.lnkThanks.Visible = this.Get<YafBoardSettings>().EnableThanksMod;
            this.ThanksFrom.Text =
                LegacyDb.user_getthanks_from(UserData.DBRow["userID"], this.PageContext.PageUserID).ToString();
            var thanksToArray = LegacyDb.user_getthanks_to(UserData.DBRow["userID"], this.PageContext.PageUserID);
            this.ThanksToTimes.Text = thanksToArray[0].ToString();
            this.ThanksToPosts.Text = thanksToArray[1].ToString();
            this.SetupUserStatistics(this.UserData);

        }

        /// <summary>
        /// The setup user statistics.
        /// </summary>
        /// <param name="userData">
        /// The user data.
        /// </param>
        private void SetupUserStatistics([NotNull] IUserData userData)
        {
            double allPosts = 0.0;

            if (userData.DBRow["NumPostsForum"].ToType<int>() > 0)
            {
                allPosts = 100.0 * userData.DBRow["NumPosts"].ToType<int>()
                           / userData.DBRow["NumPostsForum"].ToType<int>();
            }

            this.Stats.InnerHtml = "{0:N0}<br />[{1} / {2}]".FormatWith(
                userData.DBRow["NumPosts"],
                this.GetTextFormatted("NUMALL", allPosts),
                this.GetTextFormatted(
                    "NUMDAY", (double)userData.DBRow["NumPosts"].ToType<int>() / userData.DBRow["NumDays"].ToType<int>()));
        }

        #endregion

        #region Event Handler

        /// <summary>
        ///     Go to the View Thanks Page
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
        protected void lnk_ViewThanks([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.viewthanks, "u={0}", this.UserId);
        }

        #endregion
    }
}