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
            Vector<double> cameraPos = Vector<double>.Build.DenseOfArray(new double[] { cameraPosition.Z, cameraPosition.X, cameraPosition.Y });
            Vector<double> cameraTar = Vector<double>.Build.DenseOfArray(new double[] { cameraTarget.Z, cameraTarget.X, cameraTarget.Y });
            Vector<double> up = Vector<double>.Build.DenseOfArray(new double[] { up3D.Z, up3D.X, up3D.Y });

            Vector<double> zAxis = (cameraPos - cameraTar).Normalize(1);
            Vector<double> xAxis = up.Cross3D(zAxis).Normalize(1);
            Vector<double> yAxis = zAxis.Cross3D(xAxis);

            Matrix<double> viewMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {xAxis[0], yAxis[0], zAxis[0], 0 },
                {xAxis[1], yAxis[1], zAxis[1], 0 },
                {xAxis[2], yAxis[2], zAxis[2], 0 },
                {-xAxis.DotProduct(cameraPos), -yAxis.DotProduct(cameraPos), -zAxis.DotProduct(cameraPos), 1 }
            });

            return viewMatrix.Transpose();
        }
        public static Matrix<double> PerspectiveFov(double fov, double aspect, double znear, double zfar)
        {
            double yScale = (double)(1.0f / Math.Tan(fov * 0.5f));
            double q1 = -(zfar + znear) / (zfar - znear);
            double q2 = -2 * zfar * znear / (zfar - znear);
            Matrix<double> projectMatrix = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { yScale, 0, 0, 0 },
                {0, yScale/aspect, 0, 0 },
                {0, 0, q1, q2},
                {0, 0, -1, 0 }
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
            translationMatrix[0, 3] = p.Z;
            translationMatrix[1, 3] = p.X;
            translationMatrix[2, 3] = p.Y;

            return translationMatrix;

        }
    }
}
