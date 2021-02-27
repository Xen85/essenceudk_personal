﻿﻿﻿using System;
using System.Collections.Generic;
﻿﻿using System.ComponentModel;
﻿﻿using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization;
using EssenceUDK.Platform.DataTypes;
using EssenceUDK.Platform.DataTypes.FileFormat.Containers;
using EssenceUDK.Platform.UtilHelpers;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Color = System.Drawing.Color;

namespace EssenceUDK.Platform.Factories
{
	/*
animinfo.mul

 
facet00.mul
facet01.mul
facet02.mul
facet03.mul
facet04.mul
facet05.mul
fonts.mul
Gumpart.mul
Gumpidx.mul
hues.mul
light.mul
lightidx.mul

mapdif0.mul
mapdif1.mul
mapdif2.mul
mapdifl0.mul
mapdifl1.mul
mapdifl2.mul
multi.mul
palette.mul
radarcol.mul
sjis2uni.mul
skillgrp.mul
skills.mul
sound.mul
soundidx.mul
speech.mul
stadif0.mul
stadif1.mul
stadif2.mul
stadifi0.mul
stadifi1.mul
stadifi2.mul
stadifl0.mul
stadifl1.mul
stadifl2.mul
staidx0.mul
staidx0x.mul
staidx1.mul
staidx2.mul
staidx3.mul
staidx4.mul
staidx5.mul
statics0.mul
statics0x.mul
statics1.mul
statics2.mul
statics3.mul
statics4.mul
statics5.mul
	map0.mul
map0x.mul
map1.mul
map2.mul
map3.mul
map4.mul
map5.mul
	*/
	internal class ClassicFactory : IDataFactory, IDataFactoryReader, IDataFactoryWriter
	{
		public readonly UODataManager Data;
		
		private IDataContainer   container_LandData, container_ItemData, container_LandTile, container_ItemTile, container_LandTexm, container_ItemAnim;
		private IDataContainer   container_GumpData;

		private IDataContainer[] container_Animation;


		private IDataContainer[] container_UniFont;
		private IDataContainer[] container_Map;
		private IDataContainer[] container_Sta;
		private IDataContainer[] container_MapDif;
		private IDataContainer[] container_StaDif;
		private IDataContainer[] container_Facet;

		void IDataFactory.Dispose()
		{
			if (container_LandData != null) container_LandData.Dispose();
			container_LandData = null;

			if (container_ItemData != null) container_ItemData.Dispose();
			container_ItemData = null;

			if (container_LandTile != null) container_LandTile.Dispose();
			container_LandTile = null;

			if (container_ItemTile != null) container_ItemTile.Dispose();
			container_ItemTile = null;

			if (container_LandTexm != null) container_LandTexm.Dispose();
			container_LandTexm = null;

			if (container_ItemAnim != null) container_ItemAnim.Dispose();
			container_ItemAnim = null;

			if (container_GumpData != null) container_GumpData.Dispose();
			container_GumpData = null;

			if (container_Animation != null)
				for (int index = 0; index < container_Animation.Length; index++)
				{
					var animation = container_Animation[index];
					if (animation != null) animation.Dispose();
					container_Animation[index] = null;
				}
			container_Animation = null;

			if (container_Map != null)
				foreach (var obj in container_Map)
					if (obj != null) obj.Dispose();
			container_Map = null;


			if (container_Sta != null)
				foreach (var obj in container_Sta)
					if (obj != null) obj.Dispose();
			container_Sta = null;

			if (container_MapDif != null)
			    foreach (var obj in container_MapDif)
				    if (obj != null) obj.Dispose();
			container_MapDif = null;


			if (container_StaDif != null)
			    foreach (var obj in container_StaDif)
				    if (obj != null) obj.Dispose();
			container_StaDif = null;

			if (container_Facet != null)
			    foreach (var obj in container_Facet)
				    if (obj != null) obj.Dispose();
			container_Facet = null;


			
		}

		private string GetPath(string file)
		{
			var folder = Data.Location.LocalPath;
			var flocat = Path.Combine(folder, file);
			return File.Exists(flocat) ? flocat : null;
		}

		private string GetPath(string format, params object[] args)
		{
			var path = String.Format(format, args);
			return GetPath(path);
		}

