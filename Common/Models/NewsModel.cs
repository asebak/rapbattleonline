#region Using

using System.Collections.Generic;

#endregion

namespace Common.Models
{
    public class NewsModel
    {
        #region Properties

        public int NewsId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Comments { get; set; }

        #endregion
    }
}