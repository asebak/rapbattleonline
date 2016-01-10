#region Using

using System;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;
using YAF.Types;
using Yahoo.Yui.Compressor;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class CountDown : RapUserControl
    {
        #region Members

        private const string CounterScript = @"
        /*
	        Author:			Robert Hashemian (http://www.hashemian.com/)
	        Modified by:	Munsifali Rashid (http://www.munit.co.uk/)
	        Modified by:	Sam Walker (http://blog.bluesam.com)
        */

        function countdown(obj)
        {
	        this.obj		= obj;
	        this.Div		= ""clock"";
	        this.TotalSeconds		= 1000;
	        this.DaysDisplayFormat = ""%%D%% Days,"";
	        this.HoursDisplayFormat = ""%%H%% Hours,"";
	        this.MinutesDisplayFormat = ""%%M%% Minutes,""
	        this.SecondsDisplayFormat =  ""%%S%% Seconds"";
	        this.CountActive	= true;
	
	        this.DisplayStr;

	        this.Calcage		= cd_Calcage;
	        this.CountBack		= cd_CountBack;
	        this.Setup			= cd_Setup;
        }

        function cd_Calcage(seconds, num1, num2)
        {
	        s = ((Math.floor(seconds/num1))%num2).toString();
	        return (s);
        }
        function removeSIfOne(number, str)
        {
	        if(number == 1)
	        {
		        return str.substring(0,str.lastIndexOf('s')) + str.substring(str.lastIndexOf('s') + 1);
	        }
	        else
	        {
		        return str;
	        }
        }
        function cd_CountBack(secs)
        {
	        if(secs < 1)
	        {
		        this.CountActive = false;
	        }
            var days = this.Calcage(secs,86400,100000);
            var hours = this.Calcage(secs,3600,24);
            var minutes = this.Calcage(secs,60,60);
            var seconds = this.Calcage(secs,1,60);
        
            this.DisplayStr = """";
            var first = true;
            if(days > 0)
            {
		        first = false;
                this.DisplayStr += this.DaysDisplayFormat.replace(/%%D%%/g,days);
                this.DisplayStr = removeSIfOne(days, this.DisplayStr);
            }
            if(hours > 0 || days > 0)
            {
                if(!first)
                {
			        this.DisplayStr += "" "";
                }
                first = false;
                this.DisplayStr += this.HoursDisplayFormat.replace(/%%H%%/g,hours);
                this.DisplayStr = removeSIfOne(hours, this.DisplayStr);
            }
            if(minutes > 0 || hours > 0 || days > 0)
            {
                if(!first)
                {
			        this.DisplayStr += "" "";
                }
                first = false;
                this.DisplayStr += this.MinutesDisplayFormat.replace(/%%M%%/g,minutes);
                this.DisplayStr = removeSIfOne(minutes, this.DisplayStr);
            }
            if(!first)
            {
		        this.DisplayStr += "" "";
            }
            this.DisplayStr += this.SecondsDisplayFormat.replace(/%%S%%/g,seconds);
            this.DisplayStr = removeSIfOne(seconds, this.DisplayStr);

          document.getElementById(this.Div).innerHTML = this.DisplayStr;
          if (this.CountActive) setTimeout(this.obj +"".CountBack("" + (secs-1) + "")"", 990);
        }
        function cd_Setup()
        {
	        this.CountBack(this.TotalSeconds);
        }";

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the time at which the countdown timer will end.
        /// </summary>
        public DateTime? TimeToEnd { get; set; }

        #endregion

        #region Methods

        protected override void OnPreRender([NotNull] EventArgs e)
        {
            base.OnPreRender(e);
            this.clockWrapper.Visible = false;
            if (TimeToEnd.HasValue)
            {
                this.clockWrapper.Visible = true;
                this.GetService<ClientProviders>()
                    .RegisterClientScriptBlock(this, "countDownTimerScript",
                        new JavaScriptCompressor().Compress(CounterScript));
                // Writes the required javascript needed for the count down script
                var script = @"<script type=""text/javascript"">var {0}	= new countdown('{0}');{0}.Div = ""{1}"";{0}.TotalSeconds = {2};{0}.Setup();</script>";
                var countDownDivClientId = this.clockWrapper.ClientID;
                var totalTime = TimeToEnd.Value - DateTime.Now;
                script = String.Format(script, "cd" + countDownDivClientId, countDownDivClientId,
                    (int) totalTime.TotalSeconds);
                this.litScript.Text = script;
            }
        }

        #endregion
    }
}