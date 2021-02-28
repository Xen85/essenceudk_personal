using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorMountains;
using EssenceUDK.MapMaker.Elements.Textures.TexureCliff;
using EssenceUDKMVVM.ViewModel.MapMaker;
using EssenceUDKMVVM.ViewModel.MapMaker.Color.AreaColor;
using GalaSoft.MvvmLight.Ioc;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class AreaColorDesignDataService : IServiceModelAreaColor
    {
        public void GetData(Action<AreaColorViewModel, Exception> callback)
        {
            callback(null, null);
        }
    }

    public class AreaColorDesignDataServiceStatic : IServiceModelAreaColor
    {
        public void GetData(Action<AreaColorViewModel, Exception> callback)
        {

            var locator = SimpleIoc.Default.GetInstance<MapMakerLocator>();
            callback(locator?.SelectedAreaColor, null);
        }
    }
}