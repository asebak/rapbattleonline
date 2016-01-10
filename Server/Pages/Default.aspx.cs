#region Using

using System;
using Common.Types.Attributes;
using FreestyleOnline.classes.Base;

#endregion

namespace FreestyleOnline.Pages
{
    public partial class Default : RapPage
    {

        #region Methods
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender,[NotNull] EventArgs e)
        {
            //register jCarousel Script
            //this.GetService<ClientProviders>()
            //    .RegisterClientScriptBlock(this, "jCarousel", "~/js/Scripts/jquery.jcarousel.min.js", false, true);
            //register local scripts that use jCarousel
            //this.GetService<ClientProviders>()
            //    .RegisterClientScriptBlock(this, "jCarouselFeatured", "~/js/FeaturedCarousels.js", true);
        }

        #endregion
    }
}