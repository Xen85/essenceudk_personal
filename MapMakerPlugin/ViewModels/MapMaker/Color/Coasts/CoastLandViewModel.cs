using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDKMVVM.Models;
using GalaSoft.MvvmLight.Ioc;

namespace MapMakerPlugin.ViewModels.MapMaker.Color.Coasts
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    ///     this view model is related to the land transition of the color
    /// </summary>
    public class CoastLandViewModel : TransitionViewModel
    {
        private EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor _color;
        private IDataService _service;

        /// <summary>
        ///     Initializes a new instance of the CoastLandViewModel1 class.
        /// </summary>
        public CoastLandViewModel()
        {
        }

        [PreferredConstructor]
        public CoastLandViewModel(IAreaItemTransDataService service)
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
                if (value != null)
                    Transition = value.Coasts.Ground;
                else
                    Transition = null;
                RaisePropertyChanged(() => Area);
            }
        }
    }

}