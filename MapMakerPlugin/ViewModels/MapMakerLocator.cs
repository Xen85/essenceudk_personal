#region

using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.Models;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.Models.DesignData;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Color.AreaColor;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Color.Cliff;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Color.Coasts;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Textures;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Textures.AreaTexture;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Textures.ItemTransition;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Textures.TextureTransition;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels
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

        public AreaColorsViewModel AreaColors => ServiceLocator.Current.GetInstance<AreaColorsViewModel>();

        public AreaTextureViewModel AreaTextures => ServiceLocator.Current.GetInstance<AreaTextureViewModel>();

        #endregion Base Services

        #region Texture Editor

        /// <summary>
        ///     Selected Item of Texture List
        /// </summary>
        public TextureViewModel SelectedTexturesViewModel => ServiceLocator.Current.GetInstance<TextureViewModel>();

        /// <summary>
        ///     Texture Tile Group Of Selected Texture
        /// </summary>
        public SelectedTextureList SelectedTextureListViewModel
            => ServiceLocator.Current.GetInstance<SelectedTextureList>();

        /// <summary>
        ///     Item Transition List of Selected Texture
        /// </summary>
        public ItemTransitionViewModel ItemTransitionListViewModel
            => ServiceLocator.Current.GetInstance<ItemTransitionViewModel>();

        /// <summary>
        ///     Selected Transition in Item Transition List
        /// </summary>
        public ItemTransitionTextureViewModel SelectedItemTransitionList
            => ServiceLocator.Current.GetInstance<ItemTransitionTextureViewModel>();

        /// <summary>
        ///     This is the list of The transition available
        /// </summary>
        public TexturesTransitionListViewModel TextureTransitionListViewModel
            => ServiceLocator.Current.GetInstance<TexturesTransitionListViewModel>();

        /// <summary>
        ///     this is the selected item of texture transition list
        /// </summary>
        public TextureTransitionViewModel TextureTransitionViewModel
            => ServiceLocator.Current.GetInstance<TextureTransitionViewModel>();

        #endregion Texture Editor

        #region Color

        /// <summary>
        ///     this is the view model about the color area selected in the list
        /// </summary>
        public AreaColorViewModel SelectedAreaColor => ServiceLocator.Current.GetInstance<AreaColorViewModel>();

        /// <summary>
        ///     this is the view model about list of default item in coasts
        /// </summary>
        public DefaultItemListViewModel DefaultItemListCoast
            => ServiceLocator.Current.GetInstance<DefaultItemListViewModel>();

        /// <summary>
        ///     this is the land transition of coasts
        /// </summary>
        public CoastLandViewModel CoastLandTransitionViewModel
            => ServiceLocator.Current.GetInstance<CoastLandViewModel>();

        /// <summary>
        ///     this is the item transition of coasts
        /// </summary>
        public CoastItemsViewModel CoastItemTransitionViewModel
            => ServiceLocator.Current.GetInstance<CoastItemsViewModel>();

        /// <summary>
        ///     this is the view model of the coast options
        /// </summary>
        public CoastsOptionsViewModel CoastOptionsViewModel
            => ServiceLocator.Current.GetInstance<CoastsOptionsViewModel>();

        /// <summary>
        ///     this is the view model for the cliff collection
        /// </summary>
        public CliffListViewModel CliffListViewModel => ServiceLocator.Current.GetInstance<CliffListViewModel>();

        public CliffViewModel CliffViewModel => ServiceLocator.Current.GetInstance<CliffViewModel>();

        #endregion Color
    }

}