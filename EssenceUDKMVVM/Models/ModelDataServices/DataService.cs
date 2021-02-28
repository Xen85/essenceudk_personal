using System;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Model_Interfaces.Model;

namespace EssenceUDKMVVM.Model_Interfaces.ModelDataServices
	{
	public class DataService : IDataServiceDataItem
		{
	    public void GetData(Action<DataItem, Exception> callback)
	    {
            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);
	    }
		}
	}