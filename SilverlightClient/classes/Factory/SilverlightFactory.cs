#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Common.Interfaces;
using Common.Models;
using Common.Types;
using Common.Types.Attributes;
using Newtonsoft.Json;
using RapBattleAudio.classes.UI;
using RapBattleAudio.controls;

#endregion

namespace RapBattleAudio.classes.Factory
{
    public class SilverlightFactory : ISilverlightFactory
    {
        #region ISilverlightFactory Members

        [NotNull] private readonly AudioBattleModel _m;

        [NotNull] private readonly WebApi _apiHelper;
        [NotNull]
        private readonly RapBeats _beatsInstance;

        #endregion

        #region Constructor

        public SilverlightFactory([NotNull] AudioBattleModel m, [NotNull] RapBeats b)
        {
            this._m = m;
            this._beatsInstance = b;
            this._apiHelper = new WebApi("beat");
            this._apiHelper.ChangeToLocalHost();
        }


        #endregion

        #region Methods

        public void Build<T>([NotNull] params T[] args)
        {
            if (args.GetType() == typeof (Recorder[]))
            {
                var recorder1 = args[0] as Recorder;
                var recorder2 = args[1] as Recorder;
                recorder1.Timer = new RapTimer(recorder1.TimerText, this._m.Length, recorder1, this._beatsInstance);
                recorder2.Timer = new RapTimer(recorder2.TimerText, this._m.Length, recorder2, this._beatsInstance);
                recorder1.UserId = this._m.UserId1;
                recorder2.UserId = this._m.UserId2;
                recorder1.Visibility = this._m.Recorder1Visible ? Visibility.Visible : Visibility.Collapsed;
                recorder2.Visibility = this._m.Recorder2Visible ? Visibility.Visible : Visibility.Collapsed;
                recorder1.BeatsList = this._beatsInstance;
                recorder2.BeatsList = this._beatsInstance;
            }
            if (args.GetType() == typeof (RapBeats[]))
            {
                var beatsDropDown = args[0] as RapBeats;
                beatsDropDown.BeatsList.IsEnabled = this._m.EnableBeats;
                beatsDropDown.Visibility = this._m.ShowBeats ? Visibility.Visible : Visibility.Collapsed;
                beatsDropDown.WebClient.DownloadStringCompleted += (sender, e) =>
                {
                    if (e.Error == null)
                    {
                        var beats = JsonConvert.DeserializeObject<List<BeatModel>>(e.Result);
                        beatsDropDown.BeatsList.ItemsSource = beats;
                        beatsDropDown.BeatsList.DisplayMemberPath = "Name";

                        if (this._m.Beat != null)
                        {
                            beatsDropDown.UpdatedBeat((int) this._m.Beat);
                        }
                        else
                        {
                            beatsDropDown.UpdatedBeat(0);
                        }
                    }
                };
                beatsDropDown.WebClient.DownloadStringAsync(new Uri(this._apiHelper.GetByAction("getallbeats")));
            }

            #endregion
        }
    }
}