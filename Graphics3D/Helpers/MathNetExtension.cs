using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    public static class MathExtension
    {
        public static Vector<float> Cross3D(this Vector<float> v, Vector<float> v2)
        {
            Vector<float> crossResult = Vector<float>.Build.Dense(3);
            crossResult[0] = v[1] * v2[2] - v[2] * v2[1];
            crossResult[1] = -v[0] * v2[2] + v[2] * v2[0];
            crossResult[2] = v[0] * v2[1] - v[1] * v2[0];

            return crossResult;
        }

        public static Vector<float> Multiply(this Matrix<float> m1, Vector3D v)
        {
            Vector<float> vector = Vector<float>.Build.DenseOfArray(new float[] { v.Z, v.X, v.Y, 1 });
            return m1 * vector;
        }
    }
}
