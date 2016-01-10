#region Using

using FreestyleOnline___WP.Resources;

#endregion

namespace FreestyleOnline___WP.Classes
{
    /// <summary>
    ///     Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        #region Members

        /// <summary>
        ///     The _localized resources
        /// </summary>
        private static readonly AppResources _localizedResources = new AppResources();

        #endregion

        #region Constructor

        /// <summary>
        ///     Gets the localized resources.
        /// </summary>
        /// <value>
        ///     The localized resources.
        /// </value>
        public AppResources LocalizedResources
        {
            get { return _localizedResources; }
        }

        #endregion
    }
}