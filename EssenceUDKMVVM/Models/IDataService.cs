using System;

namespace EssenceUDKMVVM.Models
{
    public interface IDataService
    {
        void GetData(Action<object, Exception> callback);
    }

    public interface IDataServiceOption : IDataService
    {
    }

    public interface IDataServiceRender : IDataService
    {
    }

    public interface IServiceModelLandData : IDataService
    {
    }

    public interface IServiceModelAreaColor : IDataService
    {
    }

    public interface IServiceModelTransition : IDataService
    {
    }

    public interface IServiceModelTexture : IDataService
    {
    }

    public interface IAreaTransitionTextureDataService : IDataService
    {
    }

    public interface IAreaItemTransDataService : IDataService
    {
    }

    public interface IUoDataManagerDataService : IDataService
    {
    }

    public interface IMenuDataservice : IDataService
    {
    }

    public interface IDockingManagerModelDataService : IDataService
    {
    }

    public interface IOptionMenuItem : IDataService
    {
    }
}