using EssenceUDKMVVM.Models.Model.Menu;
using System;
using System.Collections.ObjectModel;
using EssenceUDK.PluginBase.Models;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DesignMenuDataService : IMenuDataservice
    {
        public void GetData(Action<object, Exception> callback)
        {
            var item = new ObservableCollection<SubMenuModel>() { new SubMenuModel
            {
                Header = "_Options" ,
                SubModels= new ObservableCollection<SubMenuModel>() {new SubMenuModel(){Header = "Options"}}
            }, new SubMenuModel()
            {
                Header = "test"
            } };
            callback(item, null);
        }
    }
}