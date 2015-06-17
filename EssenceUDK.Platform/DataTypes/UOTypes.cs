﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using EssenceUDK.Platform.Factories;

namespace EssenceUDK.Platform.DataTypes
{
    public struct FacetDesc
    {
        public readonly string Name;
        public readonly ushort Width;
        public readonly ushort Height;
        public readonly ushort RealWidth;
        public readonly ushort RealHeight;
        
        public FacetDesc(string name, ushort width, ushort height, ushort rwidth, ushort rheight)
        {
            Name       = name;
            Width      = width;
            Height     = height;
            RealWidth  = rwidth;
            RealHeight = rheight;
        }

        // OSI Maps
        public static readonly FacetDesc PreAlpha   = new FacetDesc("Britania",  128,  128,  128,  128);
        public static readonly FacetDesc OldFelucca = new FacetDesc("Felucca",   768,  512,  640,  512);
        public static readonly FacetDesc NewFelucca = new FacetDesc("Felucca",   896,  512,  640,  512);
        public static readonly FacetDesc ExtFelucca = new FacetDesc("Felucca",   896,  512,  640,  512);
        public static readonly FacetDesc OldTrammel = new FacetDesc("Trammel",   768,  512,  640,  512);
        public static readonly FacetDesc NewTrammel = new FacetDesc("Trammel",   896,  512,  640,  512);
        public static readonly FacetDesc ExtTrammel = new FacetDesc("Trammel",   896,  512,  640,  512);
        public static readonly FacetDesc Ilshenar   = new FacetDesc("Ilshenar",  288,  200,  288,  200);
        public static readonly FacetDesc Malas      = new FacetDesc("Malas",     320,  256,  320,  256);
        public static readonly FacetDesc Tokuno     = new FacetDesc("Tokuno",    181,  181,  181,  181);
        public static readonly FacetDesc TerMur     = new FacetDesc("TerMur",    160,  512,  160,  512);

        // Quint maps
        public static readonly FacetDesc Dangeons   = new FacetDesc("Dangeons",  896,  512, 896, 512);
        public static readonly FacetDesc Assidiya   = new FacetDesc("Assidiya", 1536, 1024, 1536, 1024);
    }

