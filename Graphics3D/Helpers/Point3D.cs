using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    public struct Point3D
    {
        public int X, Y;
        public float Z;

        public Point3D(Vector3D point)
        {
            X = (int)point.X;
            Y = (int)point.Y;
            Z = point.Z;
        }

        public Point3D(int x, int y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
