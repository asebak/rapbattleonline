#region Using

using System;
using System.Web.UI.WebControls;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Core;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Featured
{
    [Obsolete("No Need For Control")]
    public partial class RandomHoods : RapUserControl
    {
        #region Members

        public bool IsNotVisible { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            IsNotVisible = false;
            var dataSource = this.GetCore<HoodData>().GetRandomHoods();
            this.randomHoods.DataSource = dataSource;
            this.randomHoods.DataBind();
            if (dataSource.Count < 1)
            {
                IsNotVisible = true;
            }
        }

        /// <summary>
        ///     Handles the ItemDataBound event of the randomHoods control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
        protected void randomHoods_ItemDataBound([NotNull]object sender, [NotNull] RepeaterItemEventArgs e)
        {
            var randomHoodBox = (HoodBox)e.Item.FindControl("RandomHoodBox");
            randomHoodBox.Hood = (HoodData)e.Item.DataItem;
            randomHoodBox.BindDataSource();
        }

        #endregion
    }
}