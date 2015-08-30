using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.Textures.TextureArea;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace EssenceUDK.MapMaker.Elements.Textures
{
    [Serializable]
    public class CollectionAreaTexture : NotificationObject, IContainerSet
    {
        private ObservableCollection<AreaTextures> _list;

        public ObservableCollection<AreaTextures> List
        {
            get { return _list; }
            set
            {
                _list = value;
                Update(); RaisePropertyChanged(() => List);
            }
        }

        [XmlIgnore]
        [NonSerialized]
        public Dictionary<int, AreaTextures> _fast;

        public CollectionAreaTexture()
        {
            List = new ObservableCollection<AreaTextures>();
            Update();
        }

        #region Search Methods

        public AreaTextures FindByIndex(int id)
        {
            AreaTextures text = null;
            if (_fast != null)
                _fast.TryGetValue(id, out text);
            return text;
        }

        #endregion Search Methods

        public void InitializeSeaches()
        {
            _fast = new Dictionary<int, AreaTextures>();
            foreach (AreaTextures texturese in List)
            {
                _fast.Add(texturese.Index, texturese);
            }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(() => List, info);
        }

        protected CollectionAreaTexture(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            List = new ObservableCollection<AreaTextures>(Deserialize(() => List, info));
        }

        public List<int> Indexes
        {
            get
            {
                var list = List.Select(element => element.Index).ToList();
                return list;
            }
        }

        private void Update()
        {
            if (List != null)
                List.CollectionChanged += (element, arg) =>
                {
                    InitializeSeaches();
                    RaisePropertyChanged(() => Indexes);
                };
        }
    }
}