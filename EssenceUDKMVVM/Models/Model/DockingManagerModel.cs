using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.Models.Model
{
    public class DockingManagerModel
    {
        public ObservableCollection<ViewModelBase> Documents;

        public ObservableCollection<ViewModelBase> Tools;
    }
}
