using System;
using System.Collections.Generic;
using EssenceUDK.Platform.MiscHelper.Components.Enums;

namespace EssenceUDK.Platform.MiscHelper.Components.Tiles
{
    [Serializable()]
    public class TileRoof :Tile
    {
        private static readonly List<string> Poslist= new List<string>()
                           {
                           "None",
                            "North",
                            "Eeast",
                            "South",
                            "West",
                            "NSCrosspiece",
                            "EWCrosspiece",
                            "NDent",
                            "EDent",
                            "SDent",
                            "WDent",
                            "NTPiece",
                            "ETPiece",
                            "STPiece",
                            "WTPiece",
                            "XPiece",
                            "ExtraPiece"
                           };
        #region Fields
        #endregion //Fields

        public override List<string> PosList
        {
            get { return Poslist; }
        }

        public TileRoof()
            :base()
        {
            Pos = PositionRoof.None.ToString();
            Type = TypeTile.Roofs;
        }

        public TileRoof(PositionRoof pos)
            :this()
        {
            Type = TypeTile.Roofs;
            Pos = pos.ToString();
        }

    }
}
