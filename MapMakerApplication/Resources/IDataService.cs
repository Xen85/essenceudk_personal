// **********
// EssenceUDK - IDataService.cs
// **********

using System;
using EssenceUDK.MapMaker;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;
using MapMakerApplication.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace MapMakerApplication.Resources
{
    public interface IDataService<T>
    {
        void GetData(Action<T, Exception> callback);
    }

    public interface IUoDataManagerService : IDataService<UODataManager>
    {
    }

    public interface ISdkDataService : IDataService<MapSdk>
    {
    }
    public interface ISdkDataServiceGenerated : IDataService<MapSdk>
    {
    }

    public class SdkDataServiceStatic : ISdkDataService
    {
        public void GetData(Action<MapSdk, Exception> callback)
        {
            var sdk = new MapSdk();
            sdk.LoadFromXML(@"E:\Ultima\Utility SRC\essenceudk_personal\MapMakerApplication\Configurazioni\TM.xml");
            callback(sdk, null);
        }
    }
    public class SdkDataServiceDynamic : ISdkDataService
    {
        public void GetData(Action<MapSdk, Exception> callback)
        {
            
            callback(new MapSdk(), null);
        }
    }

    public class UoDataManagerServiceStatic : IUoDataManagerService
    {
        public void GetData(Action<UODataManager, Exception> callback)
        {
            var dataManager = UODataManager.GetInstance(new Uri(@"E:\Ultima\OSI_seas_mul"),
                UODataType.ClassicAdventuresOnHighSeas,
                Language.English);
            callback(dataManager, null);
        }
    }

    public class SdkDataServiceGeneratedStatic : ISdkDataServiceGenerated
    {
        public void GetData(Action<MapSdk, Exception> callback)
        {
            var sdk = new MapSdk();
            sdk.LoadFromXML(@"E:\Ultima\Utility SRC\essenceudk_personal\MapMakerApplication\Configurazioni\TM.xml");
            callback(sdk, null);
        }
    }
    public class SdkDataServiceGeneratedDynamic : ISdkDataServiceGenerated
    {
        public void GetData(Action<MapSdk, Exception> callback)
        {
            var sdkViewModel = ServiceLocator.Current.GetInstance<SdkViewModel>();
            callback(sdkViewModel.MakeMapSdk, null);
        }
    }

    public class UoDataManagerServiceDynamic : IUoDataManagerService
    {
        public void GetData(Action<UODataManager, Exception> callback)
        {
            callback(null, null);
        }
    }
}