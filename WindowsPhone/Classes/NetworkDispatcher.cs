#region Using

using System;
using System.Threading;
using Microsoft.Phone.Net.NetworkInformation;

#endregion

namespace FreestyleOnline___WP.Classes
{
    public class NetworkDispatcher
    {
        /// <summary>
        ///     Determines whether the specified completed is connected.
        /// </summary>
        /// <param name="completed">The completed.</param>
        public static void IsConnected(Action<bool> completed)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                var type = NetworkInterface.NetworkInterfaceType;
                completed(type != NetworkInterfaceType.None);
            });
        }
    }
}