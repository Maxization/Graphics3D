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

        public static Vector3D Multiply(this Matrix<double> m1, Vector3D v)
        {
            Vector<double> vector = Vector<double>.Build.DenseOfArray(new double[] { v.Z, v.X, v.Y, 1 });
            Vector<double> res = m1 * vector;
            res = res.Divide(res[res.Count - 1]);
            return new Vector3D(res[0], res[1], res[2]);
        }

        public static Vector3D InterpolateVector(Vector3D min, Vector3D max, double gradient)
        {
            return new Vector3D
            {
                X = Interpolate(min.X, max.X, gradient),
                Y = Interpolate(min.Y, max.Y, gradient),
                Z = Interpolate(min.Z, max.Z, gradient),
            };
        }
        public static double Interpolate(double min, double max, double gradient)
        {
            return min + (max - min) * Clamp(gradient);
        }
        public static double Clamp(double value, double min = 0, double max = 1)
        {
            return Math.Max(min, Math.Min(value, max));
        }
    }
}
