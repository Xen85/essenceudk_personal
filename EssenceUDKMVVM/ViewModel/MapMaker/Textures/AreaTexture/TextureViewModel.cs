using System;
using System.ComponentModel;
using System.Linq;
using EssenceUDK.MapMaker.Elements.Textures.TextureArea;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.ViewModel.DockableModels;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
namespace EssenceUDKMVVM.ViewModel.MapMaker.Textures.AreaTexture
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TextureViewModel : ViewModelDockableBase, IDataErrorInfo
    {

        private string _error;
        private AreaTextures _selected;
        private IServiceModelTexture _service;
        /// <summary>
        /// Initializes a new instance of the TextureViewModel class.
        /// </summary>
        public TextureViewModel()
        {
            var list = ServiceLocator.Current.GetInstance<AreaTextureViewModel>();
            list.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => list.SelectedAreaTextures) || e.PropertyName == null)
                    Selected = list.SelectedAreaTextures;
            };
        }

        [PreferredConstructor]
        public TextureViewModel(IServiceModelTexture service)
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
                         if(item != null)
                         Selected = (AreaTextures)item;
                     });
        }

        public AreaTextures Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                RaisePropertyChanged(() => Selected);
            }
        }

        public int Index
        {
            get => Selected.Index;
            set
            {
                Selected.Index = value;
                RaisePropertyChanged(() => Index);

            }
        }

        public string Name
        {
            get => _selected.Name;
            set
            {
                _selected.Name = value; RaisePropertyChanged(() => Name);
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Index":
                        {
                            var sdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>();
                            if (sdk == null) return string.Empty;
                            var mapSdk = sdk.Sdk;
                            if (mapSdk != null &&
                                (mapSdk.TextureIds != null && (!mapSdk.TextureIds.Contains(Index))))
                                return string.Empty;
                            _error = "Index already used, please choose another one";
                            return _error;
                        }
                        break;

                    case "Name":
                        {
                            var sdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>();
                            if (sdk == null)
                            {
                                _error = "";
                                return string.Empty;
                            }
                            var mapSdk = sdk.Sdk;
                            if (mapSdk != null &&
                                (mapSdk.TextureIds != null && (mapSdk.TextureName.Contains(Name))))
                                _error = "This name has been already used for another texture, please choose another one";
                            else
                                _error = "";
                            return _error;
                        }
                        break;
                    default:
                        _error = String.Empty;
                        return String.Empty;

                }
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>
        /// An error message indicating what is wrong with this object. The default is an empty string ("").
        /// </returns>
        public string Error
        {
            get { return _error; }
        }
    }
}