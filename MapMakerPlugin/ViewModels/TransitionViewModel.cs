using System;
using EssenceUDK.MapMaker.Elements;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using GalaSoft.MvvmLight;
using EdgeDirection = EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum.EdgeDirection;

namespace MapMakerPlugin.ViewModels
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class TransitionViewModel : ViewModelBase
    {
        private readonly TileContainerViewModel[,] _innerList;
        private Transition _trans;

        /// <summary>
        ///     Initializes a new instance of the TransitionViewModel1 class.
        /// </summary>
        public TransitionViewModel()
        {
            _innerList = new TileContainerViewModel[3, 4];
        }

        public Transition Transition
        {
            get { return _trans; }
            set
            {
                _trans = value;

                if (_trans != null)
                    foreach (
                        LineType lineType in Enum.GetValues(typeof (LineType)))
                    {
                        foreach (var direction in Enum.GetValues(typeof (Direction)))
                        {
                            if (_innerList[(int) lineType, (int) direction] == null)
                                _innerList[(int) lineType, (int) direction] = new TileContainerViewModel
                                {
                                    List = _trans.Lines[(int) lineType].List[(int) direction].List
                                };
                            else
                                _innerList[(int) lineType, (int) direction].List =
                                    _trans.Lines[(int) lineType].List[(int) direction].List;
                        }
                    }
                else
                {
                    foreach (
                        LineType lineType in Enum.GetValues(typeof (LineType)))
                    {
                        foreach (var direction in Enum.GetValues(typeof (Direction)))
                        {
                            _innerList[(int) lineType, (int) direction] = new TileContainerViewModel
                            {
                                List = null
                            };
                        }
                    }
                }
                RaisePropertyChanged(null);
            }
        }

        #region Lines

        public TileContainerViewModel LineNorth => _innerList[(int) LineType.Line, (int) LinearDirection.North];

        public TileContainerViewModel LineEast => _innerList[(int) LineType.Line, (int) LinearDirection.East];

        public TileContainerViewModel LineWest => _innerList[(int) LineType.Line, (int) LinearDirection.West];

        public TileContainerViewModel LineSouth => _innerList[(int) LineType.Line, (int) LinearDirection.South];

        #endregion Lines

        #region border

        public TileContainerViewModel BorderNorthEast => _innerList[(int) LineType.Border, (int) EdgeDirection.NortEast]
            ;

        /// <summary>
        /// </summary>
        public TileContainerViewModel BorderNorthWest
            => _innerList[(int) LineType.Border, (int) EdgeDirection.NorthWest];

        /// <summary>
        /// </summary>
        public TileContainerViewModel BorderSouthEast
            => _innerList[(int) LineType.Border, (int) EdgeDirection.SouthEast];

        /// <summary>
        /// </summary>
        public TileContainerViewModel BorderSouthWest
            => _innerList[(int) LineType.Border, (int) EdgeDirection.SouthWest];

        #endregion border

        #region edge

        public TileContainerViewModel EdgeNorthWest => _innerList[(int) LineType.Edge, (int) EdgeDirection.NorthWest];

        /// <summary>
        /// </summary>
        public TileContainerViewModel EdgeNorthEast => _innerList[(int) LineType.Edge, (int) EdgeDirection.NortEast];

        /// <summary>
        /// </summary>
        public TileContainerViewModel EdgeSouthEast => _innerList[(int) LineType.Edge, (int) EdgeDirection.SouthEast];

        /// <summary>
        /// </summary>
        public TileContainerViewModel EdgeSouthWest => _innerList[(int) LineType.Edge, (int) EdgeDirection.SouthWest];

        #endregion edge
    }

}