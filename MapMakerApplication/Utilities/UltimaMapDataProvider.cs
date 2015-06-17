using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EssenceUDK.MapMaker.Elements.Interfaces;
using EssenceUDK.Platform.DataTypes;

namespace MapMakerApplication.Utilities
	{
		public class UltimaMapDataProvider : IUltimaMapDataProvider
		{
			private uint _x, _y;
			private IMapFacet _map;
			private IList<IItemMapTile> _list;
			private int _listIndex = 0;
			private int _mapIndex;
			private IMapBlock _block;
			private ushort _land;
			private sbyte _altitude;

			/// <summary>
			/// 
			/// </summary>
			public int MapIndex {
				set
				{
					_mapIndex = value;
					_map = ApplicationController.manager.GetMapFacet(value);
				}
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="index"></param>
			/// <param name="facet"></param>
			public void mapIndex(int index, IMapFacet facet)
			{
				_mapIndex = index;
				_map = facet;
			}

			/// <summary>
			/// Land Tile Id
			/// </summary>
			public ushort Land { get { return _land;  } }

			/// <summary>
			/// Land Tile Altitude
			/// </summary>
			public sbyte Atitude { get { return _altitude; } }
		

			/// <summary>
			/// Changes Coordinates
			/// </summary>
			/// <param name="x">x coord</param>
			/// <param name="y">y coord</param>
			public void GetCoordinates(uint x, uint y)
			{
				if (_block == null || (x/8 != _x/8 || y/8 != _y/8))
				{
				if ( _block != null ) _block.Dispose();
					_block = _map[_map.FindBlockId(x, y)];
				}
				_y = y;
				_x = x;
				var tile = _block[_x%8, y%8];
				_land = tile.Land.TileId;
				_altitude = tile.Land.Altitude;
				_list = tile.ItemsList;
				_listIndex = 0;

			}

			/// <summary>
			/// Used for Items
			/// </summary>
			/// <returns>true if there are more items</returns>
			public bool HashNextItem()
			{
				if (_list == null) return false;
				if (_list.Count == 0) return false;
				return _listIndex < _list.Count - 1;
			}

			/// <summary>
			/// GetNext Items values
			/// </summary>
			/// <param name="altitude">Item z</param>
			/// <param name="id">Item Id</param>
			/// <param name="palette">Item Palette</param>
			public void GetNextItem(out sbyte altitude, out int id, out int palette)
			{
				var item = _list[_listIndex];
				altitude = item.Altitude;
				id = item.TileId;
				palette = item.Palette;
				_listIndex++;
			}
			/// <summary>
			/// Returns Last Item values
			/// </summary>
			/// <param name="altitude">Item z</param>
			/// <param name="id">item Id</param>
			/// <param name="palette">item palette</param>
			public void GetLastItem(out sbyte altitude, out int id, out int palette)
			{
				var lastItem = _list.Last<IItemMapTile>();
				altitude = lastItem.Altitude;
				id = lastItem.TileId;
				palette = lastItem.Palette;
			}

			/// <summary>
			/// Returns all item values list
			/// </summary>
			/// <param name="altitudes"> Items z</param>
			/// <param name="ids">Items id</param>
			/// <param name="palettes"> Items palette</param>
			public void GetItems(out IList<sbyte> altitudes, out IList<int> ids, out IList<int> palettes)
			{
				var alt = new List<sbyte>();
				var idss = new List<int>();
				var pal = new List<int>();

				foreach (var item in _list)
				{
					alt.Add(item.Altitude);
					idss.Add(item.TileId);
					pal.Add(item.Palette);
				}

				altitudes = alt;
				ids = idss;
				palettes = pal;
			}

			/// <summary>
			/// Clones the istance of this object
			/// </summary>
			/// <returns>the cloned instance</returns>
			public IUltimaMapDataProvider Clone()
			{
				return new UltimaMapDataProvider() {MapIndex = _mapIndex};
			}

			public void Dispose(int x, int y)
			{
			var block = _map[_map.FindBlockId(_x, _y)];
			if(block!= null)
				block.Dispose();
			}

			/// <summary>
			/// Creates a new object that is a copy of the current instance.
			/// </summary>
			/// <returns>
			/// A new object that is a copy of this instance.
			/// </returns>
			object ICloneable.Clone()
			{
				return Clone();
			}

			#region IUltimaMapDataProvider Members

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <param name="id"></param>
			/// <param name="altitude"></param>
			public void GetTile(uint x, uint y, out int id, out sbyte altitude )
			{
				_x = x;
				_y = y;
				var tile = _map[x, y];
				if (tile == null)
				{
					id = 0;
					altitude = 0;
					_list = null;
					return;
				}
				id = tile.Land != null ? tile.Land.TileId : 0;
				_land = (ushort) id;
				altitude = (sbyte) (tile.Land != null ? tile.Land.Altitude : 0);
				_altitude = altitude;
				_list = tile.ItemsList;
				_listIndex = 0;
			}

			#endregion

			/// <summary>
			/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
			/// </summary>
			public void Dispose()
			{
				if (_block != null)
				{
					_block.Dispose();
				}
				var block = _map[_map.FindBlockId(_x, _y)];
				block.Dispose();
			}
		}
	}
