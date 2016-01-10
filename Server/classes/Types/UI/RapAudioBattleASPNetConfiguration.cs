using Ext.Net;
using FreestyleOnline.classes.Core;
using FreestyleOnline.Controls;
using YAF.Types;

namespace FreestyleOnline.classes.Types.UI
{
    public class RapAudioBattleASPNetConfiguration
    {
        #region Properties

        [CanBeNull] public Button Join = new Button();

        [NotNull] public CountDown Timer = new CountDown();

        [CanBeNull] public Label TimerIcon = new Label();

        [CanBeNull] public ProfileLink User1 = new ProfileLink();

        [CanBeNull] public ProfileLink User2 = new ProfileLink();

        #endregion
    }
}