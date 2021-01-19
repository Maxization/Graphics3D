using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    public struct Vector3D
    {
        public float X, Y, Z;

        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3D operator+(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3D operator/(Vector3D v, float s)
        {
            return new Vector3D(v.X / s, v.Y / s, v.Z / s);
        }

        public static Vector3D operator*(float s, Vector3D v)
        {
            return new Vector3D(v.X * s, v.Y * s, v.Z * s);
        }

        public static float Dot(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public void Normalize()
        {
            float len = Length();
            if (len == 0) return;
            X/= len;
            Y/= len;
            Z/= len;
        }

        public Vector3D Normalized()
        {
            float len = Length();
            return new Vector3D(X / len, Y / len, Z / len);
        }

        public static float ComputeNDotL(Vector3D vertex, Vector3D normal, Vector3D lightPosition)
        {
            var lightDirection = lightPosition - vertex;

            normal.Normalize();
            lightDirection.Normalize();

            float dot = Vector3D.Dot(normal, lightDirection);
            return Math.Max(0, dot);
        }

        public static float CalculateTriangleArea(Vector3D A, Vector3D B, Vector3D C)
        {
            var res = Math.Abs((B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X)) / 2f;
            return res;
        }
    }
}
