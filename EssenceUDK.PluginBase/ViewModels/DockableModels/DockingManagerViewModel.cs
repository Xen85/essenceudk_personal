#region

using System.Collections.ObjectModel;
using EssenceUDK.PluginBase.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

#endregion

namespace EssenceUDK.PluginBase.ViewModels.DockableModels
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class DockingManagerViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelDockableBase> _docs;
        private ObservableCollection<ViewModelDockableBase> _tools;

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
                if (obj == null) return;
                Documents = obj.Documents;
                Tools = obj.Tools;
            });
        }

        public ObservableCollection<ViewModelDockableBase> Documents
        {
            get { return _docs; }
            set
            {
                _docs = value;
                RaisePropertyChanged(() => Documents);
            }
        }

        public ObservableCollection<ViewModelDockableBase> Tools
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