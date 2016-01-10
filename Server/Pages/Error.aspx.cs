#region Using

using System;
using System.Web;
using FreestyleOnline.classes.Base;

#endregion

namespace FreestyleOnline.Pages
{
    public partial class Error : RapPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ex = HttpContext.Current.Error.GetBaseException();
            HttpContext.Current.ClearError();
            Response.Write(ex.Message);
        }
    }
}