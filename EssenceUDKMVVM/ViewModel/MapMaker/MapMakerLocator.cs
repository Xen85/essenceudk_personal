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
using EssenceUDKMVVM.ViewModel.MapMaker.Color.AreaColor;
using EssenceUDKMVVM.ViewModel.MapMaker.Color.Cliff;
using EssenceUDKMVVM.ViewModel.MapMaker.Color.Coasts;
using EssenceUDKMVVM.ViewModel.MapMaker.Textures;
using EssenceUDKMVVM.ViewModel.MapMaker.Textures.AreaTexture;
using EssenceUDKMVVM.ViewModel.MapMaker.Textures.ItemTransition;
using EssenceUDKMVVM.ViewModel.MapMaker.Textures.TextureTransition;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;

namespace EssenceUDKMVVM.ViewModel.MapMaker
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
            SimpleIoc.Default.Register<MapMakerLocator>();
            SimpleIoc.Default.Register<MapMakerSdkViewModel>();
            SimpleIoc.Default.Register<AreaColorsViewModel>();
            SimpleIoc.Default.Register<AreaTextureViewModel>();
            SimpleIoc.Default.Register<TexturesTransitionListViewModel>();
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
                SimpleIoc.Default.Register<IServiceModelTexture, AreaTextureDesignDataServiceStatic>();
                SimpleIoc.Default.Register<IServiceModelAreaColor, AreaColorDesignDataServiceStatic>();
                SimpleIoc.Default.Register<IAreaItemTransDataService, ItemTransDataServiceStatic>();
                SimpleIoc.Default.Register<IDataServiceMapMakerSdk, DataServiceMapMakerSdkStatic>();
                _viewModelSdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>();
            }
            else
            {
                SimpleIoc.Default.Register<IServiceModelTexture, AreaTextureDesignDataService>();
                SimpleIoc.Default.Register<IServiceModelAreaColor, AreaColorDesignDataService>();
                SimpleIoc.Default.Register<IAreaTransitionTextureDataService, AreaTransitionTextureDataServiceStatic>();
                SimpleIoc.Default.Register<IDataServiceMapMakerSdk, DataServiceMapMakerSdk>();
                _viewModelSdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>();
            }
        }


        #region Base Services

        public MapMakerSdkViewModel SdkViewModel
        {
            get => _viewModelSdk;
            set => _viewModelSdk = value;
        }


        public AreaColorsViewModel AreaColors => ServiceLocator.Current.GetInstance<AreaColorsViewModel>();

        public AreaTextureViewModel AreaTextures => ServiceLocator.Current.GetInstance<AreaTextureViewModel>();

        #endregion

        #region Texture Editor

        /// <summary>
        ///     Selected Item of Texture List
        /// </summary>
        public TextureViewModel SelectedTexturesViewModel => ServiceLocator.Current.GetInstance<TextureViewModel>();


        /// <summary>
        ///     Texture Tile Group Of Selected Texture
        /// </summary>
        public SelectedTextureList SelectedTextureListViewModel =>
            ServiceLocator.Current.GetInstance<SelectedTextureList>();


        /// <summary>
        ///     Item Transition List of Selected Texture
        /// </summary>
        public ItemTransitionViewModel ItemTransitionListViewModel =>
            ServiceLocator.Current.GetInstance<ItemTransitionViewModel>();


        /// <summary>
        ///     Selected Transition in Item Transition List
        /// </summary>
        public ItemTransitionTextureViewModel SelectedItemTransitionList =>
            ServiceLocator.Current.GetInstance<ItemTransitionTextureViewModel>();


        /// <summary>
        ///     This is the list of The transition available
        /// </summary>
        public TexturesTransitionListViewModel TextureTransitionListViewModel =>
            ServiceLocator.Current.GetInstance<TexturesTransitionListViewModel>();


        /// <summary>
        ///     this is the selected item of texture transition list
        /// </summary>
        public TextureTransitionViewModel TextureTransitionViewModel =>
            ServiceLocator.Current.GetInstance<TextureTransitionViewModel>();

        #endregion


        #region Color

        /// <summary>
        ///     this is the view model about the color area selected in the list
        /// </summary>
        public AreaColorViewModel SelectedAreaColor => ServiceLocator.Current.GetInstance<AreaColorViewModel>();

        /// <summary>
        ///     this is the view model about list of default item in coasts
        /// </summary>
        public DefaultItemListViewModel DefaultItemListCoast =>
            ServiceLocator.Current.GetInstance<DefaultItemListViewModel>();


        /// <summary>
        ///     this is the land transition of coasts
        /// </summary>
        public CoastLandViewModel CoastLandTransitionViewModel =>
            ServiceLocator.Current.GetInstance<CoastLandViewModel>();

        /// <summary>
        ///     this is the item transition of coasts
        /// </summary>
        public CoastItemsViewModel CoastItemTransitionViewModel =>
            ServiceLocator.Current.GetInstance<CoastItemsViewModel>();

        /// <summary>
        ///     this is the view model of the coast options
        /// </summary>
        public CoastsOptionsViewModel CoastOptionsViewModel =>
            ServiceLocator.Current.GetInstance<CoastsOptionsViewModel>();

        /// <summary>
        ///     this is the view model for the cliff collection
        /// </summary>
        public CliffListViewModel CliffListViewModel => ServiceLocator.Current.GetInstance<CliffListViewModel>();


        public CliffViewModel CliffViewModel => ServiceLocator.Current.GetInstance<CliffViewModel>();

        #endregion
    }
}