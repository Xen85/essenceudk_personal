using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;
using System;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class UoDataManagerDataService : IUoDataManagerDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            var uodataManager = UODataManager.GetInstance(new Uri(@"C:\Ultima\OSI_seas_mul"), UODataType.ClassicAdventuresOnHighSeas,
                         Language.English, null, false);

            callback(uodataManager, null);
        }
    }
}