    /// <summary>
    /// An enumeration of 64 different tile flags.
    /// </summary>
    [Flags]
    public enum TileFlag : ulong
    {
        /// <summary>
        /// Nothing is flagged.
        /// </summary>
        None            = 0x0000000000000000,
        /// <summary>
        /// Not yet documented.
        /// </summary>
        Background      = 0x0000000000000001,
        /// <summary>
        /// Not yet documented.
        /// </summary>
        Weapon          = 0x0000000000000002,
        /// <summary>
        /// Not yet documented.
        /// </summary>
        Transparent     = 0x0000000000000004,
        /// <summary>
        /// The tile is rendered with partial alpha-transparency.
        /// </summary>
        Translucent     = 0x0000000000000008,
        /// <summary>
        /// The tile is a wall.
        /// </summary>
        Wall            = 0x0000000000000010,
        /// <summary>
        /// The tile can cause damage when moved over.
        /// </summary>
        Damaging        = 0x0000000000000020,
        /// <summary>
        /// The tile may not be moved over or through.
        /// </summary>
        Impassable      = 0x0000000000000040,
        /// <summary>
        /// Not yet documented.
        /// </summary>
        Wet             = 0x0000000000000080,
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown1        = 0x0000000000000100,
        /// <summary>
        /// The tile is a surface. It may be moved over, but not through.
        /// </summary>
        Surface         = 0x0000000000000200,
        /// <summary>
        /// The tile is a stair, ramp, or ladder.
        /// </summary>
        Bridge          = 0x0000000000000400,
        /// <summary>
        /// The tile is stackable
        /// </summary>
        Generic         = 0x0000000000000800,
        /// <summary>
        /// The tile is a window. Like <see cref="TileFlag.NoShoot" />, tiles with this flag block line of sight.
        /// </summary>
        Window          = 0x0000000000001000,
        /// <summary>
        /// The tile blocks line of sight.
        /// </summary>
        NoShoot         = 0x0000000000002000,
        /// <summary>
        /// For single-amount tiles, the string "a " should be prepended to the tile name.
        /// </summary>
        ArticleA        = 0x0000000000004000,
        /// <summary>
        /// For single-amount tiles, the string "an " should be prepended to the tile name.
        /// </summary>
        ArticleAn       = 0x0000000000008000,
        /// <summary>
        /// Not yet documented.
        /// </summary>
        Internal        = 0x0000000000010000,
        /// <summary>
        /// The tile becomes translucent when walked behind. Boat masts also have this flag.
        /// </summary>
        Foliage         = 0x0000000000020000,
        /// <summary>
        /// Only gray pixels will be hued
        /// </summary>
        PartialHue      = 0x0000000000040000,
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown2        = 0x0000000000080000,
        /// <summary>
        /// The tile is a map--in the cartography sense. Unknown usage.
        /// </summary>
        Map             = 0x0000000000100000,
        /// <summary>
        /// The tile is a container.
        /// </summary>
        Container       = 0x0000000000200000,
        /// <summary>
        /// The tile may be equiped.
        /// </summary>
        Wearable        = 0x0000000000400000,
        /// <summary>
        /// The tile gives off light.
        /// </summary>
        LightSource     = 0x0000000000800000,
        /// <summary>
        /// The tile is animated.
        /// </summary>
        Animation       = 0x0000000001000000,
        /// <summary>
        /// Gargoyles can fly over
        /// </summary>
        HoverOver       = 0x0000000002000000,
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown3        = 0x0000000004000000,
        /// <summary>
        /// Not yet documented.
        /// </summary>
        Armor           = 0x0000000008000000,
        /// <summary>
        /// The tile is a slanted roof.
        /// </summary>
        Roof            = 0x0000000010000000,
        /// <summary>
        /// The tile is a door. Tiles with this flag can be moved through by ghosts and GMs.
        /// </summary>
        Door            = 0x0000000020000000,
        /// <summary>
        /// Not yet documented.
        /// </summary>
        StairBack       = 0x0000000040000000,
        /// <summary>
        /// Not yet documented.
        /// </summary>
        StairRight      = 0x0000000080000000,
        UnUsed01        = 0x0000000100000000,
        /// <summary>
        /// Чтото для ноудрау тайла
        /// </summary>
        Unknown4        = 0x0000000200000000,
        UnUsed03        = 0x0000000400000000,
        /// <summary>
        /// 
        /// </summary>
        Unknown5        = 0x0000000800000000,
        /// <summary>
        /// 
        /// </summary>
        Unknown6        = 0x0000001000000000,
        /// <summary>
        /// 
        /// </summary>
        Unknown7        = 0x0000002000000000,
        UnUsed07        = 0x0000004000000000,

        UnUsed08        = 0x0000008000000000,
        /// <summary>
        /// Чтото связаное с мачтами
        /// </summary>
        Unknown8        = 0x0000010000000000,
        UnUsed10        = 0x0000020000000000,
        UnUsed11        = 0x0000040000000000,
        UnUsed12        = 0x0000080000000000,
        UnUsed13        = 0x0000100000000000,
        UnUsed14        = 0x0000200000000000,
        UnUsed15        = 0x0000400000000000,
        UnUsed16        = 0x0000800000000000,
        UnUsed17        = 0x0001000000000000,
        UnUsed18        = 0x0002000000000000,
        UnUsed19        = 0x0004000000000000,
        UnUsed20        = 0x0008000000000000,
        UnUsed21        = 0x0010000000000000,
        UnUsed22        = 0x0020000000000000,
        UnUsed23        = 0x0040000000000000,
        UnUsed24        = 0x0080000000000000,
        UnUsed25        = 0x0100000000000000,
        UnUsed26        = 0x0200000000000000,
        UnUsed27        = 0x0400000000000000,
        UnUsed28        = 0x0800000000000000,
        UnUsed29        = 0x1000000000000000,
        UnUsed30        = 0x2000000000000000,
        UnUsed31        = 0x4000000000000000,
        UnUsed32        = 0x8000000000000000
    }

