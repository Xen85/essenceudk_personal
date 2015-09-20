#region

using System;
using System.IO;
using System.Xml.Serialization;
using EssenceUDK.PluginBase.Models.Option;

#endregion

namespace EssenceUDK.PluginBase.Models.ModelDataServices
{

    public class OptionModelDataService : IDataServiceOption
    {
        /// <summary>
        ///     prendere i dati in design mode | design Mode data
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
            callback(item, null);
        }
    }

}