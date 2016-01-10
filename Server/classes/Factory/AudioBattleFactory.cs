using System;
using System.Net.Mime;
using Common.Models;
using Common.Types;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Interfaces;
using FreestyleOnline.classes.Types.UI;
using YAF.Core;
using YAF.Core.Services;
using YAF.Types;

namespace FreestyleOnline.classes.Factory
{
    public class AudioBattleFactory: BaseFactory
    {
        /// <summary>
        /// Validates the specified audio battle and builds and object to the client.
        /// </summary>
        /// <param name="a">A.</param>
        /// <returns></returns>
        public AudioBattleModel Construct([NotNull] RapBattleAudio a)
        {
            //this handles on clients for native applications and silverlight app.
            //TODO: Validation should be done for the restful post or put request if a user modifies the payload
            var avatar = new YafAvatars(YafContext.Current.BoardSettings);
            var pageUserId = a.PageUserId;
            var model = new AudioBattleModel
            {
                Length = new TimeSpan(0, 0, a.Length),
                BattleId = a.BattleId,
                User1Overall = a.User1Overall,
                User2Overall = a.User2Overall,
                UserId1 = a.UserId1,
                UserId2 = a.UserId2,
                EndDate = a.EndDate,
                IsPublic = a.IsPublic,
                Beat = a.Beat,
                WinnerId = a.WinnerId,
                User1Avatar = avatar.GetAvatarUrlForUser(a.UserId1),
                User2Avatar = a.UserId2 != null
                    ? UserMembershipHelper.GetDisplayNameFromID((int) a.UserId2)
                    : null,
                User1DisplayName = UserMembershipHelper.GetDisplayNameFromID(a.UserId1),
                User2DisplayName = a.UserId2 != null
                    ? UserMembershipHelper.GetDisplayNameFromID((int) a.UserId2)
                    : null,
                User1Audio = a.User1Audio,
                User2Audio = a.User2Audio,
                PageUserId = a.PageUserId
            };
            model.Player1Visible = !string.IsNullOrEmpty(model.User1Audio);
            model.Player2Visible = !string.IsNullOrEmpty(model.User2Audio);
            if (string.IsNullOrEmpty(a.User1Audio) && pageUserId == a.UserId1 &&
                !RapGlobalHelpers.IsDateExpired(a.EndDate))
            {
                model.Recorder1Visible = true;
            }
            else
            {
                model.Recorder1Visible = false;
            }

            if (string.IsNullOrEmpty(a.User2Audio) && pageUserId == a.UserId2 &&
                !RapGlobalHelpers.IsDateExpired(a.EndDate))
            {
                model.Recorder2Visible = true;
            }
            else
            {
                model.Recorder2Visible = false;
            }
            model.EnableBeats = a.Beat == null;

            if (a.PageUserId != a.UserId1 && pageUserId != a.UserId2)
            {
                model.ShowBeats = false;
            }
            else
            {
                model.ShowBeats = true;
            }
            model.CanJoin = (a.UserId2 == null || a.UserId1 != a.PageUserId ||
                                  !this.RapContext.IsGuest || !RapGlobalHelpers.IsDateExpired(a.EndDate));
            return model;
        }
    }
    public class ASPNetAudioBattleFactory: AudioBattleFactory, IRapConfigFactory<RapAudioBattleASPNetConfiguration, RapBattleAudio>
    {
        //this handles asp.net's UI handler
        public RapAudioBattleASPNetConfiguration Build([NotNull] RapBattleAudio config)
        {
            var settings = config;
            var aspUiConfig = new RapAudioBattleASPNetConfiguration
            {
                Timer = {TimeToEnd = settings.EndDate},
                User1 = {UserId = settings.UserId1},
                User2 = {UserId = settings.UserId2 ?? 1},
                Join = {CommandArgument = settings.PageUserId.ToString()},
                TimerIcon = {Icon = (settings.EndDate - DateTime.Now).TotalDays > 1 ? Icon.TimeGreen : Icon.TimeRed}
            };
            aspUiConfig.User2.Visible = settings.UserId2 != null;
            aspUiConfig.Join.Hidden = (settings.UserId2 != null || settings.UserId1 == settings.PageUserId ||
                                  this.RapContext.IsGuest || RapGlobalHelpers.IsDateExpired(settings.EndDate));
            return aspUiConfig;
        }
    }
}