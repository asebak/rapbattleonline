#region Using

using System;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Interfaces;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class UserComments : RapClass, IRapComment
    {
        #region Members

        private readonly int _pageUserId;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserComments" /> class.
        /// </summary>
        public UserComments()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserComments" /> class.
        /// </summary>
        /// <param name="pageUserId">The pageUser identifier.</param>
        public UserComments(int pageUserId)
        {
            this._pageUserId = pageUserId;
        }

        #endregion

        #region Properties

        public string DisplayName { get; set; }
        public string Comment { get; set; }
        public string DatePosted { get; set; }
        public string Avatar { get; set; }
        public string HyperLink { get; set; }

        #endregion

        #region IRapComment Members

        /// <summary>
        ///     Posts the comment.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void PostComment(int userId, string comment)
        {
            Db.post_profilecomment(userId, this._pageUserId, comment);
        }

        /// <summary>
        ///     Deletes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DeleteComment(int commentId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}