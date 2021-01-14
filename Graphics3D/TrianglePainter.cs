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
    public static class TrianglePainter
    {
        static int MAX = 1000;

        private static double CalculateTriangleArea(Point3D A, Point3D B, Point3D C)
        {
            return Math.Abs((B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X)) / 2f;
        }
        private static double InterpolateZ(Point3D[] points, double area, int x, int y)
        {
            double a1 = CalculateTriangleArea(new Point3D(x, y, 1), points[0], points[1]);
            double a2 = CalculateTriangleArea(new Point3D(x, y, 1), points[2], points[1]);
            double a3 = CalculateTriangleArea(new Point3D(x, y, 1), points[2], points[0]);

            double alfa = a1 / area;
            double beta = a2 / area;
            double gamma = a3 / area;

            return (alfa * points[2].Z + beta * points[0].Z + gamma * points[1].Z);
        }
        public static void Fill(Point3D[] points, Device device, Color color)
        {
            List<NodeAET>[] ET;
            double area = CalculateTriangleArea(points[0], points[1], points[2]);
            int edgeCounter;
            (ET, edgeCounter) = createET(points);

            int y = 0;
            while (ET[y] == null)
            {
                y++;
                if (y == MAX) return;
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

                for (int i = 0; i < AET.Count - 1; i += 2)
                {
                    int xMin = (int)AET[i].X;
                    int xMax = (int)AET[i + 1].X;

                    for (int x = xMin; x <= xMax; x++)
                    {
                        double z = InterpolateZ(points, area, x, y);
                        device.PutPixel(x, y, z, color);
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

        private static (List<NodeAET>[], int) createET(Point3D[] points)
        {
            int edgeCounter = 0;
            List<NodeAET>[] ET = new List<NodeAET>[MAX];
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
    }
}
