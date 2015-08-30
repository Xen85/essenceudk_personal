using EssenceUDK.Platform;
using EssenceUDKMVVM.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.ObjectModel;

namespace EssenceUDKMVVM.ViewModel.Udk
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class UoDataManagerViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the UODataManagerViewModel class.
        /// </summary>
        public UoDataManagerViewModel()
        {
        }

        [PreferredConstructor]
        public UoDataManagerViewModel(IUoDataManagerDataService dataService)
        {
            dataService.GetData((item, error) =>
            {
                UoDataManager = (UODataManager)item;
            });
        }

        private UODataManager _dataManager;

        public UODataManager UoDataManager
        {
            get { return _dataManager; }
            set
            {
                _dataManager = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ModelItemData> Items => (ObservableCollection<ModelItemData>) _dataManager?.GetItemTile();

        public ObservableCollection<ModelLandData> Lands => (ObservableCollection<ModelLandData>) _dataManager?.GetLandTile();

        public ObservableCollection<ModelGumpSurf> Gumps => (ObservableCollection<ModelGumpSurf>) _dataManager?.GetGumpSurf();

        public override void Cleanup()
        {
            base.Cleanup();
            _dataManager.Dispose();
        }
    }
}