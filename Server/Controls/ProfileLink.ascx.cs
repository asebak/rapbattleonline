#region Using

using System;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Types;
using YAF.Types.Interfaces;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     A profile link contain and image of their avatar and a hyperlink to their profile page
    /// </summary>
    public partial class ProfileLink : RapUserControl
    {
        #region Properties

        public int UserId { get; set; }
        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            //corresponds to guest, no profile link should be clickable
            if (this.UserId == 1)
            {
                ProfileImageButton.Disabled = true;
            }
            if (this.UserId == 0)
            {//todo fix this postback issue
                this.UserId = 1;
            }
        }

        /// <summary>
        ///     Handles the DirectClick event of the ProfileImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectEventArgs" /> instance containing the event data.</param>
        protected void ProfileImageButton_DirectClick([NotNull] object sender, [NotNull] DirectEventArgs e)
        {
            this.GetService<UrlProvider>().Redirect("~/Pages/Profile", UserId);
        }

        /// <summary>
        /// Updates the profile image.
        /// </summary>
        public void UpdateProfileImage()
        {
            this.ProfileImageButton.ImageUrl = this.Get<IAvatars>().GetAvatarUrlForUser(this.UserId);
            this.ProfileImageButton.DataBind();
        }
        #endregion
    }
}