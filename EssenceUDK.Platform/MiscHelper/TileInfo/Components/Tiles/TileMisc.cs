using System;
using EssenceUDK.Platform.MiscHelper.Components.Enums;

namespace EssenceUDK.Platform.MiscHelper.Components.Tiles
{
    [Serializable()]
    public class TileMisc : Tile
    {
        public TileMisc()
            :base()
        {
            Type = TypeTile.Misc;
        }
    }
}
