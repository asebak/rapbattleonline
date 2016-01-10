using System;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using YAF.Types;
using YAF.Types.Extensions;

namespace FreestyleOnline.Controls
{
    public partial class WorkItemReportingForm : RapUserControl
    {

        #region Properties

        /// <summary>
        /// Gets the get window.
        /// </summary>
        /// <value>
        /// The get window.
        /// </value>
        public Window GetWindow
        {
            get { return this.WorkReporterWindow; }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {

        }

        #endregion


        #region Client Handlers

        /// <summary>
        /// Submits the bug report.
        /// </summary>
        [DirectMethod]
        public void SubmitBugReport()
        {

            if (this.TitleMsg.Text.IsNotSet() || this.MessageContent.Text.IsNotSet())
            {
                this.GetService<ClientProviders>()
                    .DisplayRealTimeError(300, 100, true, this.Text("BUG", "REPORT_FAILED"), this.GetWindow);
            }
            else
            {
                this.SubmitBtn.Disabled = true;
                this.SubmitBtn.Text = this.Text("COMMON", "COMMON_PROCESSING");
                this.GetCore<WorkItemReporter>().ReportBug(this.TitleMsg.Text, this.MessageContent.Text);
                this.GetService<ClientProviders>()
                    .DisplayRealTimeNotification(null, Icon.Accept, 300, 100, true, this.Text("BUG", "REPORT_SUCCESS"),
                        this.GetWindow);
                this.SubmitBtn.Text = this.Text("COMMON", "COMMON_SUBMIT");
                this.SubmitBtn.Disabled = false;
                this.GetWindow.Close();
            }
        }
    }

    #endregion

}