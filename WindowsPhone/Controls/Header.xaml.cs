#region Using

using System.Windows;
using System.Windows.Controls;
using Common.Types.Attributes;
using FreestyleOnline___WP.Classes;
using FreestyleOnline___WP.Classes.UI;

#endregion

namespace FreestyleOnline___WP.Controls
{
    public partial class Header
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        public Header()
        {
            InitializeComponent();
            this.DataContext = new HeaderNotifier();
        }

        #endregion


        #region EventHandlers

        /// <summary>
        /// Handles the Click event of the Messages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Messages_Click([NotNull] object sender,[NotNull] RoutedEventArgs e)
        {
            PageRouter.Go("Pages/PrivateMessages");
        }

        #endregion

    }
}