    public interface ITileData
    {
    }

    public interface ILandData  : ITileData
    {
        string     Name         { get; set; }
        TileFlag   Flags        { get; set; }
        ushort     TexID        { get; set; }
    }

    public interface IItemData  : ITileData
    {
        string     Name         { get; set; }
        TileFlag   Flags        { get; set; }
        byte       Height       { get; set; }
        byte       Quality      { get; set; }
        byte       Quantity     { get; set; }
        ushort     Animation    { get; set; }
        byte       StackingOff  { get; set; }
    }

    public interface IEntryData
    {
        uint       EntryId      { get; set; }
        bool       IsValid      { get; }
    }

    public interface IEntrySurf : IEntryData
    {
        ISurface   Surface      { get; set; }
    }

    public interface IEntryTile : IEntrySurf, ITileData
    {
    }

    public interface ILandTile  : IEntryTile, ILandData
    {
        ISurface   Texture      { get; set; }
    }

    public interface IItemTile  : IEntryTile, IItemData
    { 
    }

    public interface IGumpEntry : IEntrySurf
    {
    }

    public abstract class EntryTile : IEntryTile
    {
        protected uint      _EntryId;
        protected ISurface  _Surface;
        protected ITileData _TileData;
        protected bool      _IsValid;

        //protected Color   RadarColor;

        protected IDataFactory dataFactory;
        internal  EntryTile(uint id, IDataFactory factory, ITileData tiledata, bool valid)
        {
            _EntryId    = id;
            dataFactory = factory;
            _TileData   = tiledata;
            _IsValid    = valid;
        }

        public uint EntryId { get { return _EntryId; } set { _EntryId = value; } }
        public bool IsValid { get { return _IsValid; } }
        public abstract ISurface Surface { get; set; }
    }

    public sealed class ItemTile  : EntryTile, IItemTile, IItemData
    {
        public override ISurface     Surface {
            get { return _Surface ?? (_Surface = (dataFactory as IDataFactoryReader).GetItemSurface(_EntryId)); }
            set { (dataFactory as IDataFactoryWriter).SetItemSurface(_EntryId, _Surface = value); }
        }

        string   IItemData.Name         { get { return ((IItemData)_TileData).Name; }           set { ((IItemData)_TileData).Name = value; } }
        TileFlag IItemData.Flags        { get { return ((IItemData)_TileData).Flags; }          set { ((IItemData)_TileData).Flags = value; } }
        byte     IItemData.Height       { get { return ((IItemData)_TileData).Height; }         set { ((IItemData)_TileData).Height = value; } }
        byte     IItemData.Quality      { get { return ((IItemData)_TileData).Quality; }        set { ((IItemData)_TileData).Quality = value; } }
        byte     IItemData.Quantity     { get { return ((IItemData)_TileData).Quantity; }       set { ((IItemData)_TileData).Quantity = value; } }
        ushort   IItemData.Animation    { get { return ((IItemData)_TileData).Animation; }      set { ((IItemData)_TileData).Animation = value; } }
        byte     IItemData.StackingOff  { get { return ((IItemData)_TileData).StackingOff; }    set { ((IItemData)_TileData).StackingOff = value; } }

        internal  ItemTile(uint id, IDataFactory factory, IItemData tiledata, bool valid) : base(id, factory, tiledata, valid)
        {          
        }
    }

    public sealed class LandTile  : EntryTile, ILandTile, ILandData
    {
        public override ISurface     Surface {
            get { return _Surface ?? (_Surface = (dataFactory as IDataFactoryReader).GetLandSurface(_EntryId)); }
            set { (dataFactory as IDataFactoryWriter).SetLandSurface(_EntryId, _Surface = value); }
        }

