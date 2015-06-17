using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class UoDataManagerDataService : IUoDataManagerDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            var UodataManager = UODataManager.GetInstance(new Uri(@"C:\Ultima\OSI_seas_mul"), UODataType.ClassicAdventuresOnHighSeas,
                         Language.English, null, false);

            callback(UodataManager, null);
        }
    }
}
