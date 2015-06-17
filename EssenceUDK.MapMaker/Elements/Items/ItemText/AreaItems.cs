using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.BaseTypes;

namespace EssenceUDK.MapMaker.Elements.Items.ItemText
{
    [Serializable]
    public class AreaItems : NotificationObject
    {
        private string _name;
        private Color _color;
        private ObservableCollection<CollectionItem> _list;
 
        #region Props
        public string Name { get { return _name; } set { _name = value; RaisePropertyChanged(()=>Name); } }
        public Color Color { get { return _color; } set { _color = value; RaisePropertyChanged(()=>Color); } }
        public ObservableCollection<CollectionItem> List { get { return _list; } set { _list = value; RaisePropertyChanged(()=>List); } }
        #endregion //Props

        #region Ctor

        public AreaItems()
        {
            Color = Colors.Black;
            List = new ObservableCollection<CollectionItem>();
            Name = "";
        }

        #endregion //Ctor

        #region Serialization

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(()=>Name,info);
            Serialize(()=>Color,info);
            Serialize(() => List, info);
        }

        protected  AreaItems(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Name = (string) Deserialize(() => Name, info);
            Color = Deserialize(() => Color, info);
            List  = new ObservableCollection<CollectionItem>(Deserialize(()=>List,info));
        }
        #endregion//Serialization
    }
}
