using System.Collections.Generic;

namespace EssenceUDK.Core.Options
{

    public interface IOptionTreeInterface
    {
        string Header { get; }
        List<IOptionTreeInterface> List { get; }
        
    }

    public interface IOptionTreeInterface<out T> : IOptionTreeInterface
    {
        T ViewModel { get; }
    }


}