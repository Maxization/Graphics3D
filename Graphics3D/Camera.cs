using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    public class Camera
    {
        public Vector<double> Position { get; set; }
        public Vector<double> Target { get; set; }

    }

    public struct Face
    {
        public int A;
        public int B;
        public int C;
    }

    public class Mesh
    {
        public string Name { get; set; }
        public Vector<double>[] Vertices { get; private set; }
        public Face[] Faces { get; set; }
        public Vector<double> Position { get; set; }
        public Vector<double> Rotation { get; set; }

        public Mesh(string name, int verticesCount, int faceCount)
        {
            Vertices = new Vector<double>[verticesCount];
            Faces = new Face[faceCount];
            Name = name;
        }
    }
}
