// **********
// EssenceUDK - FakeTypes.cs
// **********

using System;
using System.Collections.Generic;

namespace EssenceUDK.Platform.DataTypes
{
    public class FakeTypes
    {
        private const int FAKE_TILE_NEVE = 1885;
        private const int FAKE_TILE_SABBIA = 857;
        private const int FAKE_TILE_TERRA = 9;
        private const int FAKE_TILE_VUOTO = 93;
        private const int FAKE_TILE_STRADA_MATTONI = 1172;

        public static readonly int[] TILE_MIX = {
            FAKE_TILE_NEVE,
            FAKE_TILE_SABBIA,
            FAKE_TILE_TERRA,
            FAKE_TILE_VUOTO,
            FAKE_TILE_STRADA_MATTONI
        };

        public class MicroMapFake : IMapFacet
        {
            private readonly FacetDesc _desc = new FacetDesc("Fake", 3, 3, 3, 3);
            public FacetDesc Desc => _desc;
            public uint Width => _desc.Width;
            public uint Height => _desc.Height;
            public uint Count => 3 * 3;

            public uint GetBlockId(uint blockX, uint blockY)
            {
                return 1;
            }

            public uint GetBlockId(int blockX, int blockY)
            {
                return 1;
            }

            public uint GetWorldBlockId(int blockX, int blockY)
            {
                return 1;
            }

            public uint FindBlockId(uint tileX, uint tileY)
            {
                return 1;
            }

            public IMapBlock this[uint index]
            {
                get => new MapBlockFake();
                set { }
            }

            public IMapTile this[uint tileX, uint tileY]
            {
                get => new MapTileFake();
                set { }
            }
        }

        public class ItemMapTileFake : IItemMapTile
        {
            public ushort TileId { get; set; }
            public sbyte Altitude { get; set; }
            public ushort Palette { get; set; }
        }

        public class LandMapTileFake : ILandMapTile
        {
            public ushort TileId { get; set; }
            public sbyte Altitude { get; set; }
        }


        public class MapTileFake : IMapTile
        {
            public ILandMapTile Land { get; set; } = new LandMapTileFake
            {
                TileId = FAKE_TILE_STRADA_MATTONI,
                Altitude = 0
            };


            public int Count
            {
                get => 1;
                set { }
            }

            public IItemMapTile this[int index]
            {
                get => new ItemMapTileFake()
                {
                    TileId = 100
                };
                set { }
            }

            public List<IItemMapTile> ItemsList { get; set; }

            public IItemMapTile Add(ushort tileid, ushort palette, sbyte altitude)
            {
                return null;
            }

            public IItemMapTile Add()
            {
                return null;
            }

            public void Remove(int index)
            {
            }
        }

        public class MapBlockDataFake : IMapBlockData
        {
            private readonly ILandMapTile[] _land;

            public MapBlockDataFake()
            {
                var tiles = new List<ILandMapTile>();
                for (var i = 0; i < 8 * 8; i++)
                {
                    var tile = new LandMapTileFake
                    {
                        TileId = FAKE_TILE_VUOTO,
                        Altitude = 0
                    };
                    tiles.Add(tile);
                }

                _land = tiles.ToArray();
            }

            public ILandMapTile[] Lands
            {
                get => _land;
                set { }
            }

            public IItemMapTile[][] Items { get; set; }
        }

        public class MapBlockFake : IMapBlock
        {
            public void Dispose()
            {
            }

            public uint GetTileId(uint offsetX, uint offsetY)
            {
                return 3;
            }

            public IMapTile this[uint index] => new MapTileFake
            {
                Land = new LandMapTileFake
                {
                    TileId = FAKE_TILE_NEVE,
                    Altitude = 0
                }
            };

            public IMapTile this[uint offsetX, uint offsetY]
            {
                get
                {
                    if (offsetX == 0)
                    {
                        return new MapTileFake()
                        {

                        };
                    }
                    else if(offsetY == 0)
                    {
                        return new MapTileFake()
                        {
                            Land = new LandMapTileFake()
                            {
                                TileId = FAKE_TILE_NEVE
                            }
                        };
                    }
                    else
                    {
                        return new MapTileFake()
                        {
                            Land = new LandMapTileFake()
                            {
                                TileId = FAKE_TILE_VUOTO
                            }
                        };
                    }
                    
                    
                }
            }

            public IMapBlockData GetData()
            {
                return new MapBlockDataFake();
            }

            public IMapFacet Parent { get; }
            public uint EntryId => 1;
        }
    }
}