#region Using

using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Common.Types.Enums;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using FreestyleOnline.classes.Providers;
using YAF.Core;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    /// <summary>
    ///     This is a repeating list of all the votes that were done in a battle
    /// </summary>
    public partial class BattleVoteDisplay : RapUserControl
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the SQL column identifier tag.
        /// </summary>
        /// <value>
        ///     The SQL column identifier tag.
        /// </value>
        protected string SqlColumnIdTag { get; set; }

        /// <summary>
        ///     Gets or sets the type of the battle.
        /// </summary>
        /// <value>
        ///     The type of the battle.
        /// </value>
        public RapBattleType BattleType { get; set; }
        /// <summary>
        /// Gets or sets the display name1.
        /// </summary>
        /// <value>
        /// The display name1.
        /// </value>
        protected string DisplayName1 { get; set; }
        /// <summary>
        /// Gets or sets the display name2.
        /// </summary>
        /// <value>
        /// The display name2.
        /// </value>
        protected string DisplayName2 { get; set; }
        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            this.GetService<ClientProviders>()
                .RegisterClientScriptBlock(this, "votesRepeatingDisplay", "~/js/BattleVoteDisplay.js", false, true);
            base.OnInit(e);
        }
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            var query = this.GetService<UrlProvider>().GetQuery()[0];
            RapBattle battle;
            switch (BattleType)
            {
                case RapBattleType.Written:
                    battle = new RapBattleWritten(this.PageContext.PageUserID, Convert.ToInt32(query));
                    SqlColumnIdTag = "WrittenBattleRatingID";
                    break;
                case RapBattleType.Audio:
                    battle = new RapBattleAudio(this.PageContext.PageUserID, Convert.ToInt32(query));
                    SqlColumnIdTag = "AudioBattleRatingID";
                    break;
                case RapBattleType.Video:
                    battle = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var votes = battle.GetVotes();
            if (votes.Rows.Count > 0)
            {

                foreach (DataRow dr in votes.Rows)
                {
                    this.DisplayName1 = UserMembershipHelper.GetDisplayNameFromID(Convert.ToInt32(dr["UserID1"]));
                    this.DisplayName2 = UserMembershipHelper.GetDisplayNameFromID(Convert.ToInt32(dr["UserID2"]));
                }
                this.VoteDisplay.DataSource = votes;
                this.VoteDisplay.DataBind();
            }
            else
            {
                this.Visible = false;
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the ItemDataBound event of the VoteDisplay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void VoteDisplay_ItemDataBound([NotNull] object sender, [NotNull] RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (this.DisplayName1 != this.PageContext.PageUserName && this.DisplayName2 != this.PageContext.PageUserName)
                {
                    return;
                }
                var r = (DataRowView)e.Item.DataItem;
                var tr = (HtmlTableRow)(e.Item.FindControl("votingcell"));
                var u1w = (int)r["User1Wordplay"];
                var u1m = (int)r["User1Metaphores"];
                var u1f = (int)r["User1Flow"];
                var u1mu = (int)r["User1Multis"];
                var u1p = (int)r["User1Punchlines"];
                var u2w = (int)r["User2Wordplay"];
                var u2m = (int)r["User2Metaphores"];
                var u2f = (int)r["User2Flow"];
                var u2mu = (int)r["User2Multis"];
                var u2p = (int)r["User2Punchlines"];
                var u1a = (u1w + u1m + u1f + u1mu + u1p)/5;
                var u2a = (u2w + u2m + u2f + u2mu + u2p)/5;

                if (this.DisplayName1 == this.PageContext.PageUserName)
                {
                    string cssClass;
                    if (u1a > u2a)
                    {
                        cssClass = "success";
                    }else if (u1a < u2a)
                    {
                        cssClass = "danger";
                    }
                    else
                    {
                        cssClass = "info";
                    }
                    tr.Attributes.Add("class", cssClass);
                }
                else
                {
                    string cssClass;
                    if (u1a < u2a)
                    {
                        cssClass = "success";
                    }
                    else if (u1a > u2a)
                    {
                        cssClass = "danger";
                    }
                    else
                    {
                        cssClass = "info";
                    }
                    tr.Attributes.Add("class", cssClass);
                }

            }
        }

        #endregion

    }
}