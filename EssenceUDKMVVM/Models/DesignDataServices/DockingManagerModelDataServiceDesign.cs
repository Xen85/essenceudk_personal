#region

using System;
using System.Collections.ObjectModel;
using EssenceUDK.PluginBase.Models;
using EssenceUDK.PluginBase.ViewModels.DockableModels;
using EssenceUDKMVVM.ViewModel;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace EssenceUDKMVVM.Models.DesignDataServices
{

    public class DockingManagerModelDataServiceDesign : IDockingManagerModelDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            var locator = ServiceLocator.Current.GetInstance<ViewModelLocator>();
            var item = new DockingManagerModel
            {
                Documents = new ObservableCollection<ViewModelDockableBase>
                {
                    locator.MapRender,
                },

                Tools = new ObservableCollection<ViewModelDockableBase>
                {
                    locator.Option
                }
            };
            callback(item, null);
        }
    }

}