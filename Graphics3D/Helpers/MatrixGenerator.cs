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
        public static Matrix<float> LookAt(Vector3D cameraPosition, Vector3D cameraTarget, Vector3D up3D)
        {
            Vector<float> cameraPos = Vector<float>.Build.DenseOfArray(new float[] { cameraPosition.Z, cameraPosition.X, cameraPosition.Y });
            Vector<float> cameraTar = Vector<float>.Build.DenseOfArray(new float[] { cameraTarget.Z, cameraTarget.X, cameraTarget.Y });
            Vector<float> up = Vector<float>.Build.DenseOfArray(new float[] { up3D.Z, up3D.X, up3D.Y });

            Vector<float> zAxis = (cameraPos - cameraTar).Normalize(1);
            Vector<float> xAxis = up.Cross3D(zAxis).Normalize(1);
            Vector<float> yAxis = zAxis.Cross3D(xAxis);

            Matrix<float> viewMatrix = Matrix<float>.Build.DenseOfArray(new float[,]
            {
                {xAxis[0], yAxis[0], zAxis[0], 0 },
                {xAxis[1], yAxis[1], zAxis[1], 0 },
                {xAxis[2], yAxis[2], zAxis[2], 0 },
                {-xAxis.DotProduct(cameraPos), -yAxis.DotProduct(cameraPos), -zAxis.DotProduct(cameraPos), 1 }
            });

            return viewMatrix.Transpose();
        }
        public static Matrix<float> PerspectiveFov(float fov, float aspect, float znear, float zfar)
        {
            float yScale = (float)(1.0f / Math.Tan(fov * 0.5f));
            float q1 = -(zfar + znear) / (zfar - znear);
            float q2 = -2 * zfar * znear / (zfar - znear);
            Matrix<float> projectMatrix = Matrix<float>.Build.DenseOfArray(new float[,]
            {
                { yScale, 0, 0, 0 },
                {0, yScale/aspect, 0, 0 },
                {0, 0, q1, q2},
                {0, 0, -1, 0 }
            });
            return projectMatrix;
        }
        public static Matrix<float> RotationYawPitchRoll(float yaw, float pitch, float roll)
        {
            Quaternion rotation = Quaternion.RotationYawPitchRoll(yaw, pitch, roll);
            float xx = rotation.X * rotation.X;
            float yy = rotation.Y * rotation.Y;
            float zz = rotation.Z * rotation.Z;
            float xy = rotation.X * rotation.Y;
            float zw = rotation.Z * rotation.W;
            float zx = rotation.Z * rotation.X;
            float yw = rotation.Y * rotation.W;
            float yz = rotation.Y * rotation.Z;
            float xw = rotation.X * rotation.W;

            Matrix<float> rotationMatrix = Matrix<float>.Build.DenseIdentity(4);
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
        public static Matrix<float> Translation(Vector3D p)
        {
            Matrix<float> translationMatrix = Matrix<float>.Build.DenseIdentity(4);
            translationMatrix[0, 3] = p.Z;
            translationMatrix[1, 3] = p.X;
            translationMatrix[2, 3] = p.Y;

            return translationMatrix;

        }
    }
}
