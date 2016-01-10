#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Interfaces;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls.Generic
{
    /// <summary>
    ///     Generic Paginator for controls Implements for Both GridView and Repeater; For both a List and SQL DS
    /// </summary>
    public partial class Pager : RapUserControl
    {
        #region Members

        /// <summary>
        ///     The interface data source
        /// </summary>
        private IRapPager _dataSource;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the dt.
        /// </summary>
        /// <value>
        /// The dt.
        /// </value>
        public DataTable Dt { get; set; }

        /// <summary name="repeater">
        ///     Repeater Object should be left null if using a gridview
        /// </summary>
        public Repeater Repeater { get; set; }

        /// <summary name="ListDS">
        ///     List Datasource Should be casted to List(object), should be null if using a SQL DS
        /// </summary>
        public List<object> ListDs { get; set; }

        /// <summary name="gridView">
        ///     Gridview Object should be left null if using a repeater
        /// </summary>
        public GridView GridView { get; set; }

        /// <summary name="PerPage">
        ///     How many items to display on a page
        /// </summary>
        public int PerPage { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.PagingPlaceHolder.Visible = false;
            //Choices are Having a Datatable with Repeater or List with a Gridview
            //Todo: Incoporate a generic approach that can handle any combination
            if (this.Dt == null || this.Repeater == null)
            {
                this._dataSource = new GridViewPaging(this.ListDs, this.GridView, this.PerPage, this.PagingLocation,
                    this.PagingPlaceHolder);
            }
            else if (this.GridView == null || this.ListDs == null)
            {
                this._dataSource = new RepeaterPaging(this.Dt, this.Repeater, this.PerPage, this.PagingLocation,
                    this.PagingPlaceHolder);
            }
            else
            {
                return;
            }
            this._dataSource.OnLoad();
        }

        #endregion
    }

    /// <summary>
    ///     Specifc paging towards a Gridview, Implements the Interface PagedDataSource
    /// </summary>
    internal class GridViewPaging : IRapPager
    {
        #region Members

        private readonly HtmlGenericControl _div;
        private readonly GridView _gridView;
        private readonly List<object> _listDs;
        private readonly PlaceHolder _pagingLocation;
        private readonly int _perPage;

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes a new instance of the <see cref="GridViewPaging" /> class.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <param name="g">The g.</param>
        /// <param name="p">The p.</param>
        /// <param name="ph">The ph.</param>
        /// <param name="div">The div.</param>
        public GridViewPaging(List<object> l, GridView g, int p, PlaceHolder ph, HtmlGenericControl div)
        {
            this._listDs = l;
            this._gridView = g;
            this._perPage = p;
            this._pagingLocation = ph;
            this._div = div;
        }

        /// <summary>
        ///     Called when [load].
        /// </summary>
        public void OnLoad()
        {
            //TODO: use YAF Session Handler
            //var x = this.Get<HttpSessionStateBase>()["UserUpdated"];
            HttpContext.Current.Session["pageNumber"] = 1;

            this.PageData();

            var totalPages = ((float) _listDs.Count/_perPage);

            if (totalPages%1 == 0)
            {
                totalPages = (int) totalPages;
            }
            else if (totalPages%1 != 0)
            {
                totalPages = (int) totalPages + 1;
            }

            for (var i = 1; i < totalPages + 1; i++)
            {
                var pagingLink = new LinkButton {Text = i.ToString()};
                if (totalPages <= 1)
                {
                    _div.Visible = false;
                    pagingLink.Visible = false;
                }
                else
                {
                    _div.Visible = true;
                    pagingLink.Visible = true;
                }
                pagingLink.CommandArgument = i.ToString();
                pagingLink.Command += PagingLink_Command;
                this._pagingLocation.Controls.Add(pagingLink);
            }
        }

        /// <summary>
        ///     Pages the data.
        /// </summary>
        public void PageData()
        {
            var pagedResults = new PagedDataSource {DataSource = _listDs, AllowPaging = true, PageSize = _perPage};

            int pageIndex;

            Int32.TryParse(HttpContext.Current.Session["pageNumber"].ToString(), out pageIndex);
            pagedResults.CurrentPageIndex = pageIndex - 1;


            this._gridView.DataSource = pagedResults;
            this._gridView.DataBind();
        }

        /// <summary>
        ///     Handles the Command event of the PagingLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="c">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void PagingLink_Command([NotNull] object sender, [NotNull] CommandEventArgs c)
        {
            HttpContext.Current.Session["pageNumber"] = c.CommandArgument.ToString();
            this.PageData();
        }

        #endregion
    }

    /// <summary>
    ///     Specific paging towards a Repeater, Implements the Interface PagedDataSource
    /// </summary>
    internal class RepeaterPaging : IRapPager
    {
        #region Members

        private readonly HtmlGenericControl _div;
        private readonly PlaceHolder _pagingLocation;
        private readonly int _perPage;
        private readonly Repeater _repeater;
        private readonly DataTable _dataTable;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeaterPaging" /> class.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="r">The r.</param>
        /// <param name="p">The p.</param>
        /// <param name="pl">The pl.</param>
        /// <param name="div">The div.</param>
        public RepeaterPaging(DataTable dt, Repeater r, int p, PlaceHolder pl, HtmlGenericControl div)
        {
            this._dataTable = dt;
            this._repeater = r;
            this._perPage = p;
            this._pagingLocation = pl;
            this._div = div;
        }

        /// <summary>
        ///     Called when [load].
        /// </summary>
        public void OnLoad()
        {
            var dataView = new DataView(this._dataTable);

            HttpContext.Current.Session["pageNumber"] = 1;

            this.PageData();

            var totalPages = ((float) dataView.Count/_perPage);

            if (totalPages%1 == 0)
            {
                totalPages = (int) totalPages;
            }
            else if (totalPages%1 != 0)
            {
                totalPages = (int) totalPages + 1;
            }

            for (var i = 1; i < totalPages + 1; i++)
            {
                var pagingLink = new LinkButton {Text = i.ToString()};
                if (totalPages <= 1)
                {
                    _div.Style.Add("visibility", "hidden");
                    _div.Visible = false;
                    pagingLink.Visible = false;
                }
                else
                {
                    _div.Visible = true;
                    pagingLink.Visible = true;
                }
                pagingLink.CommandArgument = i.ToString();
                pagingLink.Command += PagingLink_Command;
                this._pagingLocation.Controls.Add(pagingLink);
            }
        }

        /// <summary>
        ///     Pages the data.
        /// </summary>
        public void PageData()
        {

            var dv = new DataView(this._dataTable);
            var pagedResults = new PagedDataSource {DataSource = dv, AllowPaging = true, PageSize = _perPage};
            int pageIndex;
            Int32.TryParse(HttpContext.Current.Session["pageNumber"].ToString(), out pageIndex);
            pagedResults.CurrentPageIndex = pageIndex - 1;
            this._repeater.DataSource = pagedResults;
            this._repeater.DataBind();
        }

        /// <summary>
        ///     Handles the Command event of the PagingLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="c">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        protected void PagingLink_Command([NotNull] object sender, [NotNull] CommandEventArgs c)
        {
            HttpContext.Current.Session["pageNumber"] = c.CommandArgument.ToString();
            this.PageData();
        }

        #endregion
    }
}