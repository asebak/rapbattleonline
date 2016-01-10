#region Using

using System;
using System.Net;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using YAF.Classes.Pattern;
using YAF.Types;

#endregion

namespace FreestyleOnline.classes.Services
{
    public class TfsConnector
    {
        #region Properties

        /// <summary>
        ///     Gets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        [NotNull]
        public static TfsConnector Current
        {
            get { return PageSingleton<TfsConnector>.Instance; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates the work item.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <param name="taskDescription">The task description.</param>
        public void CreateWorkItem(string taskName, string taskDescription)
        {
            var teamUrl = new Uri("https://atsebak.visualstudio.com/defaultcollection/");
            var netCred = new NetworkCredential("", "");
            var basicCred = new BasicAuthCredential(netCred);
            var tfsCred = new TfsClientCredentials(basicCred) {AllowInteractive = false};
            var tpc = new TfsTeamProjectCollection(teamUrl, tfsCred);
            tpc.Authenticate();
            var workItemStore = tpc.GetService<WorkItemStore>();
            var WorkItemType = workItemStore.Projects["Freestyle Online"].WorkItemTypes["Bug"];
            var workItem = WorkItemType.NewWorkItem();
            workItem.Fields["Title"].Value = taskName;
            workItem.Fields["Repro Steps"].Value = taskDescription;
            workItem.Save();
        }

        #endregion
    }
}