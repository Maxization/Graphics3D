using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D.Scene
{
    public class Fog
    {
        public bool isFog { get; set; }
        public double FogValue { get; set; }

        private double fogMinValue = 10;
        private double fogMaxValue;
        bool add;

        public Fog(bool isFog, double value)
        {
            this.isFog = isFog;
            this.FogValue = value;
            fogMaxValue = value;
        }

        public void ChangeFog()
        {
            if(add && FogValue != fogMinValue)
            {
                FogValue /= 2;

                if (FogValue < fogMinValue)
                    FogValue = fogMinValue;
            }

            if(!add && FogValue != fogMaxValue)
            {
                FogValue *= 2;

                if (FogValue >= fogMaxValue)
                {
                    FogValue = fogMaxValue;
                    isFog = false;
                }
                    
            }
        }

        public void ChangeState(bool state)
        {
            if (state)
            {
                add = true;
                isFog = true;
            }
            else
                add = false;
        }

    }
}
