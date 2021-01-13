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
            point = point.Divide(point[point.Count - 1]);

            double x = (point[0] + 1f) / 2f * bmp.Width;
            double y = (-point[1] + 1f) / 2f * bmp.Height;
            Vector<double> coord2D = Vector<double>.Build.DenseOfArray(new double[] { x, y });
            
            return coord2D;
        }
        public void DrawLine(Vector<double> p1, Vector<double> p2)
        {
            using (Graphics g = Graphics.FromImage(bmp.Bitmap))
            {
                g.DrawLine(Pens.Black, (int)p1[0], (int)p1[1], (int)p2[0], (int)p2[1]);
            }
        }
        public void DrawPoint(Vector<double> coord2D)
        {
            if (coord2D[0] >= 0 && coord2D[1] >= 0 && coord2D[0] < bmp.Width && coord2D[1] < bmp.Height)
            {
                //bmp.SetPixel((int)coord2D[0], (int)coord2D[1], Color.Black);
                using(Graphics g = Graphics.FromImage(bmp.Bitmap))
                {
                    g.FillRectangle(Brushes.Black, (int)coord2D[0] - 1, (int)coord2D[1] - 1, 3, 3);
                }
            }
        }

        public void Render(Camera camera, params Mesh[] meshes)
        {
            var viewMatrix = MatrixGenerator.LookAt(camera.Position, camera.Target, Vector<double>.Build.DenseOfArray(new double[] { 0, 1, 0 }));
            var projectMatrix = MatrixGenerator.PerspectiveFov(0.78f, (double)bmp.Width / bmp.Height, 0.01f, 1.0f);

            foreach (Mesh mesh in meshes)
            {
                Matrix<double> worldMatrix = MatrixGenerator.RotationYawPitchRoll(mesh.Rotation[0], mesh.Rotation[2], mesh.Rotation[1]) *
                    MatrixGenerator.Translation(mesh.Position);

                Matrix<double> transformMatrix = projectMatrix.Multiply(viewMatrix).Multiply(worldMatrix);

                for (int i = 0;i < mesh.Vertices.Length - 1;i++)
                {
                    var point0 = Project(mesh.Vertices[i], transformMatrix);
                    var point1 = Project(mesh.Vertices[i + 1], transformMatrix);
                    DrawLine(point0, point1);
                }
            }
        }
    }
}
