#region Using

using Common.Models;

#endregion

namespace RapBattleAudio.classes
{
    public class RapAudioContext
    {
        #region Members

        /// <summary>
        ///     The _instance
        /// </summary>
        private static RapAudioContext _instance;

        #endregion

        #region Constructor

        /// <summary>
        ///     Prevents a default instance of the <see cref="RapAudioContext" /> class from being created.
        /// </summary>
        private RapAudioContext()
        {
        }
         #endregion

        #region Properties

        /// <summary>
        ///     Gets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        public static RapAudioContext Current
        {
            get { return _instance ?? (_instance = new RapAudioContext()); }
        }

        /// <summary>
        ///     Gets or sets the service.
        /// </summary>
        /// <value>
        ///     The service.
        /// </value>
        public AudioBattleModel Service { get; set; }

        #endregion
    }
}