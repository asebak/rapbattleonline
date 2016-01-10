#region Using

using System;

#endregion

namespace FreestyleOnline.classes
{
    /// <summary>
    ///     Generic Carousel Data Class
    /// </summary>
    public class CarouselData
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the image path.
        /// </summary>
        /// <value>
        ///     The image path.
        /// </value>
        public string ImagePath { get; set; }

        /// <summary>
        ///     Gets or sets the caption text.
        /// </summary>
        /// <value>
        ///     The caption text.
        /// </value>
        public string CaptionText { get; set; }

        /// <summary>
        ///     Gets or sets the hyper link.
        /// </summary>
        /// <value>
        ///     The hyper link.
        /// </value>
        public string HyperLink { get; set; }

        /// <summary>
        ///     Gets or sets the total votes.
        /// </summary>
        /// <value>
        ///     The total votes.
        /// </value>
        public int TotalVotes { get; set; }

        /// <summary>
        ///     Gets or sets the rating.
        /// </summary>
        /// <value>
        ///     The rating.
        /// </value>
        public double Rating { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [enable rating].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [enable rating]; otherwise, <c>false</c>.
        /// </value>
        public bool Enable { get; set; }

        /// <summary>
        ///     Gets or sets the expiry date.
        /// </summary>
        /// <value>
        ///     The expiry date.
        /// </value>
        public DateTime ExpiryDate { get; set; }

        #endregion
    }
}