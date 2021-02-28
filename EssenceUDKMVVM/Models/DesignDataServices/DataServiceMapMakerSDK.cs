using System;
using EssenceUDK.MapMaker;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DataServiceMapMakerSdkStatic : IDataServiceMapMakerSdk
    {
        public void GetData(Action<MapSdk, Exception> callback)
        {
            const string file = @"E:\Ultima\Utility SRC\essenceudk_personal\EssenceUDKMVVM\Configurazioni\TM.xml";
            var sdk = new MapSdk();
            sdk.LoadFromXML(file);
            callback(sdk, null);
        }
    }
    
    public class DataServiceMapMakerSdk : IDataServiceMapMakerSdk
    {
        public void GetData(Action<MapSdk, Exception> callback)
        {
          
            callback(null, null);
        }
    }
}
