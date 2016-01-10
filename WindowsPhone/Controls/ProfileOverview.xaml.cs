#region Using

using System.Net;
using System.Windows.Controls;
using Common.Models;
using FreestyleOnline___WP.Classes.CRUD;
using FreestyleOnline___WP.Classes.UI;
using Newtonsoft.Json;

#endregion

namespace FreestyleOnline___WP.Controls
{
    public partial class ProfileOverview
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ProfileOverview" /> class.
        /// </summary>
        public ProfileOverview()
        {
            InitializeComponent();
            this.GetProfileContext();
        }

        #region Methods

        /// <summary>
        ///     Gets the profile context.
        /// </summary>
        private void GetProfileContext()
        {
            new Get("Profile", UserId, ProfileContextComplete, "GetProfileContext");
        }

        /// <summary>
        ///     Profiles the context complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DownloadStringCompletedEventArgs" /> instance containing the event data.</param>
        private void ProfileContextComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            var profileContext = JsonConvert.DeserializeObject<ProfileModel>(e.Result);
            this.DataContext = new ProfileNotifier(profileContext);
        }

        #endregion

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int UserId { get; set; }
    }
}