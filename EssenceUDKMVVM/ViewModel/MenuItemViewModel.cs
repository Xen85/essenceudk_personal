using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EssenceUDKMVVM.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MenuItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MenuItemViewModel class.
        /// </summary>
        public MenuItemViewModel()
        {
        }

        public ICommand Command { get; set; }

        public string Header { get; set; }

        public ObservableCollection<MenuItemViewModel> Models { get; set; }
    }
}