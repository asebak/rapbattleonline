using System;
using Common.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FreestyleOnlineTesting
{
    [TestClass]
    public class DateTimeUnitTests
    {
        /// <summary>
        ///     Testing expiry date
        /// </summary>
        [TestMethod]
        public void DateExpiredTest()
        {
            var d = new DateTime(2001, 06, 10);
            Assert.AreEqual(true, RapGlobalHelpers.IsDateExpired(d));
        }
    }
}
