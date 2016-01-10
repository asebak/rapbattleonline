#region Using

using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using YAF.Core;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     Displays a collection of MusicItems from a User
    /// </summary>
    public partial class MusicList : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     UserID for whom to display the music list
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Gets the music count.
        /// </summary>
        /// <value>
        /// The music count.
        /// </value>
        public int MusicCount
        {
            get
            {
                return this.MusicListPager.Dt.Rows.Count;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "musicList","~/js/MusicList.js", true);
            UserMembershipHelper.GetDisplayNameFromID(this.UserId);
            this.MusicListPager.Dt = this.GetCore<UserData>().GetUsersMusicDataSource(UserId);
            this.MusicListPager.Repeater = this.MusicTracks;
        }
        #endregion
    }
}