#region Using

using System;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Featured
{
    /// <summary>
    ///     Creates a Carousel of Random Profiles
    /// </summary>
    [Obsolete("No Need For Control")]
    public partial class RandomProfiles : RapUserControl
    {
        #region Members

        private readonly UserDataRandom _usersRandom = new UserDataRandom();

        #endregion

        #region Properties

        public bool IsNotVisible { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.IsPostBack)
            {
                IsNotVisible = false;
                var dataSource = _usersRandom.GetRandomUsers(this.PageContext.BoardSettings);
                this.randomProfiles.DataSource = dataSource;
                this.randomProfiles.DataBind();
                if (dataSource.Count < 1)
                {
                    IsNotVisible = true;
                }
            }
        }

        #endregion
    }
}