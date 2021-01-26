using Graphics3D.LightModels;
using Graphics3D.ShadingModels;
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

            //Vector3D lightPosition = new Vector3D(0, 10, 10);
            Light[] lights = new Light[]
            {
                new Light
                {
                    position = new Vector3D(0,10,10),
                    color = Color.White
                }
            };

            shadingModel.CalculateConst(v1, v2, v3, lights);

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

                    if (y < p2.Y)
                    {
                        shadingModel.setOrder(1);
                        shadingModel.ProcessScanLine(y, v1, v3, v1, v2, color, device);
                    }
                    else
                    {
                        shadingModel.setOrder(2);
                        shadingModel.ProcessScanLine(y, v1, v3, v2, v3, color, device);
                    }
                }
            }
            else
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {

                    if (y < p2.Y)
                    {
                        shadingModel.setOrder(3);
                        shadingModel.ProcessScanLine(y, v1, v2, v1, v3, color, device);
                    }
                    else
                    {
                        shadingModel.setOrder(4);
                        shadingModel.ProcessScanLine(y, v2, v3, v1, v3, color, device);
                    }
                }
            }
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
                device.PutPixel(x0, y0, z - 0.0005, Color.Black);

                if ((x0 == x1) && (y0 == y1)) break;
                var e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x0 += sx; }
                if (e2 < dx) { err += dx; y0 += sy; }
            }
        }
    }
}
