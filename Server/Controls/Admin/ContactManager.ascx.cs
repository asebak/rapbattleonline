#region Using

using System;
using System.Diagnostics.Contracts;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using Button = Ext.Net.Button;

#endregion

namespace FreestyleOnline.Controls.Admin
{
    public partial class ContactManager : RapUserControl
    {
        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var contactMsgs = this.GetCore<ContactMessage>().GetMessageDataSource();
            if (contactMsgs.Rows.Count <= 0)
            {
                var NoData = this.GetCore<CalloutBox>()
                    .Create(BootstrapElementType.Info, this.Text("ADMIN", "CONTACT_NONE"));
                this.NoContactMsgs.Controls.Add(NoData);
            }
            else
            {
                this.ContactManagerRepeater.DataSource = contactMsgs;
                this.ContactManagerRepeater.DataBind();
            }
        }

        #endregion

        #region EventHandlers

        /// <summary>
        ///     Handles the Command event of the RespondButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void RespondButton_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var commandEventArgs = e.CommandArgument.ToString().Split(';');
            Contract.Assert(commandEventArgs.Length == 3);
            var contactId = Convert.ToInt32(commandEventArgs[0]);
            var userId = Convert.ToInt32(commandEventArgs[1]);
            var rowIndex = Convert.ToInt32(commandEventArgs[2]);
            var textArea = (HtmlTextArea) this.ContactManagerRepeater.Items[rowIndex].FindControl("ContactMsg");
            this.GetCore<ContactMessage>().DeleteMessage(contactId, userId);
            Message.SendPmMessage(this.PageContext.PageUserID, userId, this.Text("COMMON", "COMMON_CONTACTUS"),
                textArea.Value);
            this.AddLoadMessageSession(this.Text("ADMIN", "CONTACTMANAGER_RESPONDSUCCESS"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        /// <summary>
        ///     Handles the Command event of the IgnoreButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void IgnoreButton_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var contactId = e.CommandArgument.ToString();
            this.GetCore<ContactMessage>().DeleteMessage(Convert.ToInt32(contactId), 0);
            this.AddLoadMessageSession(this.Text("ADMIN", "CONTACTMANAGER_IGNORE"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        /// <summary>
        ///     Handles the ItemDataBound event of the ContactManagerID control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs" /> instance containing the event data.</param>
        protected void ContactManagerID_ItemDataBound([NotNull] object sender, [NotNull] RepeaterItemEventArgs e)
        {
            var respondButton = (Button) e.Item.FindControl("RespondButton");
            var ignoreButton = (Button) e.Item.FindControl("IgnoreButton");
            respondButton.CommandArgument = string.Format("{0};{1};{2}", DataBinder.Eval(e.Item.DataItem, "ContactID"),
                DataBinder.Eval(e.Item.DataItem, "UserID"),
                e.Item.ItemIndex);
            respondButton.OnClientClick += "return handlepostback({0});".FormatWith(respondButton.ClientID);
            ignoreButton.OnClientClick += "return handlepostback({0});".FormatWith(ignoreButton.ClientID);
        }

        #endregion
    }
}