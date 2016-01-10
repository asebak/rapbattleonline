using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ext.Net;
using FreestyleOnline.classes.Core;
using FreestyleOnline.Controls;
using YAF.Types;

namespace FreestyleOnline.classes.Types.UI
{
    public class RapWrittenBattleASPNetConfiguration
    {
        #region Properties

        [NotNull] public Button Join = new Button();

        [NotNull] public CountDown Timer = new CountDown();

        [CanBeNull] public Label TimerIcon = new Label();

        [CanBeNull] public ProfileLink User1 = new ProfileLink();

        [NotNull] public Button User1Submit = new Button();

        [CanBeNull] public ProfileLink User2 = new ProfileLink();

        [NotNull] public Button User2Submit = new Button();

        [CanBeNull] public TextArea Verse1 = new TextArea();

        [CanBeNull] public TextArea Verse2 = new TextArea();

        #endregion
    }
}