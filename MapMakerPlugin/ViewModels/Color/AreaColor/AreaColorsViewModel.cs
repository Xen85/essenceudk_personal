#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EssenceUDK.MapMaker;
using EssenceUDK.MapMaker.Elements.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorMountains;
using EssenceUDK.MapMaker.Elements.Items.ItemCoast;
using EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.Models;
using EssenceUDKMVVM.ViewModel.DockableModels;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.ViewModels.Color.AreaColor
{

    /// <summary>
    ///     This represents the List of Area Colors
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class AreaColorsViewModel : ViewModelDockableBase
    {
        /// <summary>
        ///     Initializes a new instance of the AreaColors class.
        /// </summary>
        public AreaColorsViewModel()
        {
            AddColor = new RelayCommand(CollectionAreaColorCommandAddExecuted);

            DeleteColor = new RelayCommand(CollectionAreaColorCommandRemoveExecuted, CanDelete);
            CloneColor =
                new RelayCommand(
                    () =>
                    {
                        Clone =
                            (EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor)
                                MapSdk.CloneSdkObject(_selectedColor);
                    },
                    () => SelectedColor != null);
            PasteColorCoasts = new RelayCommand(CommandPasteCoastExecuted, CommandCanPasteCoast);
            PasteColorCoastsItems = new RelayCommand(CommandPasteWaterCoastExecuted, CommandCanPasteCoast);
            PasteColorCoastOptions = new RelayCommand(CommandPasteCoastSpecialOptionsExecuted, CommandCanPasteCoast);
            PasteColorCoastsTexture = new RelayCommand(CommandPasteWaterCliffExecuted, CommandCanPasteCoast);
        }

        [PreferredConstructor]
        public AreaColorsViewModel(IServiceModelAreaColor service)
            : this()
        {
            service.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }

                    SelectedColor = (EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor) item;
                });
        }

        #region Props

        /// <summary>
        ///     List of area colors
        /// </summary>
        public CollectionAreaColor AreaColors
            => ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>().Sdk.CollectionColorArea;

        /// <summary>
        ///     Color Index List
        /// </summary>
        public IEnumerable<int> ColorIndex
            => ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>().Sdk.AreaColorIndexes;

        /// <summary>
        ///     List of Colors
        /// </summary>
        public IEnumerable<System.Windows.Media.Color> Colors
            => ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>().Sdk.AreaColorColors;

        private EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor _selectedColor;

        /// <summary>
        ///     Selected Color
        /// </summary>
        public EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                RaisePropertyChanged(() => SelectedColor);
            }
        }

        private EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor _clone;

        /// <summary>
        ///     Cloned Color
        /// </summary>
        public EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor Clone
        {
            get { return _clone; }
            set
            {
                _clone = value;
                RaisePropertyChanged(() => Clone);
            }
        }

        public EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor WorkingCopy => _selectedColor != null
            ? (EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor) MapSdk.CloneSdkObject(_selectedColor)
            : null;

        #endregion Props

        #region Command

        public ICommand AddColor { get; private set; }

        public ICommand DeleteColor { get; private set; }

        public ICommand CloneColor { get; private set; }

        public ICommand PasteColorCoasts { get; private set; }

        public ICommand PasteColorCoastOptions { get; private set; }

        public ICommand PasteColorCoastsTexture { get; private set; }

        public ICommand PasteColorCoastsItems { get; private set; }

        private bool CanDelete()
        {
            return _selectedColor != null;
        }

        private void CollectionAreaColorCommandRemoveExecuted()
        {
            try
            {
                var area = _selectedColor;

                if (area == null)
                    return;

                AreaColors.List.Remove(_selectedColor);
            }
            catch (Exception e)
            {
                //DialogRequest.Send(new MessageDialogRequest(e.Message));
            }
        }

        private void CollectionAreaColorCommandAddExecuted()
        {
            var sdk = ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>().Sdk;
            sdk.AddAreaColor(new EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor());
        }

        #region ContexMenu Copy&Paste

        private void CommandPasteCoastExecuted()
        {
            _selectedColor.Coasts = (AreaTransitionItemCoast) MapSdk.CloneSdkObject(Clone.Coasts);
            RaisePropertyChanged();
        }

        private bool CommandCanPasteCoast()
        {
            return _selectedColor != null && Clone != null;
        }

        private void CommandPasteWaterCoastExecuted()
        {
            _selectedColor.Coasts.Coast = (TransitionItemsCoast) MapSdk.CloneSdkObject(Clone.Coasts.Coast);
            RaisePropertyChanged();
        }

        private void CommandPasteWaterCliffExecuted()
        {
            _selectedColor.Coasts.Ground = (TransitionItemsCoast) MapSdk.CloneSdkObject(Clone.Coasts.Ground);
            RaisePropertyChanged();
        }

        private void CommandPasteCoastSpecialOptionsExecuted()
        {
            _selectedColor.CoastSmoothCircles = new ObservableCollection<CircleMountain>();

            foreach (var textureName in Clone.CoastSmoothCircles)
            {
                _selectedColor.CoastSmoothCircles.Add(new CircleMountain {From = textureName.From, To = textureName.To});
            }
            _selectedColor.ItemsAltitude = Clone.ItemsAltitude;
            _selectedColor.CoastAltitude = Clone.CoastAltitude;
            _selectedColor.CliffCoast = Clone.CliffCoast;
            _selectedColor.MinCoastTextureZ = Clone.MinCoastTextureZ;
            RaisePropertyChanged();
        }

        #endregion ContexMenu Copy&Paste

        #endregion Command
    }

}