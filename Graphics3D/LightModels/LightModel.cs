using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D.LightModels
{
    public struct Light
    {
        public Vector3D position;
        public Vector3D target;
        public Color color;
        public bool isReflector;
    }

    public interface ILightModel
    {
        double Ka { get; }
        double GetColor(double Io, double Il, Vector3D l, Vector3D n, Vector3D v, Light light);
    }
    public class PhongLightModel : ILightModel
    {
        double ka;
        double kd;
        double ks;
        int m;

        public double Ka
        { 
            get
            {
                return ka;
            }
            set
            {
                ka = MathExtension.Clamp(value,0,1);
            }
        }
        public double Kd
        {
            get
            {
                return kd;
            }
            set
            {
                kd = MathExtension.Clamp(value, 0, 1);
            }
        }
        public double Ks
        {
            get
            {
                return ks;
            }
            set
            {
                ks = MathExtension.Clamp(value, 0, 1);
            }
        } 
        public int M
        {
            get
            {
                return m;
            }
            set
            {
                m = (int)MathExtension.Clamp(value, 1, 100);
            }
        }

        public PhongLightModel(double ka, double kd, double ks, int m)
        {
            Ka = ka;
            Kd = kd;
            Ks = ks;
            M = m;
        }
        public double GetColor(double Io, double Il, Vector3D l, Vector3D n, Vector3D v, Light light)
        {
            l.Normalize();
            n.Normalize();
            v.Normalize();
            Vector3D r = 2 * Vector3D.Dot(n, l) * n - l;

            if (light.isReflector)
            {
                Vector3D k = light.position - light.target;
                k.Normalize();

                Il = Il * Math.Pow(Vector3D.Dot(k, l), 5);
            }


            double Id = Kd * Io * Il;
            double Is = Ks * Io * Il * Math.Pow(Vector3D.Dot(v, r), m);

            return MathExtension.Clamp(Id + Is);
        }
    }
}
