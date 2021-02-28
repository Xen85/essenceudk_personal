using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EssenceUDK.MapMaker;
using EssenceUDK.MapMaker.Elements.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorMountains;
using EssenceUDK.MapMaker.Elements.Items.ItemCoast;
using EssenceUDKMVVM.Messages;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.ViewModel.DockableModels;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using MapMakerApplication.Messages;
using CommonServiceLocator;

namespace EssenceUDKMVVM.ViewModel.MapMaker.Color.AreaColor
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
                            (EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor) MapSdk.CloneSdkObject(
                                m_SelectedColor);
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
                    if (error != null) return;

                    if (item != null) SelectedColor =  item.SelectedAreaColor;
                });
        }

        #region Props

        /// <summary>
        ///     List of area colors
        /// </summary>
        public CollectionAreaColor AreaColors =>
            ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>().Sdk.CollectionColorArea;

        /// <summary>
        ///     Color Index List
        /// </summary>
        public IEnumerable<int> ColorIndex =>
            ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>().Sdk.AreaColorIndexes;

        /// <summary>
        ///     List of Colors
        /// </summary>
        public IEnumerable<System.Windows.Media.Color> Colors =>
            ServiceLocator.Current.GetInstance<MapMakerSdkViewModel>().Sdk.AreaColorColors;

        private EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor m_SelectedColor;

        /// <summary>
        ///     Selected Color
        /// </summary>
        public EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor SelectedColor
        {
            get => m_SelectedColor;
            private set
            {
                m_SelectedColor = value;
                RaisePropertyChanged(() => SelectedColor);
            }
        }

        private EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor m_Clone;

        /// <summary>
        ///     Cloned Color
        /// </summary>
        public EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor Clone
        {
            get => m_Clone;
            set
            {
                m_Clone = value;
                RaisePropertyChanged(() => Clone);
            }
        }

        public EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor WorkingCopy => m_SelectedColor != null
            ? (EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor) MapSdk.CloneSdkObject(m_SelectedColor)
            : null;

        #endregion

        #region Command

        public ICommand AddColor { get; }

        public ICommand DeleteColor { get; }

        public ICommand CloneColor { get; }

        public ICommand PasteColorCoasts { get; }

        public ICommand PasteColorCoastOptions { get; }

        public ICommand PasteColorCoastsTexture { get; }

        public ICommand PasteColorCoastsItems { get; }

        private bool CanDelete()
        {
            return m_SelectedColor != null;
        }

        private void CollectionAreaColorCommandRemoveExecuted()
        {
            try
            {
                var area = m_SelectedColor;

                if (area == null)
                    return;

                AreaColors.List.Remove(m_SelectedColor);
            }
            catch (Exception e)
            {
                AppMessages.DialogRequest.Send(new MessageDialogRequest(e.Message));
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
            m_SelectedColor.Coasts = (AreaTransitionItemCoast) MapSdk.CloneSdkObject(Clone.Coasts);
            RaisePropertyChanged();
        }

        private bool CommandCanPasteCoast()
        {
            return m_SelectedColor != null && Clone != null;
        }

        private void CommandPasteWaterCoastExecuted()
        {
            m_SelectedColor.Coasts.Coast = (TransitionItemsCoast) MapSdk.CloneSdkObject(Clone.Coasts.Coast);
            RaisePropertyChanged();
        }

        private void CommandPasteWaterCliffExecuted()
        {
            m_SelectedColor.Coasts.Ground = (TransitionItemsCoast) MapSdk.CloneSdkObject(Clone.Coasts.Ground);
            RaisePropertyChanged();
        }

        private void CommandPasteCoastSpecialOptionsExecuted()
        {
            m_SelectedColor.CoastSmoothCircles = new ObservableCollection<CircleMountain>();

            foreach (var textureName in Clone.CoastSmoothCircles)
                m_SelectedColor.CoastSmoothCircles.Add(new CircleMountain
                    {From = textureName.From, To = textureName.To});
            m_SelectedColor.ItemsAltitude = Clone.ItemsAltitude;
            m_SelectedColor.CoastAltitude = Clone.CoastAltitude;
            m_SelectedColor.CliffCoast = Clone.CliffCoast;
            m_SelectedColor.MinCoastTextureZ = Clone.MinCoastTextureZ;
            RaisePropertyChanged();
        }

        #endregion //ContexMenu Copy&Paste

        #endregion
    }
}