using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using GalaSoft.MvvmLight;
using System;

namespace EssenceUDKMVVM.ViewModel.MapMaker
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TransitionViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the TransitionViewModel1 class.
        /// </summary>
        public TransitionViewModel()
        {
            _innerList = new TileContainerViewModel[3, 4];
        }

        private readonly TileContainerViewModel[,] _innerList;
        private Transition _trans;

        public Transition Transition
        {
            get { return _trans; }
            set
            {
                _trans = value;

                if (_trans != null)
                    foreach (

                        LineType lineType in Enum.GetValues(typeof(LineType)))
                    {
                        foreach (var direction in Enum.GetValues(typeof(EssenceUDK.MapMaker.Elements.Direction)))
                        {
                            if (_innerList[(int)lineType, (int)direction] == null)
                                _innerList[(int)lineType, (int)direction] = new TileContainerViewModel()
                                {
                                    List = _trans.Lines[(int)lineType].List[(int)direction].List
                                };
                            else
                                _innerList[(int)lineType, (int)direction].List =
                                    _trans.Lines[(int)lineType].List[(int)direction].List;
                        }
                    }
                else
                {
                    foreach (

                        LineType lineType in Enum.GetValues(typeof(LineType)))
                    {
                        foreach (var direction in Enum.GetValues(typeof(EssenceUDK.MapMaker.Elements.Direction)))
                        {
                            _innerList[(int)lineType, (int)direction] = new TileContainerViewModel()
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

        public TileContainerViewModel LineNorth { get { return _innerList[(int)LineType.Line, (int)LinearDirection.North]; } }

        public TileContainerViewModel LineEast { get { return _innerList[(int)LineType.Line, (int)LinearDirection.East]; } }

        public TileContainerViewModel LineWest { get { return _innerList[(int)LineType.Line, (int)LinearDirection.West]; } }

        public TileContainerViewModel LineSouth { get { return _innerList[(int)LineType.Line, (int)LinearDirection.South]; } }

        #endregion Lines

        #region border

        public TileContainerViewModel BorderNorthEast { get { return _innerList[(int)LineType.Border, (int)EdgeDirection.NortEast]; } }

        /// <summary>
        ///
        /// </summary>
        public TileContainerViewModel BorderNorthWest { get { return _innerList[(int)LineType.Border, (int)EdgeDirection.NorthWest]; } }

        /// <summary>
        ///
        /// </summary>
        public TileContainerViewModel BorderSouthEast { get { return _innerList[(int)LineType.Border, (int)EdgeDirection.SouthEast]; } }

        /// <summary>
        ///
        /// </summary>
        public TileContainerViewModel BorderSouthWest { get { return _innerList[(int)LineType.Border, (int)EdgeDirection.SouthWest]; } }

        #endregion border

        #region edge

        public TileContainerViewModel EdgeNorthWest { get { return _innerList[(int)LineType.Edge, (int)EdgeDirection.NorthWest]; } }

        /// <summary>
        ///
        /// </summary>
        public TileContainerViewModel EdgeNorthEast { get { return _innerList[(int)LineType.Edge, (int)EdgeDirection.NortEast]; } }

        /// <summary>
        ///
        /// </summary>
        public TileContainerViewModel EdgeSouthEast { get { return _innerList[(int)LineType.Edge, (int)EdgeDirection.SouthEast]; } }

        /// <summary>
        ///
        /// </summary>
        public TileContainerViewModel EdgeSouthWest { get { return _innerList[(int)LineType.Edge, (int)EdgeDirection.SouthWest]; } }

        #endregion edge
    }
}