#region Using

using System.ComponentModel;
using Common.Types.Attributes;

#endregion

namespace RapBattleAudio.classes
{
    public class BusyIndicatorContext : INotifyPropertyChanged
    {
        #region Members

        [CanBeNull] private static BusyIndicatorContext _instance;
        [NotNull] private bool _busy;
        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="BusyIndicatorContext"/> class from being created.
        /// </summary>
        private BusyIndicatorContext()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        [NotNull]
        public static BusyIndicatorContext Current
        {
            get { return _instance ?? (_instance = new BusyIndicatorContext()); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BusyIndicatorContext"/> is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if busy; otherwise, <c>false</c>.
        /// </value>
        public bool Busy
        {
            get { return this._busy; }
            set
            {
                this._busy = value;
                this.RaisePropertyChanged("Busy");
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaisePropertyChanged([CanBeNull] string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}