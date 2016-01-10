#region Using

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

#endregion

namespace RapBattleAudio.classes
{
    /// <summary>
    ///     Class AudioFormatEx
    ///     Extension of AudioFormat class to wrap standard WAVEFORMATEX structure
    /// </summary>
    public class AudioFormatEx
    {
        #region Fields

        private int _bitsPerSample;
        private int _channels;
        private WaveFormatType _format;
        private int _samplesPerSecond;

        #endregion

        public AudioFormatEx(WaveFormatType format, int channels, int bitsPerSample, int samplesPerSecond)
        {
            this._format = format;
            this._channels = channels;
            this._bitsPerSample = bitsPerSample;
            this._samplesPerSecond = samplesPerSecond;
        }

        public AudioFormatEx(AudioFormat audioFormat)
            : this(audioFormat.WaveFormat, audioFormat.Channels, audioFormat.BitsPerSample, audioFormat.SamplesPerSecond
                )
        {
        }

        public AudioFormatEx(WAVEFORMATEX wavFormat)
            : this(
                (WaveFormatType) wavFormat.FormatTag, wavFormat.Channels, wavFormat.BitsPerSample,
                wavFormat.SamplesPerSec)
        {
            //note: WAVEFORMATEX.FormatPCM = 1 and also WaveFormatType.Pcm = 1 that's why the cast is OK  
        }

        // Summary:
        //     Gets the encoding format of the audio format as a System.Windows.Media.WaveFormatType
        //     value.
        //
        // Returns:
        //     The encoding format of the audio format.
        public WaveFormatType WaveFormat
        {
            get { return _format; }
            set { _format = value; }
        }

        //
        // Summary:
        //     Gets the number of channels that are provided by the audio format.
        //
        // Returns:
        //     The number of channels that are provided by the audio format.
        public int Channels
        {
            get { return _channels; }
            set { _channels = value; }
        }

        /// <summary>
        ///     Gets the number of bits that are used to store the audio information for
        ///     a single sample of an audio format.
        /// </summary>
        /// <value>
        ///     The number of bits that are used to store the audio information for a single
        ///     sample of an audio format.
        /// </value>
        public int BitsPerSample
        {
            get { return _bitsPerSample; }
            set { _bitsPerSample = value; }
        }


        //
        // Summary:
        //     Gets the number of samples per second that are provided by the audio format.
        //
        // Returns:
        //     The number of samples per second that are provided by the audio format.
        public int SamplesPerSecond
        {
            get { return _samplesPerSecond; }
            set { _samplesPerSecond = value; }
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" }, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            AudioFormatEx formatObj = null;
            if (obj.GetType() == typeof (AudioFormatEx))
                formatObj = (AudioFormatEx) obj;
            else if (obj.GetType() == typeof (AudioFormat))
                formatObj = new AudioFormatEx((AudioFormat) obj);
            else if (obj.GetType() == typeof (WAVEFORMATEX))
                formatObj = new AudioFormatEx((WAVEFORMATEX) obj);

            return (_format == formatObj._format) &&
                   (_bitsPerSample == formatObj._bitsPerSample) &&
                   (_channels == formatObj._channels) &&
                   (_samplesPerSecond == formatObj._samplesPerSecond);
        }

        public static AudioFormat PickAudioFormat(ReadOnlyCollection<AudioFormat> audioFormats,
            AudioFormatEx desiredFormat)
        {
            return audioFormats.FirstOrDefault(audioFormat => desiredFormat.Equals(audioFormat));
        }
    }
}