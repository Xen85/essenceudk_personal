using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.ViewModel.DockableModels;
using GalaSoft.MvvmLight.Ioc;

namespace MapMakerPlugin.ViewModels.MapMaker.Color.Coasts
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    ///     This is the view model which manages Option info about color coast
    /// </summary>
    public class CoastsOptionsViewModel : ViewModelDockableBase
    {
        private EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor _color;

        /// <summary>
        ///     Initializes a new instance of the CoastsViewModel class.
        /// </summary>
        public CoastsOptionsViewModel()
        {
        }

        [PreferredConstructor]
        public CoastsOptionsViewModel(IServiceModelAreaColor service)
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
                RaisePropertyChanged(() => Area);
            }
        }
    }

}