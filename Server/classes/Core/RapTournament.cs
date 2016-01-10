#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Common.Types.Enums;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Database;
using FreestyleOnline.classes.Types;
using YAF.Core;
using YAF.Core.Services;
using YAF.Types;
using YAF.Types.Extensions;

#endregion

namespace FreestyleOnline.classes.Core
{
    public class RapTournament : RapClass
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the tournament identifier.
        /// </summary>
        /// <value>
        ///     The tournament identifier.
        /// </value>
        [NotNull] public int TournamentId { get; private set; }

        /// <summary>
        ///     Gets or sets the total challengers.
        /// </summary>
        /// <value>
        ///     The total challengers.
        /// </value>
        [NotNull]
        public int TotalChallengers { get; private set; }

        /// <summary>
        ///     Gets or sets the total rounds.
        /// </summary>
        /// <value>
        ///     The total rounds.
        /// </value>
        [NotNull]
        public int TotalRounds { get; private set; }

        /// <summary>
        ///     Gets or sets the date started.
        /// </summary>
        /// <value>
        ///     The date started.
        /// </value>
        [NotNull]
        public DateTime DateStarted { get; private set; }

        /// <summary>
        ///     Gets or sets the type of the tournament.
        /// </summary>
        /// <value>
        ///     The type of the tournament.
        /// </value>
        [NotNull]
        public RapBattleType TournamentType { get; private set; }

        /// <summary>
        ///     Gets or sets the tournament status.
        /// </summary>
        /// <value>
        ///     The tournament status.
        /// </value>
        [NotNull]
        public RapTournamentStatus TournamentStatus { get; private set; }

        /// <summary>
        ///     Gets the tournament round matches.
        /// </summary>
        /// <value>
        ///     The tournament round matches.
        /// </value>
        public SortedList<int, SortedList<int, RapMatch>> TournamentRoundMatches { get; private set; }

        /// <summary>
        ///     Gets the third place match.
        /// </summary>
        /// <value>
        ///     The third place match.
        /// </value>
        public RapMatch ThirdPlaceMatch { get; private set; }

        /// <summary>
        ///     Gets the tournament matches.
        /// </summary>
        /// <value>
        ///     The tournament matches.
        /// </value>
        public List<RapMatch> TournamentMatches { get; private set; }

        #endregion

        #region Constructor


