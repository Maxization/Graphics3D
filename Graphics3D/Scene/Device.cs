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
using Graphics3D.LightModels;

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
            using (Graphics g = Graphics.FromImage(bmp.Bitmap))
            {
                g.Clear(Color.White);
            }

            for (int i = 0; i < zBuffer.Length; i++)
            {
                zBuffer[i] = double.MaxValue;
            }
        }

        public Vertex Project(Vertex vertex, Matrix<double> transMat, Matrix<double> world)
        {
            Vector3D point2d = transMat.Multiply(vertex.Coordinates);
            point2d.Z = (double)(Math.Log(1 * point2d.Z + 1) / Math.Log(1 * 100 + 1) * point2d.Z);

            Vector3D point3dWorld = world.Multiply(vertex.Coordinates);
            Vector3D normal3dWorld = world.Multiply(vertex.Normal);

            double x = (point2d.X + 1f) / 2f * RenderWidth;
            double y = (-point2d.Y + 1f) / 2f * RenderHeight;

            return new Vertex
            {
                Coordinates = new Vector3D(x, y, point2d.Z),
                Normal = new Vector3D(normal3dWorld.X, normal3dWorld.Y, normal3dWorld.Z),
                WorldCoordinates = new Vector3D(point3dWorld.X, point3dWorld.Y, point3dWorld.Z)
            };
        }

        public void PutPixel(int x, int y, double z, Color color)
        {
            if (x < 0 || y < 0 || x >= RenderWidth || y >= RenderHeight)
                return;

            int index = (x + y * RenderWidth);

            lock (lockBuffer[index])
            {
                if (zBuffer[index] < z)
                {
                    return;
                }

                zBuffer[index] = z;
                bmp.SetPixel(x, y, color);
            }
        }

        public void Render(Camera camera, ShadingModelEnum shadingType, ILightModel lightModel, params Mesh[] meshes)
        {
            var viewMatrix = MatrixGenerator.LookAt(camera.Position, camera.Target, new Vector3D(0, 1, 0));

            var projectMatrix = MatrixGenerator.PerspectiveFov(1.74f, (double)RenderHeight / RenderWidth, 1f, 100f);

            foreach (Mesh mesh in meshes)
            {
                Matrix<double> worldMatrix = MatrixGenerator.Translation(mesh.Position)
                                             * MatrixGenerator.RotationYawPitchRoll(mesh.Rotation.Y, mesh.Rotation.X, mesh.Rotation.Z);

                Matrix<double> worldViewMatrix = viewMatrix * worldMatrix;
                Matrix<double> transformMatrix = projectMatrix * worldViewMatrix;

                //Parallel.ForEach(mesh.Faces, (Face face) =>
                foreach (Face face in mesh.Faces)
                {
                    Matrix<double> worldToObject = worldViewMatrix.Inverse();
                    Vector3D cameraPosition = worldToObject.Multiply(camera.Position);
                    Vector3D T = face.Position - cameraPosition;
                    double dot = Vector3D.Dot(T, face.Normal);
                    if (dot >= 0)
                    {
                        //return;
                        continue;
                    }

                    Vertex v1 = mesh.Vertices[face.A];
                    Vertex v2 = mesh.Vertices[face.B];
                    Vertex v3 = mesh.Vertices[face.C];


                    v1 = Project(v1, transformMatrix, worldMatrix);
                    v2 = Project(v2, transformMatrix, worldMatrix);
                    v3 = Project(v3, transformMatrix, worldMatrix);

                    IShadingModel sm = ShadingModelFactory.Create(shadingType);
                    Painter.FillTriangle(v1, v2, v3, mesh.Color, sm, lightModel,camera.Position, this);

                }//);
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
                HashSet<Vector3D> vertices = new HashSet<Vector3D>();
                Dictionary<int, Vector3D> indexes = new Dictionary<int, Vector3D>();
                Dictionary<Vector3D, Vector3D> normals = new Dictionary<Vector3D, Vector3D>();


                var positionsArray = jsonObject.meshes[meshIndex].positions;
                // Faces
                var indicesArray = jsonObject.meshes[meshIndex].indices;

                var normalsArray = jsonObject.meshes[meshIndex].normals;

                var verticesStep = 3;
                var positionsCount = positionsArray.Count / verticesStep;

                var facesCount = indicesArray.Count / 3;

                for (var index = 0; index < positionsCount; index++)
                {
                    var x = (double)positionsArray[index * verticesStep].Value;
                    var y = (double)positionsArray[index * verticesStep + 1].Value;
                    var z = (double)positionsArray[index * verticesStep + 2].Value;
                    Vector3D vertex = new Vector3D(x, y, z);

                    var nx = (double)normalsArray[index * verticesStep].Value;
                    var ny = (double)normalsArray[index * verticesStep + 1].Value;
                    var nz = (double)normalsArray[index * verticesStep + 2].Value;
                    Vector3D normal = new Vector3D(nx, ny, nz);

                    indexes.Add(index, vertex);

                    if (vertices.Contains(vertex))
                    {
                        normals[vertex] = (normals[vertex] + normal);
                    }
                    else
                    {
                        vertices.Add(vertex);
                        normals.Add(vertex, normal);
                    }
                }

                var verticesArray = vertices.ToArray();
                var mesh = new Mesh(jsonObject.meshes[meshIndex].name.Value, verticesArray.Length, facesCount);
                for (int i = 0; i < verticesArray.Length; i++)
                {
                    mesh.Vertices[i] = new Vertex
                    {
                        Coordinates = verticesArray[i],
                        Normal = normals[verticesArray[i]].Normalized()
                    };
                }

                for (var index = 0; index < facesCount; index++)
                {
                    var a = (int)indicesArray[index * 3].Value;
                    var b = (int)indicesArray[index * 3 + 1].Value;
                    var c = (int)indicesArray[index * 3 + 2].Value;

                    Vector3D na = new Vector3D((double)normalsArray[a * 3], (double)normalsArray[a * 3 + 1], (double)normalsArray[a * 3 + 2]);
                    Vector3D nb = new Vector3D((double)normalsArray[b * 3], (double)normalsArray[b * 3 + 1], (double)normalsArray[b * 3 + 2]);
                    Vector3D nc = new Vector3D((double)normalsArray[c * 3], (double)normalsArray[c * 3 + 1], (double)normalsArray[c * 3 + 2]);

                    var aa = Array.IndexOf(verticesArray, indexes[a]);
                    var bb = Array.IndexOf(verticesArray, indexes[b]);
                    var cc = Array.IndexOf(verticesArray, indexes[c]);

                    mesh.Faces[index] = new Face { A = aa, B = bb, C = cc };
                    mesh.Faces[index].Normal = (na + nb + nc) / 3.0f;
                    mesh.Faces[index].Position = (mesh.Vertices[aa].Coordinates + mesh.Vertices[bb].Coordinates + mesh.Vertices[cc].Coordinates) / 3f;
                }

                var position = jsonObject.meshes[meshIndex].position;
                mesh.Position = new Vector3D((double)position[0].Value, (double)position[2].Value, (double)position[1].Value);


                meshes.Add(mesh);
            }

            return meshes.ToArray();
        }
    }
}
