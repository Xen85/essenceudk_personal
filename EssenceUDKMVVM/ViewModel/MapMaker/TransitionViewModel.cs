using System;
using System.Collections.Generic;
using System.Windows.Controls;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using GalaSoft.MvvmLight;

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
            m_InnerList = new TileContainerViewModel[3,4];
           
        }


        private readonly TileContainerViewModel[,] m_InnerList;
        private Transition _trans;

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
                        foreach (var direction in Enum.GetValues(typeof (EssenceUDK.MapMaker.Elements.Direction)))
                        {
                            if (m_InnerList[(int) lineType, (int) direction] == null)
                                m_InnerList[(int) lineType, (int) direction] = new TileContainerViewModel()
                                {
                                    List = _trans.Lines[(int) lineType].List[(int) direction].List
                                };
                            else
                                m_InnerList[(int) lineType, (int) direction].List =
                                    _trans.Lines[(int) lineType].List[(int) direction].List;
                        }


                    }
                else
                {
                    foreach (

                        LineType lineType in Enum.GetValues(typeof (LineType)))
                    {
                        foreach (var direction in Enum.GetValues(typeof (EssenceUDK.MapMaker.Elements.Direction)))
                        {
                            m_InnerList[(int) lineType, (int) direction] = new TileContainerViewModel()
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

        public TileContainerViewModel LineNorth { get { return m_InnerList[(int)LineType.Line,(int)LinearDirection.North ]; } }

        public TileContainerViewModel LineEast { get { return m_InnerList[(int)LineType.Line, (int)LinearDirection.East];  } }

        public TileContainerViewModel LineWest { get { return m_InnerList[(int)LineType.Line, (int)LinearDirection.West]; } }

        public TileContainerViewModel LineSouth { get { return m_InnerList[(int)LineType.Line, (int)LinearDirection.South]; } }
#endregion
        
        #region border

        public TileContainerViewModel BorderNorthEast => m_InnerList[(int)LineType.Border, (int)EdgeDirection.NortEast];

        /// <summary>
        /// 
        /// </summary>
        public TileContainerViewModel BorderNorthWest => m_InnerList[(int)LineType.Border, (int)EdgeDirection.NorthWest];

        /// <summary>
        /// 
        /// </summary>
        public TileContainerViewModel BorderSouthEast => m_InnerList[(int)LineType.Border, (int)EdgeDirection.SouthEast];

        /// <summary>
        /// 
        /// </summary>
        public TileContainerViewModel BorderSouthWest => m_InnerList[(int)LineType.Border, (int)EdgeDirection.SouthWest];

        #endregion
        
        #region edge

        public TileContainerViewModel EdgeNorthWest => m_InnerList[(int)LineType.Edge, (int)EdgeDirection.NorthWest];

        /// <summary>
        /// 
        /// </summary>
        public TileContainerViewModel EdgeNorthEast => m_InnerList[(int)LineType.Edge, (int)EdgeDirection.NortEast];

        /// <summary>
        /// 
        /// </summary>
        public TileContainerViewModel EdgeSouthEast => m_InnerList[(int)LineType.Edge, (int)EdgeDirection.SouthEast];

        /// <summary>
        /// 
        /// </summary>
        public TileContainerViewModel EdgeSouthWest => m_InnerList[(int)LineType.Edge, (int)EdgeDirection.SouthWest];

        #endregion

    }
}