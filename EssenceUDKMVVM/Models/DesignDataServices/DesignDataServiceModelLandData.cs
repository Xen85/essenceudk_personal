using EssenceUDKMVVM.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DesignDataServiceModelLandData : IServiceModelLandData
    {
        public void GetData(Action<object, Exception> callback)
        {
            var item = ServiceLocator.Current.GetInstance<ViewModelLocator>().UoDataManager.Lands[0];

            callback(item, null);
        }
    }
}