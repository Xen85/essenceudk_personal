using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EssenceUDK.MapMaker.Elements.Interfaces
	{
	public interface IUltimaMapDataProvider : ICloneable, IDisposable
		{
			int MapIndex { set; }
			ushort Land { get; }
			sbyte Atitude { get; }
			void GetCoordinates(uint x, uint y);
			bool HashNextItem();
			void GetNextItem(out sbyte altitude, out int id, out int palette);
			void GetLastItem(out sbyte altitude, out int id, out int palette);
			void GetItems(out IList<sbyte> altitudes, out IList<int> ids, out IList<int> palettes);
			void GetTile(uint x, uint y, out int id, out sbyte altitude);
			IUltimaMapDataProvider Clone();
			void Dispose(int x, int y);
		}
	}
