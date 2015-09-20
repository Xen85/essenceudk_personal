#region

using EssenceUDK.MapMaker.Elements.Textures.TextureArea;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.Models;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Textures.AreaTexture
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class SelectedTextureList : TileContainerViewModel
    {
        private IDataService _service;

        /// <summary>
        ///     Initializes a new instance of the SelectedTextureList class.
        /// </summary>
        public SelectedTextureList()
        {
            var list = ServiceLocator.Current.GetInstance<AreaTextureViewModel>();
            list.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => list.SelectedAreaTextures) || e.PropertyName == null)
                    List = list.SelectedAreaTextures.List;
            };
        }

        [PreferredConstructor]
        public SelectedTextureList(IServiceModelTexture service)
            : this()
        {
            _service = service;
            service.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }

                    var selected = (AreaTextures) item;
                    if (selected != null) List = selected.List;
                });
        }
    }

}