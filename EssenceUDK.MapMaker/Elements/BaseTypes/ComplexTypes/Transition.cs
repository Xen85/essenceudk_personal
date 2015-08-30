using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using EssenceUDK.MapMaker.Elements.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes
{
    [Serializable]
    public class Transition : NotificationObject, ITransition
    {
        private ObservableCollection<CollectionLine> _lines;

        #region Props

        public ObservableCollection<CollectionLine> Lines { get { return _lines; } set { _lines = value; RaisePropertyChanged(() => Lines); } }

        #endregion Props

        #region Ctor

        public Transition()
        {
            _lines = new ObservableCollection<CollectionLine>();

            for (int i = 0; i < 3; i++)
            {
                _lines.Add(new CollectionLine());
            }
        }

        #endregion Ctor

        #region Lists

        /// <summary>
        /// Pointer to the right Linear List
        /// </summary>
        public CollectionLine Line { get { return Lines[(int)LineType.Line]; } }

        #region LinearTexures

        /// <summary>
        ///
        /// </summary>
        public CollectionItem LineNorth { get { return Line.List[(int)LinearDirection.North]; } }

        /// <summary>
        ///
        /// </summary>
        public CollectionItem LineEast { get { return Line.List[(int)LinearDirection.East]; } }

        /// <summary>
        ///
        /// </summary>
        public CollectionItem LineWest { get { return Line.List[(int)LinearDirection.West]; } }

        /// <summary>
        ///
        /// </summary>
        public CollectionItem LineSouth { get { return Line.List[(int)LinearDirection.South]; } }

        #endregion LinearTexures

        /// <summary>
        /// Pointer to the right Big edge List
        /// </summary>
        public CollectionLine Border { get { return Lines[(int)LineType.Border]; } }

        #region BigEdge Lists

        /// <summary>
        ///
        /// </summary>
        public CollectionItem BorderNorthEast { get { return Border.List[(int)EdgeDirection.NortEast]; } }

        /// <summary>
        ///
        /// </summary>
        public CollectionItem BorderNorthWest { get { return Border.List[(int)EdgeDirection.NortWest]; } }

        /// <summary>
        ///
        /// </summary>
        public CollectionItem BorderSouthEast { get { return Border.List[(int)EdgeDirection.SouthEast]; } }

        /// <summary>
        ///
        /// </summary>
        public CollectionItem BorderSouthWest { get { return Border.List[(int)EdgeDirection.SoutWest]; } }

        #endregion BigEdge Lists

        /// <summary>
        /// Pointer to the right Little Edge List
        /// </summary>
        public CollectionLine Edge { get { return Lines[(int)LineType.Edge]; } }

        #region little edge Lists

        /// <summary>
        ///
        /// </summary>
        public CollectionItem EdgeNorthWest { get { return Edge.List[(int)EdgeDirection.NortWest]; } }

        /// <summary>
        ///
        /// </summary>
        public CollectionItem EdgeNorthEast { get { return Edge.List[(int)EdgeDirection.NortEast]; } }

        /// <summary>
        ///
        /// </summary>
        public CollectionItem EdgeSouthEast { get { return Edge.List[(int)EdgeDirection.SouthEast]; } }

        /// <summary>
        ///
        /// </summary>
        public CollectionItem EdgeSouthWest { get { return Edge.List[(int)EdgeDirection.SoutWest]; } }

        #endregion little edge Lists

        #endregion Lists

        #region methods

        public void AddElement(LineType line, int direction, int element)
        {
            Lines[(int)line].AddElement(direction, element);
        }

        #endregion methods

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(() => Lines, info);
        }

        protected Transition(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Lines = new ObservableCollection<CollectionLine>(Deserialize(() => Lines, info));
        }
    }
}