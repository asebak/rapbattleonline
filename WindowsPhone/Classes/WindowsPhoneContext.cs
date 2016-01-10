#region Using

using Common.Models;
using Microsoft.Phone.Net.NetworkInformation;

#endregion

namespace FreestyleOnline___WP.Classes
{
    public class WindowsPhoneContext
    {
        /// <summary>
        ///     The _instance
        /// </summary>
        private static WindowsPhoneContext _instance;

        /// <summary>
        ///     Prevents a default instance of the <see cref="WindowsPhoneContext" /> class from being created.
        /// </summary>
        private WindowsPhoneContext()
        {
        }

        /// <summary>
        ///     Gets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        public static WindowsPhoneContext Current
        {
            get { return _instance ?? (_instance = new WindowsPhoneContext()); }
        }

        public bool IsAuthenticated { get; set; }
        public ProfileModel Profile { get; set; }

        public bool HasInternet
        {
            get { return (NetworkInterface.NetworkInterfaceType != NetworkInterfaceType.None); }
        }
    }
}