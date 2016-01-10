#region Using

using System;
using System.IO;
using System.Text;

#endregion

// ReSharper disable once CheckNamespace

namespace FreestyleOnline___WP.Classes.Digital_Signal_Processing
{
    /// <summary>
    ///     Referenced from: http://damianblog.com/2011/02/07/storing-wp7-recorded-audio-as-wav-format-streams/
    /// </summary>
    public static class WavHelper
    {
        #region Methods

        /// <summary>
        ///     Writes the wav header.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="sampleRate">The sample rate.</param>
        public static void WriteWavHeader(Stream stream, int sampleRate)
        {
            //RapCodeContracts.VerifyNotNull(stream, "WavStream");
            const int bitsPerSample = 16;
            const int bytesPerSample = bitsPerSample/8;
            var encoding = Encoding.UTF8;
            stream.Write(encoding.GetBytes("RIFF"), 0, 4);
            stream.Write(BitConverter.GetBytes(0), 0, 4);
            stream.Write(encoding.GetBytes("WAVE"), 0, 4);
            stream.Write(encoding.GetBytes("fmt "), 0, 4);
            stream.Write(BitConverter.GetBytes(16), 0, 4);
            stream.Write(BitConverter.GetBytes((short) 1), 0, 2);
            stream.Write(BitConverter.GetBytes((short) 1), 0, 2);
            stream.Write(BitConverter.GetBytes(sampleRate), 0, 4);
            stream.Write(BitConverter.GetBytes(sampleRate*bytesPerSample), 0, 4);
            stream.Write(BitConverter.GetBytes((short) (bytesPerSample)), 0, 2);
            stream.Write(BitConverter.GetBytes((short) (bitsPerSample)), 0, 2);
            stream.Write(encoding.GetBytes("data"), 0, 4);
            stream.Write(BitConverter.GetBytes(0), 0, 4);
        }

        /// <summary>
        ///     Updates the wav header.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="System.Exception">Can't seek stream to update wav header</exception>
        public static void UpdateWavHeader(Stream stream)
        {
            //RapCodeContracts.VerifyNotNull(stream, "WavHeader");
            if (!stream.CanSeek)
            {
                throw new Exception("Can't seek stream to update wav header");
            }
            var oldPos = stream.Position;
            stream.Seek(4, SeekOrigin.Begin);
            stream.Write(BitConverter.GetBytes((int) stream.Length - 8), 0, 4);
            stream.Seek(40, SeekOrigin.Begin);
            stream.Write(BitConverter.GetBytes((int) stream.Length - 44), 0, 4);
            stream.Seek(oldPos, SeekOrigin.Begin);
        }

        #endregion
    }
}