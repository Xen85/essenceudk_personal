using System;
using CommonServiceLocator;
using EssenceUDK.Platform;
using EssenceUDKMVVM.ViewModel;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DesignDataServiceModelLandDataStatic : IServiceModelLandData
    {
        public void GetData(Action<ModelLandData, Exception> callback)
        {

            var item = ServiceLocator.Current.GetInstance<ViewModelLocator>().UODataManager.Lands[0];

            callback(item, null);

          
        }
    }
    public class DesignDataServiceModelLandData : IServiceModelLandData
    {
        public void GetData(Action<ModelLandData, Exception> callback)
        {


            callback(null, null);

          
        }
    }
}
