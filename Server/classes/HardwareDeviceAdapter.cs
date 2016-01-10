#region Using

using System;
using System.Linq;
using System.Web;
using YAF.Classes.Pattern;

#endregion

namespace FreestyleOnline.classes
{
    public class HardwareDeviceAdapter
    {
        public readonly string[] MobileDevices =
        {
            "iPhone", "iPad", "iPod", "BlackBerry",
            "Nokia", "Android", "WindowsPhone",
            "Mobile"
        };

        /// <summary>
        ///     Gets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        public static HardwareDeviceAdapter Current
        {
            get { return PageSingleton<HardwareDeviceAdapter>.Instance; }
        }

        /// <summary>
        ///     Gets the type of Mobile device
        /// </summary>
        /// <returns></returns>
        public string Type()
        {
            return
                MobileDevices.FirstOrDefault(
                    MobileDeviceName =>
                        (HttpContext.Current.Request.UserAgent.IndexOf(MobileDeviceName,
                            StringComparison.OrdinalIgnoreCase)) > 0);
        }
    }
}