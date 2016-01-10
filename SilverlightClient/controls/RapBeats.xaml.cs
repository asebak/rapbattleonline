#region Using

using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common.Models;
using Common.Types;
using Common.Types.Attributes;
using Common.Types.Enums;

#endregion

namespace RapBattleAudio.controls
{
    public partial class RapBeats
    {
        #region Members

        [CanBeNull] public List<BeatModel> Beats;
        [NotNull] private PlayerState _state;
        [NotNull] private readonly WebApi _apiHelper;
        [NotNull] private ImageBrush _background = new ImageBrush();
        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the web client.
        /// </summary>
        /// <value>
        ///     The web client.
        /// </value>
        [NotNull] public WebClient WebClient = new WebClient();

        /// <summary>
        ///     Gets or sets the index of the beat.
        /// </summary>
        /// <value>
        ///     The index of the beat.
        /// </value>
        [CanBeNull]
        public int BeatIndex { get; set; }

        /// <summary>
        ///     Gets or sets the selected beat.
        /// </summary>
        /// <value>
        ///     The selected beat.
        /// </value>
        [CanBeNull]
        public int SelectedBeat { get; set; }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        /// <value>
        ///     The state.
        /// </value>
        [CanBeNull]
        public PlayerState State
        {
            get { return this._state; }
            set { this._state = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapBeats" /> class.
        /// </summary>
        public RapBeats()
        {
            InitializeComponent();
            this.State = PlayerState.Stop;
            this._apiHelper = new WebApi("beat");
            this._apiHelper.ChangeToLocalHost();
            this._background.ImageSource = new BitmapImage(new Uri("../images/Play.png", UriKind.Relative));
            this.PlayBeat.Background = this._background;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Updateds the beat.
        /// </summary>
        /// <param name="beatId">The beat identifier.</param>
        public void UpdatedBeat([NotNull] int beatId)
        {
            this.BeatsList.SelectedIndex = beatId;
        }

        /// <summary>
        ///     Starts the player.
        /// </summary>
        public void StartPlay()
        {
            this.PlayFromServer();
        }

        /// <summary>
        /// Plays from server.
        /// </summary>
        private void PlayFromServer()
        {
            this.State = PlayerState.Play;
            var selectedBeat = this.BeatsList.SelectedIndex;
            this.BeatPlayer.Source =
                new Uri(this._apiHelper.GetByAction(selectedBeat, "beatstream"), UriKind.Absolute);
            this.BeatPlayer.Play();
        }

        /// <summary>
        ///     Handles the Click event of the PlayBeat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void PlayBeat_Click([CanBeNull] object sender, [CanBeNull] RoutedEventArgs e)
        {
            //TODO Make Background color of play and stuff transparent
            switch (this.State)
            {
                case PlayerState.Initial:
                    break;
                case PlayerState.Play:
                    this._background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("../images/Play.png", UriKind.Relative))
                    };
                    this.State = PlayerState.Stop;
                    this.BeatPlayer.Stop();
                    break;
                case PlayerState.Stop:
                    this._background = new ImageBrush
                    {
                        Opacity = 1.0,
                        ImageSource = new BitmapImage(new Uri("../images/Stop.png", UriKind.Relative))
                    };
                    this.PlayFromServer();
                    break;
                case PlayerState.Record:
                    break;
                case PlayerState.Save:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            this.PlayBeat.Background = this._background;
        }

        #endregion
    }
}