        public          ISurface     Texture {
            get { return _Texture ?? (_Texture = (dataFactory as IDataFactoryReader).GetTexmSurface(((ILandData)_TileData).TexID)); }
            set { (dataFactory as IDataFactoryWriter).SetTexmSurface(((ILandData)_TileData).TexID, _Texture = value); }
        }
        protected ISurface _Texture;

        string   ILandData.Name         { get { return ((ILandData)_TileData).Name; }           set { ((ILandData)_TileData).Name = value; } }
        TileFlag ILandData.Flags        { get { return ((ILandData)_TileData).Flags; }          set { ((ILandData)_TileData).Flags = value; } }
        ushort   ILandData.TexID        { get { return ((ILandData)_TileData).TexID; }          set { ((ILandData)_TileData).TexID = value; } }

        internal LandTile(uint id, IDataFactory factory, ILandData tiledata, bool valid) : base(id, factory, tiledata, valid)
        {
        }
    }

    public sealed class GumpEntry : IGumpEntry
    {
        protected uint _EntryId;
        protected ISurface _Surface;
        protected bool _IsValid;

        protected IDataFactory dataFactory;
        internal GumpEntry(uint id, IDataFactory factory, bool valid)
        {
            _EntryId    = id;
            dataFactory = factory;
            _IsValid    = valid;
        }

        public uint EntryId { get { return _EntryId; } set { _EntryId = value; } }
        public bool IsValid { get { return _IsValid; } }
        
        public ISurface Surface {
            get { return _Surface ?? (_Surface = (dataFactory as IDataFactoryReader).GetGumpSurface(_EntryId)); }
            set { (dataFactory as IDataFactoryWriter).SetGumpSurface(_EntryId, _Surface = value); }
        }
    }

    public interface IAnimation
    {     
    }

    public sealed class Animation : IAnimation
    {
        private uint        _AnimId;
        private AnimEntry[] _Entries;
    }

    public sealed class AnimEntry
    {
        private uint        _AnimId;
        private Animation   _Parent;
        private byte        _Action;
        private byte        _Direct;
        private IPalette    _Palette;
        private AnimFrame[] _Frames;
    }

    public sealed class AnimFrame
    {
        private AnimEntry   _Parent;
        private IPalette    _Palette;
        private ISurface    _Surface;
        private short       _SCentrX;
        private short       _SCentrY;
    }




    // ---- Maps -----------------------------------------------------------------------------------

    public interface IEntryMapTile
    {
        ushort TileId   { get; set; }
        sbyte  Altitude { get; set; }
    }

    public interface ILandMapTile : IEntryMapTile
    {
    }

    public interface IItemMapTile : IEntryMapTile
    {
        ushort Palette { get; set; }
    }

    public sealed class LandMapTile : ILandMapTile
    {
        private ushort _TileId;
        private sbyte  _Altitude;

        public ushort TileId   { get { return _TileId; }   set { _TileId = value; } }
        public sbyte  Altitude { get { return _Altitude; } set { _Altitude = value; } }

        public LandMapTile(ushort tileid, sbyte altitude)
        {
            _TileId   = tileid;
            _Altitude = altitude;
        }
    }

    public sealed class ItemMapTile : IItemMapTile
    {
        private ushort _TileId;
        private ushort _Palette;
        private sbyte  _Altitude;

        public ushort TileId   { get { return _TileId; }   set { _TileId = value; } }
        public ushort Palette  { get { return _Palette; }  set { _Palette = value; } }
        public sbyte  Altitude { get { return _Altitude; } set { _Altitude = value; } }

        public ItemMapTile(ushort tileid, ushort palette, sbyte altitude)
        {
            _TileId   = tileid;
            _Palette  = palette;
            _Altitude = altitude;
        }
    }

