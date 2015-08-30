using System.Collections.ObjectModel;
using System.Windows.Input;
using EssenceUDK.MapMaker;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes;
using EssenceUDK.MapMaker.Elements.Textures;
using EssenceUDK.MapMaker.Elements.Textures.TextureArea;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.ViewModel.DockableModels;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MapMakerPlugin.ViewModels.MapMaker.Textures.TextureTransition
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class TexturesTransitionListViewModel : ViewModelDockableBase
    {
        private AreaTransitionTexture _cloned;
        private AreaTransitionTexture _selected;
        private IAreaTransitionTextureDataService _service;
        private CollectionAreaTransitionTexture _transList;

        /// <summary>
        ///     Initializes a new instance of the TexturesTransitionListViewModel class.
        /// </summary>
        public TexturesTransitionListViewModel()
        {
            Clone = new RelayCommand(() => { Cloned = (AreaTransitionTexture) MapSdk.CloneSdkObject(_selected); },
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

            Remove = new RelayCommand(() => { TransitionList.List.Remove(Selected); },
                () => Selected != null && TransitionList.List != null && TransitionList.List.Count > 0);

            Add = new RelayCommand(() =>
            {
                if (TransitionList.List == null)
                    TransitionList.List = new ObservableCollection<AreaTransitionTexture>();
                TransitionList.List.Add(new AreaTransitionTexture());
            });

            var list = ServiceLocator.Current.GetInstance<AreaTextureViewModel>();
            list.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => list.SelectedAreaTextures) || e.PropertyName == null)
                {
                    TransitionList = list.SelectedAreaTextures == null
                        ? null
                        : list.SelectedAreaTextures.AreaTransitionTexture;
                }
            };
        }

        [PreferredConstructor]
        public TexturesTransitionListViewModel(IAreaTransitionTextureDataService service)
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

                    Selected = (AreaTransitionTexture) item;
                });
        }

        public ICommand Clone { get; private set; }
        public ICommand Paste { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Add { get; private set; }

        public AreaTransitionTexture Selected
        {
            get { return _selected; }

            set
            {
                _selected = value;
                RaisePropertyChanged(() => Selected);
            }
        }

        public AreaTransitionTexture Cloned
        {
            get { return _cloned; }

            set
            {
                _cloned = value;
                RaisePropertyChanged(() => Cloned);
            }
        }

        public CollectionAreaTransitionTexture TransitionList
        {
            get { return _transList; }
            set
            {
                _transList = value;

                RaisePropertyChanged(() => TransitionList);
            }
        }

        public AreaTextures Other
        {
            get
            {
                var viewModelSdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>();

                return viewModelSdk.Sdk.CollectionAreaTexture.FindByIndex(Selected.TextureIdTo);
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