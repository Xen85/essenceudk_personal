using System;

namespace EssenceUDK.Platform.MiscHelper.Components.Tiles
{
    [Serializable()]
    public class TileDoor : Tile
    {
        public TileDoor()
            :base()
        {
            Type = Enums.TypeTile.Doors;
        }
    }
}
