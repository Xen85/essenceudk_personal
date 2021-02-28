using System;
using CommonServiceLocator;
using EssenceUDK.MapMaker.Elements.Textures.TextureArea;
using EssenceUDKMVVM.ViewModel.MapMaker;
using GalaSoft.MvvmLight.Ioc;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class AreaTextureDesignDataServiceStatic : IServiceModelTexture
    {
        public void GetData(Action<AreaTextures, Exception> callback)
        {
            var selected =  ServiceLocator.Current.GetInstance<MapMakerLocator>().AreaTextures.SelectedAreaTextures;

            callback(selected, null);
        }
    }

    public class AreaTextureDesignDataService : IServiceModelTexture
    {
        public void GetData(Action<AreaTextures, Exception> callback)
        {
            callback(null, null);
        }
    }
}