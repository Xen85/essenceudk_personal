#region

using System.Collections.ObjectModel;
using EssenceUDK.PluginBase.Models;
using EssenceUDK.PluginBase.Models.Menu;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

#endregion

namespace EssenceUDK.PluginBase.ViewModels.Options
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        private ObservableCollection<SubMenuModel> _collection;

        /// <summary>
        ///     Initializes a new instance of the MenuViewModel class.
        /// </summary>
        public MenuViewModel()
        {
        }

        [PreferredConstructor]
        public MenuViewModel(IMenuDataservice dataService)
        {
            dataService.GetData((item, error) =>
            {
                if (error != null)
                    return;
                Collection = (ObservableCollection<SubMenuModel>)item;
            });
        }

        public ObservableCollection<SubMenuModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged(() => Collection);
            }
        }
    }

}