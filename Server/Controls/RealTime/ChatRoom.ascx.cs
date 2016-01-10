#region Using

using System;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.RealTime
{
    public partial class ChatRoom : RapUserControl
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
                .RegisterClientScriptBlock(this, "alertify", "~/js/Scripts/alertify.min.js", false, true);
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "displayNameToIdJs", "~/js/DisplayNameToID.js", true);
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "rapChatRoomApp", "~/js/realtime/RapChatRoom-App.js", true);
        }

        #endregion
    }
}