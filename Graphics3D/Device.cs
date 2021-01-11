using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    public class Device
    {
        private DirectBitmap bmp;

        public Device(DirectBitmap bmp)
        {
            this.bmp = bmp;
        }

        public Vector<double> Project(Vector<double> coord, Matrix<double> transMat)
        {
            Vector<double> point = transMat.Multiply(coord);
            point = point.Normalize(1);

            double x = point[0] * bmp.Width + bmp.Width / 2;
            double y = -point[1] * bmp.Height + bmp.Height / 2;
            
            Vector<double> coord2D = Vector<double>.Build.DenseOfArray(new double[] { x, y });
            
            return coord2D;
        }

        public void DrawPoint(Vector<double> coord2D)
        {
            if (coord2D[0] >= 0 && coord2D[1] >= 0 && coord2D[0] < bmp.Width && coord2D[1] < bmp.Height)
            {
                //bmp.SetPixel((int)coord2D[0], (int)coord2D[1], Color.Black);
                using(Graphics g = Graphics.FromImage(bmp.Bitmap))
                {
                    g.DrawEllipse(Pens.Red, (int)coord2D[0], (int)coord2D[1], 10, 10);
                }
            }
        }

        public void Render(Camera camera, params Mesh[] meshes)
        {
            var viewMatrix = MatrixGenerator.LookAtLH(camera.Position, camera.Target, Vector<double>.Build.DenseOfArray(new double[] { 0, 1, 0 }));
            var projectMatrix = MatrixGenerator.PerspectiveFovRH(0.78f, (double)bmp.Width / bmp.Height, 0.01f, 1.0f);
            
            foreach(Mesh mesh in meshes)
            {
                Matrix<double> worldMatrix = MatrixGenerator.RotationYawPitchRoll(mesh.Rotation[1], mesh.Rotation[0], mesh.Rotation[2]) *
                    MatrixGenerator.Translation(mesh.Position);

                Matrix<double> transformMatrix = worldMatrix.Multiply(viewMatrix).Multiply(projectMatrix);

                foreach(var vertex in mesh.Vertices)
                {
                    var point = Project(vertex, transformMatrix);
                    DrawPoint(point);
                }
            }
        }
    }
}
