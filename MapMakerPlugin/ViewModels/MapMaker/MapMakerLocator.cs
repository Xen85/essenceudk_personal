/*
  In App.xaml:
  <Application.Resources>
      <vm:MapMakerLocator xmlns:vm="clr-namespace:EssenceUDKMVVM.ViewModel.MapMaker"
                                   x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.DesignDataServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MapMakerPlugin.ViewModels.MapMaker.Color.AreaColor;
using MapMakerPlugin.ViewModels.MapMaker.Color.Cliff;
using MapMakerPlugin.ViewModels.MapMaker.Color.Coasts;
using MapMakerPlugin.ViewModels.MapMaker.Textures;
using MapMakerPlugin.ViewModels.MapMaker.Textures.AreaTexture;
using MapMakerPlugin.ViewModels.MapMaker.Textures.ItemTransition;
using MapMakerPlugin.ViewModels.MapMaker.Textures.TextureTransition;
using Microsoft.Practices.ServiceLocation;

namespace MapMakerPlugin.ViewModels.MapMaker
{

    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MapMakerLocator
    {
        private static MapMakerSdkViewModel _viewModelSdk;

        static MapMakerLocator()
        {
            SimpleIoc.Default.Register<IServiceModelAreaColor, AreaColorDesignDataService>();
            SimpleIoc.Default.Register<IServiceModelTexture, AreaTextureDesignDataService>();
            SimpleIoc.Default.Register<MapMakerSdkViewModel>();
            SimpleIoc.Default.Register<AreaColorsViewModel>();
            SimpleIoc.Default.Register<AreaTextureViewModel>();
            SimpleIoc.Default.Register<TexturesTransitionListViewModel>();
            SimpleIoc.Default.Register<IAreaTransitionTextureDataService, AreaTransitionTextureDataService>();
            SimpleIoc.Default.Register<IAreaItemTransDataService, ItemTransDataService>();
            SimpleIoc.Default.Register<ItemTransitionViewModel>();
            SimpleIoc.Default.Register<AreaColorViewModel>();
            SimpleIoc.Default.Register<TextureViewModel>();
            SimpleIoc.Default.Register<SelectedTextureList>();
            SimpleIoc.Default.Register<ItemTransitionTextureViewModel>();
            SimpleIoc.Default.Register<TextureTransitionViewModel>();
            SimpleIoc.Default.Register<CoastsOptionsViewModel>();
            SimpleIoc.Default.Register<CoastItemsViewModel>();
            SimpleIoc.Default.Register<CoastLandViewModel>();
            SimpleIoc.Default.Register<DefaultItemListViewModel>();
            SimpleIoc.Default.Register<CliffListViewModel>();
            SimpleIoc.Default.Register<CliffViewModel>();
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<DataServiceMapMakerSdk>();
                _viewModelSdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>();
            }
            else
            {
                SimpleIoc.Default.Register<DataServiceMapMakerSdk>();
                _viewModelSdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>();
            }
        }

        #region Base Services

        public MapMakerSdkViewModel SdkViewModel
        {
            get { return _viewModelSdk; }
            set { _viewModelSdk = value; }
        }

        public AreaColorsViewModel AreaColors
        {
            get { return ServiceLocator.Current.GetInstance<AreaColorsViewModel>(); }
        }

        public AreaTextureViewModel AreaTextures
        {
            get { return ServiceLocator.Current.GetInstance<AreaTextureViewModel>(); }
        }

        #endregion Base Services

        #region Texture Editor

        /// <summary>
        ///     Selected Item of Texture List
        /// </summary>
        public TextureViewModel SelectedTexturesViewModel
        {
            get { return ServiceLocator.Current.GetInstance<TextureViewModel>(); }
        }

        /// <summary>
        ///     Texture Tile Group Of Selected Texture
        /// </summary>
        public SelectedTextureList SelectedTextureListViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SelectedTextureList>(); }
        }

        /// <summary>
        ///     Item Transition List of Selected Texture
        /// </summary>
        public ItemTransitionViewModel ItemTransitionListViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ItemTransitionViewModel>(); }
        }

        /// <summary>
        ///     Selected Transition in Item Transition List
        /// </summary>
        public ItemTransitionTextureViewModel SelectedItemTransitionList
        {
            get { return ServiceLocator.Current.GetInstance<ItemTransitionTextureViewModel>(); }
        }

        /// <summary>
        ///     This is the list of The transition available
        /// </summary>
        public TexturesTransitionListViewModel TextureTransitionListViewModel
        {
            get { return ServiceLocator.Current.GetInstance<TexturesTransitionListViewModel>(); }
        }

        /// <summary>
        ///     this is the selected item of texture transition list
        /// </summary>
        public TextureTransitionViewModel TextureTransitionViewModel
        {
            get { return ServiceLocator.Current.GetInstance<TextureTransitionViewModel>(); }
        }

        #endregion Texture Editor

        #region Color

        /// <summary>
        ///     this is the view model about the color area selected in the list
        /// </summary>
        public AreaColorViewModel SelectedAreaColor
        {
            get { return ServiceLocator.Current.GetInstance<AreaColorViewModel>(); }
        }

        /// <summary>
        ///     this is the view model about list of default item in coasts
        /// </summary>
        public DefaultItemListViewModel DefaultItemListCoast
        {
            get { return ServiceLocator.Current.GetInstance<DefaultItemListViewModel>(); }
        }

        /// <summary>
        ///     this is the land transition of coasts
        /// </summary>
        public CoastLandViewModel CoastLandTransitionViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CoastLandViewModel>(); }
        }

        /// <summary>
        ///     this is the item transition of coasts
        /// </summary>
        public CoastItemsViewModel CoastItemTransitionViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CoastItemsViewModel>(); }
        }

        /// <summary>
        ///     this is the view model of the coast options
        /// </summary>
        public CoastsOptionsViewModel CoastOptionsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CoastsOptionsViewModel>(); }
        }

        /// <summary>
        ///     this is the view model for the cliff collection
        /// </summary>
        public CliffListViewModel CliffListViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CliffListViewModel>(); }
        }

        public CliffViewModel CliffViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CliffViewModel>(); }
        }

        #endregion Color
    }

}