#region Using

using System;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     MainMenu Navigation
    /// </summary>
    public partial class NavigationMenu : RapUserControl
    {
        #region Methods
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.GetService<ClientProviders>()
              .RegisterClientScriptBlock(this, "navbar", "~/js/Navbar.js", false, true);
        }
        #endregion
    }
}