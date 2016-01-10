using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Types;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Types;
using FreestyleOnline.classes.Types.UI;
using YAF.Types.Extensions;

namespace FreestyleOnline.classes.Factory
{
    public class WrittenBattleFactory: BaseFactory
    {
        public string Verse1 { get; private set; }
        public string Verse2 { get; private set; }
        public bool Verse1Disabled { get; private set; }
        public bool Verse2Disabled { get; private set; }
        public DateTime EndDate { get; private set; }
        public int UserId1 { get; private set; }
        public int? UserId2 { get; private set; }
        public bool CanSubmit1 { get; private set; }
        public bool CanSubmit2 { get; private set; }
        public bool CanJoin { get; private set; }
        public WrittenBattleFactory Validate(RapBattleWritten w)
        {
            this.Verse1 = w.User1Verse ?? "";
            this.Verse2 = w.User2Verse ?? "";
            this.EndDate = w.EndDate;
            this.Verse1Disabled = true;
            this.Verse2Disabled = true;
            this.CanSubmit1 = false;
            this.CanSubmit2 = false;
            this.UserId1 = w.UserId1;
            this.UserId2 = w.UserId2;
            var pageUserId = RapContextFacade.Current.GetUserId();
            var isUser1 = pageUserId == w.UserId1;
            var isUser2 = pageUserId == w.UserId2;
            if (isUser1)
            {
                if (this.Verse1.IsNotSet())
                {
                    this.Verse1Disabled = false;
                    this.CanSubmit1 = true;
                }
            }
            if (isUser2)
            {
                if (this.Verse2.IsNotSet())
                {
                    this.Verse2Disabled = false;
                    this.CanSubmit2 = true;
                }
            }
            this.CanJoin = !(w.UserId2 != null || w.UserId1 == pageUserId ||
                             RapContextFacade.Current.IsGuest || RapGlobalHelpers.IsDateExpired(w.EndDate));
            return this;
        }
    }

    public class ASPNetWrittenBattleFactory : WrittenBattleFactory, IRapConfigFactory<RapWrittenBattleASPNetConfiguration, RapBattleWritten>
    {
        public RapWrittenBattleASPNetConfiguration Build(RapBattleWritten config)
        {
            var factory = this.Validate(config);
            var aspUiConfig = new RapWrittenBattleASPNetConfiguration();
            aspUiConfig.Verse1.Text = factory.Verse1 ?? "";
            aspUiConfig.Verse2.Text = factory.Verse2 ?? "";
            aspUiConfig.Verse1.Disabled = factory.Verse1Disabled;
            aspUiConfig.Verse2.Disabled = factory.Verse2Disabled;
            aspUiConfig.Timer.TimeToEnd = factory.EndDate;
            aspUiConfig.User1.UserId = factory.UserId1;
            aspUiConfig.User2.Visible = factory.UserId2 != null;
            aspUiConfig.User2.UserId = factory.UserId2 ?? 1;
            aspUiConfig.User1Submit.Hidden = !factory.CanSubmit1;
            aspUiConfig.User2Submit.Hidden = !factory.CanSubmit2;
            aspUiConfig.User1Submit.CommandArgument = factory.UserId1.ToString();
            aspUiConfig.User2Submit.CommandArgument = factory.UserId2.ToString();
            aspUiConfig.Join.CommandArgument = config.PageUserId.ToString();
            aspUiConfig.Join.Hidden = !factory.CanJoin;
            aspUiConfig.TimerIcon.Icon = (factory.EndDate - DateTime.Now).TotalDays > 1 ? Icon.TimeGreen : Icon.TimeRed;
            //config.TimerIcon.Icon = (settings.EndDate - DateTime.Now).TotalDays > 1 ? Icon.TimeGreen : Icon.TimeRed;
            //if (settings.UserId2 == null) //TODO: FIx FORM TITLE
            //{
            //    //config.BattleForm.Title = string.Format("{0} is looking for a challenger!",
            //    //    UserMembershipHelper.GetDisplayNameFromID(settings.UserId1));
            //}
            //else
            //{
            //    //config.BattleForm.Title = string.Format("{0} VS {1}",
            //    //    UserMembershipHelper.GetDisplayNameFromID(settings.UserId1),
            //    //    UserMembershipHelper.GetDisplayNameFromID(((int) settings.UserId2)));
            //}
            //config.Join.Hidden = (settings.UserId2 != null || settings.UserId1 == settings.PageUserId ||
            //                     RapContextFacade.Current.IsGuest || RapGlobalHelpers.IsDateExpired(settings.EndDate));
            return aspUiConfig;
        }
    }
}