using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;

namespace EssenceUDK.MapMaker.Elements.ColorArea
{
    [Serializable]
    public class CollectionAreaColorMountains : IContainerSet
    {
        #region Props
        
        public ObservableCollection<AreaColor> List { get; set; }
        
        #endregion //Props
        
        #region Fields
        
        [NonSerialized] private Dictionary<Color, AreaColor> _mountainsDic;
        [NonSerialized] private Dictionary<int, AreaColor> _idDictionary;
        [NonSerialized] private Dictionary<Color, bool> _colordic;
        
        #endregion //Fields

        #region Ctor
        
        public CollectionAreaColorMountains()
        {
            List = new ObservableCollection<AreaColor>();
        }
        
        #endregion//Ctor

        #region search methods

        public AreaColor FindMountainByColor(Color color )
        {
            AreaColor a;
            _mountainsDic.TryGetValue(color, out a);
            return a;
        }

        public AreaColor FindMountainById(int  id)
        {
            AreaColor a;
            _idDictionary.TryGetValue(id, out a);
            return a;
        }


        public bool Contains(Color color)
        {
            bool a;
            _colordic.TryGetValue(color, out a);
            return a;
        }


        public void InitializeSeaches()
        {
            _colordic = new Dictionary<Color, bool>();
            _idDictionary = new Dictionary<int, AreaColor>();
            _mountainsDic = new Dictionary<Color, AreaColor>();
         
            foreach (var colorMountainse in List)
            {
                try
                {
                    _colordic.Add(colorMountainse.Color, true);
                    _mountainsDic.Add(colorMountainse.Color, colorMountainse);
                }
                catch
                {
                }
                _idDictionary.Add(colorMountainse.IndexTextureTop, colorMountainse);
            }
        }
        #endregion //Search Methods
    }
}
