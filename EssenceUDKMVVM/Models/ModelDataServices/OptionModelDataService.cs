using System;
using System.IO;
using System.Xml.Serialization;
using EssenceUDK.Platform;
using EssenceUDKMVVM.Model_Interfaces.Model;
using EssenceUDKMVVM.Models.Model.Option;

namespace EssenceUDKMVVM.Models.ModelDataServices
{

    public class OptionModelDataService : IDataServiceOption
	{
		/// <summary>
		/// prendere i dati in design mode | design Mode data
		/// </summary>
		/// <param name="callback"></param>
	    public void GetData(Action<OptionModel, Exception> callback)
		{
		    var item = new OptionModel();
		    if (File.Exists("options.xml"))
		    {
		        var serializer = new XmlSerializer(typeof (OptionModel));
		        using (var file = new FileStream("options.xml", FileMode.Open))
		        {
		            item = (OptionModel) serializer.Deserialize(file);
		        }
		    }
		    else
		    {
		        item.ImageSize = 60;
		        item.DataType = ClassicClientVersion.PreAlfa;
		        item.Path = "Ok path settato";
		    }
            callback(item, null);
	    }
	}
    
    public class DataItemDataService : IDataServiceDataItem
    {
	    /// <summary>
	    /// prendere i dati in design mode | design Mode data
	    /// </summary>
	    /// <param name="callback"></param>
	    public void GetData(Action<DataItem, Exception> callback)
	    {
		    DataItem item = new DataItem("Titolo fake");
		    callback(item, null);
	    }
    }

}