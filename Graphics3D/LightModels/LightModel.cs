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
        public Color color;
    }
    class LightModel
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
        public double GetColor(double Io, double Il, Vector3D l, Vector3D n, Vector3D v)
        {
            l.Normalize();
            n.Normalize();
            v.Normalize();
            Vector3D r = 2 * Vector3D.Dot(n, l) * n - l;

            double Ia = Ka;
            double Id = Kd * Io * Il * Vector3D.Dot(n, l);
            double Is = Ks * Io * Il * Math.Pow(Vector3D.Dot(v, r), m);

            return MathExtension.Clamp(Ia + Id + Is);
        }

    }
}
