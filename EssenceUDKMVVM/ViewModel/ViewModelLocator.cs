/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:EssenceUDKMVVM.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System.Diagnostics.CodeAnalysis;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.DesignDataServices;
using EssenceUDKMVVM.Models.ModelDataServices;
using EssenceUDKMVVM.ViewModel.Udk;
using EssenceUDKMVVM.ViewModel.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
namespace EssenceUDKMVVM.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class ViewModelLocator
    {
        [PreferredConstructor]
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDataServiceOption, DataServiceOptionsDesign>();
            SimpleIoc.Default.Register<IDataServiceDataItem, DesignDataService>();
            SimpleIoc.Default.Register<IDockingManagerModelDataService, DockingManagerModelDataServiceDesign>();
            SimpleIoc.Default.Register<IMenuDataservice, DesignMenuDataService>();
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataServiceRender, DataServiceRenderDesignStatic>();
                SimpleIoc.Default.Register<IServiceModelLandData, DesignDataServiceModelLandDataStatic>();
                SimpleIoc.Default.Register<IUoDataManagerDataService, UoDataManagerDataServiceStatic>();
            }
            else
            {
                SimpleIoc.Default.Register<IServiceModelLandData, DesignDataServiceModelLandData>();
                SimpleIoc.Default.Register<IDataServiceRender, DataServiceRender>();
                SimpleIoc.Default.Register<IUoDataManagerDataService, UoDataManagerDataService>();
            }

            SimpleIoc.Default.Register<UODataManagerViewModel>();
            SimpleIoc.Default.Register<ViewModelLandTile>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ViewModelOptions>();
            SimpleIoc.Default.Register<RenderViewModel>();
            SimpleIoc.Default.Register<ViewModelLocator>();
            SimpleIoc.Default.Register<MenuViewModel>();
            SimpleIoc.Default.Register<DockingManagerViewModel>();
            SimpleIoc.Default.Register<AvalonDockLayoutViewModel>();
            var dockingManagerViewModel = SimpleIoc.Default.GetInstance<DockingManagerViewModel>();
            dockingManagerViewModel.Model = SimpleIoc.Default.GetInstance<MainViewModel>();
        }


        /// <summary>
        ///     View Model for options
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ViewModelOptions Option => ServiceLocator.Current.GetInstance<ViewModelOptions>();

        /// <summary>
        ///     View Model For MapRender
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public RenderViewModel MapRender => ServiceLocator.Current.GetInstance<RenderViewModel>();

        /// <summary>
        ///     View Model For Lands
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ViewModelLandTile Land => ServiceLocator.Current.GetInstance<ViewModelLandTile>();

        /// <summary>
        ///     view model for UODataManager
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UODataManagerViewModel UODataManager => ServiceLocator.Current.GetInstance<UODataManagerViewModel>();

        /// <summary>
        ///     View Model For MenuViewModel
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MenuViewModel MenuViewModel => ServiceLocator.Current.GetInstance<MenuViewModel>();

        /// <summary>
        ///     View Model For DockingManagerViewModel
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DockingManagerViewModel DockingManagerViewModel =>
            ServiceLocator.Current.GetInstance<DockingManagerViewModel>();

        /// <summary>
        ///     View Model For AvalonDockLayoutViewModel
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AvalonDockLayoutViewModel AvalonDockLayoutViewModel =>
            ServiceLocator.Current.GetInstance<AvalonDockLayoutViewModel>();

        /// <summary>
        ///     Cleans up all the resources.
        /// </summary>
        public void Cleanup()
        {
            UODataManager.Cleanup();
        }
    }
}