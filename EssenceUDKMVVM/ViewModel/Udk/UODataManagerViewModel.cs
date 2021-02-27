using System.Collections.ObjectModel;
using EssenceUDK.Platform;
using EssenceUDKMVVM.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace EssenceUDKMVVM.ViewModel.Udk
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class UODataManagerViewModel : ViewModelBase
    {
        private UODataManager _dataManager;

        /// <summary>
        ///     Initializes a new instance of the UODataManagerViewModel class.
        /// </summary>

        public UODataManagerViewModel()
        {
        }

        [PreferredConstructor]
        public UODataManagerViewModel(IUoDataManagerDataService dataService)
        {
            dataService.GetData((item, error) => { UODataManager = (UODataManager) item; });
        }


        public UODataManager UODataManager
        {
            get => _dataManager;
            set
            {
                _dataManager = value;
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<ModelItemData> Items
        {
            get
            {
                if (_dataManager == null) return null;
                return (ObservableCollection<ModelItemData>) _dataManager.GetItemTile();
            }
        }

        public ObservableCollection<ModelLandData> Lands
        {
            get
            {
                if (_dataManager == null) return null;
                return (ObservableCollection<ModelLandData>) _dataManager.GetLandTile();
            }
        }

        public ObservableCollection<ModelGumpSurf> Gumps
        {
            get
            {
                if (_dataManager == null) return null;
                return (ObservableCollection<ModelGumpSurf>) _dataManager.GetGumpSurf();
            }
        }


        public override void Cleanup()
        {
            base.Cleanup();
            _dataManager.Dispose();
        }
    }
}