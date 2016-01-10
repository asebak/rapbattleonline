using System;
using FreestyleOnline.classes.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FreestyleOnlineTesting
{
    [TestClass]
    public class MusicUnitTests
    {

        /// <summary>
        ///     Testing if a User's Music track and visiting profile user can vote if they are the same
        /// </summary>
        [TestMethod]
        public void MusicRatingEnabledTest()
        {
            var data = new MusicData();
            const int userId = 10;
            const int pageUserId = 10;
            const int musicId = 1231;
            Assert.AreEqual(false, data.TrackRatingEnabled(userId, pageUserId, musicId));
        }
    }
}
