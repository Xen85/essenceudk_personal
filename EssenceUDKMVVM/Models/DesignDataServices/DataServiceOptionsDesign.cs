using EssenceUDKMVVM.Models.Model.Option;
using System;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DataServiceOptionsDesign : IDataServiceOption
    {
        public void GetData(Action<object, Exception> callback)
        {
            var item = new OptionModel()
            {
            };
            callback(item, null);
        }
    }
}