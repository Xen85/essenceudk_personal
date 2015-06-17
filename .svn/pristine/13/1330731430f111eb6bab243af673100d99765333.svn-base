using System;
using EssenceUDK.Platform.MiscHelper.Components.Enums;

namespace EssenceUDK.Platform.MiscHelper.Components.Tiles
{
    [Serializable()]
    public class TileStairs : Tile
    {
        #region Fields

        private PositionStairs _positionStairs;
        private Boolean _isMulti;
        #endregion //Fields
        public PositionStairs PositionStair { get { return _positionStairs; } set { _positionStairs = value; RaisePropertyChanged(() => PositionStair); } }
        public Boolean IsMulti { get { return _isMulti; } set { _isMulti = value; RaisePropertyChanged(()=>IsMulti); } }

        public TileStairs()
            :base()
        {
            IsMulti = false;
            _positionStairs = PositionStairs.None;
        }

        public TileStairs(Boolean multi, PositionStairs pos)
            :this()
        {
            _isMulti = multi;
            _positionStairs = pos;
        }

    }
}
