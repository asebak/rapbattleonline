#region Using

using System;
using FreestyleOnline.classes.Types;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class RapSocialFeedItem
    {
        #region Properties

        public int FeedId { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Urlpath { get; set; }
        public DateTime Time { get; set; }
        public bool IsDeleted { get; set; }
        public RapSocialFeedType Type { get; set; }
        public int ObjectId { get; set; }

        #endregion
    }
}