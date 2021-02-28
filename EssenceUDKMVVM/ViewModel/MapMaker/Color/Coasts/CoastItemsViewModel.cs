using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDKMVVM.Models;
using GalaSoft.MvvmLight.Ioc;

namespace EssenceUDKMVVM.ViewModel.MapMaker.Color.Coasts
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// this viewModel is related to the Coast Item Transition of the color
    /// </summary>
    public class CoastItemsViewModel : TransitionViewModel
    {
        private EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor _color;
        private IAreaItemTransDataService _service;
        /// <summary>
        /// Initializes a new instance of the CoastItemsViewModel class.
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
                         if (item == null) return;
                         Transition = (AreaTransitionItem)item;
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