		internal ClassicFactory(UODataManager data)
		{
			Data = data;
			if (data.DataType.HasFlag(UODataType.UseUopFiles))
				throw new NotImplementedException();


			//IDataContainer uop = new UopContainer(GetPath("AnimationSequence.uop"), data.RealTime);
			//var sequence = new byte[uop.EntryLength][];
			//for (var i = 0U; i < uop.EntryLength; ++i)
			//    sequence[i] = uop[i];

			MulContainer virtualcontainer = null;
			virtualcontainer   = MulContainer.GetVirtual(null, GetPath("tiledata.mul"), data.RealTime);
			container_LandData = new MulContainer(virtualcontainer, 0, (_LandLength>>5), (uint)(data.DataType.HasFlag(UODataType.UseNewDatas) ?  964 :  836));
			container_ItemData = new MulContainer(virtualcontainer,    (_LandLength>>5)* (uint)(data.DataType.HasFlag(UODataType.UseNewDatas) ?  964 :  836), 
																					  0, (uint)(data.DataType.HasFlag(UODataType.UseNewDatas) ? 1316 : 1188));

			if (!String.IsNullOrEmpty(GetPath("artidx.mul")) && !String.IsNullOrEmpty(GetPath("art.mul"))) {
				virtualcontainer   = MulContainer.GetVirtual(GetPath("artidx.mul"), GetPath("art.mul"), data.RealTime);
				container_LandTile = new MulContainer(virtualcontainer, 0, _LandLength);
				container_ItemTile = new MulContainer(virtualcontainer, _LandLength, 0);
			 }

			if (!String.IsNullOrEmpty(GetPath("texidx.mul")) && !String.IsNullOrEmpty(GetPath("texmaps.mul"))) 
				container_LandTexm = new MulContainer(GetPath("texidx.mul"),  GetPath("texmaps.mul"), data.RealTime);

			if (!String.IsNullOrEmpty(GetPath("gumpidx.mul")) && !String.IsNullOrEmpty(GetPath("gumpart.mul"))) 
				container_GumpData = new MulContainer(GetPath("gumpidx.mul"), GetPath("gumpart.mul"), data.RealTime);

			//container_ItemAnim = new MulContainer(0, GetPath("animdata.mul"), realtime);

			var animationcontainer = new List<IDataContainer>(16);
			if (!String.IsNullOrEmpty(GetPath("anim.idx")) && !String.IsNullOrEmpty(GetPath("anim.mul")))
				animationcontainer.Add(new MulContainer(GetPath("anim.idx"), GetPath("anim.mul"), data.RealTime));
			if (!String.IsNullOrEmpty(GetPath("anim2.idx")) && !String.IsNullOrEmpty(GetPath("anim2.mul")))
				animationcontainer.Add(new MulContainer(GetPath("anim2.idx"), GetPath("anim2.mul"), data.RealTime));
			if (!String.IsNullOrEmpty(GetPath("anim3.idx")) && !String.IsNullOrEmpty(GetPath("anim3.mul")))
				animationcontainer.Add(new MulContainer(GetPath("anim3.idx"), GetPath("anim3.mul"), data.RealTime));
			if (!String.IsNullOrEmpty(GetPath("anim4.idx")) && !String.IsNullOrEmpty(GetPath("anim4.mul")))
				animationcontainer.Add(new MulContainer(GetPath("anim4.idx"), GetPath("anim4.mul"), data.RealTime));
			if (!String.IsNullOrEmpty(GetPath("anim5.idx")) && !String.IsNullOrEmpty(GetPath("anim5.mul")))
				animationcontainer.Add(new MulContainer(GetPath("anim5.idx"), GetPath("anim5.mul"), data.RealTime));
			if (!String.IsNullOrEmpty(GetPath("animationframe1.uop")))
				animationcontainer.Add(new UopContainer(GetPath("animationframe1.uop"), data.RealTime));
			if (!String.IsNullOrEmpty(GetPath("animationframe2.uop")))
				animationcontainer.Add(new UopContainer(GetPath("animationframe2.uop"), data.RealTime));
			if (!String.IsNullOrEmpty(GetPath("animationframe3.uop")))
				animationcontainer.Add(new UopContainer(GetPath("animationframe3.uop"), data.RealTime));
			if (!String.IsNullOrEmpty(GetPath("animationframe4.uop")))
				animationcontainer.Add(new UopContainer(GetPath("animationframe4.uop"), data.RealTime));

			container_Animation = new IDataContainer[animationcontainer.Count];
			for ( int i = 0; i < animationcontainer.Count; i++ )
			{
				container_Animation[i] = animationcontainer[i];
			}

			var maps = data.DataOptions.majorFacet.Length;
			container_Map    = new IDataContainer[maps];
			container_Sta    = new IDataContainer[maps];
			//container_MapDif = new IDataContainer[maps];
			//container_StaDif = new IDataContainer[maps];
			//container_Facet  = new IDataContainer[maps];
			for (int m = 0; m < maps; ++m) {
				string path1, path2, x = data.DataType.HasFlag(UODataType.UseExtFacet) ? "x" : String.Empty;
				container_Map[m] =  !String.IsNullOrEmpty(path2=GetPath("map{0}{1}.mul",m,x)) ? new MulContainer(196, path2, data.RealTime) : null;
				container_Sta[m] = (!String.IsNullOrEmpty(path1=GetPath("staidx{0}{1}.mul",m,x)) && !String.IsNullOrEmpty(path2=GetPath("statics{0}{1}.mul",m,x)))
								 ? new MulContainer(path1, path2, data.RealTime) : null;


				//container_Facet[m] = !String.IsNullOrEmpty(path2 = GetPath("facet0{0}.mul", m)) ? new MulContainer(1, path2, data.RealTime) : null;
			}

			/*
			container_Map    = new IDataContainer[6];
			container_Sta    = new IDataContainer[6];
			container_MapDif = new IDataContainer[6];
			container_StaDif = new IDataContainer[6];
			container_Facet  = new IDataContainer[6];

			for (var i = 0; i < container_UniFont.Length; ++i)
				container_Map[i] = new MulContainer(0, Path.Combine(folder, String.Format("unifont{0}.mul", i), true));
			*/
			//container_UniFont = new IDataContainer[13];
			//for (var i = 0; i < container_UniFont.Length; ++i)
			//    container_UniFont[i] = new MulContainer(0, Path.Combine(folder, String.Format("unifont{0}.mul", i), true));

		}

		#region Data Types

		[StructLayout(LayoutKind.Sequential, Size = 3, Pack = 1)]
		internal unsafe struct LandMapTileData : ILandMapTile
		{
			private ushort _TileId;
			private sbyte  _Altitude;

			public ushort TileId  { get { return _TileId; }   set { _TileId = value; } }
			public sbyte Altitude { get { return _Altitude; } set { _Altitude = value; } }

			public LandMapTileData(ILandMapTile data)
			{
				_TileId   = data.TileId;
				_Altitude = data.Altitude;
			}
		}

		[StructLayout(LayoutKind.Sequential, Size = 7, Pack = 1)]
		internal unsafe struct ItemMapTileData : IItemMapTile
		{
			private ushort _TileId;
			private byte   _XOffset;
			private byte   _YOffset;
			private sbyte  _Altitude;
			private ushort _Palette;

			public ushort  TileId { get { return _TileId; } set { _TileId = value; } }
			public byte   XOffset { get { return _XOffset; } set { _XOffset = value; } }
			public byte   YOffset { get { return _YOffset; } set { _YOffset = value; } }
			public sbyte Altitude { get { return _Altitude; } set { _Altitude = value; } }
			public ushort Palette { get { return _Palette; } set { _Palette = value; } }

			public ItemMapTileData(byte x, byte y, IItemMapTile data)
			{
				_XOffset = x;
				_YOffset = y;
				_TileId = data.TileId;
				_Palette = data.Palette;
				_Altitude = data.Altitude;
			}

			public ItemMapTileData(byte xy, IItemMapTile data) : this((byte)(xy % 8), (byte)(xy >> 3), data)
			{
			}
		}

		internal sealed class MapBlockData : IMapBlockData
		{
			private uint _LandHeader;
			private ILandMapTile[]   _Lands;
			private IItemMapTile[][] _Items;

			public uint LandHeader { get { return _LandHeader; } set { _LandHeader = value; } }
			public ILandMapTile[]   Lands { get { return _Lands; } set { _Lands = value; } }
			public IItemMapTile[][] Items { get { return _Items; } set { _Items = value; } }

