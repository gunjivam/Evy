using System;
using System.IO;
using System.Threading;
using NAudio;
using NAudio.Wave;

namespace AudioAnalysis
{
    public class Reader
    {
        private NAudio.Wave.BlockAlignReductionStream stream = null;

        private int sample_rate;

        private byte[] data;

        public void Read(string filepath)
        {
            DisposeWave();

            stream = new BlockAlignReductionStream(
                    WaveFormatConversionStream.CreatePcmStream(
                        new Mp3FileReader(filepath)));
            sample_rate = stream.WaveFormat.SampleRate;
            data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
        }

        public short[] GetStream()
        {
            short[] stream = new short[data.Length];
            Buffer.BlockCopy(data, 0, stream, 0, data.Length);
            return stream;
        }

        public int GetSampleRate()
        {
            return sample_rate;
        }

        public byte[] GetData()
        {
            return data;
        }

        private void DisposeWave()
        {
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }

    }
}