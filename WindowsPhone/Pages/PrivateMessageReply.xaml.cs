using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FreestyleOnline___WP.Classes;
using FreestyleOnline___WP.Classes.CRUD;
using Common.Models;
using FreestyleOnline___WP.Classes.UI;
namespace FreestyleOnline___WP.Pages
{
    public partial class PrivateMessageReply
    {
        private PMModel _message = new PMModel();

        public PrivateMessageReply()
        {
            InitializeComponent();
            //for reply
            if (!PhoneApplicationService.Current.State.ContainsKey("newPm"))
            {
                this._message = (PMModel)PhoneApplicationService.Current.State["privateMessage"];
                if (!this._message.Subject.Contains("Re:"))
                {
                    this._message.Subject = string.Format("Re: {0}", this._message.Subject);
                }
                //quote message
                this._message.Details = string.Format("'{0}'", this._message.Details);
                this.Subject.IsEnabled = false;
                this.To.IsEnabled = false;
            }
            //new message
            else
            {
                this._message = new PMModel { SentBy = WindowsPhoneContext.Current.Profile.UserName};
                PhoneApplicationService.Current.State.Remove("newPm");
            }
            this.DataContext = new PrivateMessageReplyNotifier(this._message);
        }

        /// <summary>
        /// Handles the Click event of the Send control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Send_Click(object sender, EventArgs e)
        {
            this.Focus();
            var m = this.DataContext as PrivateMessageReplyNotifier;
            LoadingProgressIndicator.Show(true);
            new Post("PrivateMessages", new PMModel {SentBy = m.From, Subject = m.Subject, To = m.To, Details = this.Content.Text, MessageId = _message.MessageId }, SendPMComplete);
        }

        /// <summary>
        /// Sends the pm complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="UploadStringCompletedEventArgs"/> instance containing the event data.</param>
        private void SendPMComplete(object sender, UploadStringCompletedEventArgs e)
        {
            LoadingProgressIndicator.Show(false);
            if (e.Error == null)
            {
                PageRouter.Go("Pages/PrivateMessages");
            }
            else
            {
                MessageBox.Show("Message couldn't be sent", "", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Handles the Click event of the Cancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            PageRouter.Back();
        }
    }
}