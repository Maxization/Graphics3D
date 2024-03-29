﻿using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    public struct Vector3D
    {
        public double X, Y, Z;

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3D operator+(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector3D operator -(Vector3D v)
        {
            return new Vector3D(-v.X, -v.Y, -v.Z);
        }
        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3D operator/(Vector3D v, float s)
        {
            return new Vector3D(v.X / s, v.Y / s, v.Z / s);
        }

        public static Vector3D Cross(Vector3D v1, Vector3D v2)
        {
            Vector3D crossResult = new Vector3D();
            crossResult.X = v1.Y * v2.Z - v1.Z * v2.Y;
            crossResult.Y = -v1.X * v2.Z + v1.Z * v2.X;
            crossResult.Z = v1.X * v2.Y - v1.Y * v2.X;

            return crossResult;
        }

        public static Vector3D operator*(double s, Vector3D v)
        {
            return new Vector3D(v.X * s, v.Y * s, v.Z * s);
        }

        public static double Dot(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public void Normalize()
        {
            double len = Length();
            X/= len;
            Y/= len;
            Z/= len;
        }

        public Vector3D Multiply(Matrix<double> matrix)
        {
            Vector<double> v = Vector<double>.Build.DenseOfArray(new double[] { X, Y, Z, 1 });
            Vector<double> result = v * matrix;
            result = result.Divide(result[result.Count - 1]);
            return new Vector3D(result[0], result[1], result[2]);
        }
        public Vector3D Normalized()
        {
            double len = Length();
            return new Vector3D(X / len, Y / len, Z / len);
        }

        public static double ComputeNDotL(Vector3D vertex, Vector3D normal, Vector3D lightPosition)
        {
            var lightDirection = lightPosition - vertex;

            normal.Normalize();
            lightDirection.Normalize();

            double dot = Dot(normal, lightDirection);
            return Math.Max(0, dot);
        }

        public static double CalculateTriangleArea(Vector3D A, Vector3D B, Vector3D C)
        {
            var res = Math.Abs((B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X)) / 2f;
            return res;
        }
    }
}
