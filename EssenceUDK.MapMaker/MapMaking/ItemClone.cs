using EssenceUDK.MapMaker.Elements.Items.Items;

namespace EssenceUDK.MapMaker.MapMaking
{
    public struct ItemClone
    {
        
        private sbyte _z;
        private int _hue;
        private int _id;

        public int Id { get { return _id; } set { _id = value; } }

        public sbyte Z { get { return _z; } set { _z = value; } }

        public int Hue { get { return _hue; } set { _hue = value; } }

        public ItemClone(SingleItem item)
        {
            _z = (sbyte)item.Z;
            _id = item.Id;
            _hue = item.Hue;
        }

        public SingleItem ToSingleItem()
        {
            return new SingleItem() { Id = Id, Hue = _hue, X = 0, Y = 0, Z = Z };
        }
    }
}