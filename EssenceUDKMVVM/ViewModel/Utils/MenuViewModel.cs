using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.Model.Menu;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.ObjectModel;

namespace EssenceUDKMVVM.ViewModel.Utils
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MenuViewModel class.
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

        private ObservableCollection<SubMenuModel> _collection;

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