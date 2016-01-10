#region Using

using Microsoft.Phone.Shell;
using FreestyleOnline___WP.Resources;

#endregion

namespace FreestyleOnline___WP.Classes
{
    public class LoadingProgressIndicator
    {
        /// <summary>
        ///     Shows the specified value.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void Show(bool value)
        {
            UiDispatcher.Invoke(() =>
            {
                if (SystemTray.ProgressIndicator == null)
                {
                    SystemTray.ProgressIndicator = new ProgressIndicator();
                }
                SystemTray.ProgressIndicator.Text = AppResources.Loading;
                SystemTray.ProgressIndicator.IsIndeterminate = value;
                SystemTray.ProgressIndicator.IsVisible = value;
            });
        }
    }
}