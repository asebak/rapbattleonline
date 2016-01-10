#region Using

using System.ComponentModel;
using Common.Types;
using FreestyleOnline___WP.Resources;

#endregion

namespace FreestyleOnline___WP.Classes.UI
{
    public class HeaderNotifier : INotifyPropertyChanged
    {
        #region Properties

        public string AvatarImage
        {
            get { return _avatarUrl; }
            set
            {
                _avatarUrl = value;
                NotifyPropertyChanged("AvatarImage");
            }
        }

        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyPropertyChanged("UserName");
            }
        }

        public int UnreadMessages
        {
            get { return _unreadMessages; }
            set
            {
                _unreadMessages = value;
                NotifyPropertyChanged("UnreadMessages");
            }
        }

        #endregion

        #region Members

        private string _avatarUrl;
        private int _unreadMessages;
        private string _username;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public HeaderNotifier()
        {
            this.AvatarImage = RapGlobalHelpers.Address + WindowsPhoneContext.Current.Profile.Avatar;
            this.UserName = string.Format("{0} , {1}", AppResources.WelcomeMessage, WindowsPhoneContext.Current.Profile.UserName);
            this.UnreadMessages = WindowsPhoneContext.Current.Profile.UnreadMessages;
        }

        #endregion

        #region Methods

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}