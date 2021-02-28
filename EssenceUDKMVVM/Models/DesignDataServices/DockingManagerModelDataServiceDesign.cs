using System;
using System.Collections.ObjectModel;
using CommonServiceLocator;
using EssenceUDKMVVM.Models.Model;
using EssenceUDKMVVM.ViewModel;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DockingManagerModelDataServiceDesign : IDockingManagerModelDataService
    {
        public void GetData(Action<DockingManagerModel, Exception> callback)
        {
            var locator = ServiceLocator.Current.GetInstance<ViewModelLocator>();
            var item = new DockingManagerModel
            {
                Tools = new ObservableCollection<ViewModelBase>
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
