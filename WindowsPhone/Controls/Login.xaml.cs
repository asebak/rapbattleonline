#region Using

using System.Net;
using System.Windows;
using System.Windows.Input;
using Common.Models;
using FreestyleOnline___WP.Classes;
using FreestyleOnline___WP.Classes.CRUD;
using FreestyleOnline___WP.Resources;
using Newtonsoft.Json;

#endregion

namespace FreestyleOnline___WP.Controls
{
    public partial class Login
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Login" /> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            new Post("Login",
                new LoginModel {Username = this.UserNameText.Text, Password = this.PasswordText.Password}, LoginComplete);
            LoadingProgressIndicator.Show(true);
            this.LoginBtn.IsEnabled = false;
        }

        /// <summary>
        /// Logins the complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="UploadStringCompletedEventArgs"/> instance containing the event data.</param>
        private void LoginComplete(object sender, UploadStringCompletedEventArgs e)
        {
            LoadingProgressIndicator.Show(false);
            this.LoginBtn.IsEnabled = true;
            //Login Success
            if (e.Error == null)
            {
                WindowsPhoneContext.Current.Profile = JsonConvert.DeserializeObject<ProfileModel>(e.Result);
                this.RedirecToLoggedInPage();
            }
                //Login Failed
            else
            {
                UiDispatcher.Invoke(() => MessageBox.Show(AppResources.LoginFailure, "", MessageBoxButton.OK));
            }
        }

        /// <summary>
        ///     Redirecs to logged in page.
        /// </summary>
        private void RedirecToLoggedInPage()
        {
            PageRouter.Go("Pages/MainLoggedIn");
        }

        /// <summary>
        ///     Handles the Click event of the Register control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            PageRouter.Go("Pages/RegisterPage");
        }


        /// <summary>
        ///     Handles the Tap event of the PasswordText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GestureEventArgs" /> instance containing the event data.</param>
        private void PasswordText_Tap(object sender, GestureEventArgs e)
        {
            this.PasswordText.Password = "";
        }

        /// <summary>
        ///     Handles the Click event of the ForgotPwd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void ForgotPwd_Click(object sender, RoutedEventArgs e)
        {
            PageRouter.Go("Pages/ForgotPasswordPage");
        }

        #endregion
    }
}