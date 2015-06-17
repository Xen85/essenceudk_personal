using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.BaseTypes;

namespace EssenceUDK.MapMaker.Elements.Textures.TexureCliff
{
    [Serializable]
    public class AreaTransitionCliffTexture : NotificationObject
    {

        #region Fields

        private DirectionCliff _direction;

        private int _idFrom, _idTo, _idEdge;

        private Color _colorFrom, _colorTo, _colorEdge;

        private ObservableCollection<int> _list;

        private string _name;

        #endregion //Fields



        #region Props

        public String Name { get { return _name; } set { _name = value; RaisePropertyChanged(() => Name); } }

        public DirectionCliff Directions { get { return _direction; } set { _direction = value; RaisePropertyChanged(() => Directions); } }

        #region From

        public Color ColorFrom { get { return _colorFrom; } set { _colorFrom = value; RaisePropertyChanged(() => ColorFrom); } }

        public int IdFrom { get { return _idFrom; } set { _idFrom = value; RaisePropertyChanged(() => IdFrom); } }

        #endregion //From


        #region To

        public Color ColorTo { get { return _colorTo; } set { _colorTo = value; RaisePropertyChanged(() => ColorTo); } }

        public int IdTo
        {
            get { return _idTo; }
            set
            {
                _idTo = value;
                ColorTo = MapSdk.Colors[value];
                RaisePropertyChanged(() => IdTo);
            }
        }

        #endregion//to


        #region Edge

        public Color ColorEdge { get { return _colorEdge; } set { _colorEdge = value; RaisePropertyChanged(() => ColorEdge); } }

        public int IdEdge { get { return _idEdge; } set { _idEdge = value; RaisePropertyChanged(() => IdEdge); } }

        #endregion //Edge


        public ObservableCollection<int> List { get { return _list; } set { _list = value; RaisePropertyChanged(() => List); } }

        #endregion //Props

        #region Ctor

        public AreaTransitionCliffTexture()
        {
            Directions = DirectionCliff.EastEnd;
            ColorTo = Colors.Black;
            ColorFrom = Colors.Black;
            ColorEdge = Colors.Black;
            List = new ObservableCollection<int>();
            _idTo = -1;
            _idFrom = -1;
            _idEdge = -1;
        }

        #endregion

        #region Methods

        #region Override Methods

        public override string ToString()
        {
            return Directions.ToString() + " " + ColorFrom + " " + ColorTo + " " + ColorEdge;
        }

        #endregion

        #endregion

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(() => Name, info);
            Serialize(() => ColorFrom, info);
            Serialize(() => ColorTo, info);
            Serialize(() => IdFrom, info);
            Serialize(() => ColorEdge, info);
            Serialize(() => List, info);
            Serialize(() => Directions, info);
        }

        protected AreaTransitionCliffTexture(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Name = Deserialize(() => Name, info);
            ColorFrom = Deserialize(() => ColorFrom, info);
            ColorTo = Deserialize(() => ColorTo, info);
            IdFrom = Deserialize(() => IdFrom, info);
            ColorTo = Deserialize(() => ColorTo, info);
            ColorEdge = Deserialize(() => ColorEdge, info);
            List = new ObservableCollection<int>(Deserialize(() => List, info));
            Directions = Deserialize(() => Directions, info);
        }


    }

}
