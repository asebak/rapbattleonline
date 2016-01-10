#region Using

using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Services;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class WorkItemReporter : RapClass
    {
        #region Methods

        /// <summary>
        ///     Reports the bug.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        public void ReportBug(string title, string description)
        {
            TfsConnector.Current.CreateWorkItem(title, description);
        }

        #endregion
    }
}