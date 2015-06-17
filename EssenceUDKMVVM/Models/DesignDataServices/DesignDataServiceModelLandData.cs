using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EssenceUDK.Platform;
using EssenceUDKMVVM.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DesignDataServiceModelLandData : IServiceModelLandData
    {
        public void GetData(Action<object, Exception> callback)
        {
            //var modelLandDatas = ServiceLocator.Current.GetInstance<ViewModelLandTiles>().List;
            //if (modelLandDatas != null)
            //{
            //    var model = modelLandDatas[0];

            //    callback(model, null);
            //}
            //else
            //{
            //    var collection = ViewModelLocator.UODataManager.GetLandTile() as ObservableCollection<ModelLandData>;
            //    callback(collection != null ? collection[0] : null, null);
            //}
        }
    }
}
