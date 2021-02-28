using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.Textures.TextureArea;
using System.Linq;

namespace EssenceUDK.MapMaker.Elements.Textures
{
    [Serializable]
    public class CollectionAreaTexture : NotificationObject, IContainerSet
    {
        private ObservableCollection<AreaTextures> m_List;
        public ObservableCollection<AreaTextures> List { get { return m_List; } set { m_List = value;
            Update();RaisePropertyChanged(()=>List); } }
        
        [XmlIgnore]
        [NonSerialized] public Dictionary<int, AreaTextures> Fast; 
        public CollectionAreaTexture()
        {
            List = new ObservableCollection<AreaTextures>();
            Update();
        }

        #region Search Methods

        public AreaTextures FindByIndex(int id )
        {
            AreaTextures text = null;
            Fast?.TryGetValue(id,out text);
            return text;
        }
        
        #endregion

        public void InitializeSeaches()
        {
            Fast = new Dictionary<int, AreaTextures>();
            foreach (AreaTextures texturese in List)
            {
                Fast.Add(texturese.Index, texturese);
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
