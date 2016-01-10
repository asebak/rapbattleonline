#region Using

using System;
using Common.Types.Enums;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;
using YAF.Core;
using YAF.Types;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.Pages
{
    public partial class Tournaments : RapPage
    {
        #region Properties

        [NotNull]
        protected RapTournament Tournament { get; set; }

        #endregion

        #region Methods

        protected override void OnInit([NotNull] EventArgs e)
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "tournamentsuserview", "~/js/Tournaments.js", true);
            var tournamentId = Convert.ToInt32(this.GetService<UrlProvider>().GetQuery()[0]);
            this.Tournament = new RapTournament(tournamentId);
            switch (Tournament.TournamentStatus)
            {
                case RapTournamentStatus.NotStarted:
                    var canJoin = this.Tournament.ChallengerCanJoin(this.PageContext.PageUserID);
                    if (canJoin)
                    {
                        this.tournamentdescription.Visible = true;
                        this.JoinTourn.OnClientClick += "return handlepostback({0})".FormatWith(this.JoinTourn.ClientID);
                    }
                    else if (this.PageContext.IsGuest)
                    {
                        this.guestdescription.Visible = true;
                    }
                    else
                    {
                        this.tournamentjoinedalready.Visible = true;
                    }
                    break;
                case RapTournamentStatus.InProgress:
                case RapTournamentStatus.Over:
                    this.TournamentBracket.Text = this.GetCore<RapTournament>().GenerateResultsInHtml(Tournament,
                        (Tournament.TournamentType == RapBattleType.Audio)
                            ? this.GetService<UrlProvider>().GetUrl("/Pages/AudioBattles/")
                            : this.GetService<UrlProvider>().GetUrl("/Pages/WrittenBattles/"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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

        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Click event of the JoinTourn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void JoinTourn_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Tournament.Join(this.PageContext.PageUserID);
            //this.AddLoadMessageSession
            this.GetService<UrlProvider>().RefreshPage();
        }

        #endregion
    }
}