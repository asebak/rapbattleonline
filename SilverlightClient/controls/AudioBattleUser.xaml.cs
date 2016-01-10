#region Using

using System.Windows;
using System.Windows.Controls;
using RapBattleAudio.BattleService;
using RapBattleAudio.classes.Helpers;
using RapBattleAudio.UserService;

#endregion

namespace RapBattleAudio.controls
{
    public partial class AudioBattleUser
    {
        #region Members
        private readonly AudioBattleServiceClient _battleProxy = new AudioBattleServiceClient();
        private readonly UserMembershipServiceClient _proxy = new UserMembershipServiceClient();
        #endregion

        #region Properties
        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public int? UserId { get; set; }

        public int PageUserId { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the <see cref="AudioBattleUser" /> class.
        /// </summary>
        public AudioBattleUser()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers
        /// <summary>
        ///     Handles the Click event of the UserLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void UserLink_Click(object sender, RoutedEventArgs e)
        {
            if (UserId != null)
            {
                _proxy.OpenUserProfileAsync((int) UserId);
            }
        }

        /// <summary>
        ///     Handles the Click event of the JoinButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            _battleProxy.JoinBattleAsync(PageUserId, RapHelpers.BattleId);
        }

        #endregion

    }
}