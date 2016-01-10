#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Types.Enums;
using FreestyleOnline.classes;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Types;

#endregion

namespace FreestyleOnline.Controls
{
    public partial class SearchBattles : RapUserControl
    {
        protected RapBattleType Type { get; set; }

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SearchBattlesGrid.Visible = false;
        }

        private void BindUsersBattles(List<RapBattle> Battles)
        {
            this.SearchBattlesGrid.DataSource = Battles;
            this.SearchBattlesGrid.DataBind();
            this.SearchBattlesGrid.Visible = true;
        }

        /// <summary>
        ///     Called when [search button_ click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void OnSearchButton_Click(object sender, EventArgs e)
        {
            var userId = this.Users.UserIdSelected;
            Type = RapBattleType.Written;
            switch (this.BattleTypeField.Text)
            {
                case "Audio":
                    Type = RapBattleType.Audio;
                    break;
                case "Written":
                    Type = RapBattleType.Written;
                    break;
                case "Video":
                    Type = RapBattleType.Video;
                    break;
            }
            var userData = new UserData(userId);
            if (Type == RapBattleType.Written && userId != 0 && userId != 1)
            {
                BindUsersBattles(userData.GetUsersWrittenBattles().Cast<RapBattle>().ToList());
            }
            if (Type == RapBattleType.Audio && userId != 0 && userId != 1)
            {
                BindUsersBattles(userData.GetUsersAudioBattles().Cast<RapBattle>().ToList());
            }
        }
    }
}