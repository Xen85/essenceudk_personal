#region

using System;
using EssenceUDK.MapMaker;

#endregion

namespace EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.Models.DesignData
{

    public class DataServiceMapMakerSdk : IDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            const string file = @"C:\Users\Fabio\Desktop\map\TM.xml";
            var sdk = new MapSdk();
            sdk.LoadFromXML(file);
            callback(sdk, null);
        }
    }

}