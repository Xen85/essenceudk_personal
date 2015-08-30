using System;

namespace MapMakerPlugin.Models.DesignData
{
    public class DataServiceMapMakerSdk : IDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            const string file = @"C:\Users\Fabio\Desktop\map\TM.xml";
            var sdk = new EssenceUDK.MapMaker.MapSdk();
            sdk.LoadFromXML(file);
            callback(sdk, null);
        }
    }
}