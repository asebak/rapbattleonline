namespace FreestyleOnline.classes.Interfaces
{
    /// <summary>
    ///     Interface for config
    /// </summary>
    internal interface IRapConfigFactory<out TOut, in TIn>
    {
        #region Methods

        TOut Build(TIn config);

        #endregion
    }
}