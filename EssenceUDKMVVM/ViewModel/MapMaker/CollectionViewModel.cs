using GalaSoft.MvvmLight;
using System.Collections;
using System.Windows.Input;

namespace EssenceUDKMVVM.ViewModel.MapMaker
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public abstract class CollectionViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the CollectionViewModel class.
        /// </summary>
        protected CollectionViewModel()
        {
            Remove = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(() => _list.Remove(_selectedItem), () => _selectedItem != null && _list != null);
        }

        private IList _list;

        private object _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public IList List
        {
            get { return _list; }
            set
            {
                _list = value;
                RaisePropertyChanged(() => List);
            }
        }

        public ICommand Remove { get; private set; }

        public abstract ICommand Add { get; set; }
    }
}