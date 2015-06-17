using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using EssenceUDK.Model;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;
using EssenceUDK.Resources;
using GalaSoft.MvvmLight;

namespace EssenceUDK.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PreferencesViewModel : ViewModelBase
    {
        #region Declarations

        readonly UserProfile _userProfile;

        #endregion //Declarations

        #region Properties

        public UserProfile UserProfile
        {
            get { return _userProfile; }
        }

        public UODataTypeOptions ClientData
        {
            get { return _userProfile.ClientData; }
            set
            {
                _userProfile.ClientData = value;
                RaisePropertyChanged("ClientData");
            }
        }

        public String ClientFolder
        {
            get { return _userProfile.ClientFolder; }
            set
            {
                _userProfile.ClientFolder = value;
                RaisePropertyChanged("ClientFolder");
            }
        }

        public Language ClientLanguage
        {
            get { return _userProfile.ClientLanguage; }
            set
            {
                _userProfile.ClientLanguage = value;
                RaisePropertyChanged("ClientLanguage");
            }
        }

        public UODataTypeVersion ClientVersion
        {
            get { return _userProfile.ClientVersion; }
            set
            {
                _userProfile.ClientVersion = value;
                RaisePropertyChanged("ClientVersion");
            }
        }

        public Language InterfaceLanguage
        {
            get { return _userProfile.InterfaceLanguage; }
            set
            {
                _userProfile.InterfaceLanguage = value;

                RaisePropertyChanged("InterfaceLanguage");
            }
        }

        public String InterfaceTheme
        {
            get { return _userProfile.InterfaceTheme; }
            set
            {
                _userProfile.InterfaceTheme = value;
                ThemeManager.ApplyTheme(Application.Current, value);
                RaisePropertyChanged("InterfaceTheme");
            }
        }

        public String ServerAddress
        {
            get { return _userProfile.ServerAddress; }
            set
            {
                _userProfile.ServerAddress = value;
                RaisePropertyChanged("ServerAddress");
            }
        }

        public String ServerPassword
        {
            get { return _userProfile.ServerPassword; }
            set
            {
                _userProfile.ServerPassword = value;
                RaisePropertyChanged("ServerPassword");
            }
        }

        public Int32 ServerPort
        {
            get { return _userProfile.ServerPort; }
            set
            {
                _userProfile.ServerPort = value;
                RaisePropertyChanged("ServerPort");
            }
        }

        public String ServerUsername
        {
            get { return _userProfile.ServerUsername; }
            set
            {
                _userProfile.ServerUsername = value;
                RaisePropertyChanged("ServerUsername");
            }
        }

        public String UserProfileName
        {
            get { return _userProfile.UserProfileName; }
            set
            {
                _userProfile.UserProfileName = value;
                RaisePropertyChanged("UserProfileName");
            }
        }

        public string ClientInfo
        {
            get { return _userProfile.ClientInfo; }
            set
            {
                _userProfile.ClientInfo = value;
                _userProfile.ClientFolder = value;

                var clientinfo = Platform.UtilHelpers.ClientInfo.Get(value+"\\client.exe");
                if (clientinfo != null)
                {
                    _userProfile.ClientVersion = clientinfo.DetectDataVersion();
                    _userProfile.ClientData = clientinfo.DetectDataFeautures();
                }
                RaisePropertyChanged(() => ClientInfo);
                RaisePropertyChanged(()=>ClientVersion);
                RaisePropertyChanged(()=>ClientData);
                RaisePropertyChanged(() => ClientFolder);
            }
        }



        #endregion //Properties

        #region Static Lists

        public IEnumerable DataVersion { get { return Enum.GetValues(typeof(UODataTypeVersion)); } }

        public IEnumerable DataType { get { return Enum.GetValues(typeof(UODataTypeOptions)); } }

        public IList Themes { get { return ThemeManager.GetThemes(); } }

        public IList<Language> Languages { get { return new List<Language> { Language.English, Language.German, Language.Spanish, Language.Italian, Language.French, Language.Russian, Language.Japanese, Language.Korean, Language.Chinese }; } }

        public IList ClientsFound { get { return Platform.UtilHelpers.ClientInfo.GetDataPath(); } }

        #endregion //Static Lists

        #region Command Properties

        #endregion //Command Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PreferencesViewModel class.
        /// </summary>
        public PreferencesViewModel()
        {
            _userProfile = new UserProfile();
        }

        //TODO developers add your constructors here

        #endregion //Constructors

        #region Command Methods

        #endregion //Command Methods


    }
}