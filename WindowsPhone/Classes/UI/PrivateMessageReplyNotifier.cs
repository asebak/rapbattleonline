using System.ComponentModel;
using Common.Types;
using FreestyleOnline___WP.Resources;
using Common.Models;
namespace FreestyleOnline___WP.Classes.UI
{
    public class PrivateMessageReplyNotifier : INotifyPropertyChanged
    {
        #region Members

        private string _to;
        private string _from;
        private string _subject;
        private string _message;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public string To
        {
            get { return _to; }
            set
            {
                _to = value;
                NotifyPropertyChanged("To");
            }
        }

        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                NotifyPropertyChanged("From");
            }
        }

        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                NotifyPropertyChanged("Subject");
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }

        public PrivateMessageReplyNotifier(PMModel m)
        {
            this.To = m.To;
            this.From = m.SentBy;
            this.Subject = m.Subject;
            this.Message = m.Details;
        }
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
