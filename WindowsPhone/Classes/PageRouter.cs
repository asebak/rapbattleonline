#region Using

using System;
using System.Windows;
using Microsoft.Phone.Controls;

#endregion

namespace FreestyleOnline___WP.Classes
{
    public class PageRouter
    {
        /// <summary>
        ///     Goes the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        public static void Go(string page)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/" + page + ".xaml",
                UriKind.Relative));
        }

        /// <summary>
        /// Goes the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="queryString">The query string.</param>
        public static void Go(string page, string queryString)
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/" + page + ".xaml" + queryString,
                UriKind.Relative));
        }

        /// <summary>
        ///     Backs a page
        /// </summary>
        public static void Back()
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();
        }
    }
}