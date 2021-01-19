using Graphics3D.LightModels;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D
{
    public class NodeAET
    {
        public int Ymax;
        public double d, X;

        public NodeAET(int Ymax, double X, double d)
        {
            this.Ymax = Ymax;
            this.X = X;
            this.d = d;
        }
    }
    public static class Painter
    {
        static int MAX_ET = 1000;

        public static void FillTriangle(Vertex v1, Vertex v2, Vertex v3, Color color, ILightModel lightModel, Device device)
        {
            List<NodeAET>[] ET;
            float area = Vector3D.CalculateTriangleArea(v1.Coordinates, v2.Coordinates, v3.Coordinates);
            int edgeCounter;

            //For now
            Vector3D lightPos = new Vector3D(0, 10, 10);
            lightModel.CalculateConst(v1, v2, v3, lightPos);
            //

            (ET, edgeCounter) = createET(v1.Coordinates, v2.Coordinates, v3.Coordinates);

            int y = 0;
            while (ET[y] == null)
            {
                y++;
                if (y == MAX_ET) return;
            }

            List<NodeAET> AET = new List<NodeAET>();

            while (AET.Any() || edgeCounter != 0)
            {
                if (ET[y] != null)
                {
                    AET.AddRange(ET[y]);
                    edgeCounter -= ET[y].Count;
                }

                AET.Sort((NodeAET q, NodeAET p) =>
                {
                    if (q.X == p.X) return 0;
                    else if (q.X < p.X) return -1;
                    else return 1;
                });

                for (int i = 0; i < AET.Count; i += 2)
                {
                    int xMin = (int)AET[i].X;
                    int xMax = (int)AET[i+1].X;

                    for (int x = xMin; x <= xMax; x++)
                    {
                        float z = InterpolateZ(v1.Coordinates, v2.Coordinates, v3.Coordinates, area, x, y);
                        //
                        Color col = lightModel.GetColor(color, x, y);
                        //
                        device.PutPixel(x, y, z, col);
                    }
                }

                y++;
                AET.RemoveAll(node => node.Ymax == y);

                foreach (NodeAET node in AET)
                {
                    node.X += node.d;
                }
            }
        }
        private static (List<NodeAET>[], int) createET(Vector3D v1, Vector3D v2, Vector3D v3)
        {
            Point[] points = new Point[]
            {
                new Point((int)(v1.X), (int)(v1.Y)),
                new Point((int)(v2.X), (int)(v2.Y)),
                new Point((int)(v3.X), (int)(v3.Y))
            };

            int edgeCounter = 0;
            List<NodeAET>[] ET = new List<NodeAET>[MAX_ET];
            for (int i = 0; i < 2; i++)
                for (int j = i + 1; j < 3; j++)
                {
                    int ind;
                    double m;
                    int Ymax;
                    int Xmin;
                    NodeAET node;

                    if (points[i].Y == points[j].Y) continue;

                    edgeCounter++;
                    if (points[i].Y < points[j].Y)
                    {
                        m = (points[j].X - points[i].X) / (double)(points[j].Y - points[i].Y);
                        Ymax = points[j].Y;
                        Xmin = points[i].X;
                        ind = points[i].Y;
                    }
                    else
                    {
                        m = (points[i].X - points[j].X) / (double)(points[i].Y - points[j].Y);
                        Ymax = points[i].Y;
                        Xmin = points[j].X;
                        ind = points[j].Y;
                    }

                    node = new NodeAET(Ymax, Xmin, m);
                    if (ET[ind] != null)
                        ET[ind].Add(node);
                    else
                        ET[ind] = new List<NodeAET> { node };
                }
            return (ET, edgeCounter);
        }
        private static float InterpolateZ(Vector3D p1, Vector3D p2, Vector3D p3, float area, int x, int y)
        {
            float a1 = Vector3D.CalculateTriangleArea(new Vector3D(x, y, 1), p1, p2);
            float a2 = Vector3D.CalculateTriangleArea(new Vector3D(x, y, 1), p3, p2);
            float a3 = Vector3D.CalculateTriangleArea(new Vector3D(x, y, 1), p3, p1);

            float alfa = a1 / area;
            float beta = a2 / area;
            float gamma = a3 / area;

            return (alfa * p3.Z + beta * p1.Z + gamma * p2.Z);
        }
        public static void DrawLine(Vector3D v1, Vector3D v2, Color color, Device device)
        {
            Point p1 = new Point((int)v1.X, (int)v1.Y);
            Point p2 = new Point((int)v2.X, (int)v2.Y);

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
                float d1 = (float)Math.Sqrt((p1.X - x) * (p1.X - x) + (p1.Y - y) * (p1.Y - y));
                float d2 = (float)Math.Sqrt((x - p2.X) * (x - p2.X) + (y - p2.Y) * (y - p2.Y));

                float z = (d2 * v1.Z + d1 * v2.Z) / (d1 + d2);
                device.PutPixel(x, y, z - 0.00001f, color);
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

        public static void DrawPoint(Vector3D coord3D, Device device)
        {
            if (coord3D.X >= 0 && coord3D.Y >= 0 && coord3D.X < device.RenderWidth && coord3D.Y < device.RenderHeight)
            {
                device.PutPixel((int)coord3D.X, (int)coord3D.Y, coord3D.Z, Color.Black);
            }
        }
    }
}
