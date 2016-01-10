#region Using

using System;
using System.Windows;

#endregion

namespace FreestyleOnline___WP.Classes
{
    public class UiDispatcher
    {
        public static void Invoke(Action a)
        {
            if (Deployment.Current.Dispatcher == null)
                a();
            else
                Deployment.Current.Dispatcher.BeginInvoke(a);
        }
    }
}