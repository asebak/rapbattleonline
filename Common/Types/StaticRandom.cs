#region Using

using System;

#endregion

namespace Common.Types
{
    public static class StaticRandom
    {
        #region Members

        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();

        #endregion

        #region Methods

        /// <summary>
        ///     Randoms the number.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public static int Rand(int min, int max)
        {
            lock (SyncLock)
            {
                return Random.Next(min, max);
            }
        }

        #endregion
    }
}