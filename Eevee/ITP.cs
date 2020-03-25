using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eevee
{
    public interface ITP
    {
        string Predict(string text);

        float Loss(float[] v1, float[] v2);

        float[] ToArray(string vector);

        string ToString(float[] vector);
    }
}
