using EssenceUDKMVVM.Models.Model;
using EssenceUDKMVVM.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DockingManagerModelDataServiceDesign : IDockingManagerModelDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            var locator = ServiceLocator.Current.GetInstance<ViewModelLocator>();
            var item = new DockingManagerModel
            {
                Tools = new System.Collections.ObjectModel.ObservableCollection<GalaSoft.MvvmLight.ViewModelBase>
                {
                    locator.Land,
                    locator.MapRender,
                    locator.Option
                }
            };
            callback(item, null);
        }
    }
}