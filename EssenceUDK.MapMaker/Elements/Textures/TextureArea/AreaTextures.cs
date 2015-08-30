using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.Items;
using System;
using System.Collections.ObjectModel;

namespace EssenceUDK.MapMaker.Elements.Textures.TextureArea
{
    [Serializable]
    public class AreaTextures : NotificationObject
    {
        private int _index;
        private string _name;
        private ObservableCollection<int> _list;
        private CollectionAreaTransitionTexture _areaTransitionTexture;
        private CollectionAreaTransitionItems _collectionAreaItems;

        #region Props

        public int Index { get { return _index; } set { _index = value; RaisePropertyChanged(() => Index); } }
        public ObservableCollection<int> List { get { return _list; } set { _list = value; RaisePropertyChanged(() => List); } }
        public String Name { get { return _name; } set { _name = value; RaisePropertyChanged(() => Name); } }
        public CollectionAreaTransitionTexture AreaTransitionTexture { get { return _areaTransitionTexture; } set { _areaTransitionTexture = value; RaisePropertyChanged(() => AreaTransitionTexture); } }
        public CollectionAreaTransitionItems CollectionAreaItems { get { return _collectionAreaItems; } set { _collectionAreaItems = value; RaisePropertyChanged(() => CollectionAreaItems); } }

        #endregion Props

        #region Ctor

        public AreaTextures()
        {
            Index = 0;
            List = new ObservableCollection<int>();
            AreaTransitionTexture = new CollectionAreaTransitionTexture();
            CollectionAreaItems = new CollectionAreaTransitionItems();
            Name = "";
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(() => List, info);
            Serialize(() => Name, info);
            Serialize(() => Index, info);
            Serialize(() => AreaTransitionTexture, info);
            Serialize(() => CollectionAreaItems, info);
        }

        protected AreaTextures(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            List = new ObservableCollection<int>(Deserialize(() => List, info));
            Name = Deserialize(() => Name, info);
            Index = Deserialize(() => Index, info);
            try
            {
                AreaTransitionTexture = Deserialize(() => AreaTransitionTexture, info);
                CollectionAreaItems = Deserialize(() => CollectionAreaItems, info);
            }
            catch (Exception)
            {
                CollectionAreaItems = new CollectionAreaTransitionItems();
                AreaTransitionTexture = new CollectionAreaTransitionTexture();
            }
        }

        #endregion Ctor
    }
}