using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D.LightModels
{
    public interface ILightModel
    {
        Color GetColor(Color color, Vector3D lightPosition);
    }

    public class FlatLightModel : ILightModel
    {
        Vector3D vnFace;
        Vector3D centerPoint;
        public void CalculateConst(Vertex v1, Vertex v2, Vertex v3)
        {
            vnFace = (v1.Normal + v2.Normal + v3.Normal) / 3;
            centerPoint = (v1.WorldCoordinates + v2.WorldCoordinates + v3.WorldCoordinates) / 3;
        }
        public Color GetColor(Color color, Vector3D lightPosition)
        {
            float ndotl = ComputeNDotL(centerPoint, vnFace, lightPosition);
            return Color.FromArgb((int)(color.R * ndotl), (int)(color.G*ndotl), (int)(color.B * ndotl));
        }

        private static float ComputeNDotL(Vector3D vertex, Vector3D normal, Vector3D lightPosition)
        {
            var lightDirection = lightPosition - vertex;

            normal.Normalize();
            lightDirection.Normalize();

            return Math.Max(0, Vector3D.Dot(normal, lightDirection));
        }
    }

    public class GouraudLightModel //: ILightModel
    {
        Vertex v1, v2, v3;
        float area;
        float i1, i2, i3;
        public void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Vector3D lightPosition)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            Point3D p1 = new Point3D(v1.Coordinates);
            Point3D p2 = new Point3D(v2.Coordinates);
            Point3D p3 = new Point3D(v3.Coordinates);
            area = CalculateTriangleArea(p1, p2, p3);
            i1 = ComputeNDotL(v1.WorldCoordinates, v1.Normal, lightPosition);
            i2 = ComputeNDotL(v2.WorldCoordinates, v2.Normal, lightPosition);
            i3 = ComputeNDotL(v3.WorldCoordinates, v3.Normal, lightPosition);
        }

        public Color GetColor(Color color, Vector3D lightPosition, int x, int y)
        {
            if (area == 0) return color;
            float i = InterpolateI(area, x, y);
            return Color.FromArgb((int)(color.R * i), (int)(color.G * i), (int)(color.B * i));
        }

        private float InterpolateI(float area, int x, int y)
        {
            Point3D p1 = new Point3D(v1.Coordinates);
            Point3D p2 = new Point3D(v2.Coordinates);
            Point3D p3 = new Point3D(v3.Coordinates);
            float a1 = CalculateTriangleArea(new Point3D(x, y, 1), p1, p2);
            float a2 = CalculateTriangleArea(new Point3D(x, y, 1), p3, p2);
            float a3 = CalculateTriangleArea(new Point3D(x, y, 1), p3, p1);

            float alfa = a1 / area;
            float beta = a2 / area;
            float gamma = a3 / area;

            return Math.Max(0, Math.Min(1, (alfa * i3) + (beta * i1) + (gamma * i2)));
        }

        private static float ComputeNDotL(Vector3D vertex, Vector3D normal, Vector3D lightPosition)
        {
            var lightDirection = lightPosition - vertex;

            normal.Normalize();
            lightDirection.Normalize();

            return Math.Max(0, Vector3D.Dot(normal, lightDirection));
        }

        private float CalculateTriangleArea(Point3D A, Point3D B, Point3D C)
        {
            var res =  Math.Abs((B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X)) / 2f;
            return res;
        }
    }

    

}
