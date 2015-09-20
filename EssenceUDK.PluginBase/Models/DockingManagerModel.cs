using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace EssenceUDK.PluginBase.Models
{
    public class DockingManagerModel
    {
        public ObservableCollection<ViewModelBase> Documents;

        public ObservableCollection<ViewModelBase> Tools;
    }
}