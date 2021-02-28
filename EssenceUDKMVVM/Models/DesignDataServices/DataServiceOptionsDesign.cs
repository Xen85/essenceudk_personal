using System;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;
using EssenceUDKMVVM.Models.Model.Option;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DataServiceOptionsDesign : IDataServiceOption
    {
        public void GetData(Action<OptionModel, Exception> callback)
        {
            var item = new OptionModel()
            {
               
            };
            callback(item, null);
        }
    }
    
    public class DataServiceOptionsDesignStatic : IDataServiceOption
    {
        public void GetData(Action<OptionModel, Exception> callback)
        {
            var item = new OptionModel()
            {
               
            };
            callback(item, null);
        }
    }
}
