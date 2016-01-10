#region Using

using System.Web.UI.HtmlControls;

#endregion

namespace FreestyleOnline.classes.Types.UI
{
    public class BattleStatsChartAspNet
    {
        public static void CreateStatsProgressBar(double value, HtmlGenericControl h)
        {
            if (value > 8.5)
            {
                h.Attributes.Add("class", "progress-bar progress-bar-success");
            }
            else if (value > 7)
            {
                h.Attributes.Add("class", "progress-bar progress-bar-info");
            }
            else if (value > 4)
            {
                h.Attributes.Add("class", "progress-bar progress-bar-warning");
            }
            else
            {
                h.Attributes.Add("class", "progress-bar progress-bar-danger");
            }
            h.Style.Add("width", string.Format("{0}%", value*10));
        }

        public static void CreateStatsProgressBar(float value, HtmlGenericControl h)
        {
            if (value > 8.5)
            {
                h.Attributes.Add("class", "progress-bar progress-bar-success");
            }
            else if (value > 7)
            {
                h.Attributes.Add("class", "progress-bar progress-bar-info");
            }
            else if (value > 4)
            {
                h.Attributes.Add("class", "progress-bar progress-bar-warning");
            }
            else
            {
                h.Attributes.Add("class", "progress-bar progress-bar-danger");
            }
            h.Style.Add("width", string.Format("{0}%", value*10));
        }
    }
}