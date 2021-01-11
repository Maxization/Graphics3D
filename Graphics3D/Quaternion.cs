using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    class Quaternion
    {
        public double X;
        public double Y;
        public double Z;
        public double W;

        public Quaternion() { }
        public Quaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static Quaternion RotationYawPitchRoll(double yaw, double pitch, double roll)
        {
            double halfRoll = roll * 0.5f;
            double halfPitch = pitch * 0.5f;
            double halfYaw = yaw * 0.5f;

            double sinRoll = Math.Sin(halfRoll);
            double cosRoll = Math.Cos(halfRoll);
            double sinPitch = Math.Sin(halfPitch);
            double cosPitch = Math.Cos(halfPitch);
            double sinYaw = Math.Sin(halfYaw);
            double cosYaw = Math.Cos(halfYaw);

            Quaternion result = new Quaternion();
            result.X = (cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll);
            result.Y = (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll);
            result.Z = (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll);
            result.W = (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll);

            return result;
        }
    }
}
