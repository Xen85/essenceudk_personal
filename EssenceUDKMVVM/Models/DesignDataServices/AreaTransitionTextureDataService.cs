using System;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using EssenceUDKMVVM.ViewModel.MapMaker;
using GalaSoft.MvvmLight.Ioc;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class AreaTransitionTextureDataServiceStatic : IAreaTransitionTextureDataService
    {
        public void GetData(Action<AreaTransitionTexture, Exception> callback)
        {
            var locator = SimpleIoc.Default.GetInstance<MapMakerLocator>();

            var trans = locator?.AreaTextures?.SelectedAreaTextures?.AreaTransitionTexture?.List?[0];

            callback(trans, null);
        }
    }

    public class AreaTransitionTextureDataService : IAreaTransitionTextureDataService
    {
        public void GetData(Action<AreaTransitionTexture, Exception> callback)
        {
            callback(null, null);
        }
    }
}