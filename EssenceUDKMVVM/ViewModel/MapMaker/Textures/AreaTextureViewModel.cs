using System.Windows.Input;
using EssenceUDK.MapMaker;
using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDK.MapMaker.Elements.Textures;
using EssenceUDK.MapMaker.Elements.Textures.TextureArea;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.ViewModel.DockableModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM.ViewModel.MapMaker.Textures
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AreaTextureViewModel : ViewModelDockableBase
    {
        private readonly IServiceModelTexture _service;
        private AreaTextures _clone;
        private AreaTextures _selectedAreaTextures;

        private AreaTextureViewModel()
        {
            CopyTexture = new RelayCommand(() =>
            {
              
                _clone = (AreaTextures) MapSdk.CloneSdkObject(SelectedAreaTextures);
            },
                () => SelectedAreaTextures != null
                );

            AddTexture = new RelayCommand(() =>
            {
                Textures.List.Add(new AreaTextures());
                RaisePropertyChanged(() => Textures);
            }, () => Textures != null);

            RemoveTexture = new RelayCommand(() =>
            {
                Textures.List.Remove(SelectedAreaTextures);
                SelectedAreaTextures = null;
            }, ()=> SelectedAreaTextures != null);


            AddTransitionTexture = new RelayCommand(() =>
            {
                SelectedAreaTextures.AreaTransitionTexture.List.Add(new AreaTransitionTexture());

            }, ()=>SelectedAreaTextures != null);

            
            AddTransitionItem = new RelayCommand(() =>
            {
                SelectedAreaTextures.CollectionAreaItems.List.Add(new AreaTransitionItem() );

            }, ()=>SelectedAreaTextures != null);

          

            var viewModelSdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>();
            viewModelSdk.PropertyChanged += (sender, eventarg) => { RaisePropertyChanged(); };

        }

        [PreferredConstructor]
        public AreaTextureViewModel(IServiceModelTexture service)
            :this()
        {
            _service = service;
            service.GetData(
                     (item, error) =>
                     {
                         if (error != null)
                         {
                             return;
                         }

                         SelectedAreaTextures = (AreaTextures)item;
                     });
        }

        


        public CollectionAreaTexture Textures
        {
            get
            {
                return
                    ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>()
                        .Sdk.CollectionAreaTexture;
            }
        }



        public AreaTextures SelectedAreaTextures
        {
            get { return _selectedAreaTextures; }
            set
            {
                _selectedAreaTextures = value;
                RaisePropertyChanged(() => SelectedAreaTextures);
                

            }
        }

  

        public AreaTextures Clone
        {
            get { return _clone; }
            set
            {
                _clone = value;
                RaisePropertyChanged(() => _clone);
            }
        }


        #region Commands

        public ICommand CopyTexture { get; private set; }

        public ICommand PasteTextureIndex { get; private set; }

        public ICommand PasteTextureTransitionI { get; private set; }

        public ICommand CopyTextureTransition { get; private set; }

        public ICommand AddTexture { get; private set; }

        public ICommand RemoveTexture { get; private set; }

        public ICommand AddTransitionTexture { get; private set; }

        public ICommand RemoveTransitionTexture { get; private set; }

        public ICommand AddTransitionItem { get; private set; }

        public ICommand RemoveTransitionItem { get; private set; }

        public ICommand CopyTransitionItem { get; private set; }

        #endregion

    }
}