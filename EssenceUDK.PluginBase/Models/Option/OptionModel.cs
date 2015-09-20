#region

using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

#endregion

namespace EssenceUDK.PluginBase.Models.Option
{

    [Serializable]
    public class OptionModel : ObservableObject
    {
        [NonSerialized] private ObservableCollection<string> _foundProfiles;
        private ObservableCollection<OptionTreeMenu> _optiontree;
        private string _profile;
        private string _profileDataFolder;
        private string _selectedProfile;

        #region option tree menu

        public ObservableCollection<OptionTreeMenu> OptionTreeMenu
        {
            get { return _optiontree; }
            set
            {
                _optiontree = value;
                RaisePropertyChanged(() => OptionTreeMenu);
            }
        }

        #endregion option tree menu

        #region Profile Options

        /// <summary>
        ///     profile string for user
        /// </summary>
        public string Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                RaisePropertyChanged(() => Profile);
            }
        }

        /// <summary>
        ///     profile data folder
        /// </summary>
        public string ProfileDataFolder
        {
            get { return _profileDataFolder; }
            set
            {
                _profileDataFolder = value;
                RaisePropertyChanged(() => ProfileDataFolder);
            }
        }

        public ObservableCollection<string> Profiles
        {
            get { return _foundProfiles; }
            set
            {
                _foundProfiles = value;
                RaisePropertyChanged(() => _foundProfiles);
            }
        }

        public string SelectedProfile
        {
            get { return _selectedProfile; }
            set
            {
                _selectedProfile = value;
                RaisePropertyChanged(() => SelectedProfile);
            }
        }

        #endregion Profile Options
    }

}