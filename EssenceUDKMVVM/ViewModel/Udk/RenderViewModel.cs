using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Windows.Input;
using EssenceUDK.PluginBase.ViewModels.DockableModels;

namespace EssenceUDKMVVM.ViewModel.Udk
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class RenderViewModel : DocPaneViewModel
    {
        private RenderModel _model;

        /// <summary>
        /// Initializes a new instance of the RenderViewModel class.
        /// </summary>
        public RenderViewModel()
        {
            GoUp = new RelayCommand(() =>
            {
                _model.Y--;
                RaisePropertyChanged(() => X);
                RaisePropertyChanged(() => Y);
                RaisePropertyChanged(() => Model);
            }, CanMove);

            GoDown = new RelayCommand(() =>
            {
                _model.Y++;
                RaisePropertyChanged(() => X);
                RaisePropertyChanged(() => Y);
                RaisePropertyChanged(() => Model);
            }, CanMove);

            GoLeft = new RelayCommand(() =>
            {
                _model.X--;
                RaisePropertyChanged(() => X);
                RaisePropertyChanged(() => Y);
                RaisePropertyChanged(() => Model);
            }, CanMove);

            GoRight = new RelayCommand(() =>
            {
                _model.X++;
                RaisePropertyChanged(() => X);
                RaisePropertyChanged(() => Y);
                RaisePropertyChanged(() => Model);
            }, CanMove);

            GoUpLeft = new RelayCommand(() =>
            {
                _model.X--;
                _model.Y--;
                RaisePropertyChanged(() => X);
                RaisePropertyChanged(() => Y);
                RaisePropertyChanged(() => Model);
            }, CanMove);

            GoUpRight = new RelayCommand(() =>
            {
                _model.X++;
                _model.Y--;
                RaisePropertyChanged(() => X);
                RaisePropertyChanged(() => Y);
                RaisePropertyChanged(() => Model);
            }, CanMove);

            GoDownLeft = new RelayCommand(() =>
            {
                _model.X--;
                _model.Y++;
                RaisePropertyChanged(() => X);
                RaisePropertyChanged(() => Y);
                RaisePropertyChanged(() => Model);
            }, CanMove);

            GoDownRight = new RelayCommand(() =>
            {
                _model.X++;
                _model.Y++;
                RaisePropertyChanged(() => X);
                RaisePropertyChanged(() => Y);
                RaisePropertyChanged(() => Model);
            }, CanMove);

            Refresh = new RelayCommand(() => { RaisePropertyChanged(() => Model); });
        }

        [PreferredConstructor]
        public RenderViewModel(IDataServiceRender model)
            : this()
        {
            model.GetData((s, e) =>
            {
                if (e != null)
                    return;
                _model = (RenderModel)s;
            });
        }

        public EssenceUDKMVVM.Models.Model.RenderModel Model { get { return _model; } set { _model = value; RaisePropertyChanged(() => (Model)); } }

        public ICommand GoUp { get; private set; }

        public ICommand GoDown { get; private set; }

        public ICommand GoLeft { get; private set; }

        public ICommand GoRight { get; private set; }

        public ICommand GoUpLeft { get; private set; }

        public ICommand GoUpRight { get; private set; }

        public ICommand GoDownLeft { get; private set; }

        public ICommand GoDownRight { get; private set; }

        public ICommand Refresh { get; private set; }

        private bool CanMove()
        {
            return _model.X > 0 && _model.X < 12288 - 1 && _model.Y > 0 && _model.Y < 8192 - 1;
        }

        #region props

        public UInt16 Width
        {
            get { return Model.Width; }
            set
            {
                Model.Width = value;
                RaisePropertyChanged(() => (Width));
                RaisePropertyChanged(() => Model);
            }
        }

        public UInt16 Height
        {
            get { return Model.Height; }
            set
            {
                Model.Height = value;
                RaisePropertyChanged(() => (Height));
                RaisePropertyChanged(() => Model);
            }
        }

        public Byte Map { get { return Model.Map; } set { Model.Map = value; RaisePropertyChanged(() => (Map)); RaisePropertyChanged(() => Model); } }

        public Byte Range { get { return Model.Range; } set { Model.Range = value; RaisePropertyChanged(() => (Range)); RaisePropertyChanged(() => Model); } }

        public UInt16 X { get { return Model.X; } set { Model.X = value; RaisePropertyChanged(() => (X)); RaisePropertyChanged(() => Model); } }

        public UInt16 Y { get { return Model.Y; } set { Model.Y = value; RaisePropertyChanged(() => (Y)); RaisePropertyChanged(() => Model); } }

        public SByte MinZ { get { return Model.MinZ; } set { Model.MinZ = value; RaisePropertyChanged(() => (MinZ)); RaisePropertyChanged(() => Model); } }
        public SByte MaxZ { get { return Model.MaxZ; } set { Model.MaxZ = value; RaisePropertyChanged(() => (MaxZ)); RaisePropertyChanged(() => Model); } }

        public Int16 SeaLevel { get { return Model.SeaLevel; } set { Model.SeaLevel = value; RaisePropertyChanged(() => (SeaLevel)); RaisePropertyChanged(() => Model); } }

        public Byte RefreshAll
        {
            get
            {
                RaisePropertyChanged(() => Model);

                return 0;
            }
        }

        public bool Flat
        {
            get { return Model.Flat; }
            set
            {
                Model.Flat = value;
                RaisePropertyChanged(() => Flat);
                RaisePropertyChanged(() => Model);
            }
        }

        #endregion props
    }
}