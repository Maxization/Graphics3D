using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D.LightModels
{
    public interface IShadingModel
    {
        void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Vector3D lightPosition);
        Color GetColor(Color color, int x, int y);
    }

    public struct FlatShadingModel : IShadingModel
    {
        Vector3D vnFace;
        Vector3D centerPoint;
        double ndotl;
        public void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Vector3D lightPosition)
        {
            vnFace = (v1.Normal + v2.Normal + v3.Normal) / 3f;
            centerPoint = (v1.WorldCoordinates + v2.WorldCoordinates + v3.WorldCoordinates) / 3f;
            ndotl = Vector3D.ComputeNDotL(centerPoint, vnFace, lightPosition);
        }
        public Color GetColor(Color color, int x, int y)
        {
            return Color.FromArgb((int)(color.R * ndotl), (int)(color.G*ndotl), (int)(color.B * ndotl));
        }
    }

    public struct GouraudShadingModel : IShadingModel
    {
        Vertex v1, v2, v3;
        double area;
        double i1, i2, i3;

        public void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Vector3D lightPosition)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            area = Vector3D.CalculateTriangleArea(v1.Coordinates, v2.Coordinates, v3.Coordinates);

            i1 = Vector3D.ComputeNDotL(v1.WorldCoordinates, v1.Normal, lightPosition);
            i2 = Vector3D.ComputeNDotL(v2.WorldCoordinates, v2.Normal, lightPosition);
            i3 = Vector3D.ComputeNDotL(v3.WorldCoordinates, v3.Normal, lightPosition);
        }

        public Color GetColor(Color color, int x, int y)
        {
            double i = InterpolateI(area, x, y);
            return Color.FromArgb((int)(color.R * i), (int)(color.G * i), (int)(color.B * i));
        }

        private double InterpolateI(double area, int x, int y)
        {
            double a1 = Vector3D.CalculateTriangleArea(new Vector3D(x, y, 1), v1.Coordinates, v2.Coordinates);
            double a2 = Vector3D.CalculateTriangleArea(new Vector3D(x, y, 1), v3.Coordinates, v2.Coordinates);
            double a3 = Vector3D.CalculateTriangleArea(new Vector3D(x, y, 1), v3.Coordinates, v1.Coordinates);

            double alfa = a1 / area;
            double beta = a2 / area;
            double gamma = a3 / area;

            return Math.Max(0, Math.Min(1, (alfa * i3) + (beta * i1) + (gamma * i2)));
        }

        
    }

    

}
