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
        public static Matrix<double> LookAtLH(Vector<double> cameraPosition, Vector<double> cameraTarget, Vector<double> up3D)
        {
            Vector<double> zAxis = (cameraTarget - cameraPosition).Normalize(1);
            Vector<double> xAxis = up3D.Cross3D(zAxis).Normalize(1);
            Vector<double> yAxis = zAxis.Cross3D(xAxis);

            Matrix<double> viewMatrix = DenseMatrix.OfArray(new double[,]
            {
                {xAxis[0], yAxis[0], zAxis[0], 0 },
                {xAxis[1], yAxis[1], zAxis[1], 0 },
                {xAxis[2], yAxis[2], zAxis[2], 0 },
                {-xAxis.DotProduct(cameraPosition), -yAxis.DotProduct(cameraPosition), -zAxis.DotProduct(cameraPosition), 1 }
            });

            return viewMatrix;
        }
        public static Matrix<double> PerspectiveFovRH(double fov, double aspect, double znear, double zfar)
        {
            double yScale = (double)(1.0f / Math.Tan(fov * 0.5f));
            double q = zfar / (znear - zfar);

            Matrix<double> projectMatrix = DenseMatrix.OfArray(new double[,]
            {
                { yScale/aspect, 0, 0, 0 },
                {0, yScale, 0, 0 },
                {0, 0, q, -1.0f},
                {0, 0, 1*znear, 0 }
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
        public static Matrix<double> Translation(Vector<double> v3D)
        {
            Matrix<double> translationMatrix = Matrix<double>.Build.DenseIdentity(4);
            translationMatrix[3, 0] = v3D[0];
            translationMatrix[3, 1] = v3D[1];
            translationMatrix[3, 2] = v3D[2];

            return translationMatrix;

        }
    }
}
