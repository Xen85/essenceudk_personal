#region

using System.Collections.ObjectModel;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.Models;
using GalaSoft.MvvmLight.Ioc;

#endregion

namespace EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Color.Coasts
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    ///     this view model is related to the default list of the color
    /// </summary>
    public class DefaultItemListViewModel : TileContainerViewModel
    {
        private EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor _color;

        /// <summary>
        ///     Initializes a new instance of the DefaultItemListViewModel class.
        /// </summary>
        public DefaultItemListViewModel()
        {
        }

        [PreferredConstructor]
        public DefaultItemListViewModel(IServiceModelAreaColor service)
            : this()
        {
            service.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }

                    Area = (EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor) item;
                });
        }

        public EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor Area
        {
            get { return _color; }
            set
            {
                _color = value;
                List = new ObservableCollection<int> {_color.Coasts.Coast.Texture};
                RaisePropertyChanged(() => Area);
            }
        }
    }

}