			public MapBlockData(uint header, LandMapTileData[] lands, ItemMapTileData[][] items)
			{
				_LandHeader = header;
				_Lands = new LandMapTile[64];
				_Items = new ItemMapTile[64][];
				for (int i = 0; i < 64; ++i)
				{
					_Lands[i] = new LandMapTile(lands[i].TileId, lands[i].Altitude);
					_Items[i] = new ItemMapTile[items[i].Length];
					for (int k = 0; k < items[i].Length; ++k)
						_Items[i][k] = new ItemMapTile(items[i][k].TileId, items[i][k].Palette, items[i][k].Altitude);
				}
			} 

			public MapBlockData(LandMapTileData[] lands, ItemMapTileData[][] items) : this(0, lands, items)
			{
			}

			public MapBlockData(uint header, IMapTile[] tiles)
			{
				_LandHeader = header;
				_Lands = new LandMapTile[64];
				_Items = new ItemMapTile[64][];
				for (int t = 0; t < 64; ++t) {
					_Lands[t] = tiles[t].Land as LandMapTile;
					_Items[t] = new ItemMapTile[tiles[t].Count];
					for (int i = 0; i < tiles[t].Count; ++i)
						_Items[t][i] = tiles[t][i] as ItemMapTile;
				}
			}
		}



		private const uint _LandLength = 0x4000;

		internal sealed unsafe class LandData : ILandData
		{
			internal interface IRawData
			{
				TileFlag   Flags        { get; set; }
				ushort     TexID        { get; set; }
				byte*       Name        { get; set; }
			}

			private    IRawData     _Data;
			private    Language     _Lang;
			private    string       _Name;

			string     ILandData.Name         { get { return _Name ?? (_Name = _Lang.ReadAnsiString(_Data.Name, 20)); }     set { throw new NotImplementedException(); } }
			TileFlag   ILandData.Flags        { get { return _Data.Flags; }        set { _Data.Flags = value; } }
			ushort     ILandData.TexID        { get { return _Data.TexID; }        set { _Data.TexID = value; } }

			internal LandData(Language lang, IRawData data)
			{
				_Data = data;
				_Lang = lang;
				_Name = null;
			}
		}

		internal sealed unsafe class ItemData : IItemData
		{
			internal interface IRawData
			{
				TileFlag    Flags       { get; set; }
				byte        Weight      { get; set; }
				byte        Quality     { get; set; }
				ushort      Miscdata    { get; set; }
				byte        Unk1        { get; set; }
				byte        Quantity    { get; set; }
				ushort      Animation   { get; set; }
				byte        Unk2        { get; set; }
				byte        Hue         { get; set; }
				byte        StackingOff { get; set; }
				byte        Value       { get; set; }
				byte        Height      { get; set; }
				byte*       Name        { get; set; }
			}

			private    IRawData     _Data;
			private    Language     _Lang;
			private    string       _Name;

			string     IItemData.Name         { get { return _Name ?? (_Name = _Lang.ReadAnsiString(_Data.Name, 20)); }     set { throw new NotImplementedException(); } }
			TileFlag   IItemData.Flags        { get { return _Data.Flags; }        set { _Data.Flags = value; } }
			byte       IItemData.Height       { get { return _Data.Height; }       set { _Data.Height = value; } }
			byte       IItemData.Quality      { get { return _Data.Quality; }      set { _Data.Quality = value; } }
			byte       IItemData.Quantity     { get { return _Data.Quantity; }     set { _Data.Quantity = value; } }
			ushort     IItemData.Animation    { get { return _Data.Animation; }    set { _Data.Animation = value; } }
			byte       IItemData.StackingOff  { get { return _Data.StackingOff; }  set { _Data.StackingOff = value; } }

			internal ItemData(Language lang, IRawData data)
			{
				_Data = data;
				_Lang = lang;
				_Name = null;
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 26, Pack = 1)]
		private unsafe struct OldLandData : LandData.IRawData
		{
			private uint       _Flags;
			private ushort     _TexID;
			private fixed byte _Name[20];
			
			TileFlag   LandData.IRawData.Flags        { get { return (TileFlag)_Flags; }    set { _Flags = (uint)value; } }
			ushort     LandData.IRawData.TexID        { get { return _TexID; }              set { _TexID = value; } }
			byte*      LandData.IRawData.Name         { get { byte* ptr; fixed (byte* p = _Name) ptr = p; return ptr; }   
														set { ; } }
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 30, Pack = 1)]
		private unsafe struct NewLandData : LandData.IRawData
		{
			[MarshalAs(UnmanagedType.U8)]
			private TileFlag   _Flags;
			private ushort     _TexID;
			private fixed byte _Name[20];

