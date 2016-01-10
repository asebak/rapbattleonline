#region Using

using System.Data;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Interfaces;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class NewsCommentsList : BaseNews, IRapComment
    {
        #region Members

        /// <summary>
        ///     The news identifier
        /// </summary>
        public int NewsId { get; set; }

        #endregion

        #region Constructor

        public NewsCommentsList(int id)
        {
            this.NewsId = id;
        }


        public NewsCommentsList()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Posts the comment.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="comment">The comment.</param>
        public void PostComment(int userId, string comment)
        {
            Db.post_newscomment(userId, this.NewsId, comment);
        }

        /// <summary>
        ///     Deletes the comment.
        /// </summary>
        /// <param name="newsCommentId">The news comment identifier.</param>
        public void DeleteComment(int newsCommentId)
        {
        }

        /// <summary>
        ///     Gets the data source.
        /// </summary>
        /// <returns></returns>
        public override DataTable GetDataSource()
        {
            return Db.get_newsfeed_comments(this.NewsId);
        }

        #endregion
    }
}