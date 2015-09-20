#region

using System;
using EssenceUDK.PluginBase.Models.Option;

#endregion

namespace EssenceUDK.PluginBase.Models.DesignDataServices
{

    public class DataServiceOptionsDesign : IDataServiceOption
    {
        public void GetData(Action<object, Exception> callback)
        {
            var item = new OptionModel();
            callback(item, null);
        }
    }

}