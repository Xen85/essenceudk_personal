using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDKMVVM.Models;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM.ViewModel.MapMaker.Textures.ItemTransition
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ItemTransitionTextureViewModel : TransitionViewModel
    {
        private IDataService _service;
        /// <summary>
        /// Initializes a new instance of the ItemTransitionTextureViewModel class.
        /// </summary>
        public ItemTransitionTextureViewModel()
        {
            var list = ServiceLocator.Current.GetInstance<ItemTransitionViewModel>();
            list.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => list.Selected) || e.PropertyName == null)
                    Transition = list.Selected;
            };
        }

         [PreferredConstructor]
        public ItemTransitionTextureViewModel(IAreaItemTransDataService service)
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

        public string Name {get { if (Transition != null) return ((AreaTransitionItem) Transition).Name;
            return null;
        }
            set
        {
            ((AreaTransitionItem) Transition).Name = value;
            RaisePropertyChanged(() => Name);
        } }

        public int TextureTo { get { if (Transition != null) return ((AreaTransitionItem)Transition).TextureIdTo;
            return -1;
        }
            set
        {
            ((AreaTransitionItem) Transition).TextureIdTo = value;
            RaisePropertyChanged(() => TextureTo);
        } }
    }
}