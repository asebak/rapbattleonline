#region Using

using System.Data;
using System.Diagnostics.Contracts;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class News : BaseNews
    {
        #region Methods

        /// <summary>
        ///     Gets the data source.
        /// </summary>
        /// <returns></returns>
        public override DataTable GetDataSource()
        {
            return Db.get_newsfeed();
        }

        /// <summary>
        ///     Posts the news.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="content">The content.</param>
        public void PostNews(int userId, string title, string content)
        {
            Contract.Requires(!string.IsNullOrEmpty(content));
            Db.post_newsfeed(userId, title, content);
        }

        /// <summary>
        ///     Deletes the news.
        /// </summary>
        /// <param name="newsId">The news identifier.</param>
        public void DeleteNews(int newsId)
        {
            Db.delete_news(newsId);
        }

        #endregion
    }
}