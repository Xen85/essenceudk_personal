using EssenceUDKMVVM.Model_Interfaces.Model;
using EssenceUDKMVVM.Models;
using System;

namespace EssenceUDKMVVM.Model_Interfaces.ModelDataServices
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