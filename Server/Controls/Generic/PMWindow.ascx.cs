#region Using

using System;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using YAF.Types;
using MyUserData = FreestyleOnline.classes.Core.UserData;

#endregion

namespace FreestyleOnline.Controls.Generic
{
    public partial class PMWindow : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets the get window.
        /// </summary>
        /// <value>
        ///     The get window.
        /// </value>
        public Window GetWindow
        {
            get { return this.PrivateMsgWindow; }
        }

        /// <summary>
        ///     Gets to.
        /// </summary>
        /// <value>
        ///     To.
        /// </value>
        public TextField To
        {
            get { return this.ToMsg; }
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
        }

        #endregion

        #region Client Handlers

        /// <summary>
        ///     Submits the private message.
        /// </summary>
        [DirectMethod]
        public void SubmitPrivateMessage()
        {
            if (string.IsNullOrEmpty(this.Title.Text) || string.IsNullOrEmpty(this.MessageContent.Text))
            {
                this.GetService<ClientProviders>().DisplayRealTimeError(300, 300, true, "cannot leave empty fields");
                return;
            }
            this.GetService<ClientProviders>()
                .DisplayRealTimeNotification("Success", Icon.TextSubscript, 200, 200, true, "successfully submitted");
            var receiverId = MyUserData.GetUserIdFromDisplayName(this.To.Text);
            Message.SendPmMessage(this.PageContext.PageUserID, receiverId, this.Title.Text, this.MessageContent.Text);
            this.PrivateMsgWindow.Close();
        }

        #endregion
    }
}