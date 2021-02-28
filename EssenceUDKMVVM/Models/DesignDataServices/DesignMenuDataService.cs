using System;
using System.Collections.ObjectModel;
using EssenceUDKMVVM.Models.Model;
using EssenceUDKMVVM.Models.Model.Menu;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DesignMenuDataService : IMenuDataservice
    {
        public void GetData(Action<ObservableCollection<SubMenuModel>, Exception> callback)
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
