using System.Collections.Generic;
using System.Net;
using System.Windows.Controls;
using Common.Models;
using FreestyleOnline___WP.Classes;
using FreestyleOnline___WP.Classes.CRUD;
using FreestyleOnline___WP.Classes.UI;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using Coding4Fun.Toolkit.Controls.Converters;
namespace FreestyleOnline___WP.Pages
{
    public partial class PrivateMessages
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateMessages"/> class.
        /// </summary>
        public PrivateMessages()
        {
            InitializeComponent();
            this.GetPrivateMessages();
        }

        /// <summary>
        /// Called when the hardware Back button is pressed.
        /// </summary>
        /// <param name="e">Set e.Cancel to true to indicate that the request was handled by the application.</param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            this.GetPrivateMessages();
            base.OnBackKeyPress(e);
        }

        /// <summary>
        /// Gets the private messages.
        /// </summary>
        private void GetPrivateMessages()
        {
            LoadingProgressIndicator.Show(true);
            new Get("PrivateMessages", this.PrivateMessagesComplete);
        }
        /// <summary>
        /// Privates the messages complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DownloadStringCompletedEventArgs"/> instance containing the event data.</param>
        private void PrivateMessagesComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            var privateMessages = JsonConvert.DeserializeObject<List<PMModel>>(e.Result);
            this.PrivateMsgsList.ItemsSource = new PrivateMessagesNotifier(privateMessages);
            LoadingProgressIndicator.Show(false);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the PrivateMsgsList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void PrivateMsgsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var m = (sender as ListBox).SelectedItem as PMModel;
            PhoneApplicationService.Current.State["privateMessage"] = m;
            PageRouter.Go("Pages/PrivateMessageDetails");
        }

        private void NewMessage_Click(object sender, System.EventArgs e)
        {
            PhoneApplicationService.Current.State["newPm"] = 1;
            PageRouter.Go("Pages/PrivateMessageReply");
        }
    }
}