using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.ViewModel.Udk
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DockingManagerViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _docs;
        private ObservableCollection<ViewModelBase> _tools;
        /// <summary>
        /// Initializes a new instance of the DockingManagerViewModel class.
        /// </summary>
        public DockingManagerViewModel()
        {
        }

        public ObservableCollection<ViewModelBase> Documents
        {
            get { return _docs; }
            set
            {
                _docs = value;
                RaisePropertyChanged(() => Documents);
            }
        }


        public ObservableCollection<ViewModelBase> Tools
        {
            get { return _tools; }
            set
            {
                _tools = value;
                RaisePropertyChanged(() => Tools);
            }
        }
    }
}