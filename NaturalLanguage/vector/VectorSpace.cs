using System.Globalization;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace NaturalLanguage.vector
{
    public class VectorSpace
    {
        public static float[] Add(float[][] vectors)
        {
            float[] v = new float[vectors[0].Length];
            Parallel.ForEach(vectors, vector =>
            {
                Add(v, vector);
            });
            return v;
        }

        public static float[] Add(float[] vector1, float[] vector2)
        {
            Parallel.For(0, vector2.Length, i =>
            {
                vector1[i] += vector2[i];
            });
            return vector1;
        }

        public static float[] Negate(float[] vector)
        {
            Parallel.For(0, vector.Length, i =>
            {
                vector[i] = -1 * vector[i];
            });
            return vector;
        }

        public static float[] Normalize(float[] vector)
        {
            float sum = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += vector[i];
            }
            if(sum != 0)
            {
                Parallel.For(0, vector.Length, i =>
                {
                    vector[i] = vector[i] / sum;
                });
            }
            return vector;
        }


        public static float Loss1(float[] v1, float[] v2)
        {
            int n = v2.Length;
            double sum = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                sum += Math.Pow((v1[i] - v2[i]), 2) / n;
            }
            return (float)Math.Sqrt(sum);
        }

        public static float Loss(float[] v1, float[] v2)
        {
            float dot= 0, mag1 = 0, mag2 = 0;
            Thread t1 = new Thread(() => dot = DotProduct(v1, v2));
            Thread t2 = new Thread(() => mag1 = Magnitude(v1));
            Thread t3 = new Thread(() => mag2 = Magnitude(v2));
            t1.Start(); t2.Start(); t3.Start();
            t1.Join(); t2.Join(); t3.Join();
            return (float)Math.Acos(dot / (mag1 * mag2));
        }

        public static float Magnitude(float[] v)
        {
            double sum = 0;
            for(int i = 0; i < v.Length; i ++)
            {
                sum += Math.Pow((v[i]), 2);
            }
            return (float)Math.Sqrt(sum);
        }

        public static float DotProduct(float[] v1, float[] v2)
        {
            float res = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                res += v1[i]*v2[i];
            }
            return res;
        }

        public static float[] ToArray(string vector)
        {
            return vector.Split(',').Select(e => float.Parse(e, CultureInfo.InvariantCulture.NumberFormat)).ToArray();
        }

        public static string ToString(float[] vector)
        {
            return string.Join(",", vector);
        }
    }
}
