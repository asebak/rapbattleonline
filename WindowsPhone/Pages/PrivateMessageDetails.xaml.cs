using Common.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FreestyleOnline___WP.Classes.CRUD;
using FreestyleOnline___WP.Classes;
using System.Net;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using System;
namespace FreestyleOnline___WP.Pages
{
    public partial class PrivateMessageDetails
    {
        private PMModel _message = new PMModel();
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateMessageDetails"/> class.
        /// </summary>
        public PrivateMessageDetails()
        {
            InitializeComponent();
            this._message = (PMModel)PhoneApplicationService.Current.State["privateMessage"];
            //mark as read
            UiDispatcher.Invoke(() =>
            {
                if (this._message != null)
                {
                    new Put("PrivateMessages", this._message.MessageId, "", null, "Read");
                    this.From.Text = _message.SentBy;
                    this.DateSet.Text = string.Format("{0} {1}", _message.DateSent, "days ago.");
                    this.Subject.Text = _message.Subject;
                    this.Content.Text = _message.Details;
                }
            });
      
            
        }

        ///// <summary>
        ///// Handles the Click event of the Reply control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Reply_Click(object sender, System.EventArgs e)
        {
            PageRouter.Go("Pages/PrivateMessageReply");
        }

        /// <summary>
        /// Handles the Click event of the Delete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Delete_Click(object sender, System.EventArgs e)
        {
           LoadingProgressIndicator.Show(true);
           UiDispatcher.Invoke(() =>
           {
               if (this._message != null)
               {
                   new Delete("PrivateMessages", _message.MessageId, DeleteComplete, "Delete");
               }
           });
        }

        //private void ReplyComplete(

        /// <summary>
        /// Deletes the complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.UploadStringCompletedEventArgs"/> instance containing the event data.</param>
        private void DeleteComplete(object sender, UploadStringCompletedEventArgs e)
        {
            LoadingProgressIndicator.Show(false);
            if (e.Error == null)
            {
                PageRouter.Go("Pages/PrivateMessages");
            }
            else
            {
                UiDispatcher.Invoke(() => MessageBox.Show("Cannot Delete Message", "", MessageBoxButton.OK));
            }
        }

    }
}