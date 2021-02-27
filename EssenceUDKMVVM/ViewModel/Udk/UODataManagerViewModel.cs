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
        private UODataManager m_DataManager;

        /// <summary>
        ///     Initializes a new instance of the UODataManagerViewModel class.
        /// </summary>

        public UODataManagerViewModel()
        {
        }

        [PreferredConstructor]
        public UODataManagerViewModel(IUoDataManagerDataService dataService)
        {
            dataService.GetData((item, error) => { UoDataManager = (UODataManager) item; });
        }


        public UODataManager UoDataManager
        {
            get => m_DataManager;
            set
            {
                m_DataManager = value;
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<ModelItemData> Items
        {
            get
            {
                if (m_DataManager == null) return null;
                return (ObservableCollection<ModelItemData>) m_DataManager.GetItemTile();
            }
        }

        public ObservableCollection<ModelLandData> Lands => (ObservableCollection<ModelLandData>) m_DataManager?.GetLandTile();

        public ObservableCollection<ModelGumpSurf> Gumps => (ObservableCollection<ModelGumpSurf>) m_DataManager?.GetGumpSurf();


        public override void Cleanup()
        {
            base.Cleanup();
            m_DataManager.Dispose();
        }
    }
}