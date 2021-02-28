using System.Collections.Generic;
using System.Windows.Input;
using EssenceUDK.MapMaker;
using EssenceUDK.MapMaker.MapMaking;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using MapMakerApplication.Messages;
using MapMakerApplication.Resources;

namespace MapMakerApplication.ViewModel
{
    public class MapMakerViewModel : ViewModelBase
    {
        #region Command Methods

        private bool CanGenerateMap()
        {
            var tmp = !string.IsNullOrEmpty(LocationBitmapMap) &&
                      !string.IsNullOrEmpty(LocationBitmapZ) &&
                      !string.IsNullOrEmpty(OutputFolder) &&
                      this.Sdk.CollectionColorArea.List.Count > 0;
            if (!tmp) return false;
            if (Editor)
                tmp = ApplicationController.manager != null;

            return tmp;
        }

        #endregion //Command Methods

        #region Message Handler

        private void MessageHandler(MessageDialogResult result)
        {
            switch (result.Type)
            {
                case DialogType.SelectBitmapZ:
                {
                    LocationBitmapZ = result.Content;
                }
                    break;
                case DialogType.SelectBitmapMap:
                {
                    LocationBitmapMap = result.Content;
                }
                    break;

                case DialogType.OpenOptionOutputFolder:
                {
                    OutputFolder = result.Content;
                }
                    break;
            }
        }

        #endregion //Message Handler

        #region Fields

        private int _selectedIndex;
        public MapSdk Sdk { get; private set; }

        private bool _editor;

        #endregion //Fields

        #region Props

        public List<string> Names => Globals.names;

        public string LocationBitmapZ
        {
            get => this.Sdk.BitmapLocationMapZ ?? "";
            set
            {
                this.Sdk.BitmapLocationMapZ = value;
                RaisePropertyChanged(() => LocationBitmapZ);
            }
        }

        public string LocationBitmapMap
        {
            get => this.Sdk.BitmapLocationMap ?? "";
            set
            {
                this.Sdk.BitmapLocationMap = value;
                RaisePropertyChanged(() => LocationBitmapMap);
            }
        }

        public string OutputFolder
        {
            get => ApplicationController.OutputFolder;
            set
            {
                ApplicationController.OutputFolder = value;
                RaisePropertyChanged(() => OutputFolder);
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged(() => SelectedIndex);
            }
        }

        public bool Editor
        {
            get => _editor;
            set
            {
                _editor = value;
                RaisePropertyChanged(() => Editor);
            }
        }

        #endregion //Props

        #region Command Proprerties

        public ICommand CommandSelectOutputFolder { get; }

        public ICommand CommandSelectBitmapMap { get; }

        public ICommand CommandSelectBitmapZ { get; }

        public ICommand CommandGenerateMap { get; }

        public ICommand CommandEtractAltitude { get; }

        #endregion //Command Properties

        #region Ctor

        public MapMakerViewModel()
        {
            AppMessages.DialogAnwer.Register(this, MessageHandler);

            CommandSelectBitmapMap = new RelayCommand(() =>
                AppMessages.DialogRequest.Send(new MessageDialogRequest("SelectBitmapMap")));

            CommandSelectBitmapZ = new RelayCommand(() =>
                AppMessages.DialogRequest.Send(new MessageDialogRequest("SelectBitmapMapZ")));

            CommandSelectOutputFolder = new RelayCommand(() =>
                AppMessages.DialogRequest.Send(new MessageDialogRequest("OpenFolderOutput")));

            CommandGenerateMap =
                new RelayCommand(
                    () => AppMessages.MapGeneratorMessage.Send(new MapMakeMessage
                        {Index = _selectedIndex, Edit = Editor}),
                    CanGenerateMap);

            CommandEtractAltitude = new RelayCommand(
                () => AppMessages.MapAltitudeExtractor.Send(new MapAltitudeExport {Index = _selectedIndex}),
                () => !string.IsNullOrEmpty(OutputFolder) && ApplicationController.manager != null);
        }

        public MapMakerViewModel(MapSdk sdk)
        {
            Sdk = sdk;
        }


        [PreferredConstructor]
        public MapMakerViewModel(ISdkDataServiceGenerated dataservice)
            : this()
        {
            dataservice.GetData(
                (item, error) =>
                {
                    if (error != null) return;

                    if (item != null) Sdk = item;
                });
        }

        #endregion //Ctor
    }
}