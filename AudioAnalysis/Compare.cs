using System;
using System.Collections.Generic;
using System.Linq;

namespace AudioAnalysis
{
    public class Compare
    {
        public static double Similarity(int[] f1, int[] f2)
        {
            int length = f1.Length;

            Dictionary<int, int> F = new Dictionary<int, int>();
            for (int i = 0; i < length; i++)
            {
                if (f1[i] != 0)
                    F.Add(f1[i], i);
            }

            double likeness = 1;
            double total_weight = 1 / (double)length;
            double weight = total_weight / (double)length;
            int e = 0;

            for (int i = 0; i < length; i++)
            {
                e = f2[i];
                if (F.ContainsKey(e))
                {
                    likeness -= weight * (Math.Abs(F[e] - i));
                }
                else
                {
                    likeness -= total_weight;
                }
            }
            return likeness * 100;
        }

        public static int[] ToIntArray(double[] arr)
        {
            int[] r = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                r[i] = Convert.ToInt32(arr[i]);
            }
            return r;
        }

        public static string ToString(int[] buffer)
        {
            return String.Join(",", buffer);
        }

        public static int[] ToArray(string vector)
        {
            return vector.Split(',').Select(e => Int32.Parse(e)).ToArray();
        }

    }
}
