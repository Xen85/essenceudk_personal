using System.Collections.ObjectModel;
using EssenceUDK.PluginBase.ViewModels.DockableModels;

namespace EssenceUDK.PluginBase.Models
{
    public class DockingManagerModel
    {
        public ObservableCollection<ViewModelDockableBase> Documents;

        public ObservableCollection<ViewModelDockableBase> Tools;
    }
}