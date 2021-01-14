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
        public double Z;

        public Point3D(Vector<double> point)
        {
            X = (int)point[0];
            Y = (int)point[1];
            Z = point[2];
        }

        public Point3D(int x, int y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
