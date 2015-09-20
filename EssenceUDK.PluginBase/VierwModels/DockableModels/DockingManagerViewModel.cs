#region

using System.Collections.ObjectModel;

#endregion

namespace EssenceUDK.PluginBase.VierwModels.DockableModels
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
                var obj = item as DockingManagerModel;
                Documents = obj.Documents;
                Tools = obj.Tools;
            });
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