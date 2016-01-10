#region Using

using System;
using System.Data;
using System.Diagnostics.Contracts;
using FreestyleOnline.classes.Database;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class ReportMessage : Message
    {
        #region Methods

        /// <summary>
        ///     Deletes the message.
        /// </summary>
        /// <param name="id">The music identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public override void DeleteMessage(int id, int userId)
        {
            Db.delete_musictrack_report(id, userId);
        }

        /// <summary>
        ///     Gets the message data source.
        /// </summary>
        /// <returns></returns>
        public override DataTable GetMessageDataSource()
        {
            return Db.get_all_musicreports();
        }

        /// <summary>
        ///     Submits the report message.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="pageContextId">The page context identifier.</param>
        /// <param name="content">The content.</param>
        public void SubmitReportMessage(int id, int pageContextId, string content)
        {
            Contract.Requires<NullReferenceException>(!string.IsNullOrEmpty(content));
            Db.report_musictrack(id, pageContextId, content);
        }

        #endregion
    }
}