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

            double sinRoll = (double)Math.Sin(halfRoll);
            double cosRoll = (double)Math.Cos(halfRoll);
            double sinPitch = (double)Math.Sin(halfPitch);
            double cosPitch = (double)Math.Cos(halfPitch);
            double sinYaw = (double)Math.Sin(halfYaw);
            double cosYaw = (double)Math.Cos(halfYaw);

            Quaternion result = new Quaternion();
            result.X = (cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll);
            result.Y = (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll);
            result.Z = (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll);
            result.W = (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll);

            return result;
        }
    }
}
