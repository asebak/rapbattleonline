#region Using

using System;
using System.Linq;
using System.Web.UI.WebControls;
using Common.Types;
using Common.Types.Enums;
using Ext.Net;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Factory;
using FreestyleOnline.classes.Providers;
using YAF.Core;
using YAF.Types;
using YAF.Types.Extensions;
using Label = Ext.Net.Label;

#endregion

namespace FreestyleOnline.Pages
{
    public partial class AudioBattles : RapPage
    {
        #region Members

        protected RapBattleAudio Battle;

        #endregion

        #region Properties

        public string BattleId { get; set; }

        #endregion

        #region Methods

        protected override void OnInit([NotNull] EventArgs e)
        {
            this.RegisterJs();
            this.BattleId = this.GetService<UrlProvider>().GetQuery()[0];
            //for silverlight application initialization
            initParams.Attributes.Add("value",
                string.Format("userId={0},battleId={1}, address={2}", this.PageContext.PageUserID, this.BattleId,
                    this.GetService<ApplicationProvider>().GetApplicationSettings("YAF.BaseUrlMask")));
            this.Battle =
                new RapBattleAudio(this.PageContext.PageUserID, Convert.ToInt32(this.BattleId)).GetSettings() as
                    RapBattleAudio;
            var ui = this.GetFactory<ASPNetAudioBattleFactory>().Build(this.Battle);
            this.JoinButton.CommandArgument = ui.Join.CommandArgument;
            this.JoinButton.Hidden = ui.Join.Hidden;
            this.TimerIcon.Icon = ui.TimerIcon.Icon;
            this.AudioTimer.TimeToEnd = ui.Timer.TimeToEnd;
            this.BattleUser1.UserId = ui.User1.UserId;
            this.BattleUser2.UserId = ui.User2.UserId;
            this.ListenerAudio.Battle = this.Battle;
            this.BattleUser2.Visible = ui.User2.Visible;
            this.JoinButton.OnClientClick += "return handlepostback({0})".FormatWith(this.JoinButton.ClientID);
            this.silverlightappbody.Visible = false;
            if (this.IsMobile)
            {
                if ((this.Battle.UserId1 == this.PageContext.PageUserID && this.Battle.User1Audio.IsNotSet()) ||
                    (this.Battle.UserId2 == this.PageContext.PageUserID) && this.Battle.User2Audio.IsNotSet())
                {
                    this.NoSilverlight.Controls.Add(new Label
                    {
                        Html = this.Text("REQUIRED", "SILVERLIGHT")
                    });
                }
            }
            else
            {
                if ((this.Battle.UserId1 == this.PageContext.PageUserID && this.Battle.User1Audio.IsNotSet()) ||
                    (this.Battle.UserId2 == this.PageContext.PageUserID) && this.Battle.User2Audio.IsNotSet())
                {
                    this.silverlightappbody.Visible = true;
                }
            }
            base.OnInit(e);
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SetupControls();
            this.InitializeGraph();
            this.HandleBattleDateTime();
            this.HandleWinner();
            if (this.Battle.UserId2 == null && this.PageContext.PageUserID != this.Battle.UserId1)
            {
                this.User2Panel.Attributes.Add("class", "panel panel-warning");
                this.User2Panel.Controls.Add(new Label
                {
                    Html = this.Text("BATTLES", "MISSING_CHALLENGER").FormatWith("<br>", this.Battle.Length)
                });
                return;
            }
            if (this.Battle.UserId2 == null)
            {
                this.User2Panel.Attributes.Add("class", "panel panel-warning");
                this.User2Panel.Controls.Add(new Label
                {
                    Html = this.Text("BATTLES", "MISSING_CHALLENGER_YOU").FormatWith("<br>", "<br>")
                });
            }
        }

        /// <summary>
        /// Registers the js.
        /// </summary>
        private void RegisterJs()
        {
            if (!this.IsMobile)
            {
                this.GetService<ClientProviders>()
                    .RegisterRawScript(this, "Silverlight",
                        "<script type='text/javascript' src='/js/Scripts/Silverlight.js'></script>");
                this.GetService<ClientProviders>()
                    .RegisterClientScriptBlock(this, "SilverlightError", "~/js/Scripts/SilverlightErrorHandler.js", true);
            }
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "fullRapBattleRating", "~/js/RapBattleRating.js", true);
        }

