using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using GalaSoft.MvvmLight.Ioc;
using IAreaItemTransDataService = MapMakerPlugin.Models.IAreaItemTransDataService;
using IDataService = MapMakerPlugin.Models.IDataService;

namespace MapMakerPlugin.ViewModels.Color.Coasts
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    ///     this viewModel is related to the Coast Item Transition of the color
    /// </summary>
    public class CoastItemsViewModel : TransitionViewModel
    {
        private EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor _color;
        private IDataService _service;

        /// <summary>
        ///     Initializes a new instance of the CoastItemsViewModel class.
        /// </summary>
        public CoastItemsViewModel()
        {
        }

        [PreferredConstructor]
        public CoastItemsViewModel(IAreaItemTransDataService service)
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

                    Transition = (AreaTransitionItem) item;
                });
        }

        public EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor Area
        {
            get { return _color; }
            set
            {
                _color = value;
                Transition = value != null ? value.Coasts.Coast : null;
                RaisePropertyChanged(() => Area);
            }
        }
    }

}