#region

using System;

#endregion

namespace EssenceUDK.PluginBase.Models
{

    public interface IDataService
    {
        void GetData(Action<object, Exception> callback);
    }

    public interface IDataServiceOption : IDataService
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