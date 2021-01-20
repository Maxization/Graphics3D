using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics3D.ShadingModels
{
    public static class ShadingModelFactory
    {
        public static IShadingModel Create(ShadingModelEnum type)
        {
            switch(type)
            {
                case ShadingModelEnum.Gouraud:
                    return new GouraudShadingModel();
                case ShadingModelEnum.Phong:
                    return new PhongShadingModel();
                case ShadingModelEnum.Flat:
                default:
                    return new FlatShadingModel();
            }
        }
    }
}
