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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml.Serialization;
using EssenceUDK.Platform;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MapMakerApplication.Messages;
using MapMakerApplication.Resources;
using Microsoft.Practices.ServiceLocation;

namespace MapMakerApplication.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    ///     <para>
    ///         Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    ///         to this locator.
    ///     </para>
    ///     <para>
    ///         In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
    ///     </para>
    ///     <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MapMakerApplication.ViewModel"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    ///     <para>
    ///         Then use:
    ///     </para>
    ///     <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    ///     <para>
    ///         You can also use Blend to do all this with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm/getstarted
    ///     </para>
    ///     <para>
    ///         In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
    ///         the Main property and bind to the ViewModelNameStatic property instead:
    ///     </para>
    ///     <code>
    /// xmlns:vm="clr-namespace:MapMakerApplication.ViewModel"
    /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
    /// </code>
    /// </summary>
    public class ViewModelLocator : ViewModelBase
    {
        public static SdkViewModel _sdk;
        public static MapMakerViewModel _mapMaker;
        public static ViewModelOptionWindow _option;

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<SdkViewModel>();
            SimpleIoc.Default.Register<MapMakerViewModel>();
            if (IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<ISdkDataService, SdkDataServiceStatic>();
                SimpleIoc.Default.Register<IUoDataManagerService, UoDataManagerServiceStatic>();
                SimpleIoc.Default.Register<ISdkDataServiceGenerated, SdkDataServiceGeneratedStatic>();
            }
            else
            {
                SimpleIoc.Default.Register<ISdkDataService, SdkDataServiceDynamic>();
                SimpleIoc.Default.Register<IUoDataManagerService, UoDataManagerServiceDynamic>();
                SimpleIoc.Default.Register<ISdkDataServiceGenerated, SdkDataServiceGeneratedDynamic>();
            }
        }


        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            CreateOptionView();
            AppMessages.OptionAnswer.Register(this, MessageHandler);
            if (!File.Exists("options.xml")) return;
            var serializer = new XmlSerializer(typeof(ViewModelOptionWindow));
            try
            {
                using (var file = new FileStream("options.xml", FileMode.Open))
                {
                    _option = (ViewModelOptionWindow) serializer.Deserialize(file);
                }

                _option.CommandApply.Execute(null);
            }
            catch (Exception e)
            {
            }
        }

        private void MessageHandler(OptionMessage message)
        {
            RaisePropertyChanged(null);
        }

        #region SDK

        /// <summary>
        ///     Gets the SdkView property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SdkViewModel Sdk => ServiceLocator.Current.GetInstance<SdkViewModel>();


        public static void ClearSdk()
        {
            _sdk.Cleanup();
        }

        #endregion //Sdk

        #region Map Maker ViewModel

        /// <summary>
        ///     Gets the SdkView property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MapMakerViewModel MapMaker => ServiceLocator.Current.GetInstance<MapMakerViewModel>();


        public static void ClearMapMaker()
        {
            _mapMaker.Cleanup();
        }

        #endregion

        #region Options

        public static void CreateOptionView()
        {
            if (_option == null) _option = new ViewModelOptionWindow();
        }

        /// <summary>
        ///     Gets the SdkView property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ViewModelOptionWindow OptionViewModel => _option;


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
                (ObservableCollection<ModelItemData>) ApplicationController.manager.GetItemTile();
        }

        private static IEnumerable _Lands()
        {
            if (ApplicationController.manager == null)
                return null;
            return
                //new VirtualizingCollection<ModelLandData>(
                //	new ItemProviderModelLandData((IList<ModelLandData>) ApplicationController.manager.GetLandTile()),2000);
                (ObservableCollection<ModelLandData>) ApplicationController.manager.GetLandTile();
        }

        public IEnumerable Items => _Items();

        public IEnumerable Lands => _Lands();

        #endregion //Global Constants
    }
}