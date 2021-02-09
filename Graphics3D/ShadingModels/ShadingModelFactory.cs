using Graphics3D.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D.ShadingModels
{
    public static class ShadingModelFactory
    {
        public static IShadingModel Create(ShadingModelEnum type, Fog fog)
        {
            switch(type)
            {
                case ShadingModelEnum.Gouraud:
                    var sG =  new GouraudShadingModel();
                    sG.Fog = fog;
                    return sG;
                case ShadingModelEnum.Phong:
                    var sP = new PhongShadingModel();
                    sP.Fog = fog;
                    return sP;
                case ShadingModelEnum.Flat:
                default:
                    var sF = new FlatShadingModel();
                    sF.Fog = fog;
                    return sF;
            }
        }
    }
}
