﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;

namespace EssenceUDK.MapMaker.Elements.Textures
{
    [Serializable]
    public class CollectionAreaTransitionTexture : NotificationObject, IContainerSet
    {
        private ObservableCollection<AreaTransitionTexture> m_List;
        #region Props

        public ObservableCollection<AreaTransitionTexture> List { get => m_List;
            set { m_List = value;
            Update();
            RaisePropertyChanged(() => List); } }
        
        #endregion
        
        #region Fields
        
        [NonSerialized] private Dictionary<int, AreaTransitionTexture> m_DictionaryFindIndex;
        private bool m_Init = false;
        #endregion

        #region Ctor

        public CollectionAreaTransitionTexture()
        {
            List = new ObservableCollection<AreaTransitionTexture>();
            Update();
        }
        #endregion

        #region Search Methods

        public AreaTransitionTexture FindById(int id)
        {
            AreaTransitionTexture result;
            m_DictionaryFindIndex.TryGetValue(id, out result);
            return result;
        }
        
        #endregion

        #region IContainerSet
        
        public void InitializeSeaches()
        {
            if (m_Init)
                return;
            m_DictionaryFindIndex = new Dictionary<int, AreaTransitionTexture>();
            foreach (var textureSmooth in List)
            {
                try
                {
                    m_DictionaryFindIndex.Add(textureSmooth.TextureIdTo,textureSmooth);
                }
                catch (Exception)
                {
                }
            }
            m_Init = true;
        }
        #endregion //IContainerSet

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(() => List, info);
        }

        protected CollectionAreaTransitionTexture(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            List = new ObservableCollection<AreaTransitionTexture>(Deserialize(() => List, info));
        }


        List<int> TextureToIndexes
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
