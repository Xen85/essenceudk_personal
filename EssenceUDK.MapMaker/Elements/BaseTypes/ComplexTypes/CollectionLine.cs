using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes
{
    [Serializable]
    public class CollectionLine : NotificationObject
    {
        private ObservableCollection<CollectionItem> _list;

        #region Props

        public ObservableCollection<CollectionItem> List { get { return _list; } set { _list = value; RaisePropertyChanged(() => List); } }
        public CollectionItem CollectionFirst { get { return List[0]; } set { List[0] = value; RaisePropertyChanged(() => CollectionFirst); RaisePropertyChanged(() => List); } }
        public CollectionItem CollectionSecond { get { return List[1]; } set { List[1] = value; RaisePropertyChanged(() => CollectionSecond); RaisePropertyChanged(() => List); } }
        public CollectionItem CollectionThird { get { return List[2]; } set { List[2] = value; RaisePropertyChanged(() => CollectionThird); RaisePropertyChanged(() => List); } }
        public CollectionItem CollectionForth { get { return List[3]; } set { List[3] = value; RaisePropertyChanged(() => CollectionForth); RaisePropertyChanged(() => List); } }

        #endregion Props

        #region Ctor

        public CollectionLine()
        {
            List = new ObservableCollection<CollectionItem>();

            for (int i = 0; i < 4; i++)
            {
                List.Add(new CollectionItem());
            }
        }

        #endregion Ctor

        #region Methods

        public void AddElement(int direction, int element)
        {
            List[direction].Add(element);
        }

        #endregion Methods

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Serialize(() => List, info);
        }

        protected CollectionLine(SerializationInfo info, StreamingContext context)
        {
            List = new ObservableCollection<CollectionItem>(Deserialize(() => List, info));
        }
    }
}