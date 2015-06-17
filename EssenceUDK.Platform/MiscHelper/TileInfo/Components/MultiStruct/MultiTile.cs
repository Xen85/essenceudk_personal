using System;
using EssenceUDK.Platform.MiscHelper.Components;
using EssenceUDK.Platform.MiscHelper.Components.Base;
using EssenceUDK.Platform.MiscHelper.Components.Interface;

namespace EssenceUDK.Platform.MiscHelper.TileInfo.Components.MultiStruct
{
    [Serializable]
    public class MultiTile : NotificationObject,ITile
    {
        #region Fields
        
        private Tile _tile;
        
        private int _offsetX;
        
        private int _offsetY;
        
        private int _z;
        
        private uint _id;
        
        private int _flag;
        
        #endregion //Fields

        #region Props
        
        public Tile Tile { get { return _tile; } set { _tile = value; RaisePropertyChanged(()=>Tile); } }
        
        public int X { get { return _offsetX; } set { _offsetX = value; RaisePropertyChanged(()=>X);} }
        
        public int Y { get { return _offsetY; } set { _offsetY = value; RaisePropertyChanged(()=>Y); } }
        
        public int Z { get { return _z; } set { _z = value; RaisePropertyChanged(()=>Z); } }
        
        public uint Id { get { return _id; } set { _id = value; RaisePropertyChanged(()=>Id); } }
        
        public int Flag { get { return _flag; } set { _flag = value; RaisePropertyChanged(()=>Flag); } }
        
        #endregion //Pros

        #region Ctor
        
        public void SetTile(Tile tile)
        {
            _tile = tile;
            _id = tile.Id;
        }

        public MultiTile()
        {
            
        }

        public MultiTile(MultiTileStruct str)
        {
            _offsetX = str.OffsetX;
            _offsetY = str.OffsetY;
            _id = str.Id;
            _flag = str.Flag;
        }


        #endregion //Ctor
    }


    public struct MultiTileStruct
    {
        public int OffsetX { get; set; }

        public int OffsetY { get; set; }

        public int Z { get; set; }

        public uint Id { get; set; }

        public int Flag { get; set; }

    }

}