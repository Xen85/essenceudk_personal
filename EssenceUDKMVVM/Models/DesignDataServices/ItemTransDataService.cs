using System;
using EssenceUDK.MapMaker.Elements;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDKMVVM.ViewModel.MapMaker;
using GalaSoft.MvvmLight.Ioc;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    class ItemTransDataService : IAreaItemTransDataService
    {
        public void GetData(Action<AreaTransitionItem, Exception> callback)
        {
        
            callback(null, null);
        }
    }
    
    class ItemTransDataServiceStatic : IAreaItemTransDataService
    {
        public void GetData(Action<AreaTransitionItem, Exception> callback)
        {
            var selected = SimpleIoc.Default.GetInstance<MapMakerLocator>()?.AreaTextures?.SelectedAreaTextures?.CollectionAreaItems?.List?[0];
            
            callback(selected, null);
        }
    }
}
