using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class AudioAnalysis
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] fs1 = { 22, 24, 27, 30 };
            int[] fs2 = { 22, 24, 27, 30 };
            Assert.AreEqual(global::AudioAnalysis.Compare.Similarity(fs1, fs2), 100);

            int[] fs = { 40, 60, 80, 100, 200 }; double[] amps = { 5, 3, 10, 20, 4 };
            int sr = 2048;
            short[] sw = global::AudioAnalysis.Fourier.Sin(fs, 55, sr, amps);
            double[][] result = global::AudioAnalysis.Fourier.FFT(sw, 15, sr);
            double[] f = result[1];

            Assert.AreEqual(100, f[0]);
            Assert.AreEqual(80, f[1]);
            Assert.AreEqual(60, f[4]);
            Assert.AreEqual(40, f[2]);
            Assert.AreEqual(200, f[3]);

            int[] fs3 = { 22, 24, 30, 27 };
            Assert.AreEqual(global::AudioAnalysis.Compare.Similarity(fs1, fs3), 87.5);

            int[] fs4 = { 0, 4, 22, 10};
            Assert.AreEqual(global::AudioAnalysis.Compare.Similarity(fs1, fs4), 12.5);

            int[] fs5 = { 0, 4, 20, 10 };
            Assert.AreEqual(global::AudioAnalysis.Compare.Similarity(fs1, fs5), 0);

            Assert.AreEqual(global::AudioAnalysis.Fourier.nearest_power_2(5), 4);
            Assert.AreEqual(global::AudioAnalysis.Fourier.nearest_power_2(16), 16);
            Assert.AreEqual(global::AudioAnalysis.Fourier.nearest_power_2(1023), 512);
            Assert.AreEqual(global::AudioAnalysis.Fourier.nearest_power_2(1025), 1024);

            fs = new int[]{100, 200, 1000 };  amps = new double[]{ 5, 3, 10};
            sr = 4096;
            sw = global::AudioAnalysis.Fourier.Sin(fs, 55, sr, amps);
            result = global::AudioAnalysis.Fourier.FFT(sw, 15, sr);
            f = result[1];

            Assert.AreEqual(1000, f[0]);
            Assert.AreEqual(100, f[1]);
            Assert.AreEqual(200, f[2]);
        }
    }
}
