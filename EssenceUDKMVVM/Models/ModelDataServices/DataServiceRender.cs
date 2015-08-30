using EssenceUDKMVVM.Models.Model;
using System;

namespace EssenceUDKMVVM.Models.ModelDataServices
{
    public class DataServiceRender : IDataServiceRender
    {
        public void GetData(Action<object, Exception> callback)
        {
            var item = new RenderModel { Width = 1598, Height = 1015, SeaLevel = 0, Flat = false, X = 2000, Y = 2000, Range = 27, Map = 0, MaxZ = 127, MinZ = -20 };
            callback(item, null);
        }
    }
}