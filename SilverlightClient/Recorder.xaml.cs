#region Using

using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Common.Types.Attributes;
using Common.Types.Enums;
using RapBattleAudio.classes;
using RapBattleAudio.controls;

#endregion

namespace RapBattleAudio
{
    public partial class Recorder
    {
        #region Members

        [NotNull] private CaptureSource _grabber;
        [NotNull] private MemoryAudioSink _storage;
        [NotNull] private MemoryStream _theMemStream;
        [NotNull] private RapTimer _timer;
        [NotNull] private WaveMediaStreamSource _wavMss;
        [NotNull] private RapUploadClient _audioUpload;

        [NotNull]
        public RapBeats BeatsList { get; set; }

        #endregion

        #region Properties

        [NotNull]
        public PlayerState RecorderState { get; set; }

        [CanBeNull]
        public RapTimer Timer
        {
            get { return this._timer; }
            set { this._timer = value; }
        }

        [CanBeNull]
        public int? UserId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Recorder" /> class.
        /// </summary>
        public Recorder()
        {
            InitializeComponent();
            InitializeAudio();
            this.RecorderState = PlayerState.Initial;
            this.UpdateRecordingState(this.RecorderState);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the audio.
        /// </summary>
        private void InitializeAudio()
        {
            var audioDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
            this._grabber = new CaptureSource {VideoCaptureDevice = null, AudioCaptureDevice = audioDevice};
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            this.RecorderState = PlayerState.Stop;
            if (this._grabber.State == CaptureState.Started)
            {
                this._grabber.Stop();
            }
            try
            {
                this._theMemStream = new MemoryStream();
                WavManager.SavePcmToWav(this._storage.BackingStream, this._theMemStream,
                    new AudioFormatEx(this._storage.CurrentFormat));
                this._theMemStream.Position = 0;
            }
            catch (Exception ex)
            {
                this.StatusText.Text = ex.Message;
            }
        }


        /// <summary>
        ///     Records this instance.
        /// </summary>
        protected void Record()
        {
            this.RecorderState = PlayerState.Record;
            if (!this.EnsureAudioAccess())
            {
                return;
            }
            if (this._grabber.State != CaptureState.Stopped)
            {
                this._grabber.Stop();
            }
            this._storage = new MemoryAudioSink {CaptureSource = this._grabber};
            this._grabber.Start();
            this.RecordingOutput.Stop();
        }

        /// <summary>
        ///     Ensures the audio access.
        /// </summary>
        /// <returns></returns>
        private bool EnsureAudioAccess()
        {
            return CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess();
        }


        /// <summary>
        ///     Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void SaveButton_Click([NotNull] object sender, [NotNull] RoutedEventArgs e)
        {
            if (this.UserId != null)
            {
                var filename = Guid.NewGuid() + ".wav";
                this.RecorderState = PlayerState.Save;
                UpdateRecordingState(this.RecorderState);
                this._audioUpload = new RapUploadClient(this.StatusText, (int) this.UserId,
                    RapAudioContext.Current.Service.BattleId, this.BeatsList.SelectedBeat,
                    (bool) this.ProcessAudio.IsChecked);
                this._audioUpload.UploadFile(this._theMemStream, filename);
            }
        }


        /// <summary>
        ///     Handles the Click event of the PlayButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void PlayButton_Click([NotNull] object sender, [NotNull] RoutedEventArgs e)
        {
            this.RecorderState = PlayerState.Play;
            this._wavMss = new WaveMediaStreamSource(this._theMemStream);
            this.RecordingOutput.SetSource(this._wavMss);
            this.RecordingOutput.Position = TimeSpan.Zero;
            this.RecordingOutput.Play();
            this.UpdateRecordingState(this.RecorderState);
        }

        /// <summary>
        ///     Handles the Click event of the StopButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void StopButton_Click([NotNull] object sender, [NotNull] RoutedEventArgs e)
        {
            this.BeatsList.BeatPlayer.Stop();
            this.BeatsList.State = PlayerState.Stop;
            this.RecordingOutput.Stop();
            this.Stop();
            this.UpdateRecordingState(this.RecorderState);
        }

        /// <summary>
        ///     Handles the Click event of the RecordButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void RecordButton_Click([NotNull] object sender, [NotNull] RoutedEventArgs e)
        {
            this.BeatsList.StartPlay();
            this.Record();
            this.UpdateRecordingState(this.RecorderState);
        }

        /// <summary>
        ///     Updates the state of the recording device.
        /// </summary>
        /// <param name="state">The state.</param>
        public void UpdateRecordingState([NotNull] PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Initial:
                    this.PlayButton.IsEnabled = false;
                    this.StopButton.IsEnabled = false;
                    this.RecordButton.IsEnabled = true;
                    this.SaveButton.IsEnabled = false;
                    this.StatusText.Text = "";
                    this.TimerText.Text = "";
                    break;
                case PlayerState.Play:
                    this.PlayButton.IsEnabled = false;
                    this.StopButton.IsEnabled = true;
                    this.RecordButton.IsEnabled = false;
                    this.SaveButton.IsEnabled = true;
                    break;
                case PlayerState.Record:
                    this.PlayButton.IsEnabled = false;
                    this.StopButton.IsEnabled = true;
                    this.RecordButton.IsEnabled = false;
                    this.SaveButton.IsEnabled = false;
                    if (this._timer != null)
                    {
                        this._timer.Start();
                    }
                    break;
                case PlayerState.Stop:
                    this.PlayButton.IsEnabled = true;
                    this.StopButton.IsEnabled = false;
                    this.RecordButton.IsEnabled = true;
                    this.SaveButton.IsEnabled = true;
                    if (this._timer != null)
                    {
                        this._timer.Stop();
                    }
                    this.RecordingOutput.Stop();
                    break;
                case PlayerState.Save:
                    this.PlayButton.IsEnabled = false;
                    this.StopButton.IsEnabled = false;
                    this.RecordButton.IsEnabled = false;
                    this.SaveButton.IsEnabled = false;
                    break;
            }

            #endregion
        }
    }
}