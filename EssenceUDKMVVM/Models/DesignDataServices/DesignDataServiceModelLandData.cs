﻿using System;
using EssenceUDKMVVM.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DesignDataServiceModelLandDataStatic : IServiceModelLandData
    {
        public void GetData(Action<object, Exception> callback)
        {

            var item = ServiceLocator.Current.GetInstance<ViewModelLocator>().UODataManager.Lands[0];

            callback(item, null);

          
        }
    }
    public class DesignDataServiceModelLandData : IServiceModelLandData
    {
        public void GetData(Action<object, Exception> callback)
        {


            callback(null, null);

          
        }
    }
}
