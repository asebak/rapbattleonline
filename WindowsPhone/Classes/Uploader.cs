#region Using

using System.IO;
using System.IO.IsolatedStorage;

#endregion

namespace FreestyleOnline___WP.Classes
{
    public static class Uploader
    {
        #region Methods

        /// <summary>
        ///     Uploads the stream file to server.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void UploadStreamFileToServer(MemoryStream stream, string fileName)
        {
        }

        /// <summary>
        ///     Uploads the stream to isolated storage.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="localPhonePath">The local phone path.</param>
        public static void UploadStreamToIsolatedStorage(MemoryStream stream, string localPhonePath)
        {
            using (var isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var fileStream = isolatedStorage.OpenFile(localPhonePath, FileMode.Create))
                {
                    stream.CopyTo(fileStream);
                }
            }
        }

        #endregion
    }
}