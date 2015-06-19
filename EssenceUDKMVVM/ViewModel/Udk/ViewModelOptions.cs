using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using EssenceUDK.Platform;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.Model;
using EssenceUDKMVVM.ViewModel.DockableModels;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM.ViewModel.Udk
{

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelOptions : ToolPaneViewModel
    {
        private OptionModel _optionModel = new OptionModel();



        /// <summary>
        /// 
        /// </summary>
        public ViewModelOptions()
        {

            //clean command
            Clean = new RelayCommand(() =>
            {
                _optionModel = new OptionModel();
            });

            //apply command
            Apply = new RelayCommand(() =>
            {
                UpdateUoDataManager();
                var serializer = new XmlSerializer(typeof(OptionModel));
                using (var file = new FileStream("options.xml", FileMode.Create))
                {
                    serializer.Serialize(file, _optionModel);
                }

            });

            Title = "Options";
            Visibility = Visibility.Visible;
            ToolTip = "Option Window";
            ContentId = "options";

        }

        /// <summary>
        /// Ctor used by ServiceLocator
        /// DesignData : DataServiceOptionsDesign
        /// RunTimeData : OptionModelDataService
        /// </summary>
        /// <param name="dataService"></param>
        [PreferredConstructor]
        public ViewModelOptions(IDataServiceOption dataService)
            : this()
        {
            dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    _optionModel = ((OptionModel)(item));
                });
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateUoDataManager()
        {
            var locator = ServiceLocator.Current.GetInstance<ViewModelLocator>();

            if (ServiceLocator.Current.GetInstance<ViewModelLocator>().UODataManager.UODataManager != null) ServiceLocator.Current.GetInstance<ViewModelLocator>().UODataManager.UODataManager.Dispose();
            ServiceLocator.Current.GetInstance<ViewModelLocator>().UODataManager.UODataManager = UODataManager.GetInstance(new Uri(_optionModel.Path), (UODataType)_optionModel.DataType,
            OptionModel.Language, null, OptionModel.RealTime);
        }

        /// <summary>
        /// 
        /// </summary>
        public OptionModel OptionModel { get { return _optionModel; } set { _optionModel = value; RaisePropertyChanged(() => OptionModel); } }

        /// <summary>
        /// Command to clean your Option Model
        /// </summary>
        public ICommand Clean { get; private set; }

        /// <summary>
        /// Command to Apply your option model
        /// </summary>
        public ICommand Apply { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return OptionModel == null ? "OptionModelNull" : OptionModel.Path;
        }



    }

}