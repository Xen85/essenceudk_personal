using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EssenceUDK.MapMaker.Elements.Textures
{
    [Serializable]
    public class CollectionAreaTransitionTexture : NotificationObject, IContainerSet
    {
        private ObservableCollection<AreaTransitionTexture> _list;

        #region Props

        public ObservableCollection<AreaTransitionTexture> List
        {
            get { return _list; }
            set
            {
                _list = value;
                Update(); RaisePropertyChanged(() => List);
            }
        }

        #endregion Props

        #region Fields

        [NonSerialized]
        private Dictionary<int, AreaTransitionTexture> _dictionaryFindIndex;

        private bool init = false;

        #endregion Fields

        #region Ctor

        public CollectionAreaTransitionTexture()
        {
            List = new ObservableCollection<AreaTransitionTexture>();
            Update();
        }

        #endregion Ctor

        #region Search Methods

        public AreaTransitionTexture FindById(int id)
        {
            AreaTransitionTexture result;
            _dictionaryFindIndex.TryGetValue(id, out result);
            return result;
        }

        #endregion Search Methods

        #region IContainerSet

        public void InitializeSeaches()
        {
            if (init)
                return;
            _dictionaryFindIndex = new Dictionary<int, AreaTransitionTexture>();
            foreach (var textureSmooth in List)
            {
                try
                {
                    _dictionaryFindIndex.Add(textureSmooth.TextureIdTo, textureSmooth);
                }
                catch (Exception)
                {
                }
            }
            init = true;
        }

        #endregion IContainerSet

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(() => List, info);
        }

        protected CollectionAreaTransitionTexture(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            List = new ObservableCollection<AreaTransitionTexture>(Deserialize(() => List, info));
        }

        private List<int> TextureToIndexes
        {
            get
            {
                return List.Select(o => o.TextureIdTo).ToList();
            }
        }

        private void Update()
        {
            if (List != null)
            {
                List.CollectionChanged += (sender, arg) =>
                {
                    InitializeSeaches();
                    RaisePropertyChanged(() => TextureToIndexes);
                };
            }
        }
    }
}