#region Using

using System;
using System.Windows;
using FreestyleOnline___WP.Classes;
using FreestyleOnline___WP.Resources;
using Microsoft.Phone.Tasks;

#endregion

namespace FreestyleOnline___WP.Pages
{
    public partial class MainPage
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            this.HandleNetworkConnection();
            this.Copyright.Text = string.Format("{0} 2013 - {1}, RapBattleOnline.com", AppResources.CopyrightNotice,
                DateTime.Now.Year);
        }

        /// <summary>
        ///     Handles the network connection.
        /// </summary>
        private void HandleNetworkConnection()
        {
            NetworkDispatcher.IsConnected(connected =>
            {
                if (!connected)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(delegate
                    {
                        var m = MessageBox.Show(AppResources.NetworkRequired, "", MessageBoxButton.OKCancel);
                        switch (m)
                        {
                            case MessageBoxResult.OK:
                            {
                                var cTask = new ConnectionSettingsTask
                                {
                                    ConnectionSettingsType = ConnectionSettingsType.WiFi
                                };
                                cTask.Show();
                            }
                                break;
                            case MessageBoxResult.Cancel:
                                Application.Current.Terminate();
                                break;
                        }
                    });
                }
            });
        }

        ///// <summary>
        ///// Authentications the handler.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The <see cref="System.Net.DownloadStringCompletedEventArgs"/> instance containing the event data.</param>
        //private void authenticationHandler(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    var authenticated = JsonConvert.DeserializeObject<object>(e.Result);
        //    //if (authenticated)
        //    //{
        //    //    PageRouter.Go("Pages/MainLoggedIn");
        //    //}
        //}

        #endregion
    }
}