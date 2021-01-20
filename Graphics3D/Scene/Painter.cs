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
    public static class Painter
    {

        private static double Clamp(double value, double min = 0, double max = 1)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        static double Interpolate(double min, double max, double gradient)
        {
            return min + (max - min) * Clamp(gradient);
        }


        static void ProcessScanLine(ScanLineData data, Vertex va, Vertex vb, Vertex vc, Vertex vd, Color color, Device device)
        {
            Vector3D pa = va.Coordinates;
            Vector3D pb = vb.Coordinates;
            Vector3D pc = vc.Coordinates;
            Vector3D pd = vd.Coordinates;

            var gradient1 = pa.Y != pb.Y ? (data.currentY - pa.Y) / (pb.Y - pa.Y) : 1;
            var gradient2 = pc.Y != pd.Y ? (data.currentY - pc.Y) / (pd.Y - pc.Y) : 1;

            int sx = (int)Interpolate(pa.X, pb.X, gradient1);
            int ex = (int)Interpolate(pc.X, pd.X, gradient2);

            double z1 = Interpolate(pa.Z, pb.Z, gradient1);
            double z2 = Interpolate(pc.Z, pd.Z, gradient2);

            for (var x = sx; x < ex; x++)
            {
                double gradient = (x - sx) / (double)(ex - sx);

                double z = Interpolate(z1, z2, gradient);

                z = Math.Log(1 * z + 1) / Math.Log(1 * 100 + 1) * z;
                Color col = data.shadingModel.GetColor(color, x, data.currentY);
                DrawPoint(new Vector3D(x, data.currentY, z), col, device);
            }
        }


        public static void FillTriangle(Vertex v1, Vertex v2, Vertex v3, Color color, IShadingModel shadingModel, Device device)
        {
            if (v1.Coordinates.Y > v2.Coordinates.Y)
            {
                var temp = v2;
                v2 = v1;
                v1 = temp;
            }

            if (v2.Coordinates.Y > v3.Coordinates.Y)
            {
                var temp = v2;
                v2 = v3;
                v3 = temp;
            }

            if (v1.Coordinates.Y > v2.Coordinates.Y)
            {
                var temp = v2;
                v2 = v1;
                v1 = temp;
            }

            Vector3D p1 = v1.Coordinates;
            Vector3D p2 = v2.Coordinates;
            Vector3D p3 = v3.Coordinates;

            shadingModel.CalculateConst(v1, v2, v3, new Vector3D(0, 10, 10));

            ScanLineData data = new ScanLineData
            {
                shadingModel = shadingModel,
            };

            double dP1P2, dP1P3;

            if (p2.Y - p1.Y > 0)
                dP1P2 = (p2.X - p1.X) / (p2.Y - p1.Y);
            else
                dP1P2 = 0;

            if (p3.Y - p1.Y > 0)
                dP1P3 = (p3.X - p1.X) / (p3.Y - p1.Y);
            else
                dP1P3 = 0;

            if (dP1P2 > dP1P3)
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    data.currentY = y;

                    if (y < p2.Y)
                    {
                        ProcessScanLine(data, v1, v3, v1, v2, color, device);
                    }
                    else
                    {
                        ProcessScanLine(data, v1, v3, v2, v3, color, device);
                    }
                }
            }
            else
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    data.currentY = y;

                    if (y < p2.Y)
                    {
                        ProcessScanLine(data, v1, v2, v1, v3, color, device);
                    }
                    else
                    {
                        ProcessScanLine(data, v2, v3, v1, v3, color, device);
                    }
                }
            }
        }
        
        private static double InterpolateZ(Vector3D p1, Vector3D p2, Vector3D p3, double area, int x, int y)
        {
            double a1 = Vector3D.CalculateTriangleArea(new Vector3D(x, y, 1), p1, p2);
            double a2 = Vector3D.CalculateTriangleArea(new Vector3D(x, y, 1), p3, p2);
            double a3 = Vector3D.CalculateTriangleArea(new Vector3D(x, y, 1), p3, p1);

            double alfa = a1 / area;
            double beta = a2 / area;
            double gamma = a3 / area;

            return (alfa * p3.Z + beta * p1.Z + gamma * p2.Z);
        }
        public static void DrawLine(Vector3D v1, Vector3D v2, Device device)
        {
            int x0 = (int)v1.X;
            int y0 = (int)v1.Y;
            int x1 = (int)v2.X;
            int y1 = (int)v2.Y;

            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            var sx = (x0 < x1) ? 1 : -1;
            var sy = (y0 < y1) ? 1 : -1;
            var err = dx - dy;

            while (true)
            {
                double d1 = Math.Sqrt((x0 - v1.X) * (x0 - v1.X) + (y0 - v1.Y) * (y0 - v1.Y));
                double d2 = Math.Sqrt((x0 - v2.X) * (x0 - v2.X) + (y0 - v2.Y) * (y0 - v2.Y));

                double z = (d1 * v2.Z + d2 * v1.Z) / (d1 + d2);
                DrawPoint(new Vector3D(x0, y0, z - 0.0005), Color.Black, device);

                if ((x0 == x1) && (y0 == y1)) break;
                var e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x0 += sx; }
                if (e2 < dx) { err += dx; y0 += sy; }
            }
        }

        public static void DrawPoint(Vector3D coord2D, Color color, Device device)
        {
            if (coord2D.X >= 0 && coord2D.Y >= 0 && coord2D.X < device.RenderWidth && coord2D.Y < device.RenderHeight)
            {
                device.PutPixel((int)coord2D.X, (int)coord2D.Y, coord2D.Z, color);
            }
        }
    }

    struct ScanLineData
    {
        public int currentY;
        public IShadingModel shadingModel;
    }
}
