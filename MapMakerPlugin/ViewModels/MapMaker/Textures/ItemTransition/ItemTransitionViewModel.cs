using System.Collections.ObjectModel;
using System.Windows.Input;
using EssenceUDK.MapMaker;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes;
using EssenceUDK.MapMaker.Elements.Items;
using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.ViewModel.DockableModels;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MapMakerPlugin.ViewModels.MapMaker.Textures.ItemTransition
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    ///     This view is about the list of itemtransitions
    /// </summary>
    public class ItemTransitionViewModel : ViewModelDockableBase
    {
        private AreaTransitionItem _cloned;
        private AreaTransitionItem _selected;
        private IDataService _service;
        private CollectionAreaTransitionItems _transList;

        /// <summary>
        ///     Initializes a new instance of the TexturesTransitionListViewModel class.
        /// </summary>
        public ItemTransitionViewModel()
        {
            Clone = new RelayCommand(() => { Cloned = (AreaTransitionItem) MapSdk.CloneSdkObject(_selected); },
                () => _selected != null);

            Paste = new RelayCommand(() =>
            {
                var collection = new ObservableCollection<CollectionLine>();
                foreach (var trans in Cloned.Lines)
                {
                    collection.Add((CollectionLine) MapSdk.CloneSdkObject(trans));
                }
                _selected.Lines = collection;
            }, () => _cloned != null);

            var list = ServiceLocator.Current.GetInstance<AreaTextureViewModel>();
            list.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => list.SelectedAreaTextures) || e.PropertyName == null)
                    TransitionList = list.SelectedAreaTextures.CollectionAreaItems;
            };
        }

        [PreferredConstructor]
        public ItemTransitionViewModel(IAreaItemTransDataService service)
            : this()
        {
            _service = service;
            service.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }

                    Selected = (AreaTransitionItem) item;
                });
        }

        /// <summary>
        ///     Initializes a new instance of the ItemTransition class.
        /// </summary>
        public ICommand Clone { get; private set; }

        public ICommand Paste { get; private set; }

        public AreaTransitionItem Selected
        {
            get { return _selected; }

            set
            {
                _selected = value;
                RaisePropertyChanged(() => Selected);
            }
        }

        public AreaTransitionItem Cloned
        {
            get { return _cloned; }

            set
            {
                _cloned = value;
                RaisePropertyChanged(() => Cloned);
            }
        }

        public CollectionAreaTransitionItems TransitionList
        {
            get { return _transList; }
            set
            {
                _transList = value;
                RaisePropertyChanged(() => TransitionList);
            }
        }

        public override void Cleanup()
        {
            _transList = null;
            _selected = null;
            _cloned = null;
        }
    }

}