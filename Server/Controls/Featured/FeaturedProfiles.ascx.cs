#region Using

using System;
using System.Linq;
using System.Web.UI.WebControls;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Types;
using FreestyleOnline.classes.Types.UI;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Featured
{
    /// <summary>
    ///     Creates a Carousel of Featured Profiles
    /// </summary>
    public partial class FeaturedProfiles : RapUserControl
    {
        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var featuredUsers = this.GetCore<UserDataFeatured>().GetFeaturedUsers(this.PageContext.BoardSettings);
            if (featuredUsers.Any())
            {
                featuredUsers.Shuffle();
                this.FeaturedUsers.UserId = featuredUsers[0].UserId;
                this.UsersBio.Text = new UserProfile(featuredUsers[0].UserId).GetBio();
            }
            else
            {
                var noData = new Label
                {
                    Text = this.Text("DEFAULT", "NOFEATURED_USERS")
                };
                this.FeaturedUsers.Visible = false;
                this.NoFeaturedUsers.Controls.Add(noData);
            }
        }

        #endregion
    }
}