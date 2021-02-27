using System.Windows.Input;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.Model;
using EssenceUDKMVVM.ViewModel.DockableModels;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

namespace EssenceUDKMVVM.ViewModel.Udk
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class RenderViewModel : DocPaneViewModel
    {
        private RenderModel _model;

        /// <summary>
        ///     Initializes a new instance of the RenderViewModel class.
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
                if (s != null)
                    _model = (RenderModel) s;
                else
                    _model = new RenderModel();
            });
        }


        public RenderModel Model
        {
            get => _model;
            set
            {
                _model = value;
                RaisePropertyChanged(() => Model);
            }
        }

        public ICommand GoUp { get; }

        public ICommand GoDown { get; }

        public ICommand GoLeft { get; }

        public ICommand GoRight { get; }

        public ICommand GoUpLeft { get; }

        public ICommand GoUpRight { get; }

        public ICommand GoDownLeft { get; }

        public ICommand GoDownRight { get; }

        public ICommand Refresh { get; }

        private bool CanMove()
        {
            return _model.X > 0 && _model.X < 12288 - 1 && _model.Y > 0 && _model.Y < 8192 - 1;
        }


        #region props

        public ushort Width
        {
            get => Model.Width;
            set
            {
                Model.Width = value;
                RaisePropertyChanged(() => Width);
                RaisePropertyChanged(() => Model);
            }
        }

        public ushort Height
        {
            get => Model.Height;
            set
            {
                Model.Height = value;
                RaisePropertyChanged(() => Height);
                RaisePropertyChanged(() => Model);
            }
        }

        public byte Map
        {
            get => Model.Map;
            set
            {
                Model.Map = value;
                RaisePropertyChanged(() => Map);
                RaisePropertyChanged(() => Model);
            }
        }

        public byte Range
        {
            get => Model.Range;
            set
            {
                Model.Range = value;
                RaisePropertyChanged(() => Range);
                RaisePropertyChanged(() => Model);
            }
        }

        public ushort X
        {
            get => Model.X;
            set
            {
                Model.X = value;
                RaisePropertyChanged(() => X);
                RaisePropertyChanged(() => Model);
            }
        }

        public ushort Y
        {
            get => Model.Y;
            set
            {
                Model.Y = value;
                RaisePropertyChanged(() => Y);
                RaisePropertyChanged(() => Model);
            }
        }

        public sbyte MinZ
        {
            get => Model.MinZ;
            set
            {
                Model.MinZ = value;
                RaisePropertyChanged(() => MinZ);
                RaisePropertyChanged(() => Model);
            }
        }

        public sbyte MaxZ
        {
            get => Model.MaxZ;
            set
            {
                Model.MaxZ = value;
                RaisePropertyChanged(() => MaxZ);
                RaisePropertyChanged(() => Model);
            }
        }

        public short SeaLevel
        {
            get => Model.SeaLevel;
            set
            {
                Model.SeaLevel = value;
                RaisePropertyChanged(() => SeaLevel);
                RaisePropertyChanged(() => Model);
            }
        }

        public byte RefreshAll
        {
            get
            {
                RaisePropertyChanged(() => Model);

                return 0;
            }
        }

        public bool Flat
        {
            get => Model.Flat;
            set
            {
                Model.Flat = value;
                RaisePropertyChanged(() => Flat);
                RaisePropertyChanged(() => Model);
            }
        }

        #endregion
    }
}