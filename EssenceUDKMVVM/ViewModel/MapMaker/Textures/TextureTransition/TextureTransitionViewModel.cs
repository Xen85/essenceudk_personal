using System.Collections.ObjectModel;
using CommonServiceLocator;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using EssenceUDKMVVM.Models;
using GalaSoft.MvvmLight.Ioc;

namespace EssenceUDKMVVM.ViewModel.MapMaker.Textures.TextureTransition
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class TextureTransitionViewModel : TransitionViewModel
    {
        private IAreaTransitionTextureDataService _service;

        /// <summary>
        ///     Initializes a new instance of the TextureTransitionViewModel class.
        /// </summary>
        public TextureTransitionViewModel()
        {
       
        }

        [PreferredConstructor]
        public TextureTransitionViewModel(IAreaTransitionTextureDataService service)
            : this()
        {
            _service = service;
            service.GetData(
                (item, error) =>
                {
                    if (error != null) return;
                    if (item == null) return;
                    Transition = item;
                });
        }

        public string Name
        {
            get => (Transition as AreaTransitionTexture)?.Name;
            set
            {
                if (Transition != null) ((AreaTransitionTexture) Transition).Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
        
        

        public int TextureTo
        {
            get
            {
                if (Transition != null) return ((AreaTransitionTexture) Transition).TextureIdTo;
                return -1;
            }
            set
            {
                if (Transition != null)
                    ((AreaTransitionTexture) Transition).TextureIdTo = value;
                else
                    Transition = null;
                RaisePropertyChanged(() => TextureTo);
            }
        }
    }
}