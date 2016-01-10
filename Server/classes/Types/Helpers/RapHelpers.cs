#region Using

using System;
using System.Configuration;

#endregion

namespace FreestyleOnline.classes.Types.Helpers
{
    public static class RapHelpers
    {
        #region Properties

        /// <summary>
        ///     Gets the connection.
        /// </summary>
        /// <value>
        ///     The connection.
        /// </value>
        public static string DataBaseConnection
        {
            get { return ConfigurationManager.ConnectionStrings["rapbattle"].ToString(); }
        }

        /// <summary>
        ///     Gets the rap battle audio folder.
        /// </summary>
        /// <value>
        ///     The rap battle audio folder.
        /// </value>
        [Obsolete("Use Resource Provider")]
        public static string RapBattleAudioFolder
        {
            get { return @"Uploads/RapBattleAudio"; }
        }

        #endregion

        #region Methods

        #endregion
    }
}