#region Using

using System;
using FreestyleOnline.classes.Base;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Generic
{
    /// <summary>
    ///     Generic Header to be displayed above custom controls
    /// </summary>
    public partial class Header : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        /// <value>
        ///     The icon.
        /// </value>
        public string Icon { get; set; }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string TextToDisplay { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.HeaderIcon.ImageUrl = Icon;
            this.HeaderText.Text = TextToDisplay;
        }

        #endregion
    }
}