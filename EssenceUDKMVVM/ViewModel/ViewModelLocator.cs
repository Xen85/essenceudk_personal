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
using EssenceUDKMVVM.Model_Interfaces.ModelDataServices;
using EssenceUDKMVVM.ViewModel.Udk;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM.ViewModel
{
    /// <su%mmary>
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
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
                SimpleIoc.Default.Register<IDataServiceOption, DataServiceOptionsDesign>();
                SimpleIoc.Default.Register<IDataServiceRender, DataServiceRenderDesign>();
                SimpleIoc.Default.Register<IServiceModelLandData, DesignDataServiceModelLandData>();
                SimpleIoc.Default.Register<IUoDataManagerDataService, UoDataManagerDataService>();
                SimpleIoc.Default.Register<IMenuDataservice, DesignMenuDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IServiceModelLandData, DesignDataServiceModelLandData>();
                SimpleIoc.Default.Register<IDataService, DataService>();
                SimpleIoc.Default.Register<IDataServiceOption, OptionModelDataService>();
                SimpleIoc.Default.Register<IDataServiceRender, DataServiceRender>();
                SimpleIoc.Default.Register<IUoDataManagerDataService, UoDataManagerDataService>();
                SimpleIoc.Default.Register<IMenuDataservice, DesignMenuDataService>();
            }
            SimpleIoc.Default.Register<IDockingManagerModelDataService, DockingManagerModelDataServiceDesign>();
            SimpleIoc.Default.Register<UODataManagerViewModel>();
            SimpleIoc.Default.Register<ViewModelLandTile>();
            SimpleIoc.Default.Register<MainViewModel>();
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
        public ViewModelOptions Option
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModelOptions>();
            }
        }

        /// <summary>
        /// View Model For MapRender
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public RenderViewModel MapRender
        {
            get { return ServiceLocator.Current.GetInstance<RenderViewModel>(); }
        }

        /// <summary>
        /// View Model For Lands
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public ViewModelLandTile Land
        {
            get { return ServiceLocator.Current.GetInstance<ViewModelLandTile>(); }
        }

        /// <summary>
        /// view model for UODataManager
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public UODataManagerViewModel UODataManager
        {
            get { return ServiceLocator.Current.GetInstance<UODataManagerViewModel>(); }
        }

        /// <summary>
        /// View Model For MenuViewModel
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public MenuViewModel MenuViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MenuViewModel>(); }
        }

        /// <summary>
        /// View Model For DockingManagerViewModel
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public DockingManagerViewModel DockingManagerViewModel
        {
            get { return ServiceLocator.Current.GetInstance<DockingManagerViewModel>(); }
        }

        /// <summary>
        /// View Model For AvalonDockLayoutViewModel
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public AvalonDockLayoutViewModel AvalonDockLayoutViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AvalonDockLayoutViewModel>(); }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public void Cleanup()
        {
            UODataManager.Cleanup();
        }



    }
}