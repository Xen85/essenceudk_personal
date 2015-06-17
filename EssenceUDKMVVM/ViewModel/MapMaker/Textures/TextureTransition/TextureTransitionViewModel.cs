using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using EssenceUDKMVVM.Models;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM.ViewModel.MapMaker.Textures.TextureTransition
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TextureTransitionViewModel : TransitionViewModel
    {
        private IDataService _service;
        /// <summary>
        /// Initializes a new instance of the TextureTransitionViewModel class.
        /// </summary>
        public TextureTransitionViewModel()
        {
            var list = ServiceLocator.Current.GetInstance<TexturesTransitionListViewModel>();
            list.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => list.Selected) || e.PropertyName == null)
                {
                    Transition = list.Selected;
                }
            };

        }

        [PreferredConstructor]
        public TextureTransitionViewModel(IAreaTransitionTextureDataService service)
            :this()
        {
            _service = service;
            service.GetData(
                     (item, error) =>
                     {
                         if (error != null)
                         {
                             return;
                         }

                         Transition = (AreaTransitionTexture)item;
                     });
        }

        public string Name 
        { 
            get
            {
                return Transition == null ? null : ((AreaTransitionTexture) Transition).Name;
            }
            set
            {
                if (Transition != null) ((AreaTransitionTexture) Transition).Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int TextureTo {
            get
            {
                if (Transition != null) return ((AreaTransitionTexture) Transition).TextureIdTo;
                return -1;
            }
            set
            {
                if (Transition != null)
                {
                    ((AreaTransitionTexture) Transition).TextureIdTo = value;
                }
                else
                {
                    Transition = null;
                }
                RaisePropertyChanged(() => TextureTo);
            }
        }
    }
}