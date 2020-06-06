using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Accord.Math;
using NAudio.Wave;

namespace AudioAnalysis
{
    public class Fourier
    {

        public static double[] Transform(short[] buffer)
        {
            Complex[] fftComplex = new Complex[buffer.Length];

            for (int i = 0; i < buffer.Length; i++)
            {
                fftComplex[i] = new Complex(buffer[i], 0.0);
            }

            int sz = nearest_power_2(fftComplex.Length);

            Complex[] frequency_spectrum = new Complex[sz];
            int source_index = 0;
            int remaining_length = fftComplex.Length;

            Console.WriteLine(remaining_length);

            while (remaining_length > 32)
            {

                Complex[] data = new Complex[sz];

                Array.Copy(fftComplex, source_index, data, 0, sz);

                FourierTransform.FFT(data, FourierTransform.Direction.Forward);

                Add(ref frequency_spectrum, ref data);

                source_index += sz;

                remaining_length -= sz;

                sz = nearest_power_2(remaining_length);

            }

            double[] result = new double[frequency_spectrum.Length];

            for (int i = 0; i < frequency_spectrum.Length; i++)
                result[i] = frequency_spectrum[i].Magnitude;
            return result;
        }


        public static void Add(ref Complex[] a1, ref Complex[] a2)
        {
            int skip = a1.Length / a2.Length;
            double weight = 1 / (double)skip;
            int a2_index = 0;

            for (int i = 0; i < a1.Length; i += skip)
            {
                for (int j = i; j < i + skip; j++)
                {
                    a1[j] += weight * a2[a2_index];
                }
                a2_index += 1;
            }
        }

        public static KeyValuePair<double, int>[] Reduce(KeyValuePair<double, int>[] arr, int sample_rate)
        {
            int l = arr.Length;
            HashSet<int> H = new HashSet<int>();
            IList<KeyValuePair<double, int>> r = new List<KeyValuePair<double, int>>();
            int e = 0;
            for (int i = 0; i < l; i++)
            {
                e = Convert.ToInt32(Frequency(sample_rate, 2 * l, arr[i].Value));
                if (!H.Contains(e) && (e >= 20 && e <= 20000))
                {
                    H.Add(e);
                    r.Add(new KeyValuePair<double, int>(arr[i].Key, e));
                }
            }
            return r.ToArray();
        }


        public static short[] Sin(int[] fs, float time, int sampling_rate, double[] amps)
        {
            short[] v = new short[(int)(time * sampling_rate)];
            float dt = 1.0f / sampling_rate;
            float t = 0;
            for (var k = 0; k < v.Length; k += 1)
            {
                short val = 0;
                for (int j = 0; j < fs.Length; j++)
                {
                    val += Convert.ToInt16(amps[j] * Math.Sin(2 * Math.PI * fs[j] * t));
                }
                v[k] = val;
                t += dt;
            }
            return v;
        }

        public static int nearest_power_2(int x)
        {
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);

            x -= (x >> 1);
            if (Math.Log2(x) > 14)
            {
                return (int)Math.Pow(2, 14);
            }

            return x;
        }

        public static double[] FirstHalf(double[] data)
        {
            int s = (int)(data.Length / 2);
            double[] d = new double[s];
            Array.Copy(data, d, s);
            return d;
        }

        public static double Frequency(double sample_rate, double size, double bin)
        {
            return bin * sample_rate / size;
        }

        public static double[][] FFT(short[] buffer, int k, int sample_rate)
        {
            double[] data = FirstHalf(Transform(buffer));

            KeyValuePair<double, int>[] m = data.Select((x, i) => new KeyValuePair<double, int>(x, i))
                .OrderByDescending(x => x.Key).ToArray();

            m = Reduce(m, sample_rate).Take(k).ToArray();
            
            double[] vals = m.Select(x => x.Key).ToArray();
            double[] idxs = m.Select(x => (double)x.Value).ToArray();
            return new double[][] { vals, idxs };
        }

    }
}
 