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
using Graphics3D.ShadingModels;

namespace Graphics3D
{
    public class Device
    {
        private DirectBitmap bmp;
        private double[] zBuffer;
        private object[] lockBuffer;
        
        public int RenderWidth { get; }
        public int RenderHeight { get; }
        public Device(DirectBitmap bmp)
        {
            this.bmp = bmp;

            RenderWidth = bmp.Width;
            RenderHeight = bmp.Height;

            zBuffer = new double[RenderHeight * RenderWidth];
            lockBuffer = new object[RenderHeight * RenderWidth];
            for (int i = 0; i < lockBuffer.Length; i++)
                lockBuffer[i] = new object();

            Clear();
        }

        public void Clear()
        {
            using(Graphics g = Graphics.FromImage(bmp.Bitmap))
            {
                g.Clear(Color.White);
            }

            for(int i=0;i<zBuffer.Length;i++)
            {
                zBuffer[i] = double.MaxValue;
            }
        }

        public Vertex Project(Vertex vertex, Matrix<double> transMat, Matrix<double> world)
        {
            Vector<double> point2d = transMat.Multiply(vertex.Coordinates);
            point2d = point2d.Divide(point2d[point2d.Count - 1]);
            point2d[2] = (double)(Math.Log(1 * point2d[2] + 1) / Math.Log(1 * 100 + 1) * point2d[2]);

            Vector<double> point3dWorld = world.Multiply(vertex.Coordinates);
            Vector<double> normal3dWorld = world.Multiply(vertex.Normal);

            double x = (point2d[0] + 1f) / 2f * RenderWidth;
            double y = (-point2d[1] + 1f) / 2f * RenderHeight;

            return new Vertex
            {
                Coordinates = new Vector3D ( x, y, point2d[2] ),
                Normal = new Vector3D(normal3dWorld[0], normal3dWorld[1], normal3dWorld[2]),
                WorldCoordinates = new Vector3D(point3dWorld[0], point3dWorld[1], point3dWorld[2])
            };
        }

        public void PutPixel(int x, int y, double z, Color color)
        {
            if (x < 0 || y < 0 || x >= RenderWidth || y >= RenderHeight)
                return;

            int index = (x + y * RenderWidth);

            lock(lockBuffer[index])
            {
                if (zBuffer[index] < z)
                {
                    return;
                }

                zBuffer[index] = z;
                bmp.SetPixel(x, y, color);
            }
        }

        public void Render(Camera camera, ShadingModelEnum shadingType, params Mesh[] meshes)
        {
            var viewMatrix = MatrixGenerator.LookAt(camera.Position, camera.Target, new Vector3D(0,1,0));

            var projectMatrix = MatrixGenerator.PerspectiveFov(1.74f, (double)RenderHeight / RenderWidth, 1f, 100f);

            foreach (Mesh mesh in meshes)
            {
                Matrix<double> worldMatrix = MatrixGenerator.Translation(mesh.Position) *
                                                                                       MatrixGenerator.RotationYawPitchRoll(mesh.Rotation.X, mesh.Rotation.Z, mesh.Rotation.Y);

                Matrix<double> worldViewMatrix = viewMatrix * worldMatrix;
                Matrix<double> transformMatrix = projectMatrix * worldViewMatrix;
                
                Parallel.ForEach(mesh.Faces, (Face face) =>
                 {

                     Vector<double> transformedNormal = worldViewMatrix.Multiply(face.Normal);

                     if(transformedNormal[2] >= 0)
                     {
                         return;
                     }

                     Vertex v1 = mesh.Vertices[face.A];
                     Vertex v2 = mesh.Vertices[face.B];
                     Vertex v3 = mesh.Vertices[face.C];

                     v1 = Project(v1, transformMatrix, worldMatrix);
                     v2 = Project(v2, transformMatrix, worldMatrix);
                     v3 = Project(v3, transformMatrix, worldMatrix);

                     IShadingModel sm = ShadingModelFactory.Create(shadingType);
                     Painter.FillTriangle(v1, v2, v3, Color.White, sm, this);

                 });
            }
        }
    
        public Mesh[] LoadJSONFile(string fileName)
        {
            List<Mesh> meshes = new List<Mesh>();
            dynamic jsonObject;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Models\" + $"{fileName}");
            
            using (StreamReader stream = new StreamReader(path))
            {
                var data = stream.ReadToEnd();
                jsonObject = JsonConvert.DeserializeObject(data);
                stream.Close();
            }

            for (var meshIndex = 0; meshIndex < jsonObject.meshes.Count; meshIndex++)
            {
                var verticesArray = jsonObject.meshes[meshIndex].vertices;
                // Faces
                var indicesArray = jsonObject.meshes[meshIndex].indices;

                var uvCount = jsonObject.meshes[meshIndex].uvCount.Value;
                var verticesStep = 1;

                // Depending of the number of texture's coordinates per vertex
                // we're jumping in the vertices array  by 6, 8 & 10 windows frame
                switch ((int)uvCount)
                {
                    case 0:
                        verticesStep = 6;
                        break;
                    case 1:
                        verticesStep = 8;
                        break;
                    case 2:
                        verticesStep = 10;
                        break;
                }

                // the number of interesting vertices information for us
                var verticesCount = verticesArray.Count / verticesStep;
                // number of faces is logically the size of the array divided by 3 (A, B, C)
                var facesCount = indicesArray.Count / 3;
                var mesh = new Mesh(jsonObject.meshes[meshIndex].name.Value, verticesCount, facesCount);

                // Filling the Vertices array of our mesh first
                for (var index = 0; index < verticesCount; index++)
                {
                    var x = (double)verticesArray[index * verticesStep].Value;
                    var y = (double)verticesArray[index * verticesStep + 1].Value;
                    var z = (double)verticesArray[index * verticesStep + 2].Value;
                    // Loading the vertex normal exported by Blender
                    var nx = (double)verticesArray[index * verticesStep + 3].Value;
                    var ny = (double)verticesArray[index * verticesStep + 4].Value;
                    var nz = (double)verticesArray[index * verticesStep + 5].Value;
                    mesh.Vertices[index] = new Vertex { Coordinates = new Vector3D(x, y, z), Normal = new Vector3D(nx, ny, nz) };
                }

                // Then filling the Faces array
                for (var index = 0; index < facesCount; index++)
                {
                    var a = (int)indicesArray[index * 3].Value;
                    var b = (int)indicesArray[index * 3 + 1].Value;
                    var c = (int)indicesArray[index * 3 + 2].Value;
                    mesh.Faces[index] = new Face { A = a, B = b, C = c };
                }

                // Getting the position you've set in Blender
                var position = jsonObject.meshes[meshIndex].position;
                mesh.Position = new Vector3D((double)position[0].Value, (double)position[1].Value, (double)position[2].Value);

                mesh.ComputeFacesNormals();

                meshes.Add(mesh);
            }

            return meshes.ToArray();
        }
    }
}
