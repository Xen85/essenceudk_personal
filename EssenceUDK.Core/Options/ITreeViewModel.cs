using System.Collections.Generic;

namespace EssenceUDK.Core.Options
{

    public interface ITreeViewModel
    {
         IList<IOptionTreeInterface> List { get; }
         void Populate();
    }


}