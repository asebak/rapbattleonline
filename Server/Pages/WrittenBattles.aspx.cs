#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using Common.Types;
using Common.Types.Enums;
using Ext.Net;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Factory;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Secruity;
using Microsoft.AspNet.FriendlyUrls;
using YAF.Core;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Extensions;
using YAF.Types.Interfaces;
using Label = Ext.Net.Label;

#endregion

namespace FreestyleOnline.Pages
{
    public partial class WrittenBattles : RapPage
    {
        #region Members

        /// <summary>
        ///     The _battle
        /// </summary>
       [NotNull] protected RapBattleWritten Battle;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
       [NotNull] public int Id { get; set; }
        /// <summary>
        /// Gets the length of the bar.
        /// </summary>
        /// <value>
        /// The length of the bar.
        /// </value>
       [NotNull]
       protected int BarLength { get; set; }
        #endregion

        #region Methods

       protected override void OnInit([NotNull] EventArgs e)
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "fullRapBattleRating", "~/js/RapBattleRating.js", true);
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "writtenBattleApp", "~/js/WrittenBattles.js", true);

            this.Id = Convert.ToInt32(this.GetService<UrlProvider>().GetQuery()[0]);
            this.Battle = new RapBattleWritten(this.PageContext.PageUserID, Id).GetSettings() as RapBattleWritten;
            this.BarLength =
                Convert.ToInt32(
                    this.GetService<ApplicationProvider>().GetApplicationSettings("RAP.WrittenBattleBarLength"));
            this.WrittenVoteDisplay.BattleType = RapBattleType.Written;
            var ui = this.GetFactory<ASPNetWrittenBattleFactory>().Build(this.Battle);

            this.BattleJoin2.CommandArgument = ui.Join.CommandArgument;
            this.BattleJoin2.Hidden = ui.Join.Hidden;
            this.WrittenTimer.TimeToEnd = ui.Timer.TimeToEnd;
            this.TimerIcon.Icon = ui.TimerIcon.Icon;
            this.BattleUser1.UserId = ui.User1.UserId;
            this.BattleUser2.UserId = ui.User2.UserId;
            this.BattleUser2.Visible = ui.User2.Visible;
            this.BattleTextArea1.Attributes.Add("rows", this.Battle.Length.ToString());
            this.BattleTextArea2.Attributes.Add("rows", this.Battle.Length.ToString());
            this.BattleTextArea1.Value = ui.Verse1.Text;
            this.BattleTextArea1.Disabled = ui.Verse1.Disabled;
            this.BattleTextArea2.Value = ui.Verse2.Text;
            this.BattleTextArea2.Disabled = ui.Verse2.Disabled;
            this.BattleSubmit1.CommandArgument = ui.User1Submit.CommandArgument;
            this.BattleSubmit2.CommandArgument = ui.User2Submit.CommandArgument;
            this.BattleSubmit1.Hidden = ui.User1Submit.Hidden;
            this.BattleSubmit2.Hidden = ui.User2Submit.Hidden;
            this.BattleSubmit1.OnClientClick += "return handlepostback({0})".FormatWith(this.BattleSubmit1.ClientID);
            this.BattleSubmit2.OnClientClick += "return handlepostback({0})".FormatWith(this.BattleSubmit2.ClientID);
            this.BattleJoin2.OnClientClick += "return handlepostback({0})".FormatWith(this.BattleJoin2.ClientID);
            this.HandleBattleDateTime();
            this.InitializeGraph();
            this.HandleWinner();
            this.HandleVoting(Battle.UserId1, Battle.UserId2 != null ? (int) Battle.UserId2 : 0);
            base.OnInit(e);
        }

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
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
        ///     Handles the battle date time.
        /// </summary>
        private void HandleBattleDateTime()
        {
            DisableVoting(true);
            if (RapGlobalHelpers.IsDateExpired(this.Battle.EndDate) && this.Battle.UserId2 != null)
            //                (Battle.User1Verse != null && Battle.User2Verse != null))
            {
                this.TimerIcon.Text = this.Text("BATTLES", "OVER");
                this.TimerIcon.Icon = Icon.BinClosed;
                this.WrittenTimer.Visible = false;
                //TODO: here
                //this.BattleTabs.Items[2].ToolTip = "Voting period is over for this battle";
                var voteDays =
                    Convert.ToDouble(this.GetService<ApplicationProvider>().GetApplicationSettings("RAP.VoteDays"));
                //after battle is over add a few days
                if (!RapGlobalHelpers.IsDateExpired
                    (Battle.EndDate.AddDays(voteDays)))
                {
                    DisableVoting(false);
                    this.TimerIcon.Text += this.Text("BATTLES", "VOTE_ENDS");
                    this.WrittenTimer.Visible = true;
                    this.WrittenTimer.TimeToEnd = Battle.EndDate.AddDays(voteDays);
                }
            }
        }

        /// <summary>
        ///     Handles the winner.
        /// </summary>
        private void HandleWinner()
        {
            if (Battle.WinnerId == Battle.UserId1 && Battle.WinnerId != null)
            {
                this.User1Panel.Attributes.Add("class", "panel panel-success");
            }
            if (Battle.WinnerId != Battle.UserId1 && Battle.WinnerId != null)
            {
                this.User1Panel.Attributes.Add("class", "panel panel-danger");
            }
            if (Battle.WinnerId == Battle.UserId2 && Battle.WinnerId != null)
            {
                this.User2Panel.Attributes.Add("class", "panel panel-success");
            }
            if (Battle.WinnerId != Battle.UserId2 && Battle.WinnerId != null)
            {
                this.User2Panel.Attributes.Add("class", "panel panel-danger");
            }
            if (Battle.User1Overall != null && Battle.User2Overall != null && Battle.WinnerId == null &&
                Battle.User2Overall == Battle.User1Overall)
            {
                this.User1Panel.Attributes.Add("class", "panel panel-info");
                this.User2Panel.Attributes.Add("class", "panel panel-info");
            }
        }

        /// <summary>
        ///     Handles the voting.
        /// </summary>
        /// <param name="userId1">The user id1.</param>
        /// <param name="userId2">The user id2.</param>
        private void HandleVoting([NotNull] int userId1, [CanBeNull] int userId2)
        {
            this.WrittenVote.BattleType = RapBattleType.Written;
            if (userId2 != 0)
            {
                this.WrittenVote.DisplayName1 = UserMembershipHelper.GetDisplayNameFromID(userId1);
                this.WrittenVote.DisplayName2 = UserMembershipHelper.GetDisplayNameFromID(userId2);
                this.WrittenVote.UserId1 = userId1;
                this.WrittenVote.UserId2 = userId2;
            }
        }

        /// <summary>
        ///     Disables the voting.
        /// </summary>
        /// <param name="isDisabled">if set to <c>true</c> [is disabled].</param>
        private void DisableVoting([NotNull] bool isDisabled)
        {
            this.votetab.Visible = !isDisabled;
            this.WrittenVote.Visible = !isDisabled;
            var voteEnabled = this.Battle.IsUserAllowedToVote(this.PageContext.IsGuest);
            if (!voteEnabled)
            {
                this.votetab.Visible = false;
                this.WrittenVote.Visible = false;
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
                this.WrittenVoteGraph1.UserVotes = votes1;
                this.WrittenVoteGraph2.UserVotes = votes2;
            }
            else
            {
                this.statstab.Visible = false;
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Command event of the BattleSubmit1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
        protected void BattleSubmit1_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            if (this.BattleTextArea1.Value.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.Text("BATTLES", "WRITTEN_NOCONTENT"));
                return;
            }
            var userId = e.CommandArgument.ToString();
            var user1Verse = this.BattleTextArea1.Value;
            var newVerse = this.GetCore<WrittenBattleValidation>().RebuildWrittenVerse(user1Verse, this.Battle.Length);
            this.Battle.Submit(Convert.ToInt32(userId), newVerse);
            this.AddLoadMessageSession(this.Text("BATTLES", "VERSE_SUBMITTED"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        /// <summary>
        ///     Handles the Command event of the BattleSubmit2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
        protected void BattleSubmit2_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            if (this.BattleTextArea2.Value.IsNotSet())
            {
                this.PageContext.AddLoadMessage(this.Text("BATTLES", "WRITTEN_NOCONTENT"));
                return;
            }
            var userId = e.CommandArgument.ToString();
            var user2Verse = this.BattleTextArea2.Value;
            var newVerse = this.GetCore<WrittenBattleValidation>().RebuildWrittenVerse(user2Verse, this.Battle.Length);
            this.Battle.Submit(Convert.ToInt32(userId), newVerse);
            this.AddLoadMessageSession(this.Text("BATTLES", "VERSE_SUBMITTED"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        /// <summary>
        ///     Handles the Command event of the BattleJoin2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
        protected void BattleJoin2_Command([NotNull] object sender, [NotNull] CommandEventArgs e)
        {
            var userId = e.CommandArgument.ToString();
            Battle.JoinBattle(Convert.ToInt32(userId));
            this.AddLoadMessageSession(this.Text("BATTLES", "JOIN_WRITTEN"));
            this.GetService<UrlProvider>().RefreshPage();
        }

        #endregion
    }
}