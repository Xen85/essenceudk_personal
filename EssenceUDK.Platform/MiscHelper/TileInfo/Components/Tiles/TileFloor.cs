using System;
using System.Collections.Generic;
using EssenceUDK.Platform.MiscHelper.Components.Enums;

namespace EssenceUDK.Platform.MiscHelper.Components.Tiles
{
    [Serializable()]
    public class TileFloor : Tile
    {
        private static readonly List<string> RoofList = new List<string>()
                           {
                               "None",
                                "F1",
                                "F2",
                                "F3",
                                "F4",
                                "F5",
                                "F6",
                                "F7",
                                "F8",
                                "F9",
                                "F10",
                                "F11",
                                "F12",
                                "F13",
                                "F14",
                                "F15",
                                "F16"
                           };


        #region Fields
        #endregion //Fields
        #region Props
        #endregion //Props

        #region Ctor
        
        public TileFloor()
            :base()
        {
            Type = TypeTile.Floor;
        }

        public TileFloor(PositionFloors positionFloors)
            :this()
        {
            Pos = positionFloors.ToString();
        }

        public override List<string> PosList
        {
            get { return RoofList; }
        }

        #endregion //Ctor

        
        
    }
}
