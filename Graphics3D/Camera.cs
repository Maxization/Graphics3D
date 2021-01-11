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

    public class Mesh
    {
        public string Name { get; set; }
        public Vector<double>[] Vertices { get; private set; }
        public Vector<double> Position { get; set; }
        public Vector<double> Rotation { get; set; }

        public Mesh(string name, int verticesCount)
        {
            Vertices = new Vector<double>[verticesCount];
            Name = name;
        }
    }
}
