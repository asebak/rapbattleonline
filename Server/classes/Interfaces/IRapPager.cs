namespace FreestyleOnline.classes.Interfaces
{
    /// <summary>
    ///     Interface for paging datasources
    /// </summary>
    internal interface IRapPager
    {
        #region Methods

        void OnLoad();
        void PageData();

        #endregion
    }
}