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
        public static Vector<double> Cross3D(this Vector<double> v, Vector<double> v2)
        {
            Vector<double> crossResult = Vector<double>.Build.Dense(3);
            crossResult[0] = v[1] * v2[2] - v[2] * v2[1];
            crossResult[1] = -v[0] * v2[2] + v[2] * v2[0];
            crossResult[2] = v[0] * v2[1] - v[1] * v2[0];

            return crossResult;
        }

        public static Vector<double> Multiply(this Matrix<double> m1, Vector3D v)
        {
            Vector<double> vector = Vector<double>.Build.DenseOfArray(new double[] { v.Z, v.X, v.Y, 1 });
            return m1 * vector;
        }
    }
}
