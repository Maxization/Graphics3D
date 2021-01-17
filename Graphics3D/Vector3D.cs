using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    public class Vector3D
    {
        public float X, Y, Z;

        public Vector3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3D Add(Vector3D v)
        {
            return new Vector3D(X + v.X, Y + v.Y, Z + v.Z);
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public void Normalize()
        {
            float len = Length();
            X/= len;
            Y/= len;
            Z/= len;
        }
    }
}
