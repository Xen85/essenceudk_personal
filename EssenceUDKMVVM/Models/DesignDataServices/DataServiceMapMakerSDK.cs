using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EssenceUDK.MapMaker.MapMaking;

namespace EssenceUDKMVVM.Models.DesignDataServices
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
