﻿using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    public class Camera
    {
        public Vector3D Position { get; set; }
        public Vector3D Target { get; set; }

    }

    public struct Face
    {
        public int A;
        public int B;
        public int C;

        public Vector3D Normal;
        public Vector3D Position;
    }

    public class Mesh
    {
        public string Name { get; set; }
        public Vertex[] Vertices { get; set; }
        public Face[] Faces { get; set; }
        public Vector3D Position { get; set; }
        public Vector3D Rotation { get; set; }

        public Color Color { get; set; }

        public Mesh(string name, int verticesCount, int faceCount)
        {
            Vertices = new Vertex[verticesCount];
            Faces = new Face[faceCount];
            Rotation = new Vector3D(0, 0, 0);
            Position = new Vector3D(0, 0, 0);
            Name = name;
        }

    }

    public struct Vertex
    {
        public Vector3D Normal;
        public Vector3D Coordinates;
        public Vector3D WorldCoordinates;
    }
}
