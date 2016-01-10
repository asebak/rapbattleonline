#region Using

using System;
using System.Windows.Navigation;

#endregion

namespace FreestyleOnline___WP.Pages
{
    public partial class Profile
    {
        public Profile()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var queryString = Uri.UnescapeDataString(NavigationContext.QueryString["userId"]);
            this.UserProfile.UserId = Convert.ToInt32(queryString);
        }
    }
}