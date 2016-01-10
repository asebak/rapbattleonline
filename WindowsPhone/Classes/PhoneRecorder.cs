#region Using

using System;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using FreestyleOnline___WP.Classes.Digital_Signal_Processing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

#endregion

namespace FreestyleOnline___WP.Classes
{
    public class PhoneRecorder
    {
        #region Members

        private readonly Microphone _mic;
        private readonly MemoryStream _stream = new MemoryStream();
        private byte[] _buffer;
        private SoundEffectInstance _sound;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="PhoneRecorder" /> class.
        /// </summary>
        /// <param name="microphone">The microphone.</param>
        public PhoneRecorder(Microphone microphone)
        {
            _mic = microphone;
            var timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(33)};
            timer.Tick += timer_Tick;
            timer.Start();
            _mic.BufferReady += microphone_BufferReady;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Plays the recording right back
        /// </summary>
        public void PlayBack()
        {
            if (_stream.Length > 0)
            {
                var soundThread = new Thread(PlayRecording);
                soundThread.Start();
            }
        }

        /// <summary>
        ///     Plays the recording.
        /// </summary>
        private void PlayRecording()
        {
            _sound = new SoundEffect(_stream.ToArray(), _mic.SampleRate, AudioChannels.Mono).CreateInstance();
            _sound.Play();
        }

        /// <summary>
        ///     Starts Recording
        /// </summary>
        public void Start()
        {
            _mic.BufferDuration = TimeSpan.FromMilliseconds(500);
            _buffer = new byte[_mic.GetSampleSizeInBytes(_mic.BufferDuration)];
            _stream.SetLength(0);
            WavHelper.WriteWavHeader(_stream, _mic.SampleRate);
            _mic.Start();
        }

        /// <summary>
        ///     Stops Recording
        /// </summary>
        public void Stop()
        {
            if (_mic.State == MicrophoneState.Started)
            {
                _mic.Stop();
                WavHelper.UpdateWavHeader(_stream);
            }
        }

        /// <summary>
        ///     Handles the BufferReady event of the microphone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void microphone_BufferReady(object sender, EventArgs e)
        {
            _mic.GetData(_buffer);
            _stream.Write(_buffer, 0, _buffer.Length);
        }

        /// <summary>
        ///     Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            FrameworkDispatcher.Update();
        }

        #endregion
    }
}