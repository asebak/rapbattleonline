#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Common.Types.Attributes;
using Common.Types.Enums;
using RapBattleAudio.controls;

#endregion

namespace RapBattleAudio.classes
{
    public class RapTimer
    {
        #region Members

        [NotNull] private readonly TextBlock _textBlock;
        [NotNull] private readonly DispatcherTimer _timer = new DispatcherTimer();
        [NotNull] private int _currentAudioLength;
        [NotNull] private TimeSpan _maxAudioLength;
        [NotNull] private readonly Recorder _recorderInstance;
        [NotNull] private readonly RapBeats _beats;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RapTimer" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="audioLength">Length of the audio.</param>
        /// <param name="r">The r.</param>
        /// <param name="b">The b.</param>
        public RapTimer(TextBlock text, TimeSpan audioLength, Recorder r, RapBeats b)
        {
            this._textBlock = text;
            this._maxAudioLength = audioLength;
            this._timer.Interval = new TimeSpan(0, 0, 0, 1);
            this._timer.Tick += this._timer_Tick;
            this._currentAudioLength = 0;
            this._recorderInstance = r;
            this._beats = b;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Tick event of the _timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void _timer_Tick([NotNull] object sender, [NotNull] EventArgs e)
        {
            var span = new TimeSpan(0, 0, ++_currentAudioLength);
            this._textBlock.Text = string.Format("{0}:{1:00}", (int) span.TotalMinutes, span.Seconds);
            if (this._maxAudioLength.TotalSeconds - span.TotalSeconds <= 10)
            {
                this._textBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
            if (this._maxAudioLength.TotalSeconds - span.TotalSeconds <= 0)
            {
                this._timer.Stop();
                this._currentAudioLength = 0;
                this._recorderInstance.UpdateRecordingState(PlayerState.Stop);
                this._recorderInstance.Stop();
                this._beats.BeatPlayer.Stop();
            }
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            this._textBlock.Text = "";
            this._timer.Start();
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            this._currentAudioLength = 0;
            this._textBlock.Foreground = new SolidColorBrush(Colors.Black);
            this._timer.Stop();
        }

        #endregion
    }
}