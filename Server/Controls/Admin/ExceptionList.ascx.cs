#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using FreestyleOnline.classes.Types.Helpers;
using YAF.Types;
using YAF.Types.Constants;

#endregion

namespace FreestyleOnline.Controls.Admin
{
    public partial class ExceptionList : RapUserControl
    {
        #region Properties

        /// <summary>
        /// Gets the exceptions path.
        /// </summary>
        /// <value>
        /// The exceptions path.
        /// </value>
        [NotNull]
        private string ExceptionsPath
        {
            get { return this.GetService<ResourceProvider>().GetPath(RapResource.Exceptions); }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.ReadExceptionsList();
        }

        /// <summary>
        /// Reads the exceptions list.
        /// </summary>
        private void ReadExceptionsList()
        {
            var exceptionsList = this.GetCore<XmlExceptionParser>().Read(this.ExceptionsPath);
            this.ExceptionsStore.DataSource =
                exceptionsList.OrderByDescending(y => y.Date)
                    .Skip(Math.Max(0, exceptionsList.Count() - 20))
                    .ToList()
                    .ConvertAll(
                        x =>
                            new object[]
                            {
                                x.Message, x.Source, x.Stack, x.Date
                            })
                    .ToArray();
            this.ExceptionsStore.DataBind();
        }

        /// <summary>
        ///     Handles the deletion.
        /// </summary>
        [DirectMethod]
        public void HandleDeletion()
        {
            var selectedModel = this.ExceptionsGrid.GetSelectionModel() as RowSelectionModel;
            if (selectedModel.SelectedRow != null)
            {
                var contentToDelete = selectedModel.SelectedRow.RecordID;
                this.GetCore<XmlExceptionParser>().Delete(contentToDelete, this.ExceptionsPath);
                this.AddLoadMessageSession(this.Text("ADMIN", "EXCEPTION_DELETED"), MessageTypes.Success);
                this.GetService<UrlProvider>().RefreshPage();
            }
            else
            {
                this.AddLoadMessageSession(this.Text("ADMIN", "EXCEPTION_ROW"), MessageTypes.Warning);
                this.GetService<UrlProvider>().RefreshPage();
            }
        }

        #endregion
    }
}