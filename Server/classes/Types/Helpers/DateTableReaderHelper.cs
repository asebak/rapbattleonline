#region Using

using System.Data;

#endregion

namespace FreestyleOnline.classes.Types.Helpers
{
    public static class DateTableReaderHelper
    {
        #region Methods

        /// <summary>
        ///     Reads the columns.
        /// </summary>
        /// <param name="dt">The dt.</param>
        public static void ReadColumns(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    var name = column.ColumnName;
                    var data = row[column].ToString();
                }
            }
        }

        #endregion
    }
}