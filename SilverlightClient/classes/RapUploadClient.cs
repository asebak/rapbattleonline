#region Using

using System;
using System.IO;
using System.Net;
using System.Windows.Controls;
using Common.Models;
using Common.Types;
using Common.Types.Attributes;
using Newtonsoft.Json;
using RapBattleAudio.interfaces;

#endregion

namespace RapBattleAudio.classes
{
    public class RapUploadClient: ISilverlightClass
    {
        #region Members

        [NotNull] private readonly int _battleId;
        [NotNull] private readonly int _beatUsed;
        [NotNull] private readonly bool _dspProcessing;
        [NotNull] private readonly TextBlock _resultText;
        [NotNull] private readonly int _userId;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapUploadClient" /> class.
        /// </summary>
        public RapUploadClient([NotNull] TextBlock textBlock, [NotNull] int userId, [NotNull] int battleId,
            [NotNull] int beatUsed, [NotNull] bool processAudio)
        {
            this._resultText = textBlock;
            this._userId = userId;
            this._battleId = battleId;
            this._beatUsed = beatUsed;
            this._dspProcessing = processAudio;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Uploads the file.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="fileName">Name of the file.</param>
        public void UploadFile([CanBeNull] MemoryStream stream, [CanBeNull] string fileName)
        {
            BusyIndicatorContext.Current.Busy = true;
            //setup webclient to first upload audio to server
            WebApi apiHelper;
            apiHelper = new WebApi("upload");
            apiHelper.ChangeToLocalHost();
            stream.Position = 0;
            var wc = new WebClient();
            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int) stream.Length);
            var bitData = JsonConvert.SerializeObject(new UploadModel
            {
                ByteArray = bytes,
                Name = fileName
            });

            wc.UploadStringCompleted += (sender, e) =>
            {
                apiHelper = new WebApi("audiobattle");
                apiHelper.ChangeToLocalHost();
                if (e.Error != null)
                {
                    BusyIndicatorContext.Current.Busy = false;
                    this._resultText.Text = this.Get<ResourceHelper>().GetString("UPLOAD_FAILED");
                }
                else
                {
                    //setup webclient to do post request for submitting into the database the context
                    var webClient = new WebClient();
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var payload = JsonConvert.SerializeObject(new AudioBattleDspModel
                    {
                        Beat = this._beatUsed,
                        PageUserId = this._userId,
                        BattleId = this._battleId,
                        RecordedFileLocation = fileName,
                        RequiresDspEngineering = this._dspProcessing
                    });

                    webClient.UploadStringCompleted += (sender2, e2) =>
                    {
                        BusyIndicatorContext.Current.Busy = false;
                        this._resultText.Text = e2.Error == null
                            ? this.Get<ResourceHelper>().GetString("UPLOAD_SUCCESS")
                            : this.Get<ResourceHelper>().GetString("SAVE_FAILED");
                    };
                    webClient.UploadStringAsync(new Uri(apiHelper.PostByAction("submit")), "POST", payload);
                }
            };
            wc.UploadStringAsync(new Uri(apiHelper.PostByAction("uploadwav")), "POST", bitData);
        }

        #endregion

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : new()
        {
            return new T();
        }
    }
}