using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EssenceUDKMVVM.Models.Model;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    class DataServiceRenderDesign : IDataServiceRender
    {
        public void GetData(Action<object, Exception> callback)
        {
            var item = new RenderModel { Width = 1598, Height = 1015, SeaLevel = 0, Flat = false, X = 2000, Y = 2000, Range = 27, Map = 0, MaxZ = 127, MinZ = -20 };
            callback(item, null);
        }
    }
}
