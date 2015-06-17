using System.Collections.Generic;

namespace EssenceUDK.MapMaker.MapMaking
{
    public class MapObject
    {
        public byte Occupied;
        public short Texture;
        public sbyte Altitude;
        public List<ItemClone> Items;

        public void AddItem(int itemid, int hue, sbyte z)
        {
            if(Items == null)
                Items = new List<ItemClone>();

            var item = new ItemClone {Id = itemid, Hue = hue, Z = z};
            Items.Add(item);
        }

        public MapObject()
        {
            Occupied = 0;
            Texture = 0;
            Altitude = 0;
            Items = null;
        }
    }
}