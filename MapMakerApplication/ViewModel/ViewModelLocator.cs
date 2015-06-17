/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MapMakerApplication.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:MapMakerApplication.ViewModel"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;
using EssenceUDK.MapMaker;
using EssenceUDK.Platform;
using EssenceUDK.Platform.DataTypes;
using EssenceUDK.Platform.MiscHelper;
using EssenceUDK.Resources;
using EssenceUDK.Resources.Libraries.MiscUtil.DataVirtualization;
using GalaSoft.MvvmLight;
using MapMakerApplication.Messages;

namespace MapMakerApplication.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MapMakerApplication.ViewModel"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// <para>
    /// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
    /// the Main property and bind to the ViewModelNameStatic property instead:
    /// </para>
    /// <code>
    /// xmlns:vm="clr-namespace:MapMakerApplication.ViewModel"
    /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
    /// </code>
    /// </summary>
    public class ViewModelLocator : ViewModelBase
    {
        public static SdkViewModel _sdk;
        public static MapMakerViewModel _mapMaker;
        public static ViewModelOptionWindow _option;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            CreateSdkView();
            CreateMapMakerView(_sdk.MakeMapSdk);
            CreateOptionView();
            AppMessages.OptionAnswer.Register(this,MessageHandler);
            if(File.Exists("options.xml"))
            {
                var serializer = new XmlSerializer(typeof(ViewModelOptionWindow));
                try
                {
                    using (var file = new FileStream("options.xml", FileMode.Open))
                    {
                        _option = (ViewModelOptionWindow) serializer.Deserialize(file);
                    }
                    _option.CommandApply.Execute(null);
                }
                catch(Exception e)
                {
                    
                }
            }

        }

        #region SDK
        public static void CreateSdkView()
        {
           if(_sdk == null)
           {
               _sdk = new SdkViewModel();
           }
        }

        /// <summary>
        /// Gets the SdkView property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SdkViewModel Sdk
        {
            get { return _sdk ?? (_sdk=new SdkViewModel());
            }
        }


        public static void ClearSdk()
        {
            _sdk.Cleanup();
        }

        #endregion //Sdk

        #region Map Maker ViewModel

        public static void CreateMapMakerView(MapSdk sdk)
        {
            if (_mapMaker == null)
            {
                _mapMaker = new MapMakerViewModel(sdk);
            }
        }

        /// <summary>
        /// Gets the SdkView property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MapMakerViewModel MapMaker
        {
            get { return _mapMaker??(_mapMaker=new MapMakerViewModel(_sdk.MakeMapSdk)); }
        }


        public static void ClearMapMaker()
        {
            _mapMaker.Cleanup();
        }

        #endregion

        #region Options
        public static void CreateOptionView()
        {
            if (_option == null)
            {
                _option = new ViewModelOptionWindow();
            }
        }

        /// <summary>
        /// Gets the SdkView property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ViewModelOptionWindow OptionViewModel
        {
            get { return _option; }
        }


        public static void ClearOption()
        {
            _option.Cleanup();
        }

        #endregion

        #region Global Constants
        private static IEnumerable _Items()
        {
            if (ApplicationController.manager == null)
                return null;
	        return
		        //new VirtualizingCollection<ModelItemData>(
		        //	new ItemProviderModelItemData(( IList<ModelItemData> ) ApplicationController.manager.GetItemTile()), (( IList<ModelItemData> ) ApplicationController.manager.GetItemTile()).Count);
				( ObservableCollection<ModelItemData> ) ApplicationController.manager.GetItemTile();
        }

        private static IEnumerable _Lands()
        {
            if (ApplicationController.manager == null)
                return null;
	        return
		        //new VirtualizingCollection<ModelLandData>(
		        //	new ItemProviderModelLandData((IList<ModelLandData>) ApplicationController.manager.GetLandTile()),2000);
				( ObservableCollection<ModelLandData> ) ApplicationController.manager.GetLandTile();
        }
      
        public  IEnumerable Items
        {
            get
            {
                return _Items();
            }
        }

        public IEnumerable Lands { get { return _Lands(); } }

        #endregion //Global Constants

        private void MessageHandler(OptionMessage message)
        {
            RaisePropertyChanged(null);
        }


    }
}