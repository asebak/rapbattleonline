using System.Resources;
using System.Threading;
using Common.Types.Attributes;

namespace RapBattleAudio.classes
{
    public class ResourceHelper
    {
        private static readonly ResourceManager _instance = null;
        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public string GetString([CanBeNull] string propertyName)
        {
            var resourceManager = _instance ?? new ResourceManager("RapBattleAudio.language.english", GetType().Assembly);
            return resourceManager.GetString(propertyName, Thread.CurrentThread.CurrentUICulture);
        }
    }
}
