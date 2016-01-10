#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FreestyleOnline.classes.Providers;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

#endregion

namespace FreestyleOnline.classes.Types.Helpers
{
    public class RapSignalProcessing
    {
        #region Members

        private readonly int _beatLocation;
        private readonly string _file;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="RapSignalProcessing" /> class.
        /// </summary>
        /// <param name="fileLocation">The file location.</param>
        /// <param name="beatUsed">The beat used.</param>
        public RapSignalProcessing(string fileLocation, int beatUsed)
        {
            _file = fileLocation;
            _beatLocation = beatUsed;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Takes 1 wave file, Takes 1 Mp3 File and Combines the two files and resaves on top of the wav.
        /// </summary>
        /// <returns></returns>
        public string CombineAudioSignal()
        {
            var audioPath = _file.Remove(0, _file.IndexOf("Uploads"));
            var server = HttpContext.Current.Server;
            var directoryInfoBeats =
                new DirectoryInfo(new ResourceProvider().GetPath(RapResource.Beats));
            var sortedFiles = from f in directoryInfoBeats.EnumerateFiles() orderby f.CreationTime select f;
            var beats = sortedFiles.Select(item => item.FullName).ToList();
            using (var recordedAudio = new WaveFileReader(server.MapPath("~/" + audioPath)))
            using (var instrumental = new Mp3FileReader(beats[_beatLocation]))
            {
                var inputs = new List<ISampleProvider>
                {
                    recordedAudio.ToSampleProvider(),
                    instrumental.ToSampleProvider(),
                };
                var mixer = new MixingSampleProvider(inputs);
                //requires work to avoid overriding the file being used
                WaveFileWriter.CreateWaveFile16(
                    new ResourceProvider().GetPath(RapResource.RapBattleAudio) + new Guid() + ".wav", mixer);
            }
            return _file;
        }

        /// <summary>
        ///     Trims the MP3.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="begin">The begin.</param>
        /// <param name="end">The end.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">end;end should be greater than begin</exception>
        private void TrimMp3(string inputPath, string outputPath, TimeSpan? begin, TimeSpan? end)
        {
            if (begin.HasValue && end.HasValue && begin > end)
                throw new ArgumentOutOfRangeException("end", "end should be greater than begin");

            using (var reader = new Mp3FileReader(inputPath))
            using (var writer = File.Create(outputPath))
            {
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                    if (reader.CurrentTime >= begin || !begin.HasValue)
                    {
                        if (reader.CurrentTime <= end || !end.HasValue)
                            writer.Write(frame.RawData, 0, frame.RawData.Length);
                        else break;
                    }
            }
        }

        #endregion
    }
}