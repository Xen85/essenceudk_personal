/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:EssenceUDKMVVM.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System;
using EssenceUDKMVVM.Models.DesignDataServices;
using EssenceUDKMVVM.Models.ModelDataServices;
using EssenceUDKMVVM.ViewModel.Udk;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Collections.Generic;
using EssenceUDK.PluginBase.Models;
using EssenceUDK.PluginBase.Models.DesignDataServices;
using EssenceUDK.PluginBase.ViewModels.DockableModels;
using EssenceUDK.PluginBase.ViewModels.Options;
using EssenceUDKMVVM.Models;
using DockingManagerModelDataServiceDesign = EssenceUDKMVVM.Models.DesignDataServices.DockingManagerModelDataServiceDesign;


namespace EssenceUDKMVVM.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        [PreferredConstructor]
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                //SimpleIoc.Default.Register<IDataService, DesignDataService>();
                SimpleIoc.Default.Register<IDataServiceOption, DataServiceOptionsDesign>();
                SimpleIoc.Default.Register<IDataServiceRender, DataServiceRenderDesign>();
                SimpleIoc.Default.Register<IServiceModelLandData, DesignDataServiceModelLandData>();
                SimpleIoc.Default.Register<IUoDataManagerDataService, UoDataManagerDataService>();
                SimpleIoc.Default.Register<IMenuDataservice, DesignMenuDataService>();
            }
            else
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    SimpleIoc.Default.Register<IServiceModelLandData, DesignDataServiceModelLandData>();
                    SimpleIoc.Default.Register<IDataServiceOption, OptionModelDataService>();
                    SimpleIoc.Default.Register<IDataServiceRender, DataServiceRender>();
                    SimpleIoc.Default.Register<IUoDataManagerDataService, UoDataManagerDataService>();
                    SimpleIoc.Default.Register<IMenuDataservice, DesignMenuDataService>();
                }
            }
            SimpleIoc.Default.Register<IDockingManagerModelDataService, DockingManagerModelDataServiceDesign>();
            SimpleIoc.Default.Register<UoDataManagerViewModel>();
            SimpleIoc.Default.Register<ViewModelLandTile>();
            SimpleIoc.Default.Register<ViewModelOptions>();
            SimpleIoc.Default.Register<RenderViewModel>();
            SimpleIoc.Default.Register<ViewModelLocator>();
            SimpleIoc.Default.Register<MenuViewModel>();
            SimpleIoc.Default.Register<DockingManagerViewModel>();
            SimpleIoc.Default.Register<AvalonDockLayoutViewModel>();
        }

        /// <summary>
        /// View Model for options
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ViewModelOptions Option => ServiceLocator.Current.GetInstance<ViewModelOptions>();

        /// <summary>
        /// View Model For MapRender
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public RenderViewModel MapRender => ServiceLocator.Current.GetInstance<RenderViewModel>();

        /// <summary>
        /// View Model For Lands
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ViewModelLandTile Land => ServiceLocator.Current.GetInstance<ViewModelLandTile>();

        /// <summary>
        /// view model for UODataManager
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public UoDataManagerViewModel UoDataManager => ServiceLocator.Current.GetInstance<UoDataManagerViewModel>();

        /// <summary>
        /// View Model For MenuViewModel
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public MenuViewModel MenuViewModel => ServiceLocator.Current.GetInstance<MenuViewModel>();

        /// <summary>
        /// View Model For DockingManagerViewModel
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public DockingManagerViewModel DockingManagerViewModel => ServiceLocator.Current.GetInstance<DockingManagerViewModel>();

        /// <summary>
        /// View Model For AvalonDockLayoutViewModel
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AvalonDockLayoutViewModel AvalonDockLayoutViewModel => ServiceLocator.Current.GetInstance<AvalonDockLayoutViewModel>();

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public void Cleanup()
        {
            UoDataManager.Cleanup();
        }


        private static readonly Dictionary<string, ResourceDictionary> ResourceDictionaries = new Dictionary<string, ResourceDictionary>();

        public static ResourceDictionary GetResourceDictionary(string dictionaryName, string libName, string url )
        {
            if (!ResourceDictionaries.ContainsKey(dictionaryName))
            {
                ResourceDictionaries[dictionaryName] = Application.LoadComponent(
                    new Uri( string.Format("{0};", libName) + url + dictionaryName + ".xaml",
                    UriKind.Relative)) as ResourceDictionary;
            }
            return ResourceDictionaries[dictionaryName];
        }
    }
}