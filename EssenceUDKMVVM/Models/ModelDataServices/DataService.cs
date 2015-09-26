using System;
using EssenceUDK.PluginBase.Models;
using EssenceUDKMVVM.Model_Interfaces.Model;

namespace EssenceUDKMVVM.Models.ModelDataServices
{
    public class DataService : IDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
        }
    }
}