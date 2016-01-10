namespace FreestyleOnline.classes.Interfaces
{
    /// <summary>
    ///     Interface for posting comments.
    /// </summary>
    internal interface IRapComment
    {
        #region Methods

        void PostComment(int userId, string comment);
        void DeleteComment(int commentId);

        #endregion
    }
}