#region Using

using System.Data;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Interfaces;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class HoodComments : RapClass, IRapComment
    {
        #region Members

        private readonly int _hoodId;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="HoodComments" /> class.
        /// </summary>
        /// <param name="hoodId">The hood identifier.</param>
        public HoodComments(int hoodId)
        {
            _hoodId = hoodId;
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
            Db.post_hoodcomment(_hoodId, userId, comment);
        }

        /// <summary>
        ///     Deletes the comment.
        /// </summary>
        /// <param name="hoodCommentId">The hood comment identifier.</param>
        public void DeleteComment(int hoodCommentId)
        {
        }

        /// <summary>
        ///     Gets the hood comments.
        /// </summary>
        /// <returns></returns>
        public DataTable GetHoodComments()
        {
            return Db.get_hood_comments(_hoodId);
        }

        #endregion
    }
}