#region Using

using System;
using System.Diagnostics.Contracts;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Controls.Admin
{
    public partial class ReportManager : RapUserControl
    {
        #region Methods
        protected override void OnInit(EventArgs e)
        {
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
            var reportsList = this.GetCore<ReportMessage>().GetMessageDataSource();
            this.ReportManagerPager.Dt = reportsList;
            this.ReportManagerPager.Repeater = this.ReportManagerRepeater;
            this.ReportManagerPager.PerPage = 20;
            if (reportsList.Rows.Count <= 0)
            {
                var NoData = this.GetCore<CalloutBox>()
                    .Create(BootstrapElementType.Info, this.Text("ADMIN", "REPORTS_NONE"));
                this.NoReports.Controls.Add(NoData);
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Command event of the DeclineButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void DeclineButton_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var commandEventArgs = e.CommandArgument.ToString().Split(';');
            Contract.Assert(commandEventArgs.Length == 3);
            var musicId = Convert.ToInt32(commandEventArgs[1]);
            var userId = Convert.ToInt32(commandEventArgs[2]);
            this.GetCore<ReportMessage>().DeleteMessage(musicId, userId);
            this.AddLoadMessageSession(this.Text("ADMIN", "REPORT_DECLINED"), MessageTypes.Warning);
            this.GetService<UrlProvider>().RefreshPage();
        }

        /// <summary>
        ///     Handles the Command event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void DeleteButton_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var commandEventArgs = e.CommandArgument.ToString().Split(';');
            Contract.Assert(commandEventArgs.Length == 3);
            var musicId = Convert.ToInt32(commandEventArgs[1]);
            var userId = Convert.ToInt32(commandEventArgs[2]);
            this.GetCore<ReportMessage>().DeleteMessage(musicId, userId);
            this.GetCore<MusicData>().DeleteMusicTrack(musicId);
            this.AddLoadMessageSession(this.Text("ADMIN", "REPORT_ACCEPTED"), MessageTypes.Warning);
            this.GetService<UrlProvider>().RefreshPage();
        }

        #endregion

        /// <summary>
        /// Handles the ItemDataBound event of the ReportManagerRepeater control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void ReportManagerRepeater_ItemDataBound([NotNull] object sender,[NotNull] RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var button1 = (Ext.Net.Button)e.Item.FindControl("DeleteButton");
                var button2 = (Ext.Net.Button)e.Item.FindControl("DeclineButton");
                button1.OnClientClick += "return handlepostback({0});".FormatWith(button1.ClientID);
                button2.OnClientClick += "return handlepostback({0});".FormatWith(button2.ClientID);
            }
        }
    }
}