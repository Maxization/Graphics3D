using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D.ShadingModels
{
    public interface IShadingModel
    {
        void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Vector3D lightPosition);
        void setOrder(int k);
        void ProcessScanLine(int y, Vertex va, Vertex vb, Vertex vc, Vertex vd, Color color, Device device);
    }

    public struct FlatShadingModel : IShadingModel
    {
        Vector3D vnFace;
        Vector3D centerPoint;
        double ndotl;
        public void setOrder(int k) { }
        public void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Vector3D lightPosition)
        {
            vnFace = (v1.Normal + v2.Normal + v3.Normal) / 3f;
            centerPoint = (v1.WorldCoordinates + v2.WorldCoordinates + v3.WorldCoordinates) / 3f;
            ndotl = Vector3D.ComputeNDotL(centerPoint, vnFace, lightPosition);
        }
        public void ProcessScanLine(int y, Vertex va, Vertex vb, Vertex vc, Vertex vd, Color color, Device device)
        {
            Vector3D pa = va.Coordinates;
            Vector3D pb = vb.Coordinates;
            Vector3D pc = vc.Coordinates;
            Vector3D pd = vd.Coordinates;

            var gradient1 = pa.Y != pb.Y ? (y - pa.Y) / (pb.Y - pa.Y) : 1;
            var gradient2 = pc.Y != pd.Y ? (y - pc.Y) / (pd.Y - pc.Y) : 1;

            int sx = (int)MathExtension.Interpolate(pa.X, pb.X, gradient1);
            int ex = (int)MathExtension.Interpolate(pc.X, pd.X, gradient2);

            double z1 = MathExtension.Interpolate(pa.Z, pb.Z, gradient1);
            double z2 = MathExtension.Interpolate(pc.Z, pd.Z, gradient2);

            for (var x = sx; x < ex; x++)
            {
                double gradient = (x - sx) / (double)(ex - sx);

                double z = MathExtension.Interpolate(z1, z2, gradient);
                z = Math.Log(1 * z + 1) / Math.Log(1 * 100 + 1) * z;

                Color col = Color.FromArgb((int)(color.R * ndotl), (int)(color.G * ndotl), (int)(color.B * ndotl));
                device.PutPixel(x, y, z, col);
            }
        }
    }

    public struct GouraudShadingModel : IShadingModel
    {
        double nl1, nl2, nl3;
        public double ndotla;
        public double ndotlb;
        public double ndotlc;
        public double ndotld;

        public void setOrder(int k)
        {
            switch (k)
            {
                case 1:
                    ndotla = nl1;
                    ndotlb = nl3;
                    ndotlc = nl1;
                    ndotld = nl2;
                    break;
                case 2:
                    ndotla = nl1;
                    ndotlb = nl3;
                    ndotlc = nl2;
                    ndotld = nl3;
                    break;
                case 3:
                    ndotla = nl1;
                    ndotlb = nl2;
                    ndotlc = nl1;
                    ndotld = nl3;
                    break;
                case 4:
                    ndotla = nl2;
                    ndotlb = nl3;
                    ndotlc = nl1;
                    ndotld = nl3;
                    break;
            }
        }
        public void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Vector3D lightPosition)
        {
            nl1 = Vector3D.ComputeNDotL(v1.WorldCoordinates, v1.Normal, lightPosition);
            nl2 = Vector3D.ComputeNDotL(v2.WorldCoordinates, v2.Normal, lightPosition);
            nl3 = Vector3D.ComputeNDotL(v3.WorldCoordinates, v3.Normal, lightPosition);
        }

        public void ProcessScanLine(int y, Vertex va, Vertex vb, Vertex vc, Vertex vd, Color color, Device device)
        {
            Vector3D pa = va.Coordinates;
            Vector3D pb = vb.Coordinates;
            Vector3D pc = vc.Coordinates;
            Vector3D pd = vd.Coordinates;

            var gradient1 = pa.Y != pb.Y ? (y - pa.Y) / (pb.Y - pa.Y) : 1;
            var gradient2 = pc.Y != pd.Y ? (y - pc.Y) / (pd.Y - pc.Y) : 1;

            int sx = (int)MathExtension.Interpolate(pa.X, pb.X, gradient1);
            int ex = (int)MathExtension.Interpolate(pc.X, pd.X, gradient2);

            var snl = MathExtension.Interpolate(ndotla, ndotlb, gradient1);
            var enl = MathExtension.Interpolate(ndotlc, ndotld, gradient2);

            double z1 = MathExtension.Interpolate(pa.Z, pb.Z, gradient1);
            double z2 = MathExtension.Interpolate(pc.Z, pd.Z, gradient2);

            for (var x = sx; x < ex; x++)
            {
                double gradient = (x - sx) / (double)(ex - sx);

                double z = MathExtension.Interpolate(z1, z2, gradient);
                var ndotl = MathExtension.Interpolate(snl, enl, gradient);

                z = Math.Log(1 * z + 1) / Math.Log(1 * 100 + 1) * z;
                Color col = Color.FromArgb((int)(color.R * ndotl), (int)(color.G * ndotl), (int)(color.B * ndotl));
                device.PutPixel(x, y, z, col);
            }
        }
    }

    public struct PhongShadingModel : IShadingModel
    {
        Vector3D lightPosition;
        public void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Vector3D lightPosition)
        {
            this.lightPosition = lightPosition;
        }

        public void ProcessScanLine(int y, Vertex va, Vertex vb, Vertex vc, Vertex vd, Color color, Device device)
        {
            Vector3D pa = va.Coordinates;
            Vector3D pb = vb.Coordinates;
            Vector3D pc = vc.Coordinates;
            Vector3D pd = vd.Coordinates;

            var gradient1 = pa.Y != pb.Y ? (y - pa.Y) / (pb.Y - pa.Y) : 1;
            var gradient2 = pc.Y != pd.Y ? (y - pc.Y) / (pd.Y - pc.Y) : 1;

            int sx = (int)MathExtension.Interpolate(pa.X, pb.X, gradient1);
            int ex = (int)MathExtension.Interpolate(pc.X, pd.X, gradient2);

            Vector3D worldCoord1 = MathExtension.InterpolateVector(va.WorldCoordinates, vb.WorldCoordinates, gradient1);
            Vector3D worldCoord2 = MathExtension.InterpolateVector(vc.WorldCoordinates, vd.WorldCoordinates, gradient2);

            Vector3D normal1 = MathExtension.InterpolateVector(va.Normal, vb.Normal, gradient1);
            Vector3D normal2 = MathExtension.InterpolateVector(vc.Normal, vd.Normal, gradient2);

            double z1 = MathExtension.Interpolate(pa.Z, pb.Z, gradient1);
            double z2 = MathExtension.Interpolate(pc.Z, pd.Z, gradient2);

            for (var x = sx; x < ex; x++)
            {
                double gradient = (x - sx) / (double)(ex - sx);

                double z = MathExtension.Interpolate(z1, z2, gradient);
                z = Math.Log(1 * z + 1) / Math.Log(1 * 100 + 1) * z;

                Vector3D worldCoord = MathExtension.InterpolateVector(worldCoord1, worldCoord2, gradient);
                Vector3D normal = MathExtension.InterpolateVector(normal1, normal2, gradient);

                double ndotl = Vector3D.ComputeNDotL(worldCoord, normal, lightPosition);

                Color col = Color.FromArgb((int)(color.R * ndotl), (int)(color.G * ndotl), (int)(color.B * ndotl));
                device.PutPixel(x, y, z, col);
            }
        }

        public void setOrder(int k)
        {
        }
    }
}