        /// <summary>
        /// Setups the controls.
        /// </summary>
        private void SetupControls()
        {
            this.AudioVote.BattleType = RapBattleType.Audio;
            if (this.Battle.UserId2 != null)
            {
                this.AudioVote.DisplayName1 = UserMembershipHelper.GetDisplayNameFromID(this.Battle.UserId1);
                this.AudioVote.DisplayName2 = UserMembershipHelper.GetDisplayNameFromID((int)this.Battle.UserId2);
                this.AudioVote.UserId1 = this.Battle.UserId1;
                this.AudioVote.UserId2 = (int)this.Battle.UserId2;
            }
            this.AudioVoteDisplay.BattleType = RapBattleType.Audio;
        }

        /// <summary>
        /// Disables the voting.
        /// </summary>
        /// <param name="isDisabled">if set to <c>true</c> [is disabled].</param>
        private void DisableVoting([NotNull] bool isDisabled)
        {
            this.votetab.Visible = !isDisabled;
            this.AudioVote.Visible = !isDisabled;
            var voteEnabled = this.Battle.IsUserAllowedToVote(this.PageContext.IsGuest);
            if (!voteEnabled)
            {
                this.votetab.Visible = false;
                this.AudioVote.Visible = false;
            }
        }

        /// <summary>
        ///     Handles the battle date time.
        /// </summary>
        private void HandleBattleDateTime()
        {
            DisableVoting(true);
            //1. Battle is over and user2 exists but doesn't submit anything
            //2. Battle Does not finish but both audios are recorded hence voting should begin right away
            if ((RapGlobalHelpers.IsDateExpired(Battle.EndDate) && Battle.UserId2 != null) || (Battle.User1Audio != null && Battle.User2Audio != null))
            {
                this.TimerIcon.Text = this.Text("BATTLES", "OVER");
                this.TimerIcon.Icon = Icon.BinClosed;
                this.AudioTimer.Visible = false;
                var voteDays =
                    Convert.ToDouble(this.GetService<ApplicationProvider>().GetApplicationSettings("RAP.VoteDays"));
                //after battle is over add a few days
                if (!RapGlobalHelpers.IsDateExpired
                    (Battle.EndDate.AddDays(voteDays)))
                {
                    DisableVoting(false);
                    this.TimerIcon.Text += this.Text("BATTLES", "VOTE_ENDS");
                    this.AudioTimer.Visible = true;
                    this.AudioTimer.TimeToEnd = Battle.EndDate.AddDays(voteDays);
                }
            }
        }

        /// <summary>
        ///     Handles the winner.
        /// </summary>
        private void HandleWinner()
        {
            if (this.Battle.WinnerId == this.Battle.UserId1 && this.Battle.WinnerId != null)
            {
                this.User1Panel.Attributes.Add("class", "panel panel-success");
            }
            if (this.Battle.WinnerId != this.Battle.UserId1 && this.Battle.WinnerId != null)
            {
                this.User1Panel.Attributes.Add("class", "panel panel-danger");
            }
            if (this.Battle.WinnerId == this.Battle.UserId2 && this.Battle.WinnerId != null)
            {
                this.User2Panel.Attributes.Add("class", "panel panel-success");
            }
            if (this.Battle.WinnerId != this.Battle.UserId2 && this.Battle.WinnerId != null)
            {
                this.User2Panel.Attributes.Add("class", "panel panel-danger");
            }
            if (this.Battle.User1Overall != null && this.Battle.User2Overall != null && this.Battle.WinnerId == null &&
                this.Battle.User2Overall == this.Battle.User1Overall)
            {
                this.User1Panel.Attributes.Add("class", "panel panel-info");
                this.User2Panel.Attributes.Add("class", "panel panel-info");
            }
        }

        /// <summary>
        ///     Initializes the graph.
        /// </summary>
        private void InitializeGraph()
        {
            if (this.Battle.UserId2 != null)
            {
                var votes1 = this.Battle.GetVotesUser1();
                var votes2 = this.Battle.GetVotesUser2();
                if (!votes1.Any())
                {
                    this.statstab.Visible = false;
                    return;
                }
                this.AudioBattleChart1.UserVotes = votes1;
                this.AudioBattleGraph2.UserVotes = votes2;
            }
            else
            {
                this.statstab.Visible = false;
            }
        }

        #endregion

        #region Event Handlers
        /// <summary>
        ///     Handles the Command event of the JoinButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
        protected void JoinButton_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var userId = e.CommandArgument.ToString();
            this.Battle.JoinBattle(Convert.ToInt32(userId));
            this.AddLoadMessageSession(this.Text("BATTLES", "JOIN_AUDIO"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        #endregion
    }
}