    public interface IMapTile
    {
        ILandMapTile    Land  { get; }
        int             Count { get; }
        IItemMapTile    this[int index] { get; }
        List<IItemMapTile> ItemsList { get; }

        IItemMapTile    Add(ushort tileid, ushort palette, sbyte altitude);
        IItemMapTile    Add();
        void            Remove(int index);
    }

    public sealed class MapTile : IMapTile
    {
        private ILandMapTile        _Land;
        private List<IItemMapTile>  _Items;
        private MapBlock            _Parent;

        public ILandMapTile Land { get { return _Land; } }
        public int Count { get { return _Items.Count; } }
        public IItemMapTile this[int index] { get { return index < _Items.Count ? _Items[index] : null; } }

        public List<IItemMapTile> ItemsList { get {return _Items; } }

        public IItemMapTile Add()
        {
            return Add(0, 0, 0);
        }

        public IItemMapTile Add(ushort tileid, ushort palette, sbyte altitude)
        {
            var tile = new ItemMapTile(tileid, palette, altitude);
            _Items.Add(tile);
            return tile;
        }

        public void Remove(int index)
        {
            if (index < _Items.Count)
                _Items.RemoveAt(index);
        }

        public MapTile(MapBlock parent, ILandMapTile land, IItemMapTile[] items)
        {
            _Parent = parent;
            _Land   = land;
            _Items  = items != null ? new List<IItemMapTile>(items) : new List<IItemMapTile>(0);
        }
    }

    public interface IMapBlockData
    {
        LandMapTile[]   Lands { get; }
        ItemMapTile[][] Items { get; }
    }

    public interface IMapBlock : IDisposable
    {
        uint GetTileId(uint offsetX, uint offsetY);
        IMapTile this[uint index] { get; }
        IMapTile this[uint offsetX, uint offsetY] { get; }
        IMapBlockData GetData();
        MapFacet Parent { get; }
        uint EntryId { get; }
        //bool LandPatch { get; }
        //bool ItemPatch { get; }
    }

    public sealed class MapBlock : IMapBlock
    {
        private uint _EntryId;
        private MapTile[] _Tiles;
        //private MapTile[] _Patch;
        private MapFacet _Parent;

        //private bool _ItemPatch;
        //private bool _LandPatch;

        

        //public bool LandPatch { get { return _LandPatch; } }
        //public bool ItemPatch { get { return _ItemPatch; } }

        public MapFacet Parent { get { return _Parent; } }
        public uint EntryId { get { return _EntryId; } }

        public uint GetTileId(uint offsetX, uint offsetY)
        {
            return (offsetY << 3) + offsetX;
        }

        public IMapTile this[uint index] {
            get { return index < 64 ? _Tiles[index] : null;  }
            set { if (index < 64) _Tiles[index] = (value as MapTile); }
        }

        public IMapTile this[uint offsetX, uint offsetY] {
            get { return this[GetTileId(offsetX, offsetY)]; }
            set {
                var tile = this[GetTileId(offsetX, offsetY)];
                if (tile != null) tile = value;
            }
        }

        public MapBlock(MapFacet parent, uint index, IMapBlockData data)
        {
            _Parent = parent;
            _EntryId = index;
            //_Patch = null;
            _Tiles = new MapTile[64];
            for (int i = 0; i < 64; ++i)
                _Tiles[i] = new MapTile(this, data.Lands[i], data.Items[i]);
        }

        public MapBlock(MapFacet parent, sbyte altitude)
        {
            _Parent = parent;
            _EntryId = 0xFFFFFFFFu;
            _Tiles = new MapTile[64];
            for (int i = 0; i < 64; ++i)
                _Tiles[i] = new MapTile(this, new LandMapTile(0x0002, altitude), null);
        }

        public IMapBlockData GetData()
        {
            return new ClassicFactory.MapBlockData(0, _Tiles);
        }

        //public IMapBlockData GetPatchData()
        //{
        //    return new ClassicFactory.MapBlockData(0, _Patch);
        //}

