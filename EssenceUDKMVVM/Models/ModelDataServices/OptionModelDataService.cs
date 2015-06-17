using System;
using System.IO;
using System.Xml.Serialization;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;
using EssenceUDKMVVM.Models.Model;
using EssenceUDKMVVM.Model_Interfaces;

namespace EssenceUDKMVVM.Models.ModelDataServices
{

    public class OptionModelDataService : IDataServiceOption
	{
		/// <summary>
		/// prendere i dati in design mode | design Mode data
		/// </summary>
		/// <param name="callback"></param>
	    public void GetData(Action<object, Exception> callback)
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

}