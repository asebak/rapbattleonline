using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Models;

namespace FreestyleOnline___WP.Classes.UI
{
    public class PrivateMessagesNotifier : ObservableCollection<PMModel>
    {

        public PrivateMessagesNotifier(IEnumerable<PMModel> pmList)
        {
            foreach (var m in pmList)
            {
                this.Add(m);
            }
        }
    }
}
