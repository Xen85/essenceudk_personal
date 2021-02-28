using System;
using System.Collections.ObjectModel;
using EssenceUDK.MapMaker;
using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDK.MapMaker.Elements.Textures.TextureArea;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using EssenceUDK.Platform;
using EssenceUDKMVVM.Model_Interfaces.Model;
using EssenceUDKMVVM.Models.Model;
using EssenceUDKMVVM.Models.Model.Menu;
using EssenceUDKMVVM.Models.Model.Option;
using EssenceUDKMVVM.ViewModel.MapMaker.Color.AreaColor;

namespace EssenceUDKMVVM.Models
{
    public interface IDataService<T>
    {
        void GetData(Action<T, Exception> callback);
    }
    
    
    public interface IDataServiceDataItem : IDataService<DataItem>
    {
    }


    public interface IDataServiceOption : IDataService<OptionModel>
    {
    }

    public interface IDataServiceRender : IDataService<RenderModel>
    {
    }

    public interface IServiceModelLandData : IDataService<ModelLandData>
    {
    }

    public interface IServiceModelAreaColor : IDataService<AreaColorViewModel>
    {
    }

    public interface IServiceModelTransition : IDataService<object>
    {
    }

    public interface IServiceModelTexture : IDataService<AreaTextures>
    {
    }

    public interface IAreaTransitionTextureDataService : IDataService<AreaTransitionTexture>
    {
    }

    public interface IDataServiceMapMakerSdk : IDataService<MapSdk>
    {
    }

    public interface IAreaItemTransDataService : IDataService<AreaTransitionItem>
    {
    }

    public interface IUoDataManagerDataService : IDataService<UODataManager>
    {
    }

    public interface IMenuDataservice : IDataService<ObservableCollection<SubMenuModel>>
    {
    }

    public interface IDockingManagerModelDataService : IDataService<DockingManagerModel>
    {
    }

    public interface IOptionMenuItem : IDataService<OptionTreeMenu>
    {
    }
}