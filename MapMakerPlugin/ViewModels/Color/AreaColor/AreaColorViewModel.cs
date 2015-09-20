﻿#region

using System;
using System.ComponentModel;
using System.Linq;
using EssenceUDK.PluginBase.ViewModels.DockableModels;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.Models;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Color.AreaColor
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    ///     this view model is about AreaColor selected
    /// </summary>
    public class AreaColorViewModel : ViewModelDockableBase, IDataErrorInfo
    {
        private EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor _selected;

        /// <summary>
        ///     Initializes a new instance of the AreaColorViewModel1 class.
        /// </summary>
        public AreaColorViewModel()
        {
            var list =
                ServiceLocator.Current
                    .GetInstance<AreaColorsViewModel>();
            list.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == GetPropertyName(() => list.SelectedColor))
                    SelectedAreaColor = list.SelectedColor;
            };
        }

        [PreferredConstructor]
        public AreaColorViewModel(IServiceModelAreaColor service)
            : this()
        {
            service.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }

                    SelectedAreaColor = (EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor) item;
                });
        }

        public EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor SelectedAreaColor
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged(() => SelectedAreaColor);
            }
        }

        public string Name
        {
            get { return _selected.Name; }
            set
            {
                _selected.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int Index
        {
            get { return _selected.Index; }
            set
            {
                _selected.Index = value;
                RaisePropertyChanged(() => Index);
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Index":
                    {
                        var sdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>();
                        if (sdk == null) return string.Empty;
                        var mapSdk = sdk.Sdk;
                        if (mapSdk != null &&
                            (mapSdk.AreaColorIndexes != null && (!mapSdk.AreaColorIndexes.Contains(_selected.Index))))
                            return string.Empty;
                        Error = "Index already used, please choose another one";
                        return Error;
                    }
                    case "Name":
                    {
                        var sdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>();
                        if (sdk == null) return string.Empty;
                        var mapSdk = sdk.Sdk;
                        if (mapSdk != null &&
                            (mapSdk.AreaColorIndexes != null &&
                             (mapSdk.CollectionColorArea.List.All(a => a.Name != Name))))
                        {
                            return string.Empty;
                        }
                        Error = "Name already used, please choose another one";
                        return Error;
                    }
                    default:
                        Error = String.Empty;
                        return String.Empty;
                }
            }
        }

        /// <summary>
        ///     Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>
        ///     An error message indicating what is wrong with this object. The default is an empty string ("").
        /// </returns>
        public string Error { get; private set; }
    }

}