using System;
using System.IO;
using Ext.Net;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Core;
using YAF.Core;
using YAF.Types;
using YAF.Types.Extensions;

namespace FreestyleOnline.Mobile.Controls
{
    public partial class RapBattleAudioListener : RapUserControl
    {
        [NotNull] public RapBattleAudio Battle { get; set; }
        protected string AudioPath1 { get; private set; }
        protected string AudioPath2 { get; private set; }
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            //TODO: Fix performance speed in reading wav file. maybe .mp3 will increase loading time.
            if (this.Battle.User1Audio.IsNotSet())
            {
                this.audioplayer1.Visible = false;
                if (this.PageContext.PageUserID != this.Battle.UserId1)
                {
                    this.Recorder1Description.Controls.Add(new Label
                    {
                        Text =
                            this.Text("BATTLES", "NOT_RECORDED")
                                .FormatWith(UserMembershipHelper.GetDisplayNameFromID(this.Battle.UserId1))
                    });
                }
            }
            else
            {
                var wavExtension = Path.GetExtension(this.Battle.User1Audio);
                this.AudioPath1 = this.Battle.User1Audio.Substring(0, this.Battle.User1Audio.Length - wavExtension.Length);
            }
            if (this.Battle.UserId2 == null)
            {
                this.audioplayer2.Visible = false;
            }
            if (this.Battle.User2Audio.IsNotSet() && this.Battle.UserId2 != null)
            {
                this.audioplayer2.Visible = false;
                if (this.PageContext.PageUserID != this.Battle.UserId2)
                {
                    this.Recorder2Description.Controls.Add(new Label
                    {
                        Text =
                            this.Text("BATTLES", "NOT_RECORDED")
                                .FormatWith(UserMembershipHelper.GetDisplayNameFromID((int) this.Battle.UserId2))
                    });
                }
            }
            if (!this.Battle.User2Audio.IsNotSet())
            {
                var wavExtension = Path.GetExtension(this.Battle.User2Audio);
                this.AudioPath2 = this.Battle.User2Audio.Substring(0, this.Battle.User2Audio.Length - wavExtension.Length); 
            }
        }
    }
}