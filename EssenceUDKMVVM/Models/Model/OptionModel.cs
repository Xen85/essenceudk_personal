using System;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.Models.Model
{
    [Serializable]
    public class OptionModel : ObservableObject
    {
        private string _path;
        private double _imageSize = 80;
        private ClassicClientVersion _dataType;
        private Language _language;
        private bool _realTime;



        //numero di fields = ' 4 '
        #region UoDataManager
        public String Path { get { return _path; } set { _path = value; RaisePropertyChanged(() => (Path)); } }

        public ClassicClientVersion DataType { get { return _dataType; } set { _dataType = value; RaisePropertyChanged(() => (DataType)); } }

        public Language Language { get { return _language; } set { _language = value; RaisePropertyChanged(() => (Language)); } }

        public Boolean RealTime { get { return _realTime; } set { _realTime = value; RaisePropertyChanged(() => (RealTime)); } }

        #endregion


        #region Layout
        /// <summary>
        /// const for Grid Size
        /// </summary>
        private const double GridConst = 5;

        /// <summary>
        /// Const for image size
        /// </summary>
        private const double ImageConst = 15;

        /// <summary>
        /// Size of the Image
        /// </summary>
        public double ImageSize
        {
            get { return _imageSize; }
            set
            {
                _imageSize = value;
                RaisePropertyChanged(() => ImageSize);
                RaisePropertyChanged(() => GridSize);
                RaisePropertyChanged(() => TileImage);
            }
        }


        /// <summary>
        /// Size Of the Grid
        /// </summary>
        public double GridSize
        {
            get { return _imageSize + GridConst; }
        }

        /// <summary>
        /// Size of the tile
        /// </summary>
        public double TileImage { get { return _imageSize + ImageConst; } }

        #endregion

        #region Profile Options
        
        /// <summary>
        /// profile string for user
        /// </summary>
        private string _profile;

        public string Profile
        {
            get
            {
                return _profile;
            }
            set
            {
                _profile = value;
                RaisePropertyChanged(() => Profile);
            }
        }

        /// <summary>
        /// profile data folder
        /// </summary>
        private string _profileDataFolder;

        public string ProfileDataFolder
        {
            get { return _profileDataFolder; }
            set
            {
                _profileDataFolder = value;
                RaisePropertyChanged(() => ProfileDataFolder);
            }
        }

        #endregion


    }

}
