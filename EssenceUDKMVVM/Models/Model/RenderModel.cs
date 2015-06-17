using System;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.Models.Model
{
    [Serializable]
    public class RenderModel
    {
        #region fields
        public ushort Width;
        public ushort Height;
        public byte Map;
        public byte Range;
        public ushort X;
        public ushort Y;
        public sbyte MinZ;
        public sbyte MaxZ;
        public short SeaLevel;
        public bool Flat;
        #endregion
    }
}
