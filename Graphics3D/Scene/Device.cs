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
using Graphics3D.LightModels;

namespace Graphics3D
{
    public class Device
    {
        private DirectBitmap bmp;
        private float[] zBuffer;
        private object[] lockBuffer;
        
        public int RenderWidth { get; }
        public int RenderHeight { get; }
        public Device(DirectBitmap bmp)
        {
            this.bmp = bmp;

            RenderWidth = bmp.Width;
            RenderHeight = bmp.Height;

            zBuffer = new float[RenderHeight * RenderWidth];
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
                zBuffer[i] = float.MaxValue;
            }
        }

        public Vertex Project(Vertex vertex, Matrix<float> transMat, Matrix<float> world)
        {
            Vector<float> point2d = transMat.Multiply(vertex.Coordinates);
            point2d = point2d.Divide(point2d[point2d.Count - 1]);

            Vector<float> point3dWorld = world.Multiply(vertex.Coordinates);
            point3dWorld = point3dWorld.Divide(point3dWorld[point3dWorld.Count - 1]);

            Vector<float> normal3dWorld = world.Multiply(vertex.Normal);
            normal3dWorld = normal3dWorld.Divide(normal3dWorld[normal3dWorld.Count - 1]);

            float x = (point2d[0] + 1f) / 2f * RenderWidth;
            float y = (-point2d[1] + 1f) / 2f * RenderHeight;

            return new Vertex
            {
                Coordinates = new Vector3D ( x, y, point2d[2] ),
                Normal = new Vector3D(normal3dWorld[0], normal3dWorld[1], normal3dWorld[2]),
                WorldCoordinates = new Vector3D(point3dWorld[0], point3dWorld[1], point3dWorld[2])
            };
        }

        public void PutPixel(int x, int y, float z, Color color)
        {
            if(x< 0 || y<0 || x>=RenderWidth || y>=RenderHeight)
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

        public void Render(Camera camera, params Mesh[] meshes)
        {
            var viewMatrix = MatrixGenerator.LookAt(camera.Position, camera.Target, new Vector3D(0,1,0));
            var projectMatrix = MatrixGenerator.PerspectiveFov(0.78f, (float)RenderWidth / RenderHeight, 0.01f, 1.0f);

            foreach (Mesh mesh in meshes)
            {
                Matrix<float> worldMatrix = MatrixGenerator.Translation(mesh.Position) *
                    MatrixGenerator.RotationYawPitchRoll(mesh.Rotation.X, mesh.Rotation.Z, mesh.Rotation.Y);

                Matrix<float> transformMatrix = projectMatrix.Multiply(viewMatrix).Multiply(worldMatrix);
                Parallel.ForEach(mesh.Faces, (Face face) =>
                 {
                     Vertex v1 = mesh.Vertices[face.A];
                     Vertex v2 = mesh.Vertices[face.B];
                     Vertex v3 = mesh.Vertices[face.C];

                     v1 = Project(v1, transformMatrix, worldMatrix);
                     v2 = Project(v2, transformMatrix, worldMatrix);
                     v3 = Project(v3, transformMatrix, worldMatrix);

                     GouraudLightModel lm = new GouraudLightModel();
                     Painter.FillTriangle(v1,v2,v3, Color.White, lm, this);

                     //Point3D p1 = new Point3D(v1.Coordinates);
                     //Point3D p2 = new Point3D(v2.Coordinates);
                     //Point3D p3 = new Point3D(v3.Coordinates);
                     //Painter.DrawLine(p1, p2, Color.Black, this);
                     //Painter.DrawLine(p1, p3, Color.Black, this);
                     //Painter.DrawLine(p2, p3, Color.Black, this);
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
                HashSet<Vector3D> positions = new HashSet<Vector3D>();
                Dictionary<Vector3D, Vector3D> normals = new Dictionary<Vector3D, Vector3D>();

                var positionsJSONArray = jsonObject.meshes[meshIndex].positions;
                var normalsJSONArray = jsonObject.meshes[meshIndex].normals;
                // Faces
                var indicesJSONArray = jsonObject.meshes[meshIndex].indices;

                var verticesStep = 3;
                var verticesCount = positionsJSONArray.Count / verticesStep;

                Vector3D[] verticesJSON = new Vector3D[verticesCount];

                // number of faces is logically the size of the array divided by 3 (A, B, C)
                var facesCount = indicesJSONArray.Count / 3;

                // Filling the Vertices array of mesh
                for (var index = 0; index < verticesCount; index++)
                {
                    var x = (float)positionsJSONArray[index * verticesStep].Value;
                    var y = (float)positionsJSONArray[index * verticesStep + 1].Value;
                    var z = (float)positionsJSONArray[index * verticesStep + 2].Value;

                    var nx = (float)normalsJSONArray[index * verticesStep].Value;
                    var ny = (float)normalsJSONArray[index * verticesStep + 1].Value;
                    var nz = (float)normalsJSONArray[index * verticesStep + 2].Value;

                    Vector3D normal = new Vector3D(nx, ny, nz);
                    Vector3D pos = new Vector3D(x, y, z);
                    if (positions.Contains(pos))
                    {
                        normals[pos] = normals[pos] + normal;
                    }
                    else
                    {
                        positions.Add(pos);
                        normals.Add(pos, normal);
                    }
                    verticesJSON[index] = pos;
                }

                var mesh = new Mesh(jsonObject.meshes[meshIndex].name.Value, positions.Count, facesCount);
                var points = positions.ToArray();
                int ind = 0;

                foreach(var pos in points)
                {
                    Vector3D normal = normals[pos];
                    normal.Normalize();

                    mesh.Vertices[ind] = new Vertex
                    {
                        Coordinates = new Vector3D(pos.X, pos.Y, pos.Z),
                        Normal = new Vector3D(normal.X, normal.Y, normal.Z),
                    };
                    ind++;
                }

                
                // Then filling the Faces array
                for (var index = 0; index < facesCount; index++)
                {
                    var aFromJSON = (int)indicesJSONArray[index * verticesStep].Value;
                    var bFromJSON = (int)indicesJSONArray[index * verticesStep + 1].Value;
                    var cFromJSON = (int)indicesJSONArray[index * verticesStep + 2].Value;

                    var a = Array.IndexOf(points, verticesJSON[aFromJSON]);
                    var b = Array.IndexOf(points, verticesJSON[bFromJSON]);
                    var c = Array.IndexOf(points, verticesJSON[cFromJSON]);

                    mesh.Faces[index] = new Face { A = a, B = b, C = c };
                }

                var position = jsonObject.meshes[meshIndex].position;
                var rotation = jsonObject.meshes[meshIndex].rotation;
                mesh.Position = new Vector3D((float)position[0].Value, (float)position[1].Value, (float)position[2].Value);
                mesh.Rotation = new Vector3D((float)rotation[0].Value, (float)rotation[1].Value, (float)rotation[2].Value);
                meshes.Add(mesh);
            }

            return meshes.ToArray();
        }
    }
}
