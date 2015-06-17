using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
using System.Xml.Serialization;
using EssenceUDK.Platform;
using EssenceUDK.Platform.UtilHelpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MapMakerApplication.Messages;

namespace MapMakerApplication.ViewModel
{
	[Serializable()]
	public class ViewModelOptionWindow : ViewModelBase
	{
		#region Fields

		private int _selectedInformationsIndex;
		private string _selectedFolder;
		private int _selectedDataTypeIndex;
		private UODataType _data = UODataType.UseOldDatas;

		#endregion //Fields

		#region Props

		public int SelectedInformationIndex
		{
			get { return _selectedInformationsIndex; }
			set
			{
				_selectedInformationsIndex = value;
				if (value != 0)
				{
					SelectedDataTypeIndex = (int)DataSelector(ClientInfoSources[value].ProductVersion);
					SelectedFolder = ClientInfoSources[value].DirectoryPath;
				}
				RaisePropertyChanged(() => SelectedInformationIndex);
			}
		}

		public string SelectedFolder { get { return _selectedFolder; } set { _selectedFolder = value; RaisePropertyChanged(() => SelectedFolder); } }

		public int SelectedDataTypeIndex
		{
			get { return _selectedDataTypeIndex; }
			set
			{
				_selectedDataTypeIndex = value;
				_data = DataSelector((DataType) _selectedDataTypeIndex);
				RaisePropertyChanged(() => SelectedDataTypeIndex);
			}
		}

		public IList<ClientInfo> ClientInfoSources { get { return Sorter(ClientInfo.GetInSystem()); } }

		#endregion //Props

		#region Ctor
		public ViewModelOptionWindow()
		{
			AppMessages.DialogAnwer.Register(this, DialogResultHandler);
			CommandSelectDirectory = new RelayCommand(
				() => AppMessages.DialogRequest.Send(new MessageDialogRequest("OpenOptionFolder")));

			CommandApply = new RelayCommand(() =>
												{
													try
													{
														if (ApplicationController.manager != null)
														{
															ApplicationController.manager.Dispose();
															ApplicationController.manager = null;
														}
														GC.Collect();
														GC.WaitForPendingFinalizers();
														ApplicationController.manager =
													    UODataManager.GetInstance(new Uri(SelectedFolder),
																		 _data, Language.English);
														AppMessages.OptionAnswer.Send(new OptionMessage() { Success = true });
													}
													catch (Exception e)
													{
														AppMessages.DialogRequest.Send(new MessageDialogRequest(e.Message));
													}
													var serializer = new XmlSerializer(GetType());
													using (var File = new FileStream("options.xml",FileMode.Create))
													{
														serializer.Serialize(File,this);

													}

												}, () => !string.IsNullOrEmpty(SelectedFolder));

			CommandCancel = new RelayCommand(() => AppMessages.OptionAnswer.Send(new OptionMessage() { Success = false }));


			if (!ClientInfoSources.Any() || ClientInfoSources[0] == null) return;
			SelectedFolder = ClientInfoSources[0].DirectoryPath;
			SelectedDataTypeIndex = (int)DataSelector(ClientInfoSources[0].ProductVersion);
		}
		#endregion //Ctor

		#region Commands

		[IgnoreDataMember, XmlIgnore]
		public ICommand CommandSelectDirectory { get; private set; }

		[IgnoreDataMember,XmlIgnore]
		public ICommand CommandCancel { get; private set; }

		[IgnoreDataMember,XmlIgnore]
		public ICommand CommandApply { get; private set; }

		[IgnoreDataMember,XmlIgnore]
		public ICommand CommandSelectOutputFolder { get; private set; }

		#endregion Commands

		#region Message Handler

		private void DialogResultHandler(MessageDialogResult result)
		{
			switch (result.Type)
			{
				case DialogType.OpenOptionFolder:
					{
						SelectedFolder = result.Content;
					}
					break;
				
			}
		}

		#endregion //Message Handler

