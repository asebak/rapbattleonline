#region Using

using System;
using System.Web.UI.HtmlControls;
using FreestyleOnline.classes.Base;
using YAF.Types;

#endregion

namespace FreestyleOnline.classes.Types.UI
{
    public class CalloutBox : RapClass
    {
        /// <summary>
        ///     Creates the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">t</exception>
        public HtmlGenericControl Create([NotNull] BootstrapElementType t, [CanBeNull] string text)
        {
            var div = new HtmlGenericControl("div");
            switch (t)
            {
                case BootstrapElementType.Success:
                    div.Attributes.Add("class", "bs-callout bs-callout-success");
                    break;
                case BootstrapElementType.Warning:
                    div.Attributes.Add("class", "bs-callout bs-callout-warning");
                    break;
                case BootstrapElementType.Info:
                    div.Attributes.Add("class", "bs-callout bs-callout-info");
                    break;
                case BootstrapElementType.Danger:
                    div.Attributes.Add("class", "bs-callout bs-callout-danger");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("t");
            }
            div.InnerHtml = text;
            return div;
        }
    }
}