using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Graphics3D
{
    public class Device
    {
        private DirectBitmap bmp;
        private double[,] zBuffer;
        public Device(DirectBitmap bmp)
        {
            this.bmp = bmp;
            zBuffer = new double[bmp.Height, bmp.Width];

            Clear();
        }

        public void Clear()
        {
            using(Graphics g = Graphics.FromImage(bmp.Bitmap))
            {
                g.Clear(Color.White);
            }

            for(int i=0;i<bmp.Height;i++)
            {
                for(int j=0;j<bmp.Width;j++)
                {
                    zBuffer[i, j] = double.MaxValue;
                }
            }
        }

        public Vector<double> Project(Vector<double> coord, Matrix<double> transMat)
        {
            Vector<double> point = transMat.Multiply(coord);
            point = point.Divide(point[point.Count - 1]);

            double x = (point[0] + 1f) / 2f * bmp.Width;
            double y = (-point[1] + 1f) / 2f * bmp.Height;
            Vector<double> coord3Din2D = Vector<double>.Build.DenseOfArray(new double[] { x, y, point[2] });
            
            return coord3Din2D;
        }

        public void DrawLine(Point3D p1, Point3D p2, Color color)
        {
            int x = p1.X;
            int y = p1.Y;
            int w = p2.X - p1.X;
            int h = p2.Y - p1.Y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                double d1 = Math.Sqrt((p1.X - x) * (p1.X - x) + (p1.Y - y) * (p1.Y - y));
                double d2 = Math.Sqrt((x - p2.X) * (x - p2.X) + (y - p2.Y) * (y - p2.Y));

                double z = (d1 * p1.Z + d2 * p2.Z) / (d1 + d2);
                PutPixel(x, y, z - 0.0001, color);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        public void PutPixel(int x, int y, double z, Color color)
        {
            if(x< 0 || y<0 || x>=bmp.Width || y>=bmp.Height)
                return;

            if (zBuffer[y, x] < z)
            {
                return;
            }

            zBuffer[y, x] = z;
            bmp.SetPixel(x, y, color);
        }
        public void DrawPoint(Point3D coord3D)
        {
            if (coord3D.X >= 0 && coord3D.Y >= 0 && coord3D.X < bmp.Width && coord3D.Y < bmp.Height)
            {
                PutPixel(coord3D.X, coord3D.Y, coord3D.Z, Color.Black);
            }
        }

        public void Render(Camera camera, params Mesh[] meshes)
        {
            var viewMatrix = MatrixGenerator.LookAt(camera.Position, camera.Target, Vector<double>.Build.DenseOfArray(new double[] { 0, 1, 0 }));
            var projectMatrix = MatrixGenerator.PerspectiveFov(0.78f, (double)bmp.Width / bmp.Height, 0.01f, 1.0f);

            foreach (Mesh mesh in meshes)
            {
                Matrix<double> worldMatrix = MatrixGenerator.Translation(mesh.Position) *
                    MatrixGenerator.RotationYawPitchRoll(mesh.Rotation[0], mesh.Rotation[2], mesh.Rotation[1]);

                Matrix<double> transformMatrix = projectMatrix.Multiply(viewMatrix).Multiply(worldMatrix);

                foreach(Face face in mesh.Faces)
                {
                    Vector<double> v1 = mesh.Vertices[face.A];
                    Vector<double> v2 = mesh.Vertices[face.B];
                    Vector<double> v3 = mesh.Vertices[face.C];

                    Point3D p1 = new Point3D(Project(v1, transformMatrix));
                    Point3D p2 = new Point3D(Project(v2, transformMatrix));
                    Point3D p3 = new Point3D(Project(v3, transformMatrix));
                    
                    TrianglePainter.Fill(new Point3D[] { p1, p2, p3 }, this, Color.Gray);

                    DrawLine(p1, p2, Color.Black);
                    DrawLine(p1, p3, Color.Black);
                    DrawLine(p2, p3, Color.Black);
                }
            }
        }
    
        public Mesh[] LoadJSONFileAsync(string fileName)
        {
            List<Mesh> meshes = new List<Mesh>();
            dynamic jsonObject;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Models\" + $"{fileName}");
            
            using (StreamReader stream = new StreamReader(path))
            {
                var data = stream.ReadToEnd(); //await stream.ReadToEndAsync();
                jsonObject = JsonConvert.DeserializeObject(data);
                stream.Close();
            }

            for (var meshIndex = 0; meshIndex < jsonObject.meshes.Count; meshIndex++)
            {
                var positionsArray = jsonObject.meshes[meshIndex].positions;

                // Faces
                var indicesArray = jsonObject.meshes[meshIndex].indices;

                var verticesStep = 3;
                var verticesCount = positionsArray.Count / verticesStep;
                // number of faces is logically the size of the array divided by 3 (A, B, C)
                var facesCount = indicesArray.Count / 3;
                var mesh = new Mesh(jsonObject.meshes[meshIndex].name.Value, verticesCount, facesCount);

                // Filling the Vertices array of mesh
                for (var index = 0; index < verticesCount; index++)
                {
                    var x = (double)positionsArray[index * verticesStep].Value;
                    var y = (double)positionsArray[index * verticesStep + 1].Value;
                    var z = (double)positionsArray[index * verticesStep + 2].Value;
                    mesh.Vertices[index] = Vector<double>.Build.DenseOfArray(new double[] { x, y, z, 1 });
                }

                // Then filling the Faces array
                for (var index = 0; index < facesCount; index++)
                {
                    var a = (int)indicesArray[index * 3].Value;
                    var b = (int)indicesArray[index * 3 + 1].Value;
                    var c = (int)indicesArray[index * 3 + 2].Value;
                    mesh.Faces[index] = new Face { A = a, B = b, C = c };
                }

                var position = jsonObject.meshes[meshIndex].position;
                var rotation = jsonObject.meshes[meshIndex].rotation;
                mesh.Position = Vector<double>.Build.DenseOfArray(new double[] { (double)position[0].Value, (double)position[1].Value, (double)position[2].Value });
                mesh.Rotation = Vector<double>.Build.DenseOfArray(new double[] { (double)rotation[0].Value, (double)rotation[1].Value, (double)rotation[2].Value });
                meshes.Add(mesh);
            }

            return meshes.ToArray();
        }
    }
}
