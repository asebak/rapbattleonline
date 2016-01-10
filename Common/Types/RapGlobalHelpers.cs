#region Using

using System;

#endregion

namespace Common.Types
{
    public class RapGlobalHelpers
    {
        #region Properties

        public static string Address
        {
            get { return "http://192.168.2.23:56314/"; }
        }

        public static string LocalHostAddress
        {
            get { return "http://localhost:56314/"; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Determines whether [is date expired] [the specified end date].
        /// </summary>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public static bool IsDateExpired(DateTime endDate)
        {
            return DateTime.Now > endDate;
        }

        #endregion
    }
}