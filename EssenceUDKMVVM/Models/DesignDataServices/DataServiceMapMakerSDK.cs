using System;
using EssenceUDK.MapMaker;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DataServiceMapMakerSdkStatic : IDataServiceMapMakerSdk
    {
        public void GetData(Action<object, Exception> callback)
        {
            const string file = @"C:\Users\Xen\Desktop\map\TM.xml";
            var sdk = new MapSdk();
            sdk.LoadFromXML(file);
            callback(sdk, null);
        }
    }
    
    public class DataServiceMapMakerSdk : IDataServiceMapMakerSdk
    {
        public void GetData(Action<object, Exception> callback)
        {
          
            callback(null, null);
        }
    }
}
