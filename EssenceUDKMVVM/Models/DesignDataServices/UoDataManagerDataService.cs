using System;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class UoDataManagerDataService : IUoDataManagerDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            var uodataManager = UODataManager.GetInstance(new Uri(@"C:\Ultima\Client\Ultima Online 2D Client"), UODataType.ClassicAdventuresOnHighSeas,
                         Language.English, null, false);

            callback(uodataManager, null);
        }
    }
}
