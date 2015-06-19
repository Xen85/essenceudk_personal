using System;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;
using EssenceUDKMVVM.Models.Model;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DataServiceOptionsDesign : IDataServiceOption
    {
        public void GetData(Action<object, Exception> callback)
        {
            var item = new OptionModel()
            {
                DataType = ClassicClientVersion.ClientSAorHS,
                Language = Language.English,
                ImageSize = 50.0,
                Path = @"C:\Ultima\Client\Ultima Online 2D Client",
                RealTime = false
            };
            callback(item, null);
        }
    }
}
