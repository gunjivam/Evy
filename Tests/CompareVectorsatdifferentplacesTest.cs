using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] fs3 = { 22, 24, 27, 30 };
            int[] fs4 = { 22, 24, 30, 27 };
            Assert.AreEqual(AudioAnalysis.Compare.Similarity(fs3, fs4), 87.5);

            int[] fs = { 40, 60, 80, 100, 200 }; double[] amps = { 5, 3, 10, 20, 4 };
            int sr = 2048;
            short[] sw = AudioAnalysis.Fourier.Sin(fs, 55, sr, amps);
            double[][] result = AudioAnalysis.Fourier.FFT(sw, 15, sr);
            double[] f = result[1];

            Assert.AreEqual(100, f[0]);
            Assert.AreEqual(80, f[1]);
            Assert.AreEqual(60, f[4]);
            Assert.AreEqual(40, f[2]);
            Assert.AreEqual(200, f[3]);


        }
    }
}
