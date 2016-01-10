#region Using

using System.ComponentModel;

#endregion

namespace FreestyleOnline___WP.Classes
{
    public static class ProxyHandler
    {
        #region Methods

        /// <summary>
        ///     Processes the specified result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The <see cref="AsyncCompletedEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        public static T Process<T>(AsyncCompletedEventArgs result)
        {
            if (!result.Cancelled)
            {
                return (T) ((dynamic) result).Result;
            }
            return default(T);
        }

        #endregion
    }
}