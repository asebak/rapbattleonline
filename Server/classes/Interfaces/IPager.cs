namespace FreestyleOnline.classes.Interfaces
{
    /// <summary>
    ///     Interface for paging datasources
    /// </summary>
    internal interface IPagedDataSource
    {
        #region Methods

        void OnLoad();
        void PageData();

        #endregion
    }
}