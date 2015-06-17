using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.BaseTypes;

namespace EssenceUDK.MapMaker.Elements.ColorArea.ColorMountains
{
    [Serializable]
    public class AreaColorMountain : NotificationObject 
    {
        #region Props
        /// <summary>
        /// Color in the bitmap written in ColorArea
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// index of the group in ColorArea
        /// </summary>
        public int IndexMountainGroup { get; set; }
        /// <summary>
        /// Circles of automatic raise
        /// </summary>
        public ObservableCollection<CircleMountain> List { get; set; }
        /// <summary>
        /// Color of the mountains in the top
        /// </summary>
        public Color ColorMountain { get; set; }
        /// <summary>
        /// Index of what group is drawn in the top
        /// </summary>
        public int IndexGroupTop { get; set; }
        /// <summary>
        /// If the automatic raise is off or on
        /// </summary>
        public bool ModeAutomatic { get; set; }

        public string Name { get; set; }

        #endregion //Props

        #region Ctor

        public AreaColorMountain()
        {
            List = new ObservableCollection<CircleMountain>();
            ModeAutomatic = false;
            Color = Colors.Black;
            IndexGroupTop = 0;
            ColorMountain = Colors.Black;
            IndexMountainGroup = 0;
            Name = "";

        }

        #endregion //Ctor
    }
}