			TileFlag   LandData.IRawData.Flags        { get { return _Flags; }        set { _Flags = value; } }
			ushort     LandData.IRawData.TexID        { get { return _TexID; }        set { _TexID = value; } }
			byte*      LandData.IRawData.Name         { get { byte* ptr; fixed (byte* p = _Name) ptr = p; return ptr; }   
														set { ; } }
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 37, Pack = 1)]
		private unsafe struct OldItemData : ItemData.IRawData
		{
			private uint       _Flags;
			private byte       _Weight;
			private byte       _Quality;
			private ushort     _Miscdata;
			private byte       _Unk1;
			private byte       _Quantity;
			private ushort     _Animation;
			private byte       _Unk2;
			private byte       _Hue;
			private byte       _StackingOff;
			private byte       _Value;
			private byte       _Height;
			private fixed byte _Name[20];

			TileFlag   ItemData.IRawData.Flags        { get { return (TileFlag)_Flags; }set { _Flags = (uint)value; } }
			byte       ItemData.IRawData.Weight       { get { return _Weight; }         set { _Weight = value; } }
			byte       ItemData.IRawData.Quality      { get { return _Quality; }        set { _Quality = value; } }
			ushort     ItemData.IRawData.Miscdata     { get { return _Miscdata; }       set { _Miscdata = value; } }
			byte       ItemData.IRawData.Unk1         { get { return _Unk1; }           set { _Unk1 = value; } }
			byte       ItemData.IRawData.Quantity     { get { return _Quantity; }       set { _Quantity = value; } }
			ushort     ItemData.IRawData.Animation    { get { return _Animation; }      set { _Animation = value; } }
			byte       ItemData.IRawData.Unk2         { get { return _Unk2; }           set { _Unk2 = value; } }
			byte       ItemData.IRawData.Hue          { get { return _Hue; }            set { _Hue = value; } }
			byte       ItemData.IRawData.StackingOff  { get { return _StackingOff; }    set { _StackingOff = value; } }
			byte       ItemData.IRawData.Value        { get { return _Value; }          set { _Value = value; } }
			byte       ItemData.IRawData.Height       { get { return _Height; }         set { _Height = value; } }
			byte*      ItemData.IRawData.Name         { get { byte* ptr; fixed (byte* p = _Name) ptr = p; return ptr; }   
														set { ; } }
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 41, Pack = 1)]
		private unsafe struct NewItemData : ItemData.IRawData
		{
			[MarshalAs(UnmanagedType.U8)]
			private TileFlag   _Flags;
			private byte       _Weight;
			private byte       _Quality;
			private ushort     _Miscdata;
			private byte       _Unk1;
			private byte       _Quantity;
			private ushort     _Animation;
			private byte       _Unk2;
			private byte       _Hue;
			private byte       _StackingOff;
			private byte       _Value;
			private byte       _Height;
			private fixed byte _Name[20];
			
			TileFlag   ItemData.IRawData.Flags        { get { return _Flags; }        set { _Flags = value; } }
			byte       ItemData.IRawData.Weight       { get { return _Weight; }       set { _Weight = value; } }
			byte       ItemData.IRawData.Quality      { get { return _Quality; }      set { _Quality = value; } }
			ushort     ItemData.IRawData.Miscdata     { get { return _Miscdata; }     set { _Miscdata = value; } }
			byte       ItemData.IRawData.Unk1         { get { return _Unk1; }         set { _Unk1 = value; } }
			byte       ItemData.IRawData.Quantity     { get { return _Quantity; }     set { _Quantity = value; } }
			ushort     ItemData.IRawData.Animation    { get { return _Animation; }    set { _Animation = value; } }
			byte       ItemData.IRawData.Unk2         { get { return _Unk2; }         set { _Unk2 = value; } }
			byte       ItemData.IRawData.Hue          { get { return _Hue; }          set { _Hue = value; } }
			byte       ItemData.IRawData.StackingOff  { get { return _StackingOff; }  set { _StackingOff = value; } }
			byte       ItemData.IRawData.Value        { get { return _Value; }        set { _Value = value; } }
			byte       ItemData.IRawData.Height       { get { return _Height; }       set { _Height = value; } }
			byte*      ItemData.IRawData.Name         { get { byte* ptr; fixed (byte* p = _Name) ptr = p; return ptr; }   
														set { ; } }
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 40, Pack = 1)]
		private unsafe struct NewAnimHeader
		{
			internal uint       _Header; // 0x41 0x4D 0x4F (0x55) AMO(U)
			internal uint       _Version;
			internal uint       _FileSize;
			internal uint       _AnimationID;
			internal short      _MainInitX;
			internal short      _MainInitY;
			internal short      _MainEndX;
			internal short      _MainEndY;
			internal uint       _ColourCount;
			internal uint       _ColourOffset;
			internal uint       _FramesCount;
			internal uint       _FramesOffset;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 16, Pack = 1)]
		private unsafe struct NewAnimFrame
		{
			internal ushort     _Unknown1;
			internal ushort     _Unknown2;
			internal short      _MainInitX;
			internal short      _MainInitY;
			internal short      _MainEndX;
			internal short      _MainEndY;
			internal uint       _FrameLook; // offset to frame data from begining of it's header
			internal ushort     Width       { get { return (ushort)Math.Abs(_MainEndX - _MainInitX); } }
			internal ushort     Height      { get { return (ushort)Math.Abs(_MainEndY - _MainInitY); } }
		}

		#endregion

		#region Data Convertors

		// art.mul convertors    ---------------------------------------------------------------

		private static unsafe void ConvertLandSurface(UODataManager datamanager, byte[] rawdata, out ISurface surface)
		{
			if (rawdata == null || rawdata.Length == 0) {
				surface = null;
				return;
			}

			fixed (byte* data = rawdata)
			{
				ushort* bdata = (ushort*)data;
				int xOffset = 21;
				int xRun = 2;

				surface = datamanager.CreateSurface(44, 44, EssenceUDK.Platform.DataTypes.PixelFormat.Bpp16A1R5G5B5);
				lock (surface) {
					ushort*  line = surface.ImageWordPtr;
					uint    delta = surface.Stride >> 1;

					for (int y = 0; y < 22; ++y, --xOffset, xRun += 2, line += delta) {
						ushort* cur = line + xOffset;
						ushort* end = cur + xRun;

						while (cur < end)
							*cur++ = (ushort)(*bdata++ | 0x8000);
					}

					xOffset = 0;
					xRun = 44;

					for (int y = 0; y < 22; ++y, ++xOffset, xRun -= 2, line += delta) {
						ushort* cur = line + xOffset;
						ushort* end = cur + xRun;

						while (cur < end)
							*cur++ = (ushort)(*bdata++ | 0x8000);
					}
				}
			}
		}

		private static unsafe void ConvertLandSurface(ISurface surface, out byte[] rawdata)
		{
			if (surface.Stride / surface.Width != 2)
				throw new NotImplementedException();

			var bitmap = surface.GetSurface();// as BitmapSurface;
			var imrect = bitmap.GetImageRect();
			var imavrx = bitmap.Width / 2;
			var imlenx = Math.Min(Math.Max(imavrx - imrect.X1, imrect.X2 + 1 - imavrx), imavrx);
			imrect = new Clipper2D((short)(imavrx - imlenx), imrect.Y1, (short)(imavrx + imlenx - 1), (short)(bitmap.Height - 1));

			if (imrect.Width != 44 || imrect.Height != 44) {
				imrect = new Clipper2D((short)0, (short)0, (ushort)bitmap.Width, (ushort)bitmap.Height);
				if (imrect.Width != 44 || imrect.Height != 44)
					throw new ClassicFactoryException();
			}

			rawdata = new byte[0x07E8];
			lock (surface) {
				uint    delta = surface.Stride >> 1;
				ushort* line1 = surface.ImageWordPtr + imrect.Y1 * delta + imrect.X1;
				ushort* line2 = surface.ImageWordPtr + imrect.Y2 * delta + imrect.X1;  

				fixed (byte* _data = &rawdata[0]) {
					var data = (uint*)_data;

					//data[0] = 0x00000000; // header
					var i1 = 0;
					var i2 = 505;
					for (var y = 0; y < 22; ++y, line1 += delta, line2 -= delta) {
						var cur1 = (uint*)(line1 + 21 - y);
						var cur2 = (uint*)(line2 + 21 + y);
						for (var x = 0; x <= y; ++x, ++i1, --i2, ++cur1, --cur2) {
							data[i1] = *cur1 ^ 0x80008000;
							data[i2] = *cur2 ^ 0x80008000;
						}
					}
				}
			}
		}

		private static unsafe void ConvertItemSurface(UODataManager datamanager, byte[] rawdata, out ISurface surface)
		{
		    if (rawdata == null)
		    {
		        surface = null;
		        return;
		    }
		    fixed (byte* data = rawdata)
			{
				ushort* bindata = (ushort*)data;

				int count = 2;
				ushort width = bindata[count++];
				ushort height = bindata[count++];

				if (width >= 0x0400 || height >= 0x0400) {
					surface = null;
					return;
				}

				int[] lookups = new int[height];
				int start = (height + 4);
				for (int i = 0; i < height; ++i)
					lookups[i] = (int)(start + (bindata[count++]));

				surface = datamanager.CreateSurface(width, height, EssenceUDK.Platform.DataTypes.PixelFormat.Bpp16A1R5G5B5);
				lock (surface) {
					ushort*  line = surface.ImageWordPtr;
					uint    delta = surface.Stride >> 1;

					for (int y = 0; y < height; ++y, line += delta)
					{
						count = lookups[y];
						ushort* cur = line;
						ushort* end;
						int xOffset, xRun;

						while (((xOffset = bindata[count++]) + (xRun = bindata[count++])) != 0) {
							if (cur >= surface.ImageWordPtr + delta * height)
								break;

							if (2 * count >= rawdata.Length)
								break;

							if (xOffset + xRun > delta)
								break;

							cur += xOffset;
							end = cur + xRun;

							while (cur < end)
								*cur++ = (ushort)(bindata[count++] ^ 0x8000);
						}
					}
				}
			}
		}

		private static unsafe void ConvertItemSurface(ISurface surface, out byte[] rawdata)
		{
			if (surface.Stride / surface.Width != 2)
				throw new NotImplementedException();

			var bitmap = surface.GetSurface();// as BitmapSurface;
			var imrect = bitmap.GetImageRect();
			var imavrx = bitmap.Width / 2;
			var imlenx = Math.Min(Math.Max(imavrx - imrect.X1, imrect.X2 + 1 - imavrx), imavrx);
			imrect = new Clipper2D((short)(imavrx - imlenx), imrect.Y1, (short)(imavrx + imlenx - 1), (short)(bitmap.Height - 1));

			var data = new BinaryWriter(new MemoryStream());
			data.Write((uint)1234); // header
			data.Write((ushort)imrect.Width);
			data.Write((ushort)imrect.Height);

			var lookup = (int)data.BaseStream.Position;
			for (var y = 0; y < imrect.Height; ++y)// fill lookup
				data.Write((ushort)0);
  
			lock (surface) {
				uint   delta = surface.Stride >> 1;
				ushort* line = surface.ImageWordPtr + imrect.Y1 * delta;               

				for (int y = 0; y < imrect.Height; ++y, line += delta) {

					data.BaseStream.Seek(lookup + (y << 1), SeekOrigin.Begin);
					var lineoffs = ((data.BaseStream.Length - lookup) >> 1) - imrect.Height;
					if (lineoffs > 0xFFFF)
						throw new ClassicFactoryException();
					data.Write( (ushort)lineoffs );
					data.BaseStream.Seek(0, SeekOrigin.End);

					ushort* cur = line + imrect.X1;
					int x = 0, j = 0, i = 0;

					while (i < imrect.Width) {
						for (i = x; i < imrect.Width; ++i) {
							//first pixel set
							if (cur[i] != 0)
								break;
						}
						if (i < imrect.Width) {
							for (j = (i + 1); j < imrect.Width; ++j) {
								//next non set pixel
								// if (cur[j] == 0)
								if (cur[j] == 0 && (j + 1 == imrect.Width || cur[j + 1] == 0))
									break;
							}
							data.Write((ushort)(i - x)); //xoffset
							data.Write((ushort)(j - i)); //run
							for (int p = i; p < j; ++p)
								//data.Write((ushort)(cur[p] ^ 0x8000);
								data.Write((ushort)(cur[p] > 0 ? (cur[p] ^ 0x8000) : cur[p]));
							x = j;
						}
					}
					data.Write((ushort)0); //xOffset
					data.Write((ushort)0); //Run
				}
			}

			data.Flush();
			rawdata = (data.BaseStream as MemoryStream).ToArray();
			data.Close();
		}

		// texmaps.mul convertors --------------------------------------------------------------

		private static unsafe void ConvertTexmSurface(UODataManager datamanager, uint extra, byte[] rawdata, out ISurface surface)
		{
			if (rawdata == null || rawdata.Length == 0) {
				surface = null;
				return;
			}

			// TODO: its greate loooose, we need to view at osi data model
			ushort size = (ushort)(rawdata.Length == 8192 ? 64 : 128);
			//int size = extra == 0 ? 64 : 128;

			surface = datamanager.CreateSurface(size, size, EssenceUDK.Platform.DataTypes.PixelFormat.Bpp16A1R5G5B5);
			lock (surface) {
				ushort*  line = surface.ImageWordPtr;
				uint    delta = surface.Stride >> 1;

				fixed (byte* data = rawdata)
				{
					ushort* bindat = (ushort*)data;
					for (int y = 0; y < size; ++y, line += delta) {
						ushort* cur = line;
						ushort* end = cur + size;

						while (cur < end)
							*cur++ = (ushort)(*bindat++ ^ 0x8000);
					}
				}
			}
		}

		private static unsafe void ConvertTexmSurface(ISurface surface, out uint extra, out byte[] rawdata)
		{
			if (surface.Width != surface.Height || (surface.Width != 64 && surface.Width != 128))
				throw new ClassicFactoryException();

			extra = surface.Width == 64 ? 0U : 1U;

			rawdata = new byte[2 * surface.Width * surface.Height];
			lock (surface) {
				uint    delta = surface.Stride >> 1;
				ushort* tline = surface.ImageWordPtr;
				
				fixed (byte* _data = rawdata) {
					ushort* data = (ushort*)_data, cur = tline;
					for (int i = 0, y = 0; y < surface.Height; ++y, cur = (tline += delta))
						for (int x = 0; x < surface.Width; ++x, ++i, ++cur)
							data[i] = (ushort)(*cur ^ 0x8000);
				}
			}
		}

		// gumps convertors       --------------------------------------------------------------

		private static unsafe void ConvertGumpSurface(UODataManager datamanager, uint extra, byte[] rawdata, out ISurface surface)
		{
			uint width = (extra >> 16) & 0xFFFF;
			uint height = extra & 0xFFFF;
			if (rawdata == null || /* extra == 0xFFFFFFFF || */ (extra & 0x80008000) != 0 || width == 0 || height == 0) {
				surface = null;
				return;
			}

			surface = datamanager.CreateSurface((ushort)width, (ushort)height, EssenceUDK.Platform.DataTypes.PixelFormat.Bpp16A1R5G5B5);
			lock (surface) {
				ushort*  line = surface.ImageWordPtr;
				uint    delta = surface.Stride >> 1;

				fixed (byte* data = rawdata) {
					uint* lookup = (uint*)data;
					ushort* dat = (ushort*)data;

					for (uint count = 0, y = 0; y < height; ++y, line += delta)
					{
						count = (*lookup++ << 1);

						ushort* cur = line;
						ushort* end = line + width;

						while (cur < end) {
							ushort color = dat[count++];
							ushort* next = cur + dat[count++];

							if (color == 0)
								cur = next;
							else {
								color ^= 0x8000;
								while (cur < next)
									*cur++ = color;
							}
						}
					}

				}
			}
		}

		private static unsafe void ConvertGumpSurface(ISurface surface, out uint extra, out byte[] rawdata)
		{
			extra = (uint)((surface.Width << 16) + surface.Height);

			var data = new BinaryWriter(new MemoryStream());

			lock (surface) {
				uint    delta = surface.Stride >> 1;
				ushort* yline = surface.ImageWordPtr;

				for (int y = 0; y < surface.Height; ++y, yline += delta) {
					ushort* cur = yline;

					int x = 0;
					int current = (int)data.BaseStream.Position;
					data.Seek(y << 2, SeekOrigin.Begin);
					int offset = current >> 2;
					data.Write(offset);
					data.Seek(offset << 2, SeekOrigin.Begin);

					while (x < surface.Width) {
						int run = 1;
						ushort c = cur[x];
						while ((x + run) < surface.Width) {
							if (c != cur[x + run])
								break;
							++run;
						}
						if (c == 0)
							data.Write(c);
						else
							data.Write((ushort)(c ^ 0x8000));
						data.Write((short)run);
						x += run;
					}
				}
			}

			data.Flush();
			rawdata = (data.BaseStream as MemoryStream).ToArray();
			data.Close();
		}

		// animation convertors   --------------------------------------------------------------

		public static unsafe void ConvertAnimSurface(byte[] rawdata, ushort[] palette, out Bitmap bmp, int offset = 0)
		{
			bmp = null;
			var centX  = BitConverter.ToInt16(rawdata,   offset + 0);
			var centY  = BitConverter.ToInt16(rawdata,   offset + 2);
			var width  = BitConverter.ToUInt16(rawdata,  offset + 4);
			var height = BitConverter.ToUInt16(rawdata,  offset + 6);
			if (height == 0 || width == 0)
				return;

			bmp = new Bitmap(width, height, PixelFormat.Format16bppArgb1555);
			BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format16bppArgb1555);
			ushort* line = (ushort*)bd.Scan0;
			int delta = bd.Stride >> 1;

			int xBase = centX - 0x200;
			int yBase = centY + height - 0x200;

			line += xBase;
			line += yBase * delta;

			uint header; int pos = offset + 4;
			while ((header = BitConverter.ToUInt32(rawdata,  pos+=4)) != 0x7FFF7FFF) {
				header ^= 0x80200000; // DoubleXor = (0x200 << 22) | (0x200 << 12);
				var drun = (header & 0xFFF);
				var offy = ((header >> 12) & 0x3FF);
				var offx = ((header >> 22) & 0x3FF);

				ushort* cur = line + (((offy) * delta) + ((offx) & 0x3FF));
				ushort* end = cur + (drun);
				while (cur < end)
					*cur++ = palette[rawdata[pos++]];

			}
			bmp.UnlockBits(bd);
		}

		private static uint GetInternalAnimIndex(IDataContainer container, uint id)
		{
			var internid = 0U;
			var filename = Path.GetFileName(container is MulContainer ? (container as MulContainer).FNameMul :
											container is UopContainer ? (container as UopContainer).FNameUop : String.Empty).ToLower();
			switch (filename) {
				default         : internid = 0xDEADBEEFU; break;
				case "anim.mul" : internid = (id < 200) ? id * 110 : (id < 400) ? 22000 + (id - 200) * 65 : 35000 + (id - 400) * 175;    break;
				case "anim2.mul": internid = (id < 200) ? id * 110 : 22000 + (id - 200) * 65;    break;
				case "anim3.mul": internid = (id < 200) ? 9000 + id * 65 : (id < 400) ? 22000 + (id - 200) * 110 : 35000 + (id - 400) * 175; break;
				case "anim4.mul": internid = (id < 200) ? id * 110 : (id < 400) ? 22000 + (id - 200) * 65 : 35000 + (id - 400) * 175; break;
				case "anim5.mul": internid = (id < 200) ? id * 110 : (id < 400) ? 22000 + (id - 200) * 65 : 35000 + (id - 400) * 175; break;
				case "animationframe1.uop" : internid = 0xDEADBEEFU; break;
				case "animationframe2.uop" : internid = 0xDEADBEEFU; break;
				case "animationframe3.uop" : internid = 0xDEADBEEFU; break;
				case "animationframe4.uop" : internid = 0xDEADBEEFU; break;
			}
			return internid < container.EntryLength ? internid : 0xDEADBEEFU;
		}

		//                        --------------------------------------------------------------

		#endregion

		IMapFacet[] IDataFactory.GetMapFacets()
		{
			var count = Math.Max(container_Map.Length, container_Sta.Length);
			var facet = new IMapFacet[count];
			for (byte m = 0; m < count; ++m)
				facet[m] = new MapFacet(this, m, (!Data.DataType.HasFlag(UODataType.UseExtFacet) ? Data.DataOptions.majorFacet : Data.DataOptions.minorFacet)[m]);
			return facet;
		}

		IMapBlockData IDataFactoryReader.GetMapBlock(byte mapindex, uint id)
		{
			var lhead = container_Map[mapindex].Read<uint>(id, 0);
			var lands = container_Map[mapindex].Read<LandMapTileData>(id, 4, 64);
			var tiles = container_Sta[mapindex].ReadAll<ItemMapTileData>(id, 0);
			var items = new ItemMapTileData[64][];
		
			if (tiles != null) {
				List<ItemMapTileData>[] tilelist = new List<ItemMapTileData>[64];
				for (int i = 0; i < 64; ++i)
					tilelist[i] = new List<ItemMapTileData>(128);

				for (int i = 0; i < tiles.Length; ++i)
					tilelist[(tiles[i].YOffset << 3) + tiles[i].XOffset].Add(new ItemMapTileData(tiles[i].XOffset, tiles[i].YOffset, tiles[i]));

				for (int i = 0; i < 64; ++i)
					items[i] = tilelist[i].OrderBy(t => t.Altitude).ToArray();
			} else {
				for (int i = 0; i < 64; ++i)
					items[i] = null;
			}
			return new MapBlockData(lhead, lands, items);
		}

		void IDataFactoryWriter.SetMapBlock(byte mapindex, uint id, IMapBlockData tiles)
		{
			//container_Map[mapindex].Write(id, 0, (tiles as MapBlockData).LandHeader);
			var lands = tiles.Lands.Select(t => new LandMapTileData(t)).ToArray();
			container_Map[mapindex].Write(id, 4, lands, 0, 64);
			var items = new ItemMapTileData[tiles.Items.Sum(t=>t.Length)];
			for (int i = -1, c = 0; c < 64; ++c)
				for (int e = 0; e < tiles.Items[c].Length; ++e)
					items[++i] = new ItemMapTileData((byte)c, tiles.Items[c][e]);
			container_Sta[mapindex].Write(id, 0, items, 0, (uint)items.Length);
		}

		ILandTile[] IDataFactory.GetLandTiles()
		{
			uint i;
			uint tilelen = (container_LandTile != null) ? container_LandTile.EntryLength : _LandLength;
			uint datalen = (container_LandData != null) ? container_LandData.EntryLength : _LandLength;

			var tiles = new LandTile[Math.Max(tilelen, datalen)];
			for (uint d = 0, b = i = 0; i < tilelen && b < datalen; d = 0, ++b, ++i) {
				var tdata = (container_LandData != null) ? Data.DataType.HasFlag(UODataType.UseNewDatas) // we just skip block header
							? container_LandData.Read<NewLandData>(b, 4, 32).Select(t=>(ILandData)new LandData(Data.Language, t)).ToArray()
							: container_LandData.Read<OldLandData>(b, 4, 32).Select(t=>(ILandData)new LandData(Data.Language, t)).ToArray()
							: new NewLandData[32].Select(t=>{(t as LandData.IRawData).TexID=(ushort)(i+d++); return (ILandData)new LandData(Data.Language,t);}).ToArray();
				for (uint c = 0; i < tilelen && c < 32; ++c, ++i)
					tiles[i] = new LandTile(i, this, tdata[c], ((container_LandTile != null) && container_LandTile.IsValid(i)) 
															|| ((container_LandTexm != null) && container_LandTexm.IsValid(tdata[c].TexID)));
				--i;
			}
			return tiles;
		}

		ISurface IDataFactoryReader.GetLandSurface(uint id)
		{
			ISurface surface;
			ConvertLandSurface(Data, container_LandTile[id], out surface);
			return surface;
		}

		void IDataFactoryWriter.SetLandSurface(uint id, ISurface surface)
		{
			try {
				byte[] rawdata;
				ConvertLandSurface(surface, out rawdata);
				container_LandTile[id] = rawdata;
			} catch (OutOfMemoryException e) {
				throw new ClassicFactoryException("Bad image format for land tile.", container_LandTile, id);
			}
		}

		ISurface IDataFactoryReader.GetTexmSurface(uint id)
		{
			ISurface surface;
			ConvertTexmSurface(Data, container_LandTexm.GetExtra(id), container_LandTexm[id], out surface);
			return surface;
		}

		void IDataFactoryWriter.Defrag(ContainerDataType datatype)
		{
			var list = new List<IDataContainer>(16);
			if (datatype.HasFlag(ContainerDataType.GumpArt))
				list.Add(container_GumpData);
			if (datatype.HasFlag(ContainerDataType.Texture))
				list.Add(container_LandTexm);
			if (datatype.HasFlag(ContainerDataType.LandArt) || datatype.HasFlag(ContainerDataType.ItemArt))
				if (container_ItemTile is MulContainer)
					list.Add(MulContainer.GetParent(container_ItemTile as MulContainer));

			for (int i = 0; i < 6; ++i) 
				if (datatype.HasFlag((ContainerDataType)((uint)ContainerDataType.Facet00 << i)) && Data.DataOptions.majorFacet.Length > i) {
					list.Add(container_Sta[i]);
				}
			
			for (int i = 0; i < list.Count; ++i)
				list[i].Defrag();
		}

		void IDataFactoryWriter.SetTexmSurface(uint id, ISurface surface)
		{ 
			try {
				uint extra;
				byte[] rawdata;
				ConvertTexmSurface(surface, out extra, out rawdata);
				container_LandTexm.SetExtra(id, extra);
				container_LandTexm[id] = rawdata; 
			} catch (ClassicFactoryException e) {
				throw new ClassicFactoryException("Bad texture format.", container_LandTexm, id);
			}
		}

		IItemTile[] IDataFactory.GetItemTiles()
		{
			uint i;
			uint deftlen = Data.DataType.HasFlag(UODataType.UseNewDatas) ? 0xFFDCU : 0x8000U; // it's not correct, but it's no such thing that is used in normal situations...
			uint tilelen = (container_ItemTile != null) ? container_ItemTile.EntryLength : deftlen;
			uint datalen = (container_ItemData != null) ? container_ItemData.EntryLength : deftlen;

			var tiles = new ItemTile[Math.Max(tilelen, datalen)];
			for (uint b = i = 0; i < tilelen && b < datalen; ++b, ++i) {
				var tdata = (container_ItemData != null) ? Data.DataType.HasFlag(UODataType.UseNewDatas) // we just skip block header
						  ? container_ItemData.Read<NewItemData>(b, 4, 32).Select(t=>(IItemData)new ItemData(Data.Language, t)).ToArray()
						  : container_ItemData.Read<OldItemData>(b, 4, 32).Select(t=>(IItemData)new ItemData(Data.Language, t)).ToArray()
						  : new NewItemData[32].Select(t=>(IItemData)new ItemData(Data.Language, t)).ToArray();
				for (uint c = 0; i < tiles.Length && c < 32; ++c, ++i)
					tiles[i] = new ItemTile(i, this, tdata[c], (container_ItemTile != null) && container_ItemTile.IsValid(i));
				--i;
			}
			return tiles;
		}

		ISurface IDataFactoryReader.GetItemSurface(uint id)
		{
			ISurface surface;
			ConvertItemSurface(Data, container_ItemTile[id], out surface);
			return surface;
		}

		void IDataFactoryWriter.SetItemSurface(uint id, ISurface surface)
		{
			try {
				byte[] rawdata;
				ConvertItemSurface(surface, out rawdata);
				container_ItemTile[id] = rawdata;
			} catch (ClassicFactoryException e) {
				throw new ClassicFactoryException("Overflow image data for item tile.", container_ItemTile, id);
			}           
		}

		IGumpEntry[] IDataFactory.GetGumpSurfs()
		{
			var galen = (container_GumpData != null) ? container_GumpData.EntryLength : 0x10000;
			var gumps = new GumpEntry[galen];
			for (uint i = 0; i < gumps.Length && i < galen; ++i)
				gumps[i] = new GumpEntry(i, this, (container_GumpData != null) && container_GumpData.IsValid(i));
			return gumps;
		}

		ISurface IDataFactoryReader.GetGumpSurface(uint id)
		{
			ISurface surface;
			ConvertGumpSurface(Data, container_GumpData.GetExtra(id), container_GumpData[id], out surface);
			return surface;
		}

		void IDataFactoryWriter.SetGumpSurface(uint id, ISurface surface)
		{
			try {
				uint extra;
				byte[] rawdata; 
				ConvertGumpSurface(surface, out extra, out rawdata);
				container_GumpData.SetExtra(id, extra);
				container_GumpData[id] = rawdata; 
			} catch (ClassicFactoryException e) {
				throw new ClassicFactoryException("Overflow image data for gump surface.", container_GumpData, id);
			}           
		}

		IAnimation[] IDataFactory.GetAnimations()
		{
return new IAnimation[0];
int ooo = 0;
			var list = new List<IAnimation>(1000);
			foreach (var container in container_Animation) {
				if (container is MulContainer) continue;
				var anim = new Animation();
				
				for (var i = 0U; i < container.EntryLength; ++i) {
					var buffer = container[i]; // as anim use compresion is faster to work with its buffer
					var header = Utils.BuffToStruct<NewAnimHeader>(buffer, 0)[0];
					var frames = Utils.BuffToStruct<NewAnimFrame>(buffer, (int)header._FramesOffset, (int)header._FramesCount);

					
	Color[]  colors  = new Color[0x100];
	ushort[] palette = new ushort[0x100];
	for (int k = 0; k < header._ColourCount; k++) {
		//colors[k] = Color.FromArgb(BitConverter.ToInt32(buffer, (int)header._ColourOffset + 4*k));
		colors[k] = Color.FromArgb(buffer[header._ColourOffset+4*k + 0], buffer[header._ColourOffset+4*k + 1], buffer[header._ColourOffset+4*k + 2]);
		//palette[k] = (ushort)((colors[k].R >> 3) << 10 | (colors[k].G >> 3) << 5 | (colors[k].B >> 3));// | (bin.ReadByte()>>7)<<15);
		//palette[k] ^= 0x8000;
		palette[k] = (ushort)(colors[k].R >> 3 << 10 | colors[k].G >> 3 << 5 | colors[k].B >> 3 << 0); if (palette[k] > 0) palette[k] |= 0x8000;

		//palette[p] = (ushort)(bin.ReadUInt16() ^ 0x8000);
	}
	//palette = Utils.BuffToStruct<ushort>(buffer, (int)header._ColourOffset, 0x100);
	//for (int p = 0; p < 0x100; p++)
	//    palette[p] ^= 0x8000;
if (header._AnimationID == 0197) {
	

					//ushort[] palete = palette;
					//for (var f = 0U; f < frames.Length; f++) {
					//    int offset = (int)(header._FramesOffset + f * 16 + frames[f]._FrameLook);
					//    palete = Utils.BuffToStruct<ushort>(buffer, offset, 0x100);
					//    for (int p = 0; p < 0x100; p++)
					//        if (palette[p] > 0)
					//            palete[p] |= 0x8000;
					//    Bitmap   bitmap;
					//    ConvertAnimSurface(buffer, palete, out bitmap, offset + 0x200);
					//    short centrx = BitConverter.ToInt16(buffer, offset + 0x200);
					//    short centry = BitConverter.ToInt16(buffer, offset + 0x202);

					//    var path = Path.Combine(@"C:\UltimaOnline", Path.GetFileNameWithoutExtension((container as UopContainer).FNameUop), String.Format("{0:D4}_{1:D2}_{2:D2}.bmp", header._AnimationID, ooo, f));
					//    if (Directory.Exists((container as UopContainer).FNameUop))
					//        Directory.CreateDirectory((container as UopContainer).FNameUop);
					//    if (bitmap != null)
					//        bitmap.Save(path);
					//}
++ooo;
}

				}


			}
			return list.ToArray();
		}

		

		[Serializable]
		public sealed class ClassicFactoryException : OutOfMemoryException 
		{
			private string  idxdatInfo = null;
			private string  muldatInfo = null;
			private uint    dataidInfo = 0x00;

			public string IdxdatInfo
			{
				get { return idxdatInfo; }
			}

			public string MuldatInfo
			{
				get { return muldatInfo; }
			}

			public uint   DataidInfo
			{
				get { return DataidInfo; }
			}

			public override string Message
			{
				get
				{
					var message = String.Format("{0} (in \"{1}\", \"{2}\" at 0x{3:X8})", base.Message, idxdatInfo ?? "<null>", muldatInfo ?? "<null>", dataidInfo);
					return message;
				}
			}

			internal ClassicFactoryException() : base() { }
			internal ClassicFactoryException(string message) : base(message) { }
			internal ClassicFactoryException(string message, Exception innerException) : base(message, innerException) { }
			internal ClassicFactoryException(string message, IDataContainer container, uint id) : base(message) 
			{
				this.idxdatInfo = (container as MulContainer).FNameIdx;
				this.muldatInfo = (container as MulContainer).FNameMul;
				this.dataidInfo = id;
			}
		   
			protected  ClassicFactoryException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				idxdatInfo = info.GetString("IdxdatInfo");
				muldatInfo = info.GetString("MuldatInfo");
				dataidInfo = info.GetUInt32("DataidInfo");
			}

			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("IdxdatInfo", idxdatInfo);
				info.AddValue("MuldatInfo", muldatInfo);
				info.AddValue("DataidInfo", dataidInfo);

				base.GetObjectData(info, context);
			}

			public override string ToString()
			{
				return base.ToString();
			}
		}

	}
}
