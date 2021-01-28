using Graphics3D.LightModels;
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
        void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Light[] lights);
        void setOrder(int k);
        void ProcessScanLine(int y, Vertex va, Vertex vb, Vertex vc, Vertex vd, Color color, ILightModel lightModel, Vector3D cameraPosition, Device device);
    }

    public struct FlatShadingModel : IShadingModel
    {
        Vector3D vnFace;
        Vector3D centerPoint;
        double[] ndotl;
        Light[] lights;

        public void setOrder(int k) { }
        public void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Light[] lights)
        {
            vnFace = (v1.Normal + v2.Normal + v3.Normal) / 3f;
            vnFace.Normalize();


            centerPoint = (v1.WorldCoordinates + v2.WorldCoordinates + v3.WorldCoordinates) / 3f;

            this.lights = lights;
            ndotl = new double[lights.Length];
            for(int i=0;i<lights.Length;i++)
            {
                ndotl[i] = Vector3D.ComputeNDotL(centerPoint, vnFace, lights[i].position);
            } 
        }
        public void ProcessScanLine(int y, Vertex va, Vertex vb, Vertex vc, Vertex vd, Color color, ILightModel lightModel, Vector3D cameraPosition, Device device)
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

                double Ir = 0, Ig = 0, Ib = 0;
                
                for(int i=0;i<lights.Length;i++)
                {
                    double Ior = (ndotl[i] * color.R) / 255f;
                    double Iog = (ndotl[i] * color.G) / 255f;
                    double Iob = (ndotl[i] * color.B) / 255f;

                    double Ilr = lights[i].color.R / 255f;
                    double Ilg = lights[i].color.G / 255f;
                    double Ilb = lights[i].color.B / 255f;

                    Vector3D l = lights[i].position - centerPoint;
                    Vector3D n = vnFace;
                    Vector3D v = cameraPosition - centerPoint;

                    Ir += lightModel.GetColor(Ior, Ilr, l, n, v);
                    Ig += lightModel.GetColor(Iog, Ilg, l, n, v);
                    Ib += lightModel.GetColor(Iob, Ilb, l, n, v);
                }

                Ir += lightModel.Ka;
                Ig += lightModel.Ka;
                Ib += lightModel.Ka;
                
                Ir = MathExtension.Clamp(Ir);
                Ig = MathExtension.Clamp(Ig);
                Ib = MathExtension.Clamp(Ib);

                Color col = Color.FromArgb((int)(Ir * 255), (int)(Ig * 255), (int)(Ib * 255));

                double zlog = Math.Log(1 * z + 1) / Math.Log(1 * 100 + 1) * z;
                device.PutPixel(x, y, zlog, col);
            }
        }
    }

    public struct GouraudShadingModel : IShadingModel
    {
        double[] nl1, nl2, nl3;
        Vector3D vnFace;
        Vector3D centerPoint;
        public double[] ndotla;
        public double[] ndotlb;
        public double[] ndotlc;
        public double[] ndotld;
        Light[] lights;

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
        public void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Light[] lights)
        {
            this.lights = lights;
            nl1 = new double[lights.Length];
            nl2 = new double[lights.Length];
            nl3 = new double[lights.Length];

            vnFace = (v1.Normal + v2.Normal + v3.Normal) / 3f;
            centerPoint = (v1.WorldCoordinates + v2.WorldCoordinates + v3.WorldCoordinates) / 3f;

            for (int i=0;i<lights.Length;i++)
            {
                nl1[i] = Vector3D.ComputeNDotL(v1.WorldCoordinates, v1.Normal, lights[i].position);
                nl2[i] = Vector3D.ComputeNDotL(v2.WorldCoordinates, v2.Normal, lights[i].position);
                nl3[i] = Vector3D.ComputeNDotL(v3.WorldCoordinates, v3.Normal, lights[i].position);
            }
        }

        public void ProcessScanLine(int y, Vertex va, Vertex vb, Vertex vc, Vertex vd, Color color, ILightModel lightModel, Vector3D cameraPosition, Device device)
        {
            Vector3D pa = va.Coordinates;
            Vector3D pb = vb.Coordinates;
            Vector3D pc = vc.Coordinates;
            Vector3D pd = vd.Coordinates;

            var gradient1 = pa.Y != pb.Y ? (y - pa.Y) / (pb.Y - pa.Y) : 1;
            var gradient2 = pc.Y != pd.Y ? (y - pc.Y) / (pd.Y - pc.Y) : 1;

            int sx = (int)MathExtension.Interpolate(pa.X, pb.X, gradient1);
            int ex = (int)MathExtension.Interpolate(pc.X, pd.X, gradient2);

            double[] snl = new double[lights.Length];
            double[] enl = new double[lights.Length];

            for(int i=0;i<lights.Length;i++)
            {
                snl[i] = MathExtension.Interpolate(ndotla[i], ndotlb[i], gradient1);
                enl[i] = MathExtension.Interpolate(ndotlc[i], ndotld[i], gradient2);
            }

            double z1 = MathExtension.Interpolate(pa.Z, pb.Z, gradient1);
            double z2 = MathExtension.Interpolate(pc.Z, pd.Z, gradient2);

            for (var x = sx; x < ex; x++)
            {
                double gradient = (x - sx) / (double)(ex - sx);

                double z = MathExtension.Interpolate(z1, z2, gradient);

                

                double Ir = 0, Ig = 0, Ib = 0;
                for (int i = 0; i < lights.Length; i++)
                {
                    double ndotl = MathExtension.Interpolate(snl[i], enl[i], gradient);

                    double Ior = (ndotl * color.R) / 255f;
                    double Iog = (ndotl * color.G) / 255f;
                    double Iob = (ndotl * color.B) / 255f;

                    double Ilr = lights[i].color.R / 255f;
                    double Ilg = lights[i].color.G / 255f;
                    double Ilb = lights[i].color.B / 255f;

                    Vector3D l = lights[i].position - centerPoint;
                    Vector3D n = vnFace;
                    Vector3D v = cameraPosition - centerPoint;

                    Ir += lightModel.GetColor(Ior, Ilr, l, n, v);
                    Ig += lightModel.GetColor(Iog, Ilg, l, n, v);
                    Ib += lightModel.GetColor(Iob, Ilb, l, n, v);
                }

                Ir += lightModel.Ka;
                Ig += lightModel.Ka;
                Ib += lightModel.Ka;

                Ir = MathExtension.Clamp(Ir);
                Ig = MathExtension.Clamp(Ig);
                Ib = MathExtension.Clamp(Ib);

                Color col = Color.FromArgb((int)(Ir * 255), (int)(Ig * 255), (int)(Ib * 255));

                double zlog = Math.Log(1 * z + 1) / Math.Log(1 * 100 + 1) * z;
                device.PutPixel(x, y, zlog, col);
            }
        }
    }

    public struct PhongShadingModel : IShadingModel
    {
        Light[] lights;
        public void CalculateConst(Vertex v1, Vertex v2, Vertex v3, Light[] lights)
        {
            this.lights = lights;
        }

        public void ProcessScanLine(int y, Vertex va, Vertex vb, Vertex vc, Vertex vd, Color color, ILightModel lightModel, Vector3D cameraPosition, Device device)
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

                double Ir = 0, Ig = 0, Ib = 0;
                for (int i=0;i<lights.Length;i++)
                {
                    double ndotl = Vector3D.ComputeNDotL(worldCoord, normal, lights[i].position);

                    double Ior = (ndotl * color.R) / 255f;
                    double Iog = (ndotl * color.G) / 255f;
                    double Iob = (ndotl * color.B) / 255f;

                    double Ilr = lights[i].color.R / 255f;
                    double Ilg = lights[i].color.G / 255f;
                    double Ilb = lights[i].color.B / 255f;

                    Vector3D l = lights[i].position - worldCoord;
                    Vector3D n = normal;
                    Vector3D v = cameraPosition - worldCoord;

                    Ir += lightModel.GetColor(Ior, Ilr, l, n, v);
                    Ig += lightModel.GetColor(Iog, Ilg, l, n, v);
                    Ib += lightModel.GetColor(Iob, Ilb, l, n, v);
                }

                Ir += lightModel.Ka;
                Ig += lightModel.Ka;
                Ib += lightModel.Ka;

                Ir = MathExtension.Clamp(Ir);
                Ig = MathExtension.Clamp(Ig);
                Ib = MathExtension.Clamp(Ib);

                Color col = Color.FromArgb((int)(Ir*255), (int)(Ig * 255), (int)(Ib * 255));
                device.PutPixel(x, y, z, col);
            }
        }

        public void setOrder(int k)
        {
        }
    }
}
