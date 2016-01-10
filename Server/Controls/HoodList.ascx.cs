#region Using

using System;
using System.Linq;
using System.Web.UI.WebControls;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class HoodList : RapUserControl
    {
        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.HoodListPager.PerPage = 10;
            this.HoodListPager.GridView = this.HoodGridView;
            this.HoodListPager.ListDs = this.GetCore<HoodData>().GetAllHoods().Cast<object>().ToList();
        }

        /// <summary>
        ///     Handles the RowDataBound event of the HoodGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs" /> instance containing the event data.</param>
        protected void HoodGridView_RowDataBound([NotNull] object sender, [NotNull] GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            var hoodBox = (HoodBox) e.Row.FindControl("HoodBox");
            hoodBox.Hood = (HoodData) e.Row.DataItem;
            hoodBox.BindDataSource();
        }

        #endregion
    }
}