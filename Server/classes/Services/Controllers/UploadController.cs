#region Using

using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Models;
using FreestyleOnline.classes.Providers;
using FreestyleOnline.classes.Types;

#endregion

namespace FreestyleOnline.classes.Services.Controllers
{
    public class UploadController : ApiController
    {
        #region API

        /// <summary>
        ///     /api/Upload/
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ActionName("uploadwav")]
        public HttpResponseMessage UploadWav(UploadModel file)
        {
            Contract.Requires(file != null);
            try
            {
                var fullPath = new ResourceProvider().GetPath(RapResource.RapBattleAudio, file.Name);
                File.WriteAllBytes(fullPath, file.ByteArray);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        #endregion
    }
}