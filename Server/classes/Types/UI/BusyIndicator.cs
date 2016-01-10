#region Using

using System.Web.UI;

#endregion

namespace FreestyleOnline.classes.Types.UI
{
    public class BusyIndicator
    {
        /// <summary>
        ///     Shows the specified p.
        /// </summary>
        /// <param name="p">The p.</param>
        public static void Show(Page p)
        {
            p.Page.ClientScript.RegisterClientScriptBlock(p.GetType(), "busyindicatorhide",
                "sap.ui.core.BusyIndicator.show();", true);
        }

        /// <summary>
        ///     Hides the specified p.
        /// </summary>
        /// <param name="p">The p.</param>
        public static void Hide(Page p)
        {
            p.Page.ClientScript.RegisterClientScriptBlock(p.GetType(), "busyindicatorhide",
                "sap.ui.core.BusyIndicator.hide();", true);
        }

        /// <summary>
        ///     Shows the specified u.
        /// </summary>
        /// <param name="u">The u.</param>
        public static void Show(UserControl u)
        {
            u.Page.ClientScript.RegisterClientScriptBlock(u.GetType(), "busyindicatorhide",
                "sap.ui.core.BusyIndicator.show();", true);
        }

        /// <summary>
        ///     Hides the specified u.
        /// </summary>
        /// <param name="u">The u.</param>
        public static void Hide(UserControl u)
        {
            u.Page.ClientScript.RegisterClientScriptBlock(u.GetType(), "busyindicatorhide",
                "sap.ui.core.BusyIndicator.hide();", true);
        }
    }
}