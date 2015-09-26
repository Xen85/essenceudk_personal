using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace EssenceUDKMVVM.Models.Model
{
    public class DockingManagerModel
    {
        public ObservableCollection<ViewModelBase> Documents;

        public ObservableCollection<ViewModelBase> Tools;
    }
}