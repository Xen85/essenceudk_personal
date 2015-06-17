using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using EssenceUDK.MapMaker;
using EssenceUDK.MapMaker.MapMaking;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MapMakerApplication.Messages;

namespace MapMakerApplication.ViewModel
{
	public class MapMakerViewModel : ViewModelBase
	{
		#region Fields

		private int _selectedIndex;
		private readonly MapSdk _sdk;

		private bool _editor;

		#endregion //Fields

		#region Props

		public List<string> Names { get { return Globals.names; } }

		public string LocationBitmapZ { get { return _sdk.BitmapLocationMapZ??""; } set { _sdk.BitmapLocationMapZ = value; RaisePropertyChanged(()=>LocationBitmapZ); } }

		public string LocationBitmapMap { get { return _sdk.BitmapLocationMap ?? ""; } set { _sdk.BitmapLocationMap = value; RaisePropertyChanged(() => LocationBitmapMap); } }

		public string OutputFolder { get { return ApplicationController.OutputFolder; } set { ApplicationController.OutputFolder = value;RaisePropertyChanged(()=>OutputFolder); } }

		public int SelectedIndex
		{
			get { return _selectedIndex; }
			set
			{
				_selectedIndex = value;
				RaisePropertyChanged(() => SelectedIndex);
			}
		}

		public bool Editor
		{
			get { return _editor; }
			set
			{
				_editor = value;
				RaisePropertyChanged(() => Editor);
			}
		}

		#endregion //Props

		#region Command Proprerties

		public ICommand CommandSelectOutputFolder { get; private set; }

		public ICommand CommandSelectBitmapMap { get; private set; }

		public ICommand CommandSelectBitmapZ { get; private set; }

		public ICommand CommandGenerateMap { get; private set; }

		public ICommand CommandEtractAltitude { get; private set; }



		#endregion //Command Properties

		#region Ctor
		public MapMakerViewModel(MapSdk sdk)
		{
			_sdk = sdk;
			AppMessages.DialogAnwer.Register(this, MessageHandler);

			CommandSelectBitmapMap = new RelayCommand(()=> AppMessages.DialogRequest.Send(new MessageDialogRequest("SelectBitmapMap")));

			CommandSelectBitmapZ = new RelayCommand(() => AppMessages.DialogRequest.Send(new MessageDialogRequest("SelectBitmapMapZ")));

			CommandSelectOutputFolder = new RelayCommand(()=>AppMessages.DialogRequest.Send(new MessageDialogRequest("OpenFolderOutput")));

			CommandGenerateMap =
				new RelayCommand(
					() => AppMessages.MapGeneratorMessage.Send(new MapMakeMessage() {Index = _selectedIndex, Edit = Editor}),
					CanGenerateMap);

			CommandEtractAltitude = new RelayCommand(()=>AppMessages.MapAltitudeExtractor.Send(new MapAltitudeExport(){Index =_selectedIndex}),
				()=>!string.IsNullOrEmpty(OutputFolder) && ApplicationController.manager!=null);
			


		}
		#endregion //Ctor

		#region Command Methods

		private bool CanGenerateMap()
		{
			var tmp = !string.IsNullOrEmpty(LocationBitmapMap) &&
					            !string.IsNullOrEmpty(LocationBitmapZ) &&
					            !string.IsNullOrEmpty(OutputFolder) &&
					            _sdk.CollectionColorArea.List.Count > 0;
			if (!tmp) return false;
			if (Editor)
				tmp = (ApplicationController.manager!=null);

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


	}
}
