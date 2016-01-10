#region Using

using System.Net;
using System.Windows;
using Common.Models;
using FreestyleOnline___WP.Classes;
using FreestyleOnline___WP.Classes.CRUD;
using FreestyleOnline___WP.Resources;

#endregion

namespace FreestyleOnline___WP.Controls
{
    public partial class Register
    {
        #region Methods

        /// <summary>
        ///     Initializes a new instance of the <see cref="Register" /> class.
        /// </summary>
        public Register()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the GetRegistrationRulesCompleted event of the _proxy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            PageRouter.Back();
        }

        /// <summary>
        ///     Handles the 1 event of the Button_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            LoadingProgressIndicator.Show(true);
            new Post("Register",
                new RegisterModel
                {
                    Username = this.UserName.Text,
                    Password = this.Password.Password,
                    Email = this.Email.Text
                }, registrationComplete);
        }

        private void registrationComplete(object sender, UploadStringCompletedEventArgs e)
        {
            LoadingProgressIndicator.Show(false);
            var registerSuccess = (e.Error == null);
            MessageBox.Show(registerSuccess ? AppResources.RegisterSuccess : AppResources.RegisterFailure, "",
                MessageBoxButton.OK);
        }

        #endregion
    }
}