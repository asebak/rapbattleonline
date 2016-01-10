#region Using

using YAF.Classes;
using YAF.Types;

#endregion

namespace FreestyleOnline.classes.Providers
{
    public class ApplicationProvider : BaseRapProvider
    {
        #region Methods

        /// <summary>
        ///     Gets the application settings from app.config .
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetApplicationSettings([CanBeNull] string key)
        {
            return Config.GetConfigValueAsString(key);
        }

        #endregion
    }
}