		#region Data Management
		private IList<ClientInfo> Sorter(IEnumerable<ClientInfo> clientInfos)
		{

			var list = new List<ClientInfo>();
			foreach (var clientInfo in clientInfos)
			{
				var client = list.Where(o => o.DirectoryPath == clientInfo.DirectoryPath);
				if (!client.Any())
				{
					list.Add(clientInfo);
				}
				else
				{
					if (clientInfo.ProductVersion > client.First().ProductVersion)
					{
						list.Remove(client.First());
						list.Add(clientInfo);
					}
				}
			}
			return list;
		}

		private DataType DataSelector(Version version)
		{
			if (version < new Version(1, 23, 27))
			{
				_data = UODataType.ClassicPreAlphaVersion;
				return DataType.ClassicPreAlphaVersion;
			}
			// /// Classic Client (UO: Shattered Legacy)
			// /// Versions: from 1.23.27 up to 1.26.4j
			if (version >= new Version(1, 23, 27) && version <= new Version(1, 26, 4))
			{
				_data = UODataType.ClassicShatteredLegacy;
				return DataType.ClassicShatteredLegacy;
			}

			//// /// Classic Client (UO: Renaissance)
			//// /// Versions: from 2.0.0 up to 2.0.9a
			if (version >= new Version(2, 0, 0) && version <= new Version(2, 0, 9))
			{
				_data = UODataType.ClassicRenaissance;
				return DataType.ClassicRenaissance;
			}


			//// /// Classic Client (UO: Third Dawn)
			//// /// Versions: from 3.0.0 up to 3.0.7a
			if (version >= new Version(3, 0, 0) && version <= new Version(3, 0, 7))
			{
				_data = UODataType.ClassicThirdDawn;
				return DataType.ClassicThirdDawn;
			}


			//// /// Classic Client (UO: Blackthorn's Revenge)
			//// /// Versions: from 3.0.7b up to 3.0.8r
			if (version >= new Version(3, 0, 7) && version <= new Version(3, 0, 8))
			{
				_data = UODataType.ClassicLordBlackthornsRevenge;
				return DataType.ClassicLordBlackthornsRevenge;
			}

			//// /// Classic Client (UO: Age of Shadows)
			//// /// Versions: from 3.0.8z up to 4.0.4b2
			if (version >= new Version(3, 0, 8) && version <= new Version(4, 0, 4))
			{
				_data = UODataType.ClassicAgeOfShadows;
				return DataType.ClassicAgeOfShadows;
			}


			//// /// Classic Client (UO: Samurai Empire)
			//// /// Versions: from 4.0.5a up to 4.0.11c
			if (version >= new Version(4, 0, 5) && version <= new Version(4, 0, 11))
			{
				_data = UODataType.ClassicSamuraiEmpire;
				return DataType.ClassicSamuraiEmpire;
			}

			//// /// Classic Client (UO: Mondain's Legacy)
			//// /// Versions: from 4.0.11d up to 6.0.14.2
			if (version >= new Version(4, 0, 11) && version <= new Version(6, 0, 14, 2))
			{
				_data = UODataType.ClassicMondainsLegacy;
				return DataType.ClassicMondainsLegacy;
			}

			//// /// Classic Client (UO: Stygian Abyss)
			//// /// Versions: from 6.0.14.3 up to 7.0.8.2
			if (version >= new Version(6, 0, 14, 3) && version <= new Version(7, 0, 8, 2))
			{
				_data = UODataType.ClassicStygianAbyss;
				return DataType.ClassicStygianAbyss;
			}

			//// /// Classic Client (UO: Adventures On High Seas)
			//// /// Versions: from 7.0.8.44 up to 7.0.23.1
			if (version >= new Version(7, 0, 8, 44) && version <= new Version(7, 0, 23, 1))
			{
				_data = UODataType.ClassicAdventuresOnHighSeas;
				return DataType.ClassicAdventuresOnHighSeas;
			}

			//// /// Classic Client (UO: Adventures On High Seas)
			//// /// Versions: from 7.0.23.2 up to 7.0.X.X
			if (version > new Version(7, 0, 23, 2))
			{
				_data = UODataType.ClassicAdventuresOnHighSeasUpdated;
				return DataType.ClassicAdventuresOnHighSeasUpdated;
			}

			_data = UODataType.UseOldDatas;
			return 0;
		}

