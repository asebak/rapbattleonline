#region Using

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using Common.Models;
using Common.Types;
using Common.Types.Attributes;
using Newtonsoft.Json;
using RapBattleAudio.classes;
using RapBattleAudio.classes.Factory;

#endregion

namespace RapBattleAudio
{
    public partial class MainPage
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        /// <param name="battleId"></param>
        public MainPage([NotNull] int battleId)
        {
            InitializeComponent();
            this.InitializeBusyDialog();
            //get battle context via web api
            var webClient = new WebClient();
            var apiHelper = new WebApi("audiobattle");
            apiHelper.ChangeToLocalHost();
            BusyIndicatorContext.Current.Busy = true;
            webClient.OpenReadCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    var stream = new StreamReader(e.Result);
                    var jsonObj = stream.ReadToEnd().ToString();
                    RapAudioContext.Current.Service = JsonConvert.DeserializeObject<AudioBattleModel>(jsonObj);
                    var factory = new SilverlightFactory(RapAudioContext.Current.Service, this.Beat);
                    factory.Build(this.RecorderUser1, this.RecorderUser2);
                    factory.Build(this.Beat);
                }
                BusyIndicatorContext.Current.Busy = false;
            };
            webClient.OpenReadAsync(
                new Uri(apiHelper.GetByAction(battleId, "getbattle")));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void UserControl_Loaded([NotNull] object sender, [NotNull] RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Initializes the busy dialog.
        /// </summary>
        private void InitializeBusyDialog()
        {
            this.BusyWindow.DataContext = BusyIndicatorContext.Current;
        }
        #endregion
    }
}