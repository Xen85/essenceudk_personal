using EssenceUDKMVVM.Model_Interfaces.Model;
using System;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }
    }
}