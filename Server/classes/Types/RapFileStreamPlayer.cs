#region Using

using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

#endregion

namespace FreestyleOnline.classes.Types
{
    public class RapFileStreamPlayer
    {
        /// <summary>
        ///     Gets the specified file stream and loads it into response
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static HttpResponseMessage Get(string filePath)
        {
            var streamPath = filePath;
            if (File.Exists(streamPath))
            {
                var memoryStream = new MemoryStream();
                var file = new FileStream(streamPath, FileMode.Open, FileAccess.Read);
                var bytes = new byte[file.Length];
                file.Read(bytes, 0, (int) file.Length);
                memoryStream.Write(bytes, 0, (int) file.Length);
                file.Close();
                var httpResponseMessage = new HttpResponseMessage
                {
                    Content = new ByteArrayContent(memoryStream.ToArray())
                };
                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                return httpResponseMessage;
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}