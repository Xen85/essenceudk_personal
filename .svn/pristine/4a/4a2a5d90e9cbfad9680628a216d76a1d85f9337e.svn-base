using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.Textures.TexureCliff;

namespace EssenceUDK.MapMaker.Elements.Textures
{
    [Serializable]
    public class CollectionAreaTransitionCliffTexture : NotificationObject, IContainerSet
    {
        private Color _color;
        private ObservableCollection<AreaTransitionCliffTexture> _list;
        #region Props

        public Color Color { get { return _color; } set { _color = value; RaisePropertyChanged(()=>Color);} }

        public ObservableCollection<AreaTransitionCliffTexture> List { get { return _list; } set { _list = value; RaisePropertyChanged(()=>List); } }

        #endregion // Props

        #region Ctor
        public CollectionAreaTransitionCliffTexture()
        {
            Color = Colors.White;
            List = new ObservableCollection<AreaTransitionCliffTexture>();
        }
        #endregion //Ctor

        #region Search Methods

        public IEnumerable<AreaTransitionCliffTexture> FindFromByColor(Color color)
        {
            return List.Where(textureCliff => textureCliff.ColorFrom == color);
        }

        public IEnumerable<AreaTransitionCliffTexture> FindToByColor(Color color)
        {
            return List.Where(textureCliff => textureCliff.ColorFrom != color && (textureCliff.ColorTo.Equals(color) || textureCliff.ColorEdge.Equals(color)));
        }

        public IEnumerable<AreaTransitionCliffTexture> FindByColor(Color color)
        {
            return List.Where(textureCliff => (textureCliff.ColorTo.Equals(color) || textureCliff.ColorFrom.Equals(color)  || textureCliff.ColorEdge.Equals(color)));
        }

        public IEnumerable<Color> AllColors()
        {
           return List.Select(t =>t.ColorFrom).Union(List.Select(t=>t.ColorTo)).Union(List.Select(t=>t.ColorEdge)).Distinct() ;
        }

        #endregion //Search Methods

        #region IContainerSet
        public void InitializeSeaches()
        {
        }
        #endregion

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(() => List, info);
        }

        protected CollectionAreaTransitionCliffTexture(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            List = new ObservableCollection<AreaTransitionCliffTexture>(Deserialize(() => List, info));
        }
    }
}
