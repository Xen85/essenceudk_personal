using System.Collections.ObjectModel;
using EssenceUDKMVVM.Models.Model;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.ViewModel.Udk
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