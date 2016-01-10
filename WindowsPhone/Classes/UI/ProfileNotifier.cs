#region Using

using System.ComponentModel;
using Common.Models;
using Common.Types;
using FreestyleOnline___WP.Resources;

#endregion

namespace FreestyleOnline___WP.Classes.UI
{
    public class ProfileNotifier : INotifyPropertyChanged
    {
        #region Properties

        public string ProfileImage
        {
            get { return _profileImage; }
            set
            {
                _profileImage = value;
                NotifyPropertyChanged("ProfileImage");
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        public string AIM
        {
            get { return _aim; }
            set
            {
                _aim = value;
                NotifyPropertyChanged("AIM");
            }
        }

        public string MSN
        {
            get { return _msn; }
            set
            {
                _msn = value;
                NotifyPropertyChanged("MSN");
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                NotifyPropertyChanged("Country");
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                NotifyPropertyChanged("City");
            }
        }

        public string Interests
        {
            get { return _interests; }
            set
            {
                _interests = value;
                NotifyPropertyChanged("Interests");
            }
        }

        public string Facebook
        {
            get { return _facebook; }
            set
            {
                _facebook = value;
                NotifyPropertyChanged("Facebook");
            }
        }

        public string Twitter
        {
            get { return _twitter; }
            set
            {
                _twitter = value;
                NotifyPropertyChanged("Twitter");
            }
        }

        public string Skype
        {
            get { return _skype; }
            set
            {
                _skype = value;
                NotifyPropertyChanged("Skype");
            }
        }

        public string Website
        {
            get { return _website; }
            set
            {
                _website = value;
                NotifyPropertyChanged("Website");
            }
        }

        public string Occupation
        {
            get { return _occupation; }
            set
            {
                _occupation = value;
                NotifyPropertyChanged("Occupation");
            }
        }

        #endregion

        #region Constructor

        public ProfileNotifier(ProfileModel p)
        {
            this.ProfileImage = RapGlobalHelpers.Address + p.Avatar;
            this.UserName = string.Format("{0}: {1}", AppResources.DisplayName, p.UserName);
            this.AIM = string.Format("{0}: {1}", "AIM", p.AIM);
            this.City = string.Format("{0}: {1}", AppResources.City, p.City);
            this.Country = string.Format("{0}: {1}", AppResources.Country, p.City);
            this.Facebook = string.Format("{0}: {1}", "Facebook", p.Facebook);
            this.Interests = string.Format("{0}: {1}", AppResources.Interests, p.Interests);
            this.MSN = string.Format("{0}: {1}", "MSN", p.MSN);
            this.Occupation = string.Format("{0}: {1}", AppResources.Occupation, p.Occupation);
            this.Skype = string.Format("{0}: {1}", "Skype", p.Skype);
            this.Twitter = string.Format("{0}: {1}", "Twitter", p.Twitter);
            this.Website = string.Format("{0}: {1}", AppResources.Website, p.Homepage);
        }

        #endregion

        #region Memebers

        private string _aim;
        private string _city;
        private string _country;
        private string _facebook;
        private string _interests;
        private string _msn;
        private string _occupation;
        private string _profileImage;
        private string _skype;
        private string _twitter;
        private string _userName;
        private string _website;
        public event PropertyChangedEventHandler PropertyChanged;

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