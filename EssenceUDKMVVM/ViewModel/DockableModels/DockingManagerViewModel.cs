using System.Collections.ObjectModel;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.Model;
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
    public class DockingManagerViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _docs;
        private ObservableCollection<ViewModelBase> _tools;
        private ViewModelBase model;

        /// <summary>
        ///     Initializes a new instance of the DockingManagerViewModel class.
        /// </summary>
      
        public DockingManagerViewModel()
        {
        }

        [PreferredConstructor]
        public DockingManagerViewModel(IDockingManagerModelDataService dataservice)
        {
            dataservice.GetData((item, error) =>
            {
                if (error != null)
                    return;
                if (!(item is DockingManagerModel obj)) return;
                Documents = obj.Documents;
                Tools = obj.Tools;

            });
        }

        public ObservableCollection<ViewModelBase> Documents
        {
            get => _docs;
            set
            {
                _docs = value;
                RaisePropertyChanged(() => Documents);
            }
        }


        public ObservableCollection<ViewModelBase> Tools
        {
            get => _tools;
            set
            {
                _tools = value;
                RaisePropertyChanged(() => Tools);
            }
        }

        public ViewModelBase Model
        {
            get => model;
            set
            {
                model = value;
                RaisePropertyChanged(() => Tools);
            }
        }
    }
}