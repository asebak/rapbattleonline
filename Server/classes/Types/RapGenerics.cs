#region Using

using System;
using System.Collections.Generic;
using System.Security.Cryptography;

#endregion

namespace FreestyleOnline.classes.Types
{
    public static class RapGenerics
    {
        #region Methods

        /// <summary>
        ///     Shuffles the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            var provider = new RNGCryptoServiceProvider();
            var n = list.Count;
            while (n > 1)
            {
                var box = new byte[1];
                do provider.GetBytes(box); while (!(box[0] < n*(Byte.MaxValue/n)));
                var k = (box[0]%n);
                n--;
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        #endregion
    }
}