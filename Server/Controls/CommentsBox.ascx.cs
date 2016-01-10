#region Using

using System;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Types;
#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     Creates a Commenting Box structure
    /// </summary>
    public partial class CommentsBox : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     The UserID To be Shown As a DisplayName
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     The Content of the Comment
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     The Date of the Comment
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     The Title For the Comment [Required Only For News Items]
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     The Hyperlink for a Title [Required Only For News Items]
        /// </summary>
        public string TitleHyperLink { get; set; }

        /// <summary>
        /// Gets or sets the news feed identifier.
        /// </summary>
        /// <value>
        /// The news feed identifier.
        /// </value>
        public int CommentsCount { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
        }

        #endregion
    }
}