		private UODataType DataSelector(DataType index)
		{
			switch (index)
			{
				case DataType.ClassicAdventuresOnHighSeasUpdated:
					{
						return UODataType.ClassicAdventuresOnHighSeasUpdated;
					}
				case DataType.ClassicAdventuresOnHighSeas:
					{
						return UODataType.ClassicAdventuresOnHighSeas;
					}
				case DataType.ClassicStygianAbyss:
					{
						return UODataType.ClassicStygianAbyss;
					}
				case DataType.EssenceInfinityClient:
					{
						return UODataType.EssenceInfinityClient;
					}
				case DataType.ClassicMondainsLegacy:
					{
						return UODataType.ClassicMondainsLegacy;
					}
				case DataType.ClassicSamuraiEmpire:
					{
						return UODataType.ClassicSamuraiEmpire;
					}
				case DataType.ClassicShatteredLegacy:
					{
						return UODataType.ClassicShatteredLegacy;
					}
				case DataType.ClassicLordBlackthornsRevenge:
					{
						return UODataType.ClassicLordBlackthornsRevenge;
					}
				case DataType.ClassicRenaissance:
					{
						return UODataType.ClassicRenaissance;
					}
				case DataType.ClassicPreAlphaVersion:
					{
						return UODataType.ClassicRenaissance;
					}
				case DataType.ClassicThirdDawn:
					{
						return UODataType.ClassicThirdDawn;
					}
				case DataType.ClassicAgeOfShadows:
					{
						return UODataType.ClassicAgeOfShadows;
					}
				default:
					{
						return UODataType.UseOldDatas;
					}
			}
		}
		#endregion //Data Management

		

	}



	public enum DataType
	{
		/// <summary>
		/// For future purporse....
		/// </summary>
		EssenceInfinityClient,

		/// <summary>
		/// Classic Client (UO: Pre Alpha Client)
		/// Versions: ---
		/// </summary>
		ClassicPreAlphaVersion,
		/// <summary>
		/// Classic Client (UO: Shattered Legacy)
		/// Versions: from 1.23.27 up to 1.26.4j
		/// </summary>
		ClassicShatteredLegacy,
		/// <summary>
		/// Classic Client (UO: Renaissance)
		/// Versions: from 2.0.0 up to 2.0.9a
		/// </summary>
		ClassicRenaissance,
		/// <summary>
		/// Classic Client (UO: Third Dawn)
		/// Versions: from 3.0.0 up to 3.0.7a
		/// </summary>
		ClassicThirdDawn,
		/// <summary>
		/// Classic Client (UO: Blackthorn's Revenge)
		/// Versions: from 3.0.7b up to 3.0.8r
		/// </summary>
		ClassicLordBlackthornsRevenge,
		/// <summary>
		/// Classic Client (UO: Age of Shadows)
		/// Versions: from 3.0.8z up to 4.0.4b2
		/// </summary>
		ClassicAgeOfShadows,
		/// <summary>
		/// Classic Client (UO: Samurai Empire)
		/// Versions: from 4.0.5a up to 4.0.11c
		/// </summary>
		ClassicSamuraiEmpire,
		/// <summary>
		/// Classic Client (UO: Mondain's Legacy)
		/// Versions: from 4.0.11d up to 6.0.14.2
		/// </summary>
		ClassicMondainsLegacy,
		/// <summary>
		/// Classic Client (UO: Stygian Abyss)
		/// Versions: from 6.0.14.3 up to 7.0.8.2
		/// </summary>
		ClassicStygianAbyss,
		/// <summary>
		/// Classic Client (UO: Adventures On High Seas)
		/// Versions: from 7.0.8.44 up to 7.0.23.1
		/// </summary>
		ClassicAdventuresOnHighSeas,
		/// <summary>
		/// Classic Client (UO: Adventures On High Seas)
		/// Versions: from 7.0.23.2 up to 7.0.X.X
		/// </summary>
		ClassicAdventuresOnHighSeasUpdated,
	}

}
