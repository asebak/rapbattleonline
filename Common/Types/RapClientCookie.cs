#region Using

using System.Net;

#endregion

namespace Common.Types
{
    public class RapClientCookie
    {
        #region Members

        private static CookieContainer _instance;

        #endregion

        #region Constructor

        private RapClientCookie()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        public static CookieContainer Current
        {
            get { return _instance ?? (_instance = new CookieContainer()); }
        }

        #endregion
    }
}