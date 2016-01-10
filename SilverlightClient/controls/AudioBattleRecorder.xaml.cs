using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RapBattleAudio.classes;
using System.IO;
using RapBattleAudio.BattleService;
using RapBattleAudio.classes.Types.Enums;
using RapBattleAudio.classes.Helpers;
namespace RapBattleAudio.controls
{
    public partial class AudioBattleRecorder : UserControl
    {
        #region Members
        private MemoryStream _theMemStream;
        private MemoryAudioSink _storage;
        private CaptureSource _grabber;
        private WaveMediaStreamSource _wavMss;
        private readonly RapUploadClient _audioUpload;
        private readonly AudioBattleServiceClient _proxy = new AudioBattleServiceClient();
        private RapTimer _timer;
        #endregion

        #region Properties
        public PlayerState RecorderState { get; set; }
        public RapTimer Timer { get { return _timer; } set { _timer = value; } }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioBattleRecorder"/> class.
        /// </summary>
        public AudioBattleRecorder()
        {
            InitializeComponent();
            RecorderState = PlayerState.Stop;
            this.PlayButton.IsEnabled = false;
            this.StopButton.IsEnabled = false;
            this.SaveButton.IsEnabled = false;
            _audioUpload = new RapUploadClient(this.StatusText);
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var audioDevice = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
            _grabber = new CaptureSource { VideoCaptureDevice = null, AudioCaptureDevice = audioDevice };
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            RecorderState = PlayerState.Stop;
            _timer.Stop();
            if (_grabber.State == CaptureState.Started)
            {
                _grabber.Stop();
            }
            try
            {
                _theMemStream = new MemoryStream();
                WavManager.SavePcmToWav(_storage.BackingStream, _theMemStream, new AudioFormatEx(_storage.CurrentFormat));
                _theMemStream.Position = 0;
            }
            catch (Exception ex)
            {
                this.StatusText.Text = ex.Message;
            }
        }


        /// <summary>
        /// Records this instance.
        /// </summary>
        protected void Record()
        {
            RecorderState = PlayerState.Record;
            if (!EnsureAudioAccess())
            {
                return;
            }
            if (_grabber.State != CaptureState.Stopped)
            {
                _grabber.Stop();
            }
            _storage = new MemoryAudioSink { CaptureSource = _grabber };
            _grabber.Start();
            this.AudioRecorder.Stop();//stops audio player
            _timer.Start();
        }

        /// <summary>
        /// Ensures the audio access.
        /// </summary>
        /// <returns></returns>
        private static bool EnsureAudioAccess()
        {
            return CaptureDeviceConfiguration.AllowedDeviceAccess || CaptureDeviceConfiguration.RequestDeviceAccess();
        }


        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var filename = Guid.NewGuid() + ".wav";
            _audioUpload.UploadFile(_theMemStream, filename, string.Format("{0}Uploads/RapBattleAudio", RapHelpers.Address));
        }

        /// <summary>
        /// Handles the Click event of the PlayButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            RecorderState = PlayerState.Play;
            _wavMss = new WaveMediaStreamSource(_theMemStream);
            this.PlayButton.IsEnabled = false;
            this.StopButton.IsEnabled = true;
            this.SaveButton.IsEnabled = true;
            this.RecordButton.IsEnabled = false;
            this.AudioRecorder.SetSource(_wavMss);
            this.AudioRecorder.Position = TimeSpan.Zero;
            this.AudioRecorder.Play();
        }

        /// <summary>
        /// Handles the Click event of the StopButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            //access main page and stop beat player
            this.AudioRecorder.Stop();
            this.PlayButton.IsEnabled = true;
            this.StopButton.IsEnabled = false;
            this.SaveButton.IsEnabled = true;
            this.RecordButton.IsEnabled = true;
            Stop();
        }

        /// <summary>
        /// Handles the Click event of the RecordButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            var mainPage = (MainPage)Application.Current.RootVisual;
            //access main pages audio player and play
            Record();
            this.PlayButton.IsEnabled = false;
            this.StopButton.IsEnabled = true;
            this.SaveButton.IsEnabled = false;
            this.RecordButton.IsEnabled = false;

        }
    }
}
