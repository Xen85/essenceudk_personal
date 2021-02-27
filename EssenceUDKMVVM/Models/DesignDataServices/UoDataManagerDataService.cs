using System;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class UoDataManagerDataServiceStatic : IUoDataManagerDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            UODataManager uodataManager = null;

            try
            {
                uodataManager = UODataManager.GetInstance(new Uri(@"E:\Ultima\The Miracle"),
                    UODataType.ClassicMondainsLegacy,
                    Language.English, null, false);
                callback(uodataManager, null);
            }
            catch (Exception e)
            {
                callback(uodataManager, e);
            }
        }
    }

    public class UoDataManagerDataService : IUoDataManagerDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            callback(null, null);
        }
    }
}