        public void Dispose()
        {
            _Tiles = null;
            _Parent[_EntryId] = null;
        }
    }

    public interface IMapFacet
    {
        FacetDesc Desc { get; }
        uint Width     { get; }
        uint Height    { get; }
        uint Count     { get; }
        uint GetBlockId(uint blockX, uint blockY);
        uint GetBlockId(int blockX, int blockY);
        uint GetWorldBlockId(int blockX, int blockY);
        uint FindBlockId(uint tileX, uint tileY);
        IMapBlock this[uint index] { get; }
        IMapTile this[uint tileX, uint tileY] { get; }
    }

    public sealed class MapFacet : IMapFacet
    {
        private MapBlock[] _Blocks;
        private FacetDesc _Desc;
        private uint _Width;
        private uint _Height;
        private byte _MapIndex;
        private IDataFactory dataFactory;

        public FacetDesc Desc { get { return _Desc; } }
        public uint Width  { get { return _Width; } }
        public uint Height { get { return _Height; } }
        public uint Count  { get { return (uint)_Blocks.Length; } }
        public byte MapIndex { get { return _MapIndex; } }

        public uint GetBlockId(uint blockX, uint blockY)
        {
            return (blockX * _Height) + blockY;
        }

        public uint GetBlockId(int blockX, int blockY)
        {
            if (blockX < 0 || blockY < 0) return 0xFFFFFFFFu;
            if (blockX >= Width || blockY >= Height) return 0xFFFFFFFFu;
            return (uint)((blockX * _Height) + blockY);
        }

        public uint GetWorldBlockId(int blockX, int blockY)
        {
            if (blockX < 0)
                blockX += (int)Width;
            else if (blockX >= Width)
                blockX -= (int)Width;
            if (blockY < 0)
                blockY += (int)Height;
            else if (blockY >= Height)
                blockY -= (int)Height;
            return (uint)((blockX * _Height) + blockY);
        }

        public uint FindBlockId(uint tileX, uint tileY)
        {
            return ((tileX >> 3) * _Height) + (tileY >> 3);
        }

        public IMapBlock this[uint index] {
            get { return index < _Blocks.Length ? _Blocks[index] ?? 
                (_Blocks[index] = new MapBlock(this, (uint)index, (dataFactory as IDataFactoryReader).GetMapBlock(_MapIndex, index))) : null;  }
            set { if (index < _Blocks.Length) _Blocks[index] = (value as MapBlock); }
        }

        public IMapTile this[uint tileX, uint tileY] {
            get {
                var block = this[FindBlockId(tileX, tileY)] as MapBlock;
                if (block == null) return null;
                return block[tileX % 8, tileY % 8]; 
            }
            set {
                var tile = this[tileX, tileY];
                if (tile != null) tile = value;
            }
        }

        public MapFacet(IDataFactory factory, byte mapindex, FacetDesc desc)
        {
            _Desc   = desc;
            _Width  = desc.Width;
            _Height = desc.Height;
            _Blocks = new MapBlock[_Width * _Height];
            dataFactory = factory;
            _MapIndex = mapindex;
        }
    }





    /*


    public sealed class LandMapBlock : MapBlock
    {
        internal LandMapBlock(uint id, IDataFactory factory) : base(id, factory)
        {
        }

        public uint Header {
            get { return _Header; }
            set { (dataFactory as IDataFactoryWriter).SetLandMapTile(_EntryId, _Header = value); }
        }
        protected uint _Header;

        public   ILandMapTile[]     Tiles {
            get { return _Tiles ?? (_Tiles = (dataFactory as IDataFactoryReader).GetLandMapTile(_EntryId, out _Header)); }
            set { (dataFactory as IDataFactoryWriter).SetLandMapTile(_EntryId, _Header, _Tiles = value); }
        }
        protected ILandMapTile[] _Tiles;

        public ILandMapTile this[uint id] {
            get { return Tiles[id]; }
            set { Tiles[id] = value; }
        }
    }
    */
}