        /// <summary>
        ///     Initializes a new instance of the <see cref="RapTournament" /> class.
        /// </summary>
        public RapTournament()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapTournament" /> class.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        public RapTournament([NotNull] int tournamentId)
        {
            this.TournamentId = tournamentId;
            this.GetTournamentSettings();
            var rounds = this.TotalRounds;
            this.TournamentRoundMatches = new SortedList<int, SortedList<int, RapMatch>>();
            if (this.TournamentStatus != RapTournamentStatus.NotStarted)
            {
                this.GenerateTournamentResults(rounds);
                if (rounds > 1)
                {
                    this.GenerateThirdPlaceResult(rounds);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Joins the specified user identifier into a tournament.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void Join([NotNull] int userId)
        {
            var challengerNumber = Db.add_tournament_user(this.TournamentId, userId);
            var matchId = 0;
            if (challengerNumber >= this.TotalChallengers && this.TournamentStatus == RapTournamentStatus.NotStarted)
            {
                this.TournamentStatus = RapTournamentStatus.InProgress;
                Db.update_tournamentstate(this.TournamentId, (int) this.TournamentStatus);
                var challengers = this.GetTournamentChallengers();
                challengers.Shuffle();
                for (var i = 0; i < challengers.Count; i += 2)
                {
                    matchId++;
                    Db.add_tournament_match(this.TournamentId, matchId, 1,
                        this.TournamentType, challengers[i].UserId, challengers[i + 1].UserId);
                }
            }
        }

        /// <summary>
        ///     Challengers the can join.
        /// </summary>
        /// <param name="pageUserId">The page user identifier.</param>
        /// <returns></returns>
        public bool ChallengerCanJoin([NotNull] int pageUserId)
        {
            return GetTournamentChallengers().All(u => u.UserId != pageUserId) && !this.RapContext.IsGuest;
        }

        /// <summary>
        ///     Gets the tournament challengers.
        /// </summary>
        /// <returns></returns>
        public List<RapMatchChallenger> GetTournamentChallengers()
        {
            var tournamentsUsersDs = Db.get_tournament_users(this.TournamentId);
            var allChallengers = (from r in tournamentsUsersDs.Tables[0].AsEnumerable()
                select new RapMatchChallenger
                {
                    UserId = r.Field<int>("UserID"),
                    EntryNumber = r.Field<int>("EntryNumber")
                }).OrderBy(x => x.EntryNumber).ToList();
            return allChallengers;
        }

        /// <summary>
        ///     Gets the tournament settings.
        /// </summary>
        private void GetTournamentSettings()
        {
            var tournamentDs = Db.get_tournament(this.TournamentId);
            foreach (DataRow r in tournamentDs.Tables[0].Rows)
            {
                this.TournamentId = r.Field<int>("TournamentID");
                this.DateStarted = r.Field<DateTime>("DateStarted");
                this.TournamentStatus = r.Field<RapTournamentStatus>("TournamentState");
                this.TournamentType = r.Field<RapBattleType>("BattleType");
                this.TotalRounds = r.Field<int>("TotalRounds");
                this.TotalChallengers = r.Field<int>("Contestants");
            }
        }

        /// <summary>
        ///     Gets all tournaments.
        /// </summary>
        /// <returns></returns>
        public List<RapTournament> GetAllTournaments()
        {
            var tournamentsDs = Db.get_all_tournaments();
            var allTournaments = (from r in tournamentsDs.Tables[0].AsEnumerable()
                select new RapTournament
                {
                    TournamentId = r.Field<int>("TournamentID"),
                    DateStarted = r.Field<DateTime>("DateStarted"),
                    TournamentStatus = (RapTournamentStatus) r.Field<int>("TournamentState"),
                    TournamentType = (RapBattleType) r.Field<int>("BattleType"),
                    TotalRounds = r.Field<int>("TotalRounds"),
                    TotalChallengers = r.Field<int>("Contestants"),
                }).ToList();
            return allTournaments;
        }

        /// <summary>
        ///     Gets the active tournaments.
        /// </summary>
        /// <returns></returns>
        public List<RapTournament> GetActiveTournaments()
        {
            return this.GetAllTournaments().Where(t => t.TournamentStatus != RapTournamentStatus.Over).ToList();
        }

        /// <summary>
        ///     Gets all matches for a specific tournament.
        /// </summary>
        /// <returns></returns>
        private List<RapMatch> GetAllMatches()
        {
            var tournamentMatchesDs = Db.get_tournamentmatches(this.TournamentId, this.TournamentType);
            var allTournamentMatches = (from r in tournamentMatchesDs.Tables[0].AsEnumerable()
                select new RapMatch
                {
                    BattleId =
                        (this.TournamentType == RapBattleType.Written)
                            ? r.Field<int>("WrittenBattleID")
                            : r.Field<int>("AudioBattleID"),
                    MatchId = r.Field<int>("MatchPosition"),
                    RoundNumber = r.Field<int>("MatchRound"),
                    UserId1 = r.Field<int?>("UserID1"),
                    UserId2 = r.Field<int?>("UserID2"),
                    WinnerId = r.Field<int?>("WinnerID")
                }).OrderBy(x => x.MatchId).ToList();
            return allTournamentMatches;
        }

        /// <summary>
        ///     Adds the match.
        /// </summary>
        /// <param name="m">The m.</param>
        private void AddMatch([NotNull] RapMatch m)
        {
            if (this.TournamentRoundMatches.ContainsKey(m.RoundNumber))
            {
                if (!this.TournamentRoundMatches[m.RoundNumber].ContainsKey(m.MatchId))
                {
                    this.TournamentRoundMatches[m.RoundNumber].Add(m.MatchId, m);
                }
            }
            else
            {
                this.TournamentRoundMatches.Add(m.RoundNumber, new SortedList<int, RapMatch>());
                this.TournamentRoundMatches[m.RoundNumber].Add(m.MatchId, m);
            }
        }

        /// <summary>
        ///     Generates the tournament results.
        /// </summary>
        /// <param name="rounds">The rounds.</param>
        private void GenerateTournamentResults([NotNull] int rounds)
        {
            TournamentMatches = GetAllMatches();
            for (int round = 1, matchId = 1; round <= rounds; round++)
            {
                var matchesInARound = 1 << (rounds - round);
                for (var roundMatch = 1; roundMatch <= matchesInARound; roundMatch++, matchId++)
                {
                    int? userId1;
                    int? userId2;
                    int? winner = null;
                    int? battleId;
                    if (round == 1)
                    {
                        userId1 = TournamentMatches[roundMatch - 1].UserId1;
                        userId2 = TournamentMatches[roundMatch - 1].UserId2;
                    }
                    else
                    {
                        var match1 = (matchId - (matchesInARound*2) + (roundMatch - 1));
                        var match2 = match1 + 1;
                        userId1 = this.TournamentRoundMatches[round - 1][match1].WinnerId;
                        userId2 = this.TournamentRoundMatches[round - 1][match2].WinnerId;
                    }
                    try
                    {
                        battleId = this.TournamentMatches[matchId - 1].BattleId;
                    }
                    catch (Exception)
                    {
                        battleId = null;
                    }
                    try
                    {
                        winner = TournamentMatches.Find(m => m.MatchId == matchId).WinnerId;
                    }
                    catch (NullReferenceException)
                    {
                        if (userId1 != null && userId2 != null)
                        {
                            Db.add_tournament_match(this.TournamentId, matchId, round, this.TournamentType,
                                (int) userId1, (int) userId2);
                        }
                        else if (userId1 != null)
                        {
                            //winner = userId1;
                        }
                        else if (userId2 != null)
                        {
                            //winner = userId2;
                        }
                        else
                        {
                            //no winner chosen yet, no match created
                            winner = null;
                        }
                    }
                    this.AddMatch(new RapMatch
                    {
                        BattleId = battleId,
                        MatchId = matchId,
                        UserId1 = userId1,
                        UserId2 = userId2,
                        RoundNumber = round,
                        WinnerId = winner
                    });
                }
            }
        }

        /// <summary>
        ///     Generates the third place result.
        /// </summary>
        /// <param name="rounds">The rounds.</param>
        private void GenerateThirdPlaceResult([NotNull] int rounds)
        {
            var semifinalMatchId1 = this.TournamentRoundMatches[rounds - 1].Keys.ElementAt(0);
            var semifinalMatchId2 = this.TournamentRoundMatches[rounds - 1].Keys.ElementAt(1);
            var semifinal1 = this.TournamentRoundMatches[rounds - 1][semifinalMatchId1];
            var semifinal2 = this.TournamentRoundMatches[rounds - 1][semifinalMatchId2];
            int? semifinalLoser1 = null;
            int? semifinalLoser2 = null;
            //to fix a bug where it would place third place results automatically
            if (semifinal1.WinnerId != null)
            {
                semifinalLoser1 = (semifinal1.WinnerId == semifinal1.UserId1)
                    ? semifinal1.UserId2
                    : semifinal1.UserId1;
            }
            if (semifinal2.WinnerId != null)
            {
                semifinalLoser2 = (semifinal2.WinnerId == semifinal2.UserId1)
                    ? semifinal2.UserId2
                    : semifinal2.UserId1;
            }
            int? thirdPlacewinner = null;
            var thirdPlaceMatchId = (1 << rounds) + 1;
            try
            {
                thirdPlacewinner = this.TournamentMatches.Find(m => m.MatchId == thirdPlaceMatchId).WinnerId;
            }
            catch (NullReferenceException)
            {
                if (semifinalLoser1 != null && semifinalLoser2 != null)
                {
                    Db.add_tournament_match(this.TournamentId, thirdPlaceMatchId, -1, this.TournamentType,
                        (int) semifinalLoser1, (int) semifinalLoser2);
                }
            }
            try
            {
                this.ThirdPlaceMatch = new RapMatch
                {
                    BattleId = this.TournamentMatches.Find(m => m.MatchId == thirdPlaceMatchId).BattleId,
                    MatchId = (1 << rounds) + 1,
                    UserId1 = semifinalLoser1,
                    UserId2 = semifinalLoser2,
                    RoundNumber = 1,
                    WinnerId = thirdPlacewinner
                };
            }
            catch
            {
                this.ThirdPlaceMatch = new RapMatch
                {
                    BattleId = null,
                    MatchId = (1 << rounds) + 1,
                    UserId1 = semifinalLoser1,
                    UserId2 = semifinalLoser2,
                    RoundNumber = 1,
                    WinnerId = thirdPlacewinner
                };
            }
        }

        /// <summary>
        ///     Generates the results in HTML5
        /// </summary>
        /// <param name="tournament">The tournament.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public string GenerateResultsInHtml([NotNull] RapTournament tournament, [NotNull] string url)
        {
            Contract.Requires(tournament != null);
            var rounds = tournament.TournamentRoundMatches.Count;
            var battlers = 1 << rounds;
            var maxRows = battlers << 1;
            var htmlTable = new StringBuilder();
            const string avatarHtml = "<a href='/pages/profile/{0}'><img src='{1}' class='pull-left' alt='' height='25' width='25' />";
            htmlTable.AppendLine("<style type=\"text/css\">");
            htmlTable.AppendLine("    .thd {background: rgb(220,220,220); text-align: center;}");
            htmlTable.AppendLine(
                "    .team {background-color: #f5f5f5; font: bold 10pt Arial;}");
            htmlTable.AppendLine("    .winner {background-color: #f5f5f5;}");
            htmlTable.AppendLine("    .vs {font: bold 7pt Arial; vertical-align: middle !important;}");
            htmlTable.AppendLine(
                "    td, th {padding: 3px 15px; border-right: dotted 2px rgb(200,200,200); text-align: right;}");
            htmlTable.AppendLine("    h1 { margin-top: 24pt;}");
            htmlTable.AppendLine("</style>");

            htmlTable.AppendLine("<div class=\"bs-callout bs-callout-info\">");
            htmlTable.AppendLine("Tournament Results");
            htmlTable.AppendLine("</div>");
            htmlTable.AppendLine("<table id=\"tournamentTable\" class=\"table\" border=\"0\" cellspacing=\"0\">");
            for (var row = 0; row <= maxRows; row++)
            {
                var cumulativeMatches = 0;
                htmlTable.AppendLine("    <tr>");
                for (var col = 1; col <= rounds + 1; col++)
                {
                    var matchSpan = 1 << (col + 1);
                    var matchWhiteSpan = (1 << col) - 1;
                    var columnStaggerOffset = matchWhiteSpan >> 1;
                    switch (row)
                    {
                        case 0:
                            if (col <= rounds)
                            {
                                htmlTable.AppendLine("        <th class=\"thd\">Round " + col + "</th>");
                            }
                            else
                            {
                                htmlTable.AppendLine("        <th class=\"thd\">Winner</th>");
                            }
                            break;
                        case 1:
                            htmlTable.AppendLine("        <td class=\"white_span\" rowspan=\"" +
                                                 (matchWhiteSpan - columnStaggerOffset) + "\">&nbsp;</td>");
                            break;
                        default:
                        {
                            var effectiveRow = row + columnStaggerOffset;
                            if (col <= rounds)
                            {
                                var positionInMatchSpan = effectiveRow%matchSpan;
                                positionInMatchSpan = (positionInMatchSpan == 0) ? matchSpan : positionInMatchSpan;
                                var colMatchNum = (effectiveRow/matchSpan) + ((positionInMatchSpan < matchSpan) ? 1 : 0);
                                var effectiveMatchId = cumulativeMatches + colMatchNum;
                                if ((positionInMatchSpan == 1) && (effectiveRow%matchSpan == positionInMatchSpan))
                                {
                                    htmlTable.AppendLine("        <td class=\"white_span\" rowspan=\"" + matchWhiteSpan +
                                                         "\">&nbsp;</td>");
                                }
                                else if ((positionInMatchSpan == (matchSpan >> 1)) &&
                                         (effectiveRow%matchSpan == positionInMatchSpan))
                                {
                                    var userId = tournament.TournamentRoundMatches[col][effectiveMatchId].UserId1;
                                    var winnerId = tournament.TournamentRoundMatches[col][effectiveMatchId].WinnerId;
                                    if (userId != null)
                                    {
                                        var avatar =
                                            new YafAvatars(YafContext.Current.BoardSettings).GetAvatarUrlForUser((int)userId);
                                        if (winnerId == null)
                                        {
                                            htmlTable.AppendLine("        <td class=\"team\"> " + avatarHtml.FormatWith(userId, avatar) +
                                                                 UserMembershipHelper.GetDisplayNameFromID(
                                                                     (int)
                                                                         tournament.TournamentRoundMatches[col][
                                                                             effectiveMatchId]
                                                                             .UserId1) + "</td>");
                                        }
                                        else
                                        {
                                            if (winnerId == userId)
                                            {
                                                htmlTable.AppendLine("        <td class=\"team success\"> " + avatarHtml.FormatWith(userId, avatar) +
                                                                     UserMembershipHelper.GetDisplayNameFromID(
                                                                         (int)
                                                                             tournament.TournamentRoundMatches[col][
                                                                                 effectiveMatchId]
                                                                                 .UserId1) + "</td>");
                                            }
                                            else
                                            {
                                                htmlTable.AppendLine("        <td class=\"team danger\"> " + avatarHtml.FormatWith(userId, avatar) +
                                                                     UserMembershipHelper.GetDisplayNameFromID(
                                                                         (int)
                                                                             tournament.TournamentRoundMatches[col][
                                                                                 effectiveMatchId]
                                                                                 .UserId1) + "</td>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        htmlTable.AppendLine("        <td class=\"team\"> " + "</td>");
                                    }
                                }
                                else if ((positionInMatchSpan == ((matchSpan >> 1) + 1)) &&
                                         (effectiveRow%matchSpan == positionInMatchSpan))
                                {
                                    var battleUrl = url +
                                                    tournament.TournamentRoundMatches[col][effectiveMatchId].BattleId;
                                    htmlTable.AppendLine("        <td class=\"vs\" rowspan=\"" + matchWhiteSpan +
                                                         "\"><a href='" + battleUrl + "'>VS</a></td>");
                                }
                                else if ((positionInMatchSpan == matchSpan) && (effectiveRow%matchSpan == 0))
                                {
                                    var userId = tournament.TournamentRoundMatches[col][effectiveMatchId].UserId2;
                                    var winnerId = tournament.TournamentRoundMatches[col][effectiveMatchId].WinnerId;
                                    if (userId != null)
                                    {
                                        var avatar =
                                            new YafAvatars(YafContext.Current.BoardSettings).GetAvatarUrlForUser(
                                                (int) userId);
                                        if (winnerId == null)
                                        {
                                            htmlTable.AppendLine("        <td class=\"team\"> " + avatarHtml.FormatWith(userId, avatar) +
                                                                 UserMembershipHelper.GetDisplayNameFromID(
                                                                     (int)
                                                                         tournament.TournamentRoundMatches[col][
                                                                             effectiveMatchId].UserId2)
                                                                 + "</td>");
                                        }
                                        else
                                        {
                                            if (winnerId == userId)
                                            {
                                                htmlTable.AppendLine("        <td class=\"team success\"> " + avatarHtml.FormatWith(userId, avatar) +
                                                                     UserMembershipHelper.GetDisplayNameFromID(
                                                                         (int)
                                                                             tournament.TournamentRoundMatches[col][
                                                                                 effectiveMatchId].UserId2)
                                                                     + "</td>");
                                            }
                                            else
                                            {
                                                htmlTable.AppendLine("        <td class=\"team danger\"> " + avatarHtml.FormatWith(userId, avatar) +
                                                                     UserMembershipHelper.GetDisplayNameFromID(
                                                                         (int)
                                                                             tournament.TournamentRoundMatches[col][
                                                                                 effectiveMatchId].UserId2)
                                                                     + "</td>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        htmlTable.AppendLine("        <td class=\"team\"> " + "</td>");
                                    }
                                }
                            }
                            else
                            {
                                if (row == columnStaggerOffset + 2)
                                {
                                    var winnerId = tournament.TournamentRoundMatches[rounds][cumulativeMatches].WinnerId;
                                    if (winnerId != null)
                                    {
                                        var avatar =
                                            new YafAvatars(YafContext.Current.BoardSettings).GetAvatarUrlForUser(
                                                (int)winnerId);
                                        if (tournament.TournamentStatus != RapTournamentStatus.Over)
                                        {
                                            tournament.TournamentStatus = RapTournamentStatus.Over;
                                            Db.update_tournamentstate(tournament.TournamentId,
                                                (int) tournament.TournamentStatus);
                                        }
                                        htmlTable.AppendLine("        <td class=\"winner warning\"> " + avatarHtml.FormatWith(winnerId, avatar) +
                                                             UserMembershipHelper.GetDisplayNameFromID(
                                                                 (int)
                                                                     tournament.TournamentRoundMatches[rounds][
                                                                         cumulativeMatches].WinnerId) + "</td>");
                                    }
                                    else
                                    {
                                        htmlTable.AppendLine("        <td class=\"winner\"> " + "</td>");
                                    }
                                }
                                else if (row == columnStaggerOffset + 3)
                                {
                                    htmlTable.AppendLine("        <td class=\"white_span\" rowspan=\"" +
                                                         (matchWhiteSpan - columnStaggerOffset) + "\">&nbsp;</td>");
                                }
                            }
                        }
                            break;
                    }
                    if (col <= rounds)
                    {
                        cumulativeMatches += tournament.TournamentRoundMatches[col].Count;
                    }
                }
                htmlTable.AppendLine("    </tr>");
            }
            htmlTable.AppendLine("</table>");
            if (tournament.ThirdPlaceMatch.UserId1 != null && tournament.ThirdPlaceMatch.UserId2 != null)
            {
                htmlTable.AppendLine("<div class=\"bs-callout bs-callout-info\">");
                htmlTable.AppendLine("Third Place Match");
                htmlTable.AppendLine("</div>");
                htmlTable.AppendLine("<table border=\"0\" class=\"table\" cellspacing=\"0\">");
                htmlTable.AppendLine("    <tr class=\"info\">");
                htmlTable.AppendLine("        <th class=\"thd\">Round 1</th>");
                htmlTable.AppendLine("        <th class=\"thd\">Third Place</th>");
                htmlTable.AppendLine("    </tr>");
                htmlTable.AppendLine("    <tr>");
                htmlTable.AppendLine("        <td class=\"white_span\">&nbsp;</td>");
                htmlTable.AppendLine("        <td class=\"white_span\" rowspan=\"2\">&nbsp;</td>");
                htmlTable.AppendLine("    </tr>");
                htmlTable.AppendLine("    <tr>");
                if (tournament.ThirdPlaceMatch.UserId1 != null)
                {
                    var avatar =
                        new YafAvatars(YafContext.Current.BoardSettings).GetAvatarUrlForUser(
                            (int)tournament.ThirdPlaceMatch.UserId1);
                    if (tournament.ThirdPlaceMatch.WinnerId == tournament.ThirdPlaceMatch.UserId1)
                    {
                        htmlTable.AppendLine("        <td class=\"team success\"> " + avatarHtml.FormatWith((int)tournament.ThirdPlaceMatch.UserId1, avatar) +
                                             UserMembershipHelper.GetDisplayNameFromID(
                                                 (int) tournament.ThirdPlaceMatch.UserId1) +
                                             "</td>");
                    }
                    else if (tournament.ThirdPlaceMatch.WinnerId != null)
                    {
                        htmlTable.AppendLine("        <td class=\"team danger\"> " + avatarHtml.FormatWith((int)tournament.ThirdPlaceMatch.UserId1, avatar) +
                                             UserMembershipHelper.GetDisplayNameFromID(
                                                 (int) tournament.ThirdPlaceMatch.UserId1) +
                                             "</td>");
                    }
                    else
                    {
                        htmlTable.AppendLine("        <td class=\"team\"> " + avatarHtml.FormatWith((int)tournament.ThirdPlaceMatch.UserId1, avatar) +
                                             UserMembershipHelper.GetDisplayNameFromID(
                                                 (int) tournament.ThirdPlaceMatch.UserId1) +
                                             "</td>");
                    }
                }
                else
                {
                    htmlTable.AppendLine("        <td class=\"team\"> " + "</td>");
                }
                htmlTable.AppendLine("    </tr>");
                htmlTable.AppendLine("    <tr>");
                htmlTable.AppendLine("        <td class=\"vs\"><a href='" + url + tournament.ThirdPlaceMatch.BattleId + "'>VS</a></td>");
                if (tournament.ThirdPlaceMatch.WinnerId != null)
                {
                    var avatar =
                        new YafAvatars(YafContext.Current.BoardSettings).GetAvatarUrlForUser(
                            (int)tournament.ThirdPlaceMatch.WinnerId);
                    htmlTable.AppendLine("        <td class=\"winner warning\"> " + avatarHtml.FormatWith((int)tournament.ThirdPlaceMatch.WinnerId, avatar) +
                                         UserMembershipHelper.GetDisplayNameFromID(
                                             (int) tournament.ThirdPlaceMatch.WinnerId) +
                                         "</td>");
                }
                else
                {
                    htmlTable.AppendLine("        <td class=\"winner\"> " + "</td>");
                }
                htmlTable.AppendLine("    </tr>");
                htmlTable.AppendLine("    <tr>");
                if (tournament.ThirdPlaceMatch.UserId2 != null)
                {
                    var avatar =
                        new YafAvatars(YafContext.Current.BoardSettings).GetAvatarUrlForUser(
                            (int)tournament.ThirdPlaceMatch.UserId2);
                    if (tournament.ThirdPlaceMatch.WinnerId == tournament.ThirdPlaceMatch.UserId2)
                    {
                        htmlTable.AppendLine("        <td class=\"team success\"> " + avatarHtml.FormatWith((int)tournament.ThirdPlaceMatch.UserId2, avatar) +
                                             UserMembershipHelper.GetDisplayNameFromID(
                                                 (int) tournament.ThirdPlaceMatch.UserId2) +
                                             "</td>");
                    }
                    else if (tournament.ThirdPlaceMatch.WinnerId != null)
                    {
                        htmlTable.AppendLine("        <td class=\"team danger\"> " + avatarHtml.FormatWith((int)tournament.ThirdPlaceMatch.UserId2, avatar) +
                                             UserMembershipHelper.GetDisplayNameFromID(
                                                 (int) tournament.ThirdPlaceMatch.UserId2) +
                                             "</td>");
                    }
                    else
                    {
                        htmlTable.AppendLine("        <td class=\"team\"> " + avatarHtml.FormatWith((int)tournament.ThirdPlaceMatch.UserId2, avatar) +
                                             UserMembershipHelper.GetDisplayNameFromID(
                                                 (int) tournament.ThirdPlaceMatch.UserId2) +
                                             "</td>");
                    }
                }
                else
                {
                    htmlTable.AppendLine("        <td class=\"team\"> " + "</td>");
                }
                htmlTable.AppendLine("        <td class=\"white_span\">&nbsp;</td>");
                htmlTable.AppendLine("    </tr>");
                htmlTable.AppendLine("</table>");
            }
            return htmlTable.ToString();
        }

        /// <summary>
        ///     Creates the tournament.
        /// </summary>
        public void CreateTournament([NotNull] int contestants, [NotNull] int rounds,
            [NotNull] RapBattleType tournamentType)
        {
            Db.create_tournament(contestants, rounds, DateTime.Now, (int) RapTournamentStatus.NotStarted,
                (int) tournamentType);
        }

        #endregion
    }
}