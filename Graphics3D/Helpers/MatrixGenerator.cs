using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    public static class MatrixGenerator
    {
        public static Matrix<double> LookAt(Vector3D cameraPosition, Vector3D cameraTarget, Vector3D up3D)
        {
            Vector3D zAxis = (cameraTarget - cameraPosition).Normalized();
            Vector3D xAxis = Vector3D.Cross(zAxis, up3D).Normalized();
            Vector3D yAxis = Vector3D.Cross(xAxis, zAxis);

            zAxis = -zAxis;

            Matrix<double> viewMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {xAxis.X, xAxis.Y, xAxis.Z, -Vector3D.Dot(xAxis,cameraPosition) },
                {yAxis.X, yAxis.Y, yAxis.Z, -Vector3D.Dot(yAxis,cameraPosition) },
                {zAxis.X, zAxis.Y, zAxis.Z, -Vector3D.Dot(zAxis,cameraPosition) },
                {0, 0, 0, 1 }
            });
            return viewMatrix;
        }
        public static Matrix<double> PerspectiveFov(double fov, double aspect, double znear, double zfar)
        {
            double h = (double)(1.0f / Math.Tan(fov * 0.5f));
            double w = h * aspect;
            double q1 = zfar / (znear - zfar);
            double q2 =  zfar * znear / (znear - zfar);
            Matrix<double> projectMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { w, 0, 0, 0 },
                {0, h, 0, 0 },
                {0, 0, q1, -1},
                {0, 0, q2, 0 }
            });
            return projectMatrix;
        }
        public static Matrix<double> RotationYawPitchRoll(double yaw, double pitch, double roll)
        {
            Quaternion rotation = Quaternion.RotationYawPitchRoll(yaw, pitch, roll);
            double xx = rotation.X * rotation.X;
            double yy = rotation.Y * rotation.Y;
            double zz = rotation.Z * rotation.Z;
            double xy = rotation.X * rotation.Y;
            double zw = rotation.Z * rotation.W;
            double zx = rotation.Z * rotation.X;
            double yw = rotation.Y * rotation.W;
            double yz = rotation.Y * rotation.Z;
            double xw = rotation.X * rotation.W;

            Matrix<double> rotationMatrix = Matrix<double>.Build.DenseIdentity(4);
            rotationMatrix[0,0] = 1.0f - (2.0f * (yy + zz)); ;
            rotationMatrix[0, 1] = 2.0f * (xy + zw);
            rotationMatrix[0, 2] = 2.0f * (zx - yw);
            rotationMatrix[1, 0] = 2.0f * (xy - zw);
            rotationMatrix[1, 1] = 1.0f - (2.0f * (zz + xx));
            rotationMatrix[1, 2] = 2.0f * (yz + xw);
            rotationMatrix[2, 0] = 2.0f * (zx + yw);
            rotationMatrix[2, 1] = 2.0f * (yz - xw);
            rotationMatrix[2, 2] = 1.0f - (2.0f * (yy + xx));

            return rotationMatrix;

        }
        public static Matrix<double> Translation(Vector3D p)
        {
            Matrix<double> translationMatrix = Matrix<double>.Build.DenseIdentity(4);
            translationMatrix[0, 3] = p.X;
            translationMatrix[1, 3] = p.Y;
            translationMatrix[2, 3] = p.Z;

            return translationMatrix;

        }
    }
}
