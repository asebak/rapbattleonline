using System;
using System.IO;
using System.Security.Principal;
using System.Web;
using FreestyleOnline.classes.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FreestyleOnlineTesting
{
    [TestClass]
    public class HoodUnitTests
    {
        /// <summary>
        /// Initializes test method, needs to mock httpcontext
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            HttpContext.Current = new HttpContext(new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter()));
        }
        /// <summary>
        ///     Testing a valid hood name
        /// </summary>
        [TestMethod]
        public void ValidHoodNameTest()
        {
            var noHoodName = new HoodData {Name = ""};
            Assert.AreEqual(false, noHoodName.ValidHood(noHoodName));
            var nullHood = new HoodData();
            Assert.AreEqual(false, nullHood.ValidHood(nullHood));
        }
    }
}
