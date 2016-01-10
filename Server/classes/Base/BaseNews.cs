#region Using

using System.Data;

#endregion

namespace FreestyleOnline.classes.Base
{
    public abstract class BaseNews : RapClass
    {
        #region Methods

        /// <summary>
        ///     Gets the data source.
        /// </summary>
        /// <returns></returns>
        public abstract DataTable GetDataSource();

        #endregion
    }
}