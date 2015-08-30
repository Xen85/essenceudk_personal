using EssenceUDK.MapMaker.Elements;
using EssenceUDK.MapMaker.Elements.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;
using EssenceUDK.MapMaker.Elements.Interfaces;
using EssenceUDK.MapMaker.Elements.Items.Items;
using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDK.MapMaker.Elements.Textures;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using EssenceUDK.MapMaker.Elements.Textures.TexureCliff;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Color = System.Windows.Media.Color;

namespace EssenceUDK.MapMaker.MapMaking
{
    public class MapMaker
    {
        #region Fields

        public int MinX = 2;
        public int MinY = 1;
        private readonly int _stride;

        //private Color[] _bitmap;
        private sbyte[] _bitmapZ;

        private double _progressPerc;

        private readonly AreaColor[] _bitmapAreaColor;

        /// <summary>
        /// max x of your map
        /// </summary>
        private readonly int _x;

        /// <summary>
        /// max y of your map
        /// </summary>
        private readonly int _y;

        #endregion Fields

        #region props

        #region scripts

        #region scripts for areas

        public CollectionAreaColor CollectionAreaColor { get; set; }
        //public CollectionAreaColor ColorAreasCoast { get; set; }
        //public CollectionAreaColorMountains ColorMountainsAreas { get; set; }

        #endregion scripts for areas

        //#region scripts for Items
        //public CollectionAreaItems Items { get; set; }
        //public CollectionAreaTransitionItemCoast ItemsCoasts { get; set; }
        //public CollectionAreaTransitionItems ItemsSmooth { get; set; }
        //#endregion

        #region Textures

        public CollectionAreaTexture TextureAreas { get; set; }
        //public CollectionAreaTransitionTexture TxtureSmooth { get; set; }
        //public CollectionAreaTransitionCliffTexture CollectionAreaCliffs { get; set; }

        #endregion Textures

        #endregion scripts

        #region Matrix for making

        ///// <summary>
        ///// temp map used for mountains and smooth
        ///// </summary>
        //private readonly int[] _MapOcc;

        ///// <summary>
        ///// Calculated altitude of the map
        ///// </summary>
        //private readonly int[] _MapAlt;

        ///// <summary>
        ///// Id Texture of the map
        ///// </summary>
        //private readonly int[] _MapID;

        ///// <summary>
        ///// items of the map
        ///// </summary>
        //private readonly List<Item>[] _AddItemMap;

        /// <summary>
        /// all the tiles of the map
        /// </summary>
        private readonly MapObject[] _mapObjects;

        ///// <summary>
        ///// temp copy of the map
        ///// </summary>
        //private readonly Color[] _Tmp;

        #endregion Matrix for making

        /// <summary>
        /// directory where you're going to write your mul files
        /// </summary>
        public string MulDirectory { get; set; }

        /// <summary>
        /// index of the map that you're going to make
        /// </summary>
        public int MapIndex { get; set; }

        /// <summary>
        /// if you want to edit muls or not
        /// </summary>
        public bool EditMul { get; set; }

        /// <summary>
        /// Directory of the muls you're editing
        /// </summary>
        public String MulEditingDirectory { get; set; }

        /// <summary>
        /// if you want to process the Z automatically or not
        /// </summary>
        public Boolean AutomaticZMode { get; set; }

        #endregion props

        #region ctor

        ///// <summary>
        ///// Class costructor
        ///// </summary>
        ///// <param name="map">map cached previusly</param>
        ///// <param name="alt">map altitude cached</param>
        ///// <param name="x">max x of the map</param>
        ///// <param name="y">max y of the map</param>
        ///// <param name="index">index of the map</param>
        //public MapMaker(Color[] map, Color[] alt, int x, int y, int index)
        //{
        //    _bitmap = map;
        //    var x1 = x + 10;
        //    var y1 = y + 10;
        //    var lenght = x1 * y1;
        //    #region InitArrays

        //    _mapObjects = new MapObject[lenght];
        //    _bitmapZ = new sbyte[lenght];
        //    for (int i = 0; i < alt.Length; i++)
        //    {
        //        _bitmapZ[i] = CalculateHeightValue(alt[i]);

        //    }

        //    for (int i = 0; i < _mapObjects.Length; i++)
        //    {
        //        _mapObjects[i] = new MapObject();
        //    }
        //    #endregion

        //    _X = x;
        //    _Y = y;

        //    MulDirectory = "";
        //    mapIndex = index;
        //    _stride = _X;
        //    Random = new Random(DateTime.Now.Millisecond);

        //    AutomaticZMode = true;
        //}

        public MapMaker(sbyte[] altitude, AreaColor[] colors, int x, int y, int index)
        {
            var x1 = x + 10;
            var y1 = y + 10;
            var lenght = x1 * y1;

            #region Init Arrays

            _mapObjects = new MapObject[lenght];
            _bitmapZ = new sbyte[lenght];
            _bitmapAreaColor = new AreaColor[lenght];

            for (int i = 0; i < altitude.Length; i++)
            {
                _bitmapZ[i] = altitude[i];
                _bitmapAreaColor[i] = colors[i];
            }

            for (int i = 0; i < lenght; i++)
            {
                _mapObjects[i] = new MapObject();
            }

            #endregion Init Arrays

            _x = x;
            _y = y;
            MulDirectory = "";
            MapIndex = index;
            _stride = _x;
        }

        #endregion ctor

        #region Methods

        #region Make

        public IUltimaMapDataProvider EditingMap { get; set; }

        public void MapEditing()
        {
            #region initialize data search

            CollectionAreaColor.InitializeSeaches();
            foreach (var variable in CollectionAreaColor.List)
            {
                variable.InizializeSearches();
            }
            TextureAreas.InitializeSeaches();

            #endregion initialize data search

            var count = Environment.ProcessorCount;
            var maptasks = new Task[count];
            OnProgressText(new ProgressEventArgs() { PayLoad = "Making Editing" });

            //for ( int i = 0; i < count; i++ )
            //	{
            //	var minX = i == 0 ? MinX : ( _x/count )*( i ) - 1;
            //	var MaxX = i < count ? ( _x/count )*( i + 1 ) : _x;

            //		maptasks[i] = new Task(() => EditMapThread(MaxX, _y, minX, MinY));
            //		maptasks[i].Start();
            //	}
            EditMapThread(_x, _y, 0, 0);
            try
            {
                //Task.WaitAll(maptasks);
            }
            catch (AggregateException e)
            {
                throw e;
            }

            if (!AutomaticZMode)
            {
                _bitmapZ = null;
            }
            if (AutomaticZMode)
            {
                OnProgressText(new ProgressEventArgs() { PayLoad = "Making Altitude" });
                try
                {
                    ProcessZ(AutomaticZMode, null, new Coordinates(0, 0, 0, 0, 0, 0));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            _bitmapZ = null;

            OnProgressText(new ProgressEventArgs() { PayLoad = "Writing files" });
            try
            {
                var writeMul = new Task(WriteMUL);
                var writestatic = new Task(WriteStatics);
                var tasks = new[] { writeMul, writestatic };
                writeMul.Start();
                writestatic.Start();
                Task.WaitAll(tasks);
            }
            catch (Exception e)
            {
                throw e;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();

            OnProgressText(new ProgressEventArgs() { PayLoad = "Done" });
        }

        /// <summary>
        /// main method to build the map
        /// </summary>
        public void Bmp2Map()
        {
            #region initialize data search

            CollectionAreaColor.InitializeSeaches();
            foreach (var variable in CollectionAreaColor.List)
            {
                variable.InizializeSearches();
            }
            TextureAreas.InitializeSeaches();

            #endregion initialize data search

            var count = Environment.ProcessorCount;
            var maptasks = new Task[count];
            OnProgressText(new ProgressEventArgs() { PayLoad = "Making Map" });

            for (int i = 0; i < count; i++)
            {
                var minX = i == 0 ? MinX : (_x / count) * (i) - 1;
                var MaxX = i < count ? (_x / count) * (i + 1) : _x;

                maptasks[i] = new Task(() => BuildMapThread(MaxX, _y, minX, MinY));
                maptasks[i].Start();
            }
            try
            {
                Task.WaitAll(maptasks);
            }
            catch (AggregateException e)
            {
                throw e;
            }

            if (!AutomaticZMode)
            {
                _bitmapZ = null;
            }
            if (AutomaticZMode)
            {
                OnProgressText(new ProgressEventArgs() { PayLoad = "Making Altitude" });
                try
                {
                    ProcessZ(AutomaticZMode, null, new Coordinates(0, 0, 0, 0, 0, 0));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            _bitmapZ = null;

            OnProgressText(new ProgressEventArgs() { PayLoad = "Writing files" });
            try
            {
                var writeMul = new Task(WriteMUL);
                var writestatic = new Task(WriteStatics);
                var tasks = new[] { writeMul, writestatic };
                writeMul.Start();
                writestatic.Start();
                Task.WaitAll(tasks);
            }
            catch (Exception e)
            {
                throw e;
            }

            OnProgressText(new ProgressEventArgs() { PayLoad = "Done" });
        }

        #endregion Make

        #region MapInit

        private const int WaterLimitMin = 0x1796;
        private const int WaterLimitMax = 0x17B2;
        private const int WaterLimitMin2 = 0x346E;
        private const int WaterLimitMax2 = 0x3485;
        private const int WaterLimitMin3 = 0x3490;
        private const int WaterLimitMax3 = 0x34AB;
        private const int WaterLimitMin4 = 0x34B5;
        private const int WaterLimitMax4 = 0x34D5;

        /// <summary>
        /// Thread task to read the bmp and the muls files and generate the mapobject relatives
        /// </summary>
        /// <param name="X">max X coordinate</param>
        /// <param name="Y">max Y coordinate</param>
        /// <param name="minX">min X coordinate</param>
        /// <param name="minY">min Y coordinate </param>
        private void EditMapThread(int X, int Y, int minX, int minY)
        {
            //int y = minY;
            //int x = minX;
            //int ylast = minY;
            //int xlast = minX;
            var provider = EditingMap.Clone();
            var random = new Random();
            var mapCoordProvider = new BlockCoordinatesProvider(X, Y, minX, minY);
            var tmp = minY;
            try
            {
                while (mapCoordProvider.HasNextCoord())
                {
                    int x;
                    int y;

                    mapCoordProvider.GetNext(out x, out y);
                    var coordinates = MakeIndexesDirections(x, y, 1, 1);
                    var areacolorcoordinates = new AreaColorCoordinates(coordinates, _bitmapAreaColor);
                    var buildMapCoordinates = new MapObjectCoordinates(coordinates, _mapObjects);

                    if (ImportingStaticsAndMapFromMul(areacolorcoordinates.Center.Type == TypeColor.Special, x, y, provider,
                        buildMapCoordinates))
                        continue;

                    if (AutomaticZMode)
                        Mountain(areacolorcoordinates, buildMapCoordinates, coordinates, random);

                    TransparentFluid(areacolorcoordinates, buildMapCoordinates, coordinates, random);
                    MakeCoastUolStyle(areacolorcoordinates, buildMapCoordinates, coordinates, random);
                    TextureTransition(coordinates, areacolorcoordinates, buildMapCoordinates, random);
                    MakeCliffs(coordinates, areacolorcoordinates, buildMapCoordinates, random);
                    ItemsTransitions(coordinates, areacolorcoordinates, buildMapCoordinates, random);
                    PlaceTextures(areacolorcoordinates, buildMapCoordinates, coordinates, random);
                    if (!AutomaticZMode)
                        ProcessZ(AutomaticZMode, buildMapCoordinates, coordinates);

                    GetItemsFromTile(provider, buildMapCoordinates, false);

                    if (tmp != y)
                    {
                        tmp = y;
                        var percent1 = (byte)(Math.Round((double)((100 * mapCoordProvider.Progress) / (Y))));
                        OnProgressText(new ProgressEventArgs() { PayLoad = "Editing Map", Progress = (int)percent1 });
                    }
                }
                //while ( y < Y && x < X )
                //	{
                //	for ( y = ylast; y < ylast + 8 && y < Y; y++ )
                //		{
                //			for ( x = xlast; x < X  && x < xlast + 8; x++ )
                //				{
                //				//provider.GetCoordinates(( uint ) x, ( uint ) y);
                //				var coordinates = MakeIndexesDirections(x, y, 1, 1);
                //				var areacolorcoordinates = new AreaColorCoordinates(coordinates, _bitmapAreaColor);
                //				var buildMapCoordinates = new MapObjectCoordinates(coordinates, _mapObjects);

                //				ImportingStaticsAndMapFromMul(areacolorcoordinates.Center.Type == TypeColor.Special, x, y, provider,
                //						buildMapCoordinates);
                //				if ( areacolorcoordinates.Center.Type == TypeColor.Special )
                //					continue;

                //				if ( AutomaticZMode )
                //					Mountain(areacolorcoordinates, buildMapCoordinates, coordinates, random);

                //				TransparentFluid(areacolorcoordinates, buildMapCoordinates, coordinates, random);
                //				MakeCoastUolStyle(areacolorcoordinates, buildMapCoordinates, coordinates, random);
                //				TextureTransition(coordinates, areacolorcoordinates, buildMapCoordinates, random);
                //				MakeCliffs(coordinates, areacolorcoordinates, buildMapCoordinates, random);
                //				ItemsTransitions(coordinates, areacolorcoordinates, buildMapCoordinates, random);
                //				PlaceTextures(areacolorcoordinates, buildMapCoordinates, coordinates, random);
                //				if ( !AutomaticZMode )
                //					ProcessZ(AutomaticZMode, buildMapCoordinates, coordinates);

                //				}
                //		//ended
                //		if ( y % 8 == 7 && y!=0 )
                //			xlast += 8;

                //		}

                //	// i'm in the end
                //	if ( xlast < X - 8 || y % 8 != 0 ) continue;
                //	xlast = 0;
                //	x = 0;
                //	ylast += 8;
                //	}

                //var percent1 = ( 100*( X - minX ) )/( _x );
                //	_progressPerc += percent1;
                //	OnProgressText(new ProgressEventArgs() { PayLoad = "Editing Map", Progress = ( byte ) Math.Round(_progressPerc) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool ImportingStaticsAndMapFromMul(bool isSpecial, int x, int y, IUltimaMapDataProvider provider, MapObjectCoordinates buildMapCoordinates)
        {
            //int idTile;
            //sbyte altitudeTile;

            //provider.GetTile((uint) x, (uint) y,out idTile,out altitudeTile);
            provider.GetCoordinates((uint)x, (uint)y);
            if (isSpecial) GetItemsFromTile(provider, buildMapCoordinates);

            if (!isSpecial) return false;
            buildMapCoordinates.Center.Texture = (short)provider.Land;
            buildMapCoordinates.Center.Altitude = provider.Atitude;

            return true;
        }

        private static void GetItemsFromTile(IUltimaMapDataProvider provider, MapObjectCoordinates buildMapCoordinates, bool isSpecial = true)
        {
            IList<int> idItems;
            IList<sbyte> altitudeItems;
            IList<int> palette;
            provider.GetItems(out altitudeItems, out idItems, out palette);

            for (int i = 0; i < idItems.Count; i++)
            {
                if (isSpecial == false)
                {
                    var id = idItems[i];
                    if (id >= WaterLimitMin && id <= WaterLimitMax)
                        continue;
                    if (id >= WaterLimitMin2 && id <= WaterLimitMax2)
                        continue;
                    if (id >= WaterLimitMin3 && id <= WaterLimitMax3)
                        continue;
                    if (id >= WaterLimitMin4 && id <= WaterLimitMax4)
                        continue;
                }
                buildMapCoordinates.Center.AddItem(idItems[i], palette[i], altitudeItems[i]);
            }
        }

        /// <summary>
        /// Thread task to read the bmp file and generate the mapobject relatives
        /// </summary>
        /// <param name="X">max X coordinate</param>
        /// <param name="Y">max Y coordinate</param>
        /// <param name="minX">min X coordinate</param>
        /// <param name="minY">min Y coordinate </param>
        private void BuildMapThread(int X, int Y, int minX, int minY)
        {
            try
            {
                var random = new Random();
                for (var x = minX; x < X - 1; x++)
                {
                    for (var y = minY; y < Y - 1; y++)
                    {
                        var coordinates = MakeIndexesDirections(x, y, 1, 1);
                        var areacolorcoordinates = new AreaColorCoordinates(coordinates, _bitmapAreaColor);

                        var buildMapCoordinates = new MapObjectCoordinates(coordinates, _mapObjects);
                        if (AutomaticZMode)
                            Mountain(areacolorcoordinates, buildMapCoordinates, coordinates, random);

                        TransparentFluid(areacolorcoordinates, buildMapCoordinates, coordinates, random);
                        MakeCoastUolStyle(areacolorcoordinates, buildMapCoordinates, coordinates, random);
                        TextureTransition(coordinates, areacolorcoordinates, buildMapCoordinates, random);
                        MakeCliffs(coordinates, areacolorcoordinates, buildMapCoordinates, random);
                        ItemsTransitions(coordinates, areacolorcoordinates, buildMapCoordinates, random);
                        PlaceTextures(areacolorcoordinates, buildMapCoordinates, coordinates, random);
                        if (!AutomaticZMode)
                            ProcessZ(AutomaticZMode, buildMapCoordinates, coordinates);
                    }
                }
                var percent1 = (100 * (X - minX)) / (_x);
                _progressPerc += percent1;
                OnProgressText(new ProgressEventArgs()
                { PayLoad = "Making Map", Progress = (byte)Math.Round(_progressPerc) });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void PlaceTextures(AreaColorCoordinates areaColorCoordinates, MapObjectCoordinates mapObjectCoordinates,
                                   Coordinates coordinates, Random random)
        {
            if (areaColorCoordinates.Center == null) return;

            if (mapObjectCoordinates.Center.Texture == 0)
                mapObjectCoordinates.Center.Texture =
                    (short)RandomTexture(areaColorCoordinates.Center.TextureIndex, random);
        }

        /// <summary>
        /// it's used to process the map automatically
        /// </summary>
        /// <param name="mode">mode of how you want to process the map 0 for following the map, 1 to calculate automatically</param>
        /// <param name="mapObjectCoordinates"> </param>
        /// <param name="coordinates"> </param>
        private void ProcessZ(bool mode, MapObjectCoordinates mapObjectCoordinates, Coordinates coordinates)
        {
            if (!mode)
            {
                mapObjectCoordinates.Center.Altitude = _bitmapZ[coordinates.Center];
            }
            else
            {
                try
                {
                    var Random = new Random();
                    int x;
                    for (x = MinX; x < _x - 1; x++)
                    {
                        var percent1 = (byte)((100 * x) / (_x));
                        OnProgressText(new ProgressEventArgs() { PayLoad = "Making Altitude", Progress = percent1 });
                        int y;
                        for (y = MinY; y < _y - 1; y++)
                        {
                            var location = CalculateZone(x, y, _stride);
                            var area = _bitmapAreaColor[location];

                            if (_mapObjects[location].Altitude == 0 && area.Type != TypeColor.Special)
                                _mapObjects[location].Altitude += (sbyte)Random.Next(area.Min, area.Max);
                            if (_mapObjects[location].Altitude >= 120 && area.Type != TypeColor.Special)
                                _mapObjects[location].Altitude = (sbyte)(Random.Next(120, 125));
                            if (area.Type != TypeColor.Special)
                            {
                                var z = _bitmapZ[location];
                                var tmp = _mapObjects[location].Altitude + z;
                                if (tmp < sbyte.MinValue)
                                    tmp = sbyte.MinValue;
                                if (tmp > sbyte.MaxValue)
                                    tmp = sbyte.MaxValue;
                                _mapObjects[location].Altitude = (sbyte)tmp;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        #endregion MapInit

        #region Transitions

        #region mountains

        /// <summary>
        /// method to init the mountains textures
        /// </summary>
        private void Mountain(AreaColorCoordinates areacoord, MapObjectCoordinates mapObjectCoordinates,
                              Coordinates coord, Random random)
        {
            if (areacoord.Center == null || areacoord.Center.Type != TypeColor.Moutains) return;

            mapObjectCoordinates.Center.Altitude =
                (sbyte)random.Next(areacoord.Center.Min, areacoord.Center.Max);
            if (!areacoord.Center.ModeAutomatic) return;

            for (int index = 0; index < areacoord.Center.List.Count; index++)
            {
                var cirlce = areacoord.Center.List[index];

                var areacircles =
                    new AreaColorCoordinates(
                        new Coordinates(index, index, coord.X, coord.Y, _stride, _bitmapAreaColor.Length),
                        _bitmapAreaColor);

                if (areacircles.List == null)
                {
                    break;
                }
                if (areacircles.List.Any(c => c == null || c.Type != TypeColor.Moutains))
                {
                    break;
                }

                mapObjectCoordinates.Center.Altitude = (sbyte)random.Next(cirlce.From, cirlce.To);
                if (mapObjectCoordinates.Center.Altitude > 127)
                    mapObjectCoordinates.Center.Altitude = (sbyte)(random.Next(120, 125));
                if (index >= (areacoord.Center.List.Count / 3) * 2 && areacoord.Center.IndexColorTopMountain != 0)
                {
                    var area = _bitmapAreaColor[coord.Center];
                    area = CollectionAreaColor.FindByIndex(area.IndexColorTopMountain);
                    mapObjectCoordinates.Center.Texture = (short)RandomTexture(area.TextureIndex, random);
                    _bitmapAreaColor[coord.Center] = CollectionAreaColor.FindByColor(area.ColorTopMountain);
                }
            }
        }

        #endregion mountains

        #region Texture Transitions

        /// <summary>
        /// transitions from a kind of terrain to anotherkind
        /// </summary>
        /// <param name="coordinates"> </param>
        /// <param name="areaColorCoordinates"> </param>
        /// <param name="mapObjectCoordinates"> </param>
        private void TextureTransition(Coordinates coordinates, AreaColorCoordinates areaColorCoordinates,
                                       MapObjectCoordinates mapObjectCoordinates, Random random)
        {
            var textureids = areaColorCoordinates.List.Select(o => o.TextureIndex).Distinct();
            var enumerable = textureids as int[] ?? textureids.ToArray();
            if (enumerable.Count() == 1)
                return;
            if (areaColorCoordinates.Center.Type == TypeColor.Cliff) return;

            var transitionList = TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture;

            if (!transitionList.List.Any())
                return;

            transitionList.InitializeSeaches();

            if (areaColorCoordinates.List.All(o => o.Color == areaColorCoordinates.Center.Color))
                return;
            AreaTransitionTexture textureTransition = null;
            foreach (var id in enumerable)
            {
                textureTransition = transitionList.FindById(id);
                if (textureTransition != null)
                    break;
            }
            if (textureTransition == null)
                return;

            int special = 0;
            int z = 0;

            if (mapObjectCoordinates.Center.Occupied != 0) return;

            #region Line

            //Line
            // B
            //xAx
            //y1 = y - 1;
            if (
                areaColorCoordinates.North.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.East.TextureIndex != areaColorCoordinates.North.TextureIndex
                && areaColorCoordinates.West.TextureIndex != areaColorCoordinates.North.TextureIndex
                )
            {
                var transation =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.North.TextureIndex);
                if (transation != null)
                {
                    special = 1;
                    mapObjectCoordinates.Center.Texture = (short)RandomFromList(transation.LineSouth.List, random);
                    z = mapObjectCoordinates.North.Altitude +
                        mapObjectCoordinates.South.Altitude;
                }
            }

            //xAx
            // B
            ////y1 = y + 1;
            if (
                areaColorCoordinates.South.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.East.TextureIndex != areaColorCoordinates.South.TextureIndex
                && areaColorCoordinates.West.TextureIndex != areaColorCoordinates.South.TextureIndex
                )
            {
                var transation =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.South.TextureIndex);
                if (transation != null)
                {
                    special = 2;
                    mapObjectCoordinates.Center.Texture = (short)RandomFromList(transation.LineNorth.List, random);
                    z = mapObjectCoordinates.South.Altitude +
                        mapObjectCoordinates.North.Altitude;
                }
            }
            //x
            //AB
            //x
            //x1 = x + 1;
            if (
                areaColorCoordinates.East.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.North.TextureIndex != areaColorCoordinates.East.TextureIndex
                && areaColorCoordinates.South.TextureIndex != areaColorCoordinates.East.TextureIndex
                )
            {
                var transation =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.East.TextureIndex);
                if (transation != null)
                {
                    special = 2;
                    mapObjectCoordinates.Center.Texture =
                        (short)RandomFromList(transation.LineWest.List, random);
                    z = mapObjectCoordinates.East.Altitude +
                        mapObjectCoordinates.West.Altitude;
                }
            }
            // x
            //BA
            // x
            //x1 = x - 1;
            if (
                areaColorCoordinates.West.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.West.TextureIndex != areaColorCoordinates.North.TextureIndex
                && areaColorCoordinates.West.TextureIndex != areaColorCoordinates.South.TextureIndex
                )
            {
                var transation =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.West.TextureIndex);
                if (transation != null)
                {
                    special = 1;
                    mapObjectCoordinates.Center.Texture =
                        (short)RandomFromList(transation.LineEast.List, random);
                    z = mapObjectCoordinates.West.Altitude +
                        mapObjectCoordinates.East.Altitude;
                }
            }

            #endregion Line

            #region Border

            //Border
            //xB
            //Ax
            if (
                areaColorCoordinates.NorthEast.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.East.TextureIndex != areaColorCoordinates.NorthEast.TextureIndex
                && areaColorCoordinates.North.TextureIndex != areaColorCoordinates.NorthEast.TextureIndex
                )
            {
                //var transition = areaColorCoordinates.Center.FindTransitionTexture(areaColorCoordinates.NorthEast.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.NorthEast.TextureIndex);
                special = 2;
                if (transition != null)
                {
                    mapObjectCoordinates.Center.Texture =
                        (short)RandomFromList(transition.BorderSouthWest.List, random);
                    z = mapObjectCoordinates.NorthEast.Altitude +
                        mapObjectCoordinates.SouthWest.Altitude;
                }
            }

            //Bx
            //xA
            if (
                areaColorCoordinates.NorthWest.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.West.TextureIndex != areaColorCoordinates.NorthWest.TextureIndex
                && areaColorCoordinates.North.TextureIndex != areaColorCoordinates.NorthWest.TextureIndex
                )
            {
                //var transition = areaColorCoordinates.Center.FindTransitionTexture(areaColorCoordinates.NorthWest.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.NorthWest.TextureIndex);
                if (transition != null)
                {
                    special = 1;
                    mapObjectCoordinates.Center.Texture =
                        (short)RandomFromList(transition.BorderSouthEast.List, random);

                    z = mapObjectCoordinates.NorthWest.Altitude +
                        mapObjectCoordinates.SouthEast.Altitude;
                }
            }

            //GA
            //BG
            if (
                areaColorCoordinates.SouthWest.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.West.TextureIndex != areaColorCoordinates.SouthWest.TextureIndex
                && areaColorCoordinates.South.TextureIndex != areaColorCoordinates.SouthWest.TextureIndex
                )
            {
                //var transation = areaColorCoordinates.Center.FindTransitionTexture(areaColorCoordinates.SouthWest.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.SouthWest.TextureIndex);
                if (transition != null)
                {
                    special = 2;

                    mapObjectCoordinates.Center.Texture =
                        (short)RandomFromList(transition.BorderNorthEast.List, random);
                    z = mapObjectCoordinates.NorthWest.Altitude +
                        mapObjectCoordinates.NorthEast.Altitude;
                }
            }
            //Ax
            //xB
            if (
                areaColorCoordinates.SouthEast.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.East.TextureIndex != areaColorCoordinates.SouthEast.TextureIndex
                && areaColorCoordinates.South.TextureIndex != areaColorCoordinates.SouthEast.TextureIndex
                )
            {
                //var transation = areaColorCoordinates.Center.FindTransitionTexture(areaColorCoordinates.SouthEast.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.SouthEast.TextureIndex);
                if (transition != null)
                {
                    special = 2;
                    mapObjectCoordinates.Center.Texture =
                        (short)RandomFromList(transition.BorderNorthWest.List, random);
                    z = _mapObjects[coordinates.SouthEast].Altitude +
                        _mapObjects[coordinates.NorthWest].Altitude;
                }
            }

            #endregion Border

            #region Edge

            //Edge
            //B
            //AB

            if (
                areaColorCoordinates.NorthEast.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.North.TextureIndex == areaColorCoordinates.NorthEast.TextureIndex
                && areaColorCoordinates.NorthEast.TextureIndex == areaColorCoordinates.East.TextureIndex
                )
            {
                //var transition =
                //    areaColorCoordinates.Center.FindTransitionTexture(areaColorCoordinates.NorthEast.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.NorthEast.TextureIndex);
                if (transition != null)

                {
                    special = 2;
                    mapObjectCoordinates.Center.Texture =
                        (short)RandomFromList(transition.EdgeSouthWest.List, random);
                    z = mapObjectCoordinates.NorthEast.Altitude +
                        mapObjectCoordinates.SouthWest.Altitude;
                }
            }

            // B
            //BA
            if (
                areaColorCoordinates.NorthWest.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.North.TextureIndex == areaColorCoordinates.NorthWest.TextureIndex
                && areaColorCoordinates.NorthWest.TextureIndex == areaColorCoordinates.West.TextureIndex
                )
            {
                //var transation =
                //    areaColorCoordinates.Center.FindTransitionTexture(areaColorCoordinates.West.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.West.TextureIndex);
                if (transition != null)
                {
                    special = 1;
                    mapObjectCoordinates.Center.Texture =
                        (short)RandomFromList(transition.EdgeSouthEast.List, random);
                    z = mapObjectCoordinates.NorthEast.Altitude +
                        mapObjectCoordinates.SouthWest.Altitude;
                }
            }

            //BA
            // B
            if (
                areaColorCoordinates.SouthWest.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.South.TextureIndex == areaColorCoordinates.SouthWest.TextureIndex
                && areaColorCoordinates.SouthWest.TextureIndex == areaColorCoordinates.West.TextureIndex
                )
            {
                //var transation =
                //    areaColorCoordinates.Center.FindTransitionTexture(areaColorCoordinates.SouthWest.Color);

                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.SouthWest.TextureIndex);
                if (transition != null)
                {
                    special = 2;

                    mapObjectCoordinates.Center.Texture = (short)RandomFromList(transition.EdgeNorthEast.List, random);
                    z = mapObjectCoordinates.SouthWest.Altitude +
                        mapObjectCoordinates.NorthEast.Altitude;
                }
            }

            //AB
            //B
            if (
                areaColorCoordinates.SouthEast.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.South.TextureIndex == areaColorCoordinates.SouthEast.TextureIndex
                && areaColorCoordinates.East.TextureIndex == areaColorCoordinates.SouthEast.TextureIndex
                )
            {
                //var transation =
                //    areaColorCoordinates.Center.FindTransitionTexture(areaColorCoordinates.SouthEast.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].AreaTransitionTexture.FindById(
                        areaColorCoordinates.SouthEast.TextureIndex);
                if (transition != null)
                {
                    special = 2;

                    mapObjectCoordinates.Center.Texture = (short)RandomFromList(transition.EdgeNorthWest.List, random);
                    z = mapObjectCoordinates.SouthEast.Altitude +
                        mapObjectCoordinates.NorthWest.Altitude;
                }
            }
            if (special > 0)
                mapObjectCoordinates.Center.Occupied = 1;

            if (_bitmapZ[coordinates.Center] == 128)
            {
                _mapObjects[coordinates.Center].Altitude = (sbyte)(z / 2);
            }

            #endregion Edge
        }

        #endregion Texture Transitions

        #region Items Transitions

        /// <summary>
        /// function to make the translations with items
        /// </summary>
        /// <param name="coordinates"> </param>
        /// <param name="areaColorCoordinates"> </param>
        /// <param name="mapObjectCoordinates"> </param>
        private void ItemsTransitions
            (Coordinates coordinates,
             AreaColorCoordinates areaColorCoordinates,
             MapObjectCoordinates
                 mapObjectCoordinates,
             Random random)
        {
            if (areaColorCoordinates.Center.Type == TypeColor.Cliff) return;
            if (mapObjectCoordinates.Center.Items != null || mapObjectCoordinates.Center.Occupied != 0) return;
            //int special = 0;
            var texturelist = areaColorCoordinates.List.Select(o => o.TextureIndex).Distinct();
            if (texturelist.Count() <= 1)
                return;

            var transitionList = TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].CollectionAreaItems;
            if (!transitionList.List.Any())
                return;
            transitionList.InitializeSeaches();
            if (areaColorCoordinates.List.All(o => o.Color == areaColorCoordinates.Center.Color))
                return;

            var indexes = areaColorCoordinates.List.Select(o => o.TextureIndex);
            AreaTransitionItem k = null;

            foreach (var id in indexes)
            {
                k = transitionList.FindById(id);
                if (k != null) break;
            }
            if (k == null) return;

            int zlev = 0;
            var item = new ItemClone();

            #region Border

            //Border
            //GB
            //xG
            if (areaColorCoordinates.NorthEast.Color != areaColorCoordinates.Center.Color)
            {
                var transation =
                    areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.NorthEast.Color);
                //7
                if (transation != null)
                    item = new ItemClone() { Id = RandomFromList(transation.BorderSouthWest.List, random) };
            }
            //BG
            //Gx
            if (areaColorCoordinates.NorthWest.Color != areaColorCoordinates.Center.Color)
            {
                var transation =
                    areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.NorthWest.Color);
                //6
                if (transation != null)
                    item = new ItemClone { Id = RandomFromList(transation.BorderSouthEast.List, random) };
            }
            //Gx
            //BG
            if (areaColorCoordinates.SouthWest.Color != areaColorCoordinates.Center.Color)
            {
                var transation =
                    areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.SouthWest.Color);
                //5
                if (transation != null)
                    item = new ItemClone { Id = RandomFromList(transation.BorderNorthEast.List, random) };
            }
            //xG
            //GB
            if (areaColorCoordinates.SouthEast.Color != areaColorCoordinates.Center.Color)
            {
                var transation =
                    areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.SouthEast.Color);
                //4
                if (transation != null)
                    item = new ItemClone { Id = RandomFromList(transation.BorderNorthWest.List, random) };
            }

            #endregion Border

            #region Line

            //Line
            // B
            //GxG
            if (areaColorCoordinates.North.TextureIndex != areaColorCoordinates.Center.TextureIndex)
            {
                //var transation = areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.North.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].CollectionAreaItems.FindById(
                        areaColorCoordinates.North.TextureIndex);
                //2
                if (transition != null) item = new ItemClone { Id = RandomFromList(transition.LineSouth.List, random) };
            }
            //GxG
            // B
            if (areaColorCoordinates.South.TextureIndex != areaColorCoordinates.Center.TextureIndex)
            {
                //var transation = areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.South.Color);
                var transition = TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].CollectionAreaItems.
                    FindById(
                        areaColorCoordinates.South.TextureIndex);
                //0
                if (transition != null) item = new ItemClone { Id = RandomFromList(transition.LineNorth.List, random) };
            }
            //G
            //xB
            //G
            if (areaColorCoordinates.East.TextureIndex != areaColorCoordinates.Center.TextureIndex)
            {
                //var transation = areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.East.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].CollectionAreaItems.FindById(
                        areaColorCoordinates.East.TextureIndex);
                //3
                if (transition != null) item = new ItemClone { Id = RandomFromList(transition.LineWest.List, random) };
            }
            // G
            //Bx
            // G
            if (areaColorCoordinates.West.TextureIndex != areaColorCoordinates.Center.TextureIndex)
            {
                //var transation = areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.West.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].CollectionAreaItems.FindById(
                        areaColorCoordinates.West.TextureIndex);
                //1
                if (transition != null) item = new ItemClone { Id = RandomFromList(transition.LineEast.List, random) };
            }

            #endregion Line

            #region Edge

            //Edge
            //B
            //xB
            if (
                areaColorCoordinates.East.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.North.TextureIndex != areaColorCoordinates.Center.TextureIndex
                )
            {
                //var transation = areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.NorthEast.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].CollectionAreaItems.FindById(
                        areaColorCoordinates.NorthEast.TextureIndex);
                //11
                if (transition != null)
                    item = new ItemClone { Id = RandomFromList(transition.EdgeSouthWest.List, random) };
            }
            // B
            //Bx
            if (
                areaColorCoordinates.West.TextureIndex != areaColorCoordinates.Center.TextureIndex &&
                areaColorCoordinates.North.TextureIndex != areaColorCoordinates.Center.TextureIndex
                )
            {
                //var transation = areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.NorthWest.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].CollectionAreaItems.FindById(
                        areaColorCoordinates.NorthWest.TextureIndex);
                //10
                if (transition != null)
                    item = new ItemClone { Id = RandomFromList(transition.EdgeSouthEast.List, random) };
            }
            //Bx
            // B
            if (
                areaColorCoordinates.West.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.South.TextureIndex != areaColorCoordinates.Center.TextureIndex
                )
            {
                //var transation = areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.SouthWest.Color);
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].CollectionAreaItems.FindById(
                        areaColorCoordinates.SouthWest.TextureIndex);
                //9
                if (transition != null)
                    item = new ItemClone { Id = RandomFromList(transition.EdgeNorthEast.List, random) };
            }
            //xB
            //B
            if (
                areaColorCoordinates.East.TextureIndex != areaColorCoordinates.Center.TextureIndex
                && areaColorCoordinates.South.TextureIndex != areaColorCoordinates.Center.TextureIndex
                )
            {
                var transition =
                    TextureAreas._fast[areaColorCoordinates.Center.TextureIndex].CollectionAreaItems.FindById(
                        areaColorCoordinates.SouthEast.TextureIndex);
                //var transation = areaColorCoordinates.Center.FindTransationItemByColor(areaColorCoordinates.SouthEast.Color);
                //8
                if (transition != null)
                    item = new ItemClone { Id = RandomFromList(transition.EdgeNorthWest.List, random) };
            }

            #endregion Edge

            if (item.Id == 0) return;

            var coast = areaColorCoordinates.Center;

            if (coast.Type == TypeColor.Water)
            {
                //zlev = random.Next(coast.Min, coast.Max);
                item.Z = (sbyte)areaColorCoordinates.Center.ItemsAltitude;
            }

            if (mapObjectCoordinates.Center.Items == null)
                mapObjectCoordinates.Center.Items = new List<ItemClone>();
            mapObjectCoordinates.Center.Items.Add(item);
        }

        #endregion Items Transitions

        #endregion Transitions

        #region Coasts UOL style

        #region Lines

        #region South

        private static bool PlaceObjectSouth(
            AreaColorCoordinates areaColorCoordinates,
            MapObjectCoordinates mapObjectCoordinates,
            Coordinates coordinates,
            TypeColor trueType,
            sbyte zItem,
            sbyte altitude,
            int itemid,
            int texture = -1,
            bool ground = false,
            bool occupied = true,
            int hue = 0
            )
        {
            return areaColorCoordinates.IsSouthLine(trueType) &&
                   mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates, altitude, itemid, zItem, texture, ground, occupied, hue);
        }

        #endregion South

        #region North

        private static bool PlaceObjectNorth(
               AreaColorCoordinates areaColorCoordinates,
               MapObjectCoordinates mapObjectCoordinates,
               Coordinates coordinates,
               TypeColor trueType,
               sbyte zItem,
               sbyte altitude,
               int itemid,
                Random random,
                int texture = -1,
                bool ground = false,
            bool occupied = true,
            int hue = 0
               )
        {
            return areaColorCoordinates.IsNorthLine(trueType) && mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates, altitude, itemid, zItem, texture, ground, occupied, hue);
        }

        #endregion North

        #region WEST LINE

        private static bool PlaceObjectWest(AreaColorCoordinates areaColorCoordinates,
            MapObjectCoordinates mapObjectCoordinates,
            Coordinates coordinates,
            TypeColor type,
            sbyte zItem,
            sbyte altitude,
            int itemid,
            Random random,
            int texture = -1,
            bool ground = false,
            bool occupied = true,
            int hue = 0
            )
        {
            return areaColorCoordinates.IsWestLine(type) && mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates, altitude, itemid, zItem, texture, ground, occupied, hue);
        }

        #endregion WEST LINE

        #region EAST LINE

        private static bool PlaceObjectEast(AreaColorCoordinates areaColorCoordinates,
            MapObjectCoordinates mapObjectCoordinates,
            Coordinates coordinates,
            TypeColor type,
            sbyte zItem,
            sbyte altitude,
            int itemid,
            int texture = -1,
            bool ground = false,
            bool occupied = true,
            int hue = 0
            )
        {
            return areaColorCoordinates.IsEastLine(type)
                && mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates, altitude, itemid, zItem, texture, ground, occupied, hue);
        }

        #endregion EAST LINE

        #endregion Lines

        #region Edges

        #region SouthWestEdge

        private static bool PlaceObjectSouthWestEdge(
            AreaColorCoordinates areaColorCoordinates,
            MapObjectCoordinates mapObjectCoordinates,
            Coordinates coordinates,
            TypeColor type,
            sbyte zItem,
            sbyte altitude,
            int itemid,
            Random random,
            int texture = -1,
            bool ground = false,
            bool occupied = true,
            int hue = 0
            )
        {
            return areaColorCoordinates.IsSouthWestEdge(type) && mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates, altitude, itemid, zItem, texture, ground, occupied, hue);
        }

        #endregion SouthWestEdge

        #region NorthEastEdge

        private static bool PlaceObjectNorthEastEdge(
            AreaColorCoordinates areaColorCoordinates,
            MapObjectCoordinates mapObjectCoordinates,
            Coordinates coordinates,
            TypeColor type,
            sbyte zItem,
            sbyte altitude,
            int itemid,
            Random random,
            int texture = -1,
            bool ground = false,
            bool occupied = true,
            int hue = 0
            )
        {
            return areaColorCoordinates.IsNortEastEdge(type) &&
                   mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates, altitude, itemid, zItem, texture, ground, occupied, hue);
        }

        #endregion NorthEastEdge

        #region SouthEastEdge

        private static bool PlaceObjectSouthEastEdge(
           AreaColorCoordinates areaColorCoordinates,
           MapObjectCoordinates mapObjectCoordinates,
           Coordinates coordinates,
           TypeColor type,
           sbyte zItem,
           sbyte altitude,
           int itemid,
            int texture = -1,
            bool ground = false,
            bool occupied = true,
            int hue = 0
           )
        {
            return areaColorCoordinates.IsSouthEastEdge(type) && mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates, altitude, itemid, zItem, texture, ground, occupied, hue);
        }

        #endregion SouthEastEdge

        #region NorthWestEdge

        private static bool PlaceObjectNorthWestEdge(
            AreaColorCoordinates areaColorCoordinates,
            MapObjectCoordinates mapObjectCoordinates,
            Coordinates coordinates,
            TypeColor type,
            sbyte zItem,
            sbyte altitude,
            int itemid,
            int texture = -1,
            bool ground = false,
            bool occupied = true,
            int hue = 0
            )
        {
            return areaColorCoordinates.IsNorthWestEdge(type) &&
                mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates, altitude, itemid, zItem, texture, ground, occupied, hue);
        }

        #endregion NorthWestEdge

        #endregion Edges

        #region BORDER

        private static bool PlaceObjectBorder(
            AreaColorCoordinates areaColorCoordinates,
            MapObjectCoordinates mapObjectCoordinates,
            Coordinates coordinates,
            TypeColor type,
            sbyte zItem,
            sbyte altitude,
            int itemid,
            AreaColor border,
            int texture = -1,
            bool ground = false,
            bool occupied = true,
            int hue = 0
            )
        {
            return areaColorCoordinates.List.Count(o => o != border && o.Type == type) == 8 && mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates, altitude, itemid, zItem, texture, ground, occupied, hue);
        }

        #endregion BORDER

        #region dobuleBorder

        private static bool PlaceDoubleBorder
            (
            AreaColorCoordinates areaColorCoordinates,
            MapObjectCoordinates mapObjectCoordinates,
            Coordinates coordinates,
            TypeColor type,
            sbyte zItem,
            sbyte altitude,
            int itemid,
            AreaColor border,
            AreaColor border2,
            int texture = -1,
            bool ground = false,
            bool occupied = true,
            int hue = 0
            )
        {
            return areaColorCoordinates.List.Count(o => o.Type == type && o != border && o != border2) == 7 && mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates, altitude, itemid, zItem, texture, ground, occupied, hue);
        }

        #endregion dobuleBorder

        #region Transparent Fluids

        private void TransparentFluid(AreaColorCoordinates areaColorCoordinates, MapObjectCoordinates mapObjectCoordinates, Coordinates coordinates,
            Random random)
        {
            if (areaColorCoordinates.Center.Type != TypeColor.TransparentFluid)
                return;

            var hue = areaColorCoordinates.Center.Coasts.Coast.Hue;
            var z = areaColorCoordinates.Center.ItemsAltitude;
            var item = areaColorCoordinates.Center.Coasts.Coast.Texture;
            var index = areaColorCoordinates.Center.Index;
            var area = areaColorCoordinates.Center;
            if (areaColorCoordinates.IsAllColor())
            {
                mapObjectCoordinates.Center.AddItem(item, hue, (sbyte)z);
                return;
            }

            #region casual

            #region BORDER

            #region NORTH WEST

            //NWB corretto
            if (areaColorCoordinates.List.Count(o => o != areaColorCoordinates.NorthWest && o.Index == index) == 8)
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.BorderSouthEast.List, random), hue, (sbyte)z);
                return;
            }

            #endregion NORTH WEST

            #region NORTH EAST

            //NEB
            if (areaColorCoordinates.List.Count(o => o != areaColorCoordinates.NorthEast && o.Index == index) == 8)
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.BorderSouthWest.List, random), hue, (sbyte)z);
                return;
            }

            #endregion NORTH EAST

            #region SOUTH EAST

            //SEB
            if (areaColorCoordinates.List.Count(o => o != areaColorCoordinates.SouthEast && o.Index == index) == 8)
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.BorderNorthWest.List, random), hue, (sbyte)z);
                return;
            }

            #endregion SOUTH EAST

            #region SOUTHWEST

            //SWB
            if (areaColorCoordinates.List.Count(o => o != areaColorCoordinates.SouthWest && o.Index == index) == 8)
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.BorderNorthEast.List, random), hue, (sbyte)z);
                return;
            }

            #endregion SOUTHWEST

            #endregion BORDER

            #region Lines

            #region NORTH

            if (areaColorCoordinates.IsNorthLine(index))
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.LineSouth.List, random), hue, (sbyte)z);
                return;
            }

            #endregion NORTH

            #region WEST

            if (areaColorCoordinates.IsWestLine(index))
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.LineEast.List, random), hue, (sbyte)z);
                return;
            }

            #endregion WEST

            #region EAST

            if (areaColorCoordinates.IsEastLine(index))
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.LineWest.List, random), hue, (sbyte)z);
                return;
            }

            #endregion EAST

            #region SOUTH

            if (areaColorCoordinates.IsSouthLine(index))
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.LineNorth.List, random), hue, (sbyte)z);
                return;
            }

            #endregion SOUTH

            #endregion Lines

            #region Edges

            #region North East Edge

            if (areaColorCoordinates.IsNortEastEdge(index))
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.EdgeSouthWest.List, random), hue, (sbyte)z);
                return;
            }

            #endregion North East Edge

            #region South West Edge

            if (areaColorCoordinates.IsSouthWestEdge(index))
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.EdgeNorthEast.List, random), hue, (sbyte)z);
                return;
            }

            #endregion South West Edge

            #region North West Edge

            if (areaColorCoordinates.IsNorthWestEdge(index))
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.EdgeSouthEast.List, random), hue, (sbyte)z);
                return;
            }

            #endregion North West Edge

            #region South East Edge

            if (areaColorCoordinates.IsSouthEastEdge(index))
            {
                mapObjectCoordinates.Center.AddItem(RandomFromList(area.Coasts.Coast.EdgeNorthWest.List, random), hue, (sbyte)z);
                return;
            }

            #endregion South East Edge

            #endregion Edges
        }

        #endregion casual

        #region Make coasts UOL style

        private void MakeCoastUolStyle
            (
            AreaColorCoordinates areaColorCoordinates,
            MapObjectCoordinates mapObjectCoordinates,
            Coordinates coordinates,
            Random random
            )
        {
            if (areaColorCoordinates.Center.Type == TypeColor.Water) return;

            if (areaColorCoordinates.List.All(o => o.Type != TypeColor.WaterCoast))
                return;
            var hue = areaColorCoordinates.Center.Coasts.Coast.Hue;

            #region WaterCoasts

            if (areaColorCoordinates.Center.Type == TypeColor.WaterCoast)
            {
                mapObjectCoordinates.PlaceObjectOcc(areaColorCoordinates,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    areaColorCoordinates.Center.CliffCoast ? areaColorCoordinates.Center.Coasts.Coast.Texture : (int)SpecialAboutItems.Nothing,
                    (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                           RandomTexture(areaColorCoordinates.Center.TextureIndex, random), true, true, hue);

                if (areaColorCoordinates.List.All(o => o.Type == TypeColor.WaterCoast || o.Type == TypeColor.Water || o.Type == TypeColor.Special))
                {
                    mapObjectCoordinates.PlaceObjectOcc(
                        areaColorCoordinates,
                        (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                        areaColorCoordinates.Center.CliffCoast ? areaColorCoordinates.Center.Coasts.Coast.Texture : (int)SpecialAboutItems.Nothing,
                        (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                            RandomTexture(areaColorCoordinates.Center.TextureIndex, random), false, true, hue);
                    return;
                }

                #region Borders

                if
                (
                    PlaceObjectBorder
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                    (sbyte)areaColorCoordinates.Center.CoastAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    areaColorCoordinates.Center.Coasts.Coast.Texture,
                    areaColorCoordinates.NorthWest, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 65;

                    if (areaColorCoordinates.North.CliffCoast)
                        mapObjectCoordinates.Center.Texture = (short)
                            RandomFromList(areaColorCoordinates.North.Coasts.Ground.BorderNorthEast.List, random);
                    return;
                }

                if
                (
                    PlaceObjectBorder
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                    (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    areaColorCoordinates.Center.Coasts.Coast.Texture,
                    areaColorCoordinates.SouthEast, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 70;
                    return;
                }
                if
                (
                    PlaceObjectBorder
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                    (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    areaColorCoordinates.Center.Coasts.Coast.Texture,
                    areaColorCoordinates.NorthEast, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 75;
                    return;
                }

                if
                (
                    PlaceObjectBorder
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                    (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    areaColorCoordinates.Center.Coasts.Coast.Texture,
                    areaColorCoordinates.SouthWest, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 80;
                    return;
                }

                #endregion Borders

                #region Edges

                if
                (
                    PlaceObjectNorthWestEdge
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                    //-5,
                    (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    //-15, non per le coste basse
                    //-5,
                    areaColorCoordinates.Center.Coasts.Coast.Texture, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 85;
                    if (areaColorCoordinates.North.CliffCoast)//non sicuro
                        mapObjectCoordinates.Center.Texture = (short)
                            RandomFromList(areaColorCoordinates.North.Coasts.Ground.BorderNorthEast.List, random);
                    return;
                }

                if
                (
                    PlaceObjectSouthWestEdge
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                    (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    //-5,
                    //-15, non per le coste basse
                    //-5,
                    areaColorCoordinates.Center.Coasts.Coast.Texture,
                    random, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 90;
                    return;
                }

                if
                (
                    PlaceObjectSouthEastEdge
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                     (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    //-5,
                    //-15, non per le coste
                    //-5,
                    areaColorCoordinates.Center.Coasts.Coast.Texture, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 90;
                    return;
                }

                if
                (
                    PlaceObjectNorthEastEdge
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                     //-5,
                     //-15,
                     (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    areaColorCoordinates.Center.Coasts.Coast.Texture,
                    random, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 95;
                    return;
                }

                #endregion Edges

                #region Lines

                if
                (
                    PlaceObjectSouth
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                     //-5,
                     //-15,
                     (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    areaColorCoordinates.Center.Coasts.Coast.Texture, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 100;
                    return;
                }

                if
                (
                    PlaceObjectWest
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                     //-5,
                     //-15,
                     (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    areaColorCoordinates.Center.Coasts.Coast.Texture,
                    random, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 100;
                    return;
                }

                if
                (
                    PlaceObjectEast
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                     //-5,
                     //-15,
                     (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    areaColorCoordinates.Center.Coasts.Coast.Texture, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 105;
                    return;
                }

                if
                (
                    PlaceObjectNorth
                    (
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                     //-5,
                     //-15,
                     (sbyte)areaColorCoordinates.Center.ItemsAltitude,
                    (sbyte)random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max),
                    areaColorCoordinates.Center.Coasts.Coast.Texture,
                    random, -1, false, true, hue
                    )
                )
                {
                    if (DebugWater)
                        mapObjectCoordinates.Center.Altitude = 110;
                    return;
                }

                #endregion Lines

                return;
            }

            #endregion WaterCoasts

            #region casual

            #region BORDER

            #region NORTH WEST

            //NWB corretto
            if (
                PlaceObjectBorder(areaColorCoordinates,
                                  mapObjectCoordinates,
                                  coordinates,
                                  areaColorCoordinates.Center.Type,
                                  (sbyte)areaColorCoordinates.Center.CoastAltitude,
                                  (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                                  RandomFromList(areaColorCoordinates.Center.Coasts.Coast.EdgeNorthWest.List, random),
                                  areaColorCoordinates.NorthWest,
                                  RandomFromList(areaColorCoordinates.Center.Coasts.Ground.EdgeNorthWest.List, random),
                                  areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 40;

                if (areaColorCoordinates.Center.CliffCoast && !DebugCoast)
                    mapObjectCoordinates.SouthEast.Altitude =
                        (sbyte)(random.Next(areaColorCoordinates.SouthEast.Min, areaColorCoordinates.SouthEast.Max) + random.Next(1, 5));

                if (!DebugCoast && !areaColorCoordinates.Center.CliffCoast)
                    SouthEastSmooth(coordinates, _x, _y, _stride, _bitmapAreaColor, _mapObjects, random);
                return;
            }

            #endregion NORTH WEST

            #region NORTH EAST

            //NEB
            if (
                PlaceObjectBorder(
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                    (sbyte)areaColorCoordinates.Center.CoastAltitude,
                    areaColorCoordinates.Center.CliffCoast ? (sbyte)(random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max) + random.Next(-2, 3)) :
                    (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                    RandomFromList(areaColorCoordinates.Center.Coasts.Coast.EdgeNorthEast.List, random),
                    areaColorCoordinates.NorthEast,
                    RandomFromList(areaColorCoordinates.Center.Coasts.Ground.EdgeNorthEast.List, random),
                    areaColorCoordinates.Center.CliffCoast, true, hue
                    ))
            {
                if (!DebugCoast && !areaColorCoordinates.Center.CliffCoast)
                    SouthEastSmooth(coordinates, _x, _y, _stride, _bitmapAreaColor, _mapObjects, random);
                //OK
                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 35;

                return;
            }

            #endregion NORTH EAST

            #region SOUTH EAST

            //SEB
            if (
                PlaceObjectBorder(
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                    (sbyte)areaColorCoordinates.Center.CoastAltitude,
                    areaColorCoordinates.Center.CliffCoast ? (sbyte)(random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max) + random.Next(-3, -1)) :
                    (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                    RandomFromList(areaColorCoordinates.Center.Coasts.Coast.EdgeSouthEast.List, random),
                    areaColorCoordinates.SouthEast,
                    RandomFromList(areaColorCoordinates.Center.Coasts.Ground.EdgeSouthEast.List, random),
                    areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 30;
                return;
            }

            #endregion SOUTH EAST

            #region SOUTHWEST

            //SWB
            if (
                PlaceObjectBorder(
                    areaColorCoordinates,
                    mapObjectCoordinates,
                    coordinates,
                    areaColorCoordinates.Center.Type,
                    (sbyte)areaColorCoordinates.Center.CoastAltitude,
                    areaColorCoordinates.Center.CliffCoast ? (sbyte)(random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max) + random.Next(-3, -1)) :
                    (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                    RandomFromList(areaColorCoordinates.Center.Coasts.Coast.EdgeSouthWest.List, random),
                    areaColorCoordinates.SouthWest,
                    RandomFromList(areaColorCoordinates.Center.Coasts.Ground.EdgeSouthWest.List, random),
                    areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 25;
                return;
            }

            #endregion SOUTHWEST

            #endregion BORDER

            #region Double Border

            //INS
            if (PlaceDoubleBorder(areaColorCoordinates, mapObjectCoordinates, coordinates,
                                  areaColorCoordinates.Center.Type, 0, 0, 0, areaColorCoordinates.SouthWest,
                                  areaColorCoordinates.NorthWest,
                                  RandomFromList(areaColorCoordinates.Center.Coasts.Ground.BorderSouthWest.List, random), true, true, hue))
                return;
            //IWE
            if (PlaceDoubleBorder(areaColorCoordinates, mapObjectCoordinates, coordinates,
                                  areaColorCoordinates.Center.Type, 0, 0, 0, areaColorCoordinates.NorthWest,
                                  areaColorCoordinates.SouthEast,
                                  RandomFromList(areaColorCoordinates.Center.Coasts.Ground.BorderSouthWest.List, random), true, true, hue))
                return;

            #endregion Double Border

            #region Lines

            #region NORTH

            if (
                PlaceObjectNorth(areaColorCoordinates,
                              mapObjectCoordinates,
                              coordinates,
                              areaColorCoordinates.Center.Type,
                              (sbyte)areaColorCoordinates.Center.CoastAltitude,
                              (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                               RandomFromList(areaColorCoordinates.Center.Coasts.Coast.LineNorth.List, random),
                              random,
                              RandomFromList(areaColorCoordinates.Center.Coasts.Ground.LineNorth.List, random),
                              areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (areaColorCoordinates.Center.CliffCoast && !DebugCoast)
                    mapObjectCoordinates.South.Altitude =
                        (sbyte)(random.Next((areaColorCoordinates.South.Min), areaColorCoordinates.South.Max) +
                                 random.Next(-3, 0));
                if (!DebugCoast && !areaColorCoordinates.Center.CliffCoast)
                    SouthSmooth(coordinates, _x, _y, _stride, _bitmapAreaColor, _mapObjects, random);
                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 60;
                return;
            }

            #endregion NORTH

            #region WEST

            if (
                PlaceObjectWest(areaColorCoordinates,
                              mapObjectCoordinates,
                              coordinates,
                              areaColorCoordinates.Center.Type,
                              (sbyte)areaColorCoordinates.Center.CoastAltitude,
                               (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                               RandomFromList(areaColorCoordinates.Center.Coasts.Coast.LineWest.List, random),
                              random,
                              RandomFromList(areaColorCoordinates.Center.Coasts.Ground.LineWest.List, random), areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (areaColorCoordinates.Center.CliffCoast && !DebugCoast)
                    mapObjectCoordinates.East.Altitude =
                        (sbyte)(random.Next((areaColorCoordinates.East.Min), areaColorCoordinates.East.Max) +
                                 random.Next(-3, -2));

                if (!DebugCoast && !areaColorCoordinates.Center.CliffCoast)
                    SouthEastSmooth(coordinates, _x, _y, _stride, _bitmapAreaColor, _mapObjects, random);
                //OK
                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 45;
                return;
            }

            #endregion WEST

            #region EAST

            if (
                PlaceObjectEast(
                areaColorCoordinates,
                              mapObjectCoordinates,
                              coordinates,
                              areaColorCoordinates.Center.Type,
                              (sbyte)areaColorCoordinates.Center.CoastAltitude,
                              areaColorCoordinates.Center.CliffCoast ? (sbyte)(random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max) + random.Next(-2, 5))
                              : (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                              RandomFromList(areaColorCoordinates.Center.Coasts.Coast.LineEast.List, random),
                              RandomFromList(areaColorCoordinates.Center.Coasts.Ground.LineEast.List, random), areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (!DebugCoast && !areaColorCoordinates.Center.CliffCoast)
                    WestSmooth(coordinates, _x, _y, _stride, _bitmapAreaColor, _mapObjects, random);
                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 50;
                return;
            }

            #endregion EAST

            #region SOUTH

            if (
                PlaceObjectSouth(areaColorCoordinates,
                              mapObjectCoordinates,
                              coordinates,
                              areaColorCoordinates.Center.Type,
                              (sbyte)areaColorCoordinates.Center.CoastAltitude,
                              areaColorCoordinates.Center.CliffCoast ? (sbyte)(random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max) + random.Next(-3, 1)) :
                              (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                              RandomFromList(areaColorCoordinates.Center.Coasts.Coast.LineSouth.List, random),
                              RandomFromList(areaColorCoordinates.Center.Coasts.Ground.LineSouth.List, random), areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (!DebugCoast && !areaColorCoordinates.Center.CliffCoast)
                    NorthSmooth(coordinates, _x, _y, _stride, _bitmapAreaColor, _mapObjects, random);

                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 55;
                return;
            }

            #endregion SOUTH

            #endregion Lines

            #region Edges

            #region North East Edge

            if (PlaceObjectNorthEastEdge(
               areaColorCoordinates,
               mapObjectCoordinates,
               coordinates,
               areaColorCoordinates.Center.Type,
               (sbyte)areaColorCoordinates.Center.CoastAltitude,
               (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                RandomFromList(areaColorCoordinates.Center.Coasts.Coast.BorderNorthEast.List, random),
               random,
               RandomFromList(areaColorCoordinates.Center.Coasts.Ground.BorderNorthEast.List, random), areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (!DebugCoast && !areaColorCoordinates.Center.CliffCoast)
                    SouthWestSmooth(coordinates, _x, _y, _stride, _bitmapAreaColor, _mapObjects, random);

                // precedentemente c'era una linea nord?

                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 20;
                //OK
                return;
            }

            #endregion North East Edge

            #region South West Edge

            if (PlaceObjectSouthWestEdge(areaColorCoordinates,
                               mapObjectCoordinates,
                               coordinates,
                               areaColorCoordinates.Center.Type,
                                (sbyte)areaColorCoordinates.Center.CoastAltitude,
                                (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                               //-5,
                               RandomFromList(areaColorCoordinates.Center.Coasts.Coast.BorderSouthWest.List, random),
                               random,
                               RandomFromList(areaColorCoordinates.Center.Coasts.Ground.BorderSouthWest.List, random), areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (!DebugCoast && !areaColorCoordinates.Center.CliffCoast)
                    NorthEastSmooth(coordinates, _x, _y, _stride, _bitmapAreaColor, _mapObjects, random);
                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 5;
                return;
            }

            #endregion South West Edge

            #region North West Edge

            if (PlaceObjectNorthWestEdge
                (areaColorCoordinates
                 , mapObjectCoordinates,
                 coordinates,
                 areaColorCoordinates.Center.Type,
                (sbyte)areaColorCoordinates.Center.CoastAltitude,
                (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                 RandomFromList(areaColorCoordinates.Center.Coasts.Coast.BorderNorthWest.List, random),
                 RandomFromList(areaColorCoordinates.Center.Coasts.Ground.BorderNorthWest.List, random), areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (!DebugCoast && !areaColorCoordinates.Center.CliffCoast)
                    SouthEastSmooth(coordinates, _x, _y, _stride, _bitmapAreaColor, _mapObjects, random);

                if (!DebugCoast && areaColorCoordinates.Center.CliffCoast)
                    mapObjectCoordinates.SouthEast.Altitude =
                        (sbyte)
                        (random.Next(areaColorCoordinates.SouthEast.Min, areaColorCoordinates.SouthEast.Max) +
                         random.Next(-4, -2));

                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 10;
                return;
            }

            #endregion North West Edge

            #region South East Edge

            if (PlaceObjectSouthEastEdge(areaColorCoordinates,
                               mapObjectCoordinates,
                               coordinates,
                               areaColorCoordinates.Center.Type,
                                (sbyte)areaColorCoordinates.Center.CoastAltitude,
                                areaColorCoordinates.Center.CliffCoast ? (sbyte)(random.Next(areaColorCoordinates.Center.Min, areaColorCoordinates.Center.Max) + random.Next(-3, 1)) :
                                (sbyte)areaColorCoordinates.Center.MinCoastTextureZ,
                               RandomFromList(areaColorCoordinates.Center.Coasts.Coast.BorderSouthEast.List, random),
                               //-1,
                               RandomFromList(areaColorCoordinates.Center.Coasts.Ground.BorderSouthEast.List, random), areaColorCoordinates.Center.CliffCoast, true, hue))
            {
                if (!DebugCoast && !areaColorCoordinates.Center.CliffCoast)
                    NorthWestSmooth(coordinates, _x, _y, _stride, _bitmapAreaColor, _mapObjects, random);
                if (DebugCoast)
                    mapObjectCoordinates.Center.Altitude = 15;
                return;
            }

            #endregion South East Edge

            #endregion Edges

            var coasts = areaColorCoordinates.List.FirstOrDefault(o => o.Type == TypeColor.WaterCoast);

            _bitmapAreaColor[coordinates.Center] = coasts;
            areaColorCoordinates = new AreaColorCoordinates(coordinates, _bitmapAreaColor);
            MakeCoastUolStyle(areaColorCoordinates, mapObjectCoordinates, coordinates, random);

            #endregion casual
        }

        #endregion Make coasts UOL style

        #region Coast Smoothing

        #region NorthDirection

        private static void NorthSmooth
            (Coordinates coordinates,
            int xmax,
            int ymax,
            int stride,
            AreaColor[] bitmap,
            MapObject[] mapobjects,
            Random random)
        {
            for (int i = 0; i < bitmap[coordinates.Center].CoastSmoothCircles.Count; i++)
            {
                var coordnew = new Coordinates(i + 1, i + 1, coordinates.X, coordinates.Y, stride, xmax * ymax);
                var areacoloronew = new AreaColorCoordinates(coordnew, bitmap);
                var mapobjectCoordnew = new MapObjectCoordinates(coordnew, mapobjects);

                SmoothWorker(areacoloronew.North, areacoloronew.Center, mapobjectCoordnew.North, random, i);
                SmoothWorker(areacoloronew.NorthWest, areacoloronew.Center, mapobjectCoordnew.NorthWest, random, i);
                SmoothWorker(areacoloronew.NorthEast, areacoloronew.Center, mapobjectCoordnew.NorthEast, random, i);
            }
        }

        #endregion NorthDirection

        #region SouthDirection

        private static void SouthSmooth
            (Coordinates coordinates,
            int xmax,
            int ymax,
            int stride,
            AreaColor[] bitmap,
            MapObject[] mapobjects,
            Random random)
        {
            for (int i = 0; i < bitmap[coordinates.Center].CoastSmoothCircles.Count; i++)
            {
                var coordnew = new Coordinates(i + 1, i + 1, coordinates.X, coordinates.Y, stride, xmax * ymax);
                var areacoloronew = new AreaColorCoordinates(coordnew, bitmap);
                var mapobjectCoordnew = new MapObjectCoordinates(coordnew, mapobjects);

                SmoothWorker(areacoloronew.South, areacoloronew.Center, mapobjectCoordnew.South, random, i);
                SmoothWorker(areacoloronew.SouthEast, areacoloronew.Center, mapobjectCoordnew.SouthEast, random, i);
                SmoothWorker(areacoloronew.SouthWest, areacoloronew.Center, mapobjectCoordnew.SouthWest, random, i);
            }
        }

        #endregion SouthDirection

        #region EastDirection

        private static void EastSmooth
            (Coordinates coordinates,
            int xmax,
            int ymax,
            int stride,
            AreaColor[] bitmap,
            MapObject[] mapobjects,
            Random random)
        {
            for (int i = 0; i < bitmap[coordinates.Center].CoastSmoothCircles.Count; i++)
            {
                var coordnew = new Coordinates(i + 1, i + 1, coordinates.X, coordinates.Y, stride, xmax * ymax);
                var areacoloronew = new AreaColorCoordinates(coordnew, bitmap);
                var mapobjectCoordnew = new MapObjectCoordinates(coordnew, mapobjects);

                SmoothWorker(areacoloronew.East, areacoloronew.Center, mapobjectCoordnew.East, random, i);
                SmoothWorker(areacoloronew.NorthEast, areacoloronew.Center, mapobjectCoordnew.NorthEast, random, i);
                SmoothWorker(areacoloronew.SouthEast, areacoloronew.Center, mapobjectCoordnew.SouthEast, random, i);
            }
        }

        #endregion EastDirection

        #region Westdirection

        private static void WestSmooth
            (Coordinates coordinates,
            int xmax,
            int ymax,
            int stride,
            AreaColor[] bitmap,
            MapObject[] mapobjects,
            Random random)
        {
            for (int i = 0; i < bitmap[coordinates.Center].CoastSmoothCircles.Count; i++)
            {
                var coordnew = new Coordinates(i + 1, i + 1, coordinates.X, coordinates.Y, stride, xmax * ymax);
                var areacoloronew = new AreaColorCoordinates(coordnew, bitmap);
                var mapobjectCoordnew = new MapObjectCoordinates(coordnew, mapobjects);

                SmoothWorker(areacoloronew.West, areacoloronew.Center, mapobjectCoordnew.West, random, i);
                SmoothWorker(areacoloronew.NorthWest, areacoloronew.Center, mapobjectCoordnew.NorthWest, random, i);
                SmoothWorker(areacoloronew.SouthWest, areacoloronew.Center, mapobjectCoordnew.SouthWest, random, i);
            }
        }

        #endregion Westdirection

        #region SouthEastDirection

        private static void SouthEastSmooth
            (Coordinates coordinates,
            int xmax,
            int ymax,
            int stride,
            AreaColor[] bitmap,
            MapObject[] mapobjects,
            Random random)
        {
            for (int i = 0; i < bitmap[coordinates.Center].CoastSmoothCircles.Count; i++)
            {
                var coordnew = new Coordinates(i + 1, i + 1, coordinates.X, coordinates.Y, stride, xmax * ymax);
                var areacoloronew = new AreaColorCoordinates(coordnew, bitmap);
                var mapobjectCoordnew = new MapObjectCoordinates(coordnew, mapobjects);

                SmoothWorker(areacoloronew.East, areacoloronew.Center, mapobjectCoordnew.East, random, i);
                SmoothWorker(areacoloronew.South, areacoloronew.Center, mapobjectCoordnew.South, random, i);
                SmoothWorker(areacoloronew.SouthEast, areacoloronew.Center, mapobjectCoordnew.SouthEast, random, i);
            }
        }

        #endregion SouthEastDirection

        #region SouthWest Direction

        private static void SouthWestSmooth
            (Coordinates coordinates,
            int xmax,
            int ymax,
            int stride,
            AreaColor[] bitmap,
            MapObject[] mapobjects,
            Random random)
        {
            for (int i = 0; i < bitmap[coordinates.Center].CoastSmoothCircles.Count; i++)
            {
                var coordnew = new Coordinates(i + 1, i + 1, coordinates.X, coordinates.Y, stride, xmax * ymax);
                var areacoloronew = new AreaColorCoordinates(coordnew, bitmap);
                var mapobjectCoordnew = new MapObjectCoordinates(coordnew, mapobjects);

                SmoothWorker(areacoloronew.West, areacoloronew.Center, mapobjectCoordnew.West, random, i);
                SmoothWorker(areacoloronew.South, areacoloronew.Center, mapobjectCoordnew.South, random, i);
                SmoothWorker(areacoloronew.SouthWest, areacoloronew.Center, mapobjectCoordnew.SouthWest, random, i);
            }
        }

        #endregion SouthWest Direction

        #region NorthWest Direction

        private static void NorthWestSmooth
            (Coordinates coordinates,
            int xmax,
            int ymax,
            int stride,
            AreaColor[] bitmap,
            MapObject[] mapobjects,
            Random random)
        {
            for (int i = 0; i < bitmap[coordinates.Center].CoastSmoothCircles.Count; i++)
            {
                var coordnew = new Coordinates(i + 1, i + 1, coordinates.X, coordinates.Y, stride, xmax * ymax);
                var areacoloronew = new AreaColorCoordinates(coordnew, bitmap);
                var mapobjectCoordnew = new MapObjectCoordinates(coordnew, mapobjects);

                SmoothWorker(areacoloronew.West, areacoloronew.Center, mapobjectCoordnew.West, random, i);
                SmoothWorker(areacoloronew.North, areacoloronew.Center, mapobjectCoordnew.North, random, i);
                SmoothWorker(areacoloronew.NorthWest, areacoloronew.Center, mapobjectCoordnew.NorthWest, random, i);
            }
        }

        #endregion NorthWest Direction

        #region NorthEastDirection

        private static void NorthEastSmooth
            (Coordinates coordinates,
            int xmax,
            int ymax,
            int stride,
            AreaColor[] bitmap,
            MapObject[] mapobjects,
            Random random)
        {
            for (int i = 0; i < bitmap[coordinates.Center].CoastSmoothCircles.Count; i++)
            {
                var coordnew = new Coordinates(i + 1, i + 1, coordinates.X, coordinates.Y, stride, xmax * ymax);
                var areacoloronew = new AreaColorCoordinates(coordnew, bitmap);
                var mapobjectCoordnew = new MapObjectCoordinates(coordnew, mapobjects);

                SmoothWorker(areacoloronew.East, areacoloronew.Center, mapobjectCoordnew.East, random, i);
                SmoothWorker(areacoloronew.North, areacoloronew.Center, mapobjectCoordnew.North, random, i);
                SmoothWorker(areacoloronew.NorthEast, areacoloronew.Center, mapobjectCoordnew.NorthEast, random, i);
            }
        }

        #endregion NorthEastDirection

        #region Smooth Worker

        private static void SmoothWorker(AreaColor area, AreaColor center, MapObject mapObject, Random random, int index)
        {
            if (area.Type == TypeColor.WaterCoast || area.Type == TypeColor.Water || mapObject.Occupied != 0) return;

            var k = (sbyte)random.Next(center.CoastSmoothCircles[index].From, center.CoastSmoothCircles[index].To);
            if (mapObject.Altitude > k)
                mapObject.Altitude = k;
        }

        #endregion Smooth Worker

        #endregion Coast Smoothing

        private static Boolean DebugCoast { get { return false; } }

        private static Boolean DebugWater { get { return false; } }

        #endregion Transparent Fluids

        #region CollectionAreaCliffs

        /// <summary>
        /// method to make cliff
        /// </summary>
        /// <param name="coordinates"> </param>
        /// <param name="Areacoordinates"> </param>
        /// <param name="mapObjectCoordinates"> </param>
        private static void MakeCliffs(Coordinates coordinates, AreaColorCoordinates Areacoordinates, MapObjectCoordinates mapObjectCoordinates, Random random)
        {
            if (Areacoordinates.Center.Type != TypeColor.Cliff) return;

            mapObjectCoordinates.Center.Altitude = 0;

            //**********************
            //*       Line         *
            //**********************

            if (Areacoordinates.North.Type == TypeColor.Cliff && Areacoordinates.South.Type == TypeColor.Cliff)
            {
                var areaTransitionCliffTexture =
                    Areacoordinates.West.TransitionCliffTextures.FirstOrDefault(
                        o => o.Directions == DirectionCliff.WestEast && o.ColorTo == Areacoordinates.East.Color);

                if (areaTransitionCliffTexture != null)
                    mapObjectCoordinates.Center.Texture = (short)RandomFromList(areaTransitionCliffTexture.List, random);
                return;
            }

            if (Areacoordinates.East.Type == TypeColor.Cliff && Areacoordinates.West.Type == TypeColor.Cliff)
            {
                var areaTransitionCliffTexture =
                    Areacoordinates.North.TransitionCliffTextures.FirstOrDefault(
                        o => o.Directions == DirectionCliff.NorthSouth && o.ColorTo == Areacoordinates.South.Color);
                if (areaTransitionCliffTexture != null)
                    mapObjectCoordinates.Center.Texture = (short)RandomFromList(areaTransitionCliffTexture.List, random);
                return;
            }

            ////**********************
            ////* Anfang und Ende    *
            ////**********************

            //  !
            // ?X?
            //  C
            if (Areacoordinates.South.Type == TypeColor.Cliff && Areacoordinates.North.Type != TypeColor.Cliff
                && Areacoordinates.East.Type != TypeColor.Cliff && Areacoordinates.West.Type != TypeColor.Cliff)
            {
                AreaTransitionCliffTexture areaTransitionCliffTexture = null;
                if (Areacoordinates.North.Type != TypeColor.Cliff)

                    areaTransitionCliffTexture =
                        Areacoordinates.North.TransitionCliffTextures.FirstOrDefault(
                            c => c.Directions == DirectionCliff.NorthEnd && c.ColorTo == Areacoordinates.North.Color);

                if (areaTransitionCliffTexture != null)
                    AddTexture(RandomFromList(areaTransitionCliffTexture.List, random), mapObjectCoordinates);
                return;
            }

            //  ?
            // CX!
            //  ?
            if (Areacoordinates.West.Type == TypeColor.Cliff && Areacoordinates.East.Type != TypeColor.Cliff
                && Areacoordinates.North.Type != TypeColor.Cliff && Areacoordinates.South.Type != TypeColor.Cliff)
            {
                var areaTransitionCliffTexture = Areacoordinates.South.TransitionCliffTextures.
                    FirstOrDefault(
                        c => c.Directions == DirectionCliff.EastEnd && c.ColorTo == Areacoordinates.North.Color);

                if (areaTransitionCliffTexture != null)
                    AddTexture(RandomFromList(areaTransitionCliffTexture.List, random), mapObjectCoordinates);
                return;
            }

            //  C
            // ?X?
            //  !
            if (Areacoordinates.South.Type != TypeColor.Cliff && Areacoordinates.North.Type == TypeColor.Cliff
                && Areacoordinates.East.Type != TypeColor.Cliff && Areacoordinates.West.Type != TypeColor.Cliff)
            {
                var areaTransitionCliffTexture = Areacoordinates.East.TransitionCliffTextures.
                    FirstOrDefault(c => c.Directions == DirectionCliff.SouthEnd &&
                                        c.ColorTo == Areacoordinates.West.Color);

                if (areaTransitionCliffTexture != null)
                    AddTexture(RandomFromList(areaTransitionCliffTexture.List, random), mapObjectCoordinates);
                return;
            }

            //  ?
            // !XC
            //  ?
            if (Areacoordinates.East.Type == TypeColor.Cliff && Areacoordinates.West.Type != TypeColor.Cliff
                && Areacoordinates.North.Type != TypeColor.Cliff && Areacoordinates.South.Type != TypeColor.Cliff)
            {
                var areaTransitionCliffTexture = Areacoordinates.South.TransitionCliffTextures.
                    FirstOrDefault(c => c.Directions == DirectionCliff.WestEnd
                                        && c.ColorTo == Areacoordinates.North.Color);

                if (areaTransitionCliffTexture != null)
                    AddTexture(RandomFromList(areaTransitionCliffTexture.List, random), mapObjectCoordinates);
                return;
            }

            //**********************
            //* Rundungen          *
            //**********************

            //  C
            // CX
            //   ?
            if (Areacoordinates.West.Type == TypeColor.Cliff && Areacoordinates.North.Type == TypeColor.Cliff
                && Areacoordinates.NorthWest.Type != TypeColor.Cliff)
            {
                var areaTransitionCliffTexture = Areacoordinates.NorthWest.TransitionCliffTextures.
                    FirstOrDefault(c => c.Directions == DirectionCliff.NorthWestRounding &&
                                        c.ColorTo == Areacoordinates.NorthWest.Color);

                if (areaTransitionCliffTexture != null)
                    AddTexture(RandomFromList(areaTransitionCliffTexture.List, random), mapObjectCoordinates);
                return;
            }

            //  C
            //  XC
            // ?
            if (Areacoordinates.East.Type == TypeColor.Cliff && Areacoordinates.North.Type == TypeColor.Cliff && Areacoordinates.NorthEast.Type != TypeColor.Cliff)
            {
                var areaTransitionCliffTexture = Areacoordinates.NorthEast.TransitionCliffTextures.
                    FirstOrDefault(
                        c => c.Directions == DirectionCliff.NorthEastRounding
                             && c.ColorTo == Areacoordinates.NorthEast.Color);

                if (areaTransitionCliffTexture != null)
                    AddTexture(RandomFromList(areaTransitionCliffTexture.List, random), mapObjectCoordinates);
                return;
            }

            // ?
            //  XC
            //  C
            if (Areacoordinates.East.Type == TypeColor.Cliff && Areacoordinates.South.Type == TypeColor.Cliff && Areacoordinates.SouthEast.Type != TypeColor.Cliff)
            {
                var areaTransitionCliffTexture = Areacoordinates.SouthEast.TransitionCliffTextures.
                    FirstOrDefault(
                        c => c.Directions == DirectionCliff.SouthEastRounding
                             && c.ColorTo == Areacoordinates.SouthEast.Color);

                if (areaTransitionCliffTexture != null)
                    AddTexture(RandomFromList(areaTransitionCliffTexture.List, random), mapObjectCoordinates);
                return;
            }

            //   ?
            // CX
            //  C
            if (Areacoordinates.West.Type == TypeColor.Cliff && Areacoordinates.South.Type == TypeColor.Cliff && Areacoordinates.SouthWest.Type != TypeColor.Cliff)
            {
                var areaTransitionCliffTexture = Areacoordinates.SouthWest.TransitionCliffTextures.
                    FirstOrDefault(
                        c => c.Directions == DirectionCliff.SouthWestRounding
                             && c.ColorTo == Areacoordinates.SouthWest.Color);

                if (areaTransitionCliffTexture != null)
                    AddTexture(RandomFromList(areaTransitionCliffTexture.List, random), mapObjectCoordinates);
                return;
            }
        }

        private static void AddTexture(int texture, MapObjectCoordinates coordinates)
        {
            coordinates.Center.Texture = (short)texture;
        }

        #endregion CollectionAreaCliffs

        #region Items

        /// <summary>
        /// method to set items in the map
        /// </summary>
        private void SetItem()
        {
            var randomGen = new Random();
            int x, y, z = 0;

            for (x = MinX; x < _x; x++)
                for (y = MinY; y < _y; y++)
                {
                    var location = CalculateZone(x, y, _stride);

                    if (_mapObjects[location].Occupied != 0) continue;

                    var itemgroups = _bitmapAreaColor[location].Items;
                    if (itemgroups == null || itemgroups.List.Count <= 0) continue;

                    var group = itemgroups.List[randomGen.Next(0, itemgroups.List.Count)];
                    var random = randomGen.Next(0, 100);
                    if (random > @group.Percent) continue;

                    var tmp_item = @group.List.First();
                    if (@group.List.Count > 1)
                    {
                        z = tmp_item.Z;
                    }

                    foreach (SingleItem item in @group.List)
                    {
                        var locationshift = CalculateZone(x + item.X, y + item.Y, _stride);

                        if (_mapObjects[locationshift].Items == null)
                            _mapObjects[locationshift].Items = new List<ItemClone>();

                        var itemclone = new ItemClone(item);

                        _mapObjects[locationshift].Items.Add(itemclone);
                        if (tmp_item == item)
                        {
                            itemclone.Z = (sbyte)((_mapObjects[locationshift].Altitude +
                                                   _mapObjects[CalculateZone(x + item.X + 1, y + item.Y, _stride)].Altitude +
                                                   _mapObjects[CalculateZone(x + item.X, y + item.Y + 1, _stride)].Altitude +
                                                   _mapObjects[CalculateZone(x + item.X + 1, y + item.Y + 1, _stride)].Altitude) / 4 + item.Z);
                        }
                        else
                        {
                            itemclone.Z = (sbyte)((_mapObjects[CalculateZone(x + tmp_item.X, y + tmp_item.Y, _stride)].Altitude +
                                                   _mapObjects[CalculateZone(x + tmp_item.X + 1, y + tmp_item.Y, _stride)].Altitude +
                                                   _mapObjects[CalculateZone(x + tmp_item.X, y + tmp_item.Y + 1, _stride)].Altitude +
                                                   _mapObjects[CalculateZone(x + tmp_item.X + 1, y + tmp_item.Y + 1, _stride)].Altitude) / 4 + tmp_item.Z + z);
                        }
                    }
                }
        }

        #endregion Items

        #region Mul Handers

        /// <summary>
        /// method to write statics
        /// </summary>
        private void WriteStatics()
        {
            int blockx, blocky, x, y, items;
            short color;
            byte x2, y2;
            sbyte waterlevel = -120;
            Int32 length = 0, start;

            var staidx = new FileStream(Path.Combine(MulDirectory, string.Format("staidx{0}.mul", MapIndex)), FileMode.OpenOrCreate);
            var statics = new FileStream(Path.Combine(MulDirectory, string.Format("statics{0}.mul", MapIndex)), FileMode.OpenOrCreate);
            var statics0 = new BinaryWriter(statics);
            var staidx0 = new BinaryWriter(staidx);

            items = 0;
            start = 0;

            using (staidx)
            {
                using (statics)
                {
                    using (statics0)
                    {
                        using (staidx0)
                        {
                            for (blockx = 0; blockx < (_x / 8); ++blockx)
                            {
                                for (blocky = 0; blocky < (_y / 8); ++blocky)
                                {
                                    length = 0;
                                    for (y = (8 * blocky); y < (8 * (blocky + 1)); y++)
                                    {
                                        for (x = (8 * blockx); x < (8 * (blockx + 1)); x++)
                                        {
                                            x2 = (byte)(x % 8);
                                            y2 = (byte)(y % 8);
                                            var local = CalculateZone(x, y, _stride);
                                            if (_mapObjects[local].Items == null) continue;
                                            foreach (var item in _mapObjects[local].Items)
                                            {
                                                statics0.Write((ushort)item.Id);
                                                statics0.Write((byte)x2);
                                                statics0.Write((byte)y2);
                                                statics0.Write((sbyte)item.Z);
                                                statics0.Write((Int16)item.Hue);
                                                length += 7;
                                                items++;
                                            }
                                        }
                                    }

                                    staidx0.Write(start);
                                    start += length;
                                    staidx0.Write(length);
                                    staidx0.Write((Int32)1);
                                }
                            }
                            statics0.Flush();
                            staidx0.Flush();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// method to write map.mul
        /// </summary>
        private void WriteMUL()
        {
            int blockx, blocky, x, y;
            int empty = 0;
            int grey = 0x0244;

            var mapmul = new FileStream(Path.Combine(MulDirectory, "map" + MapIndex + ".mul"), FileMode.OpenOrCreate);
            var map0 = new BinaryWriter(mapmul);
            using (mapmul)
            {
                using (map0)
                {
                    for (blockx = 0; blockx < (_x / 8); ++blockx)
                    {
                        for (blocky = 0; blocky < (_y / 8); ++blocky)
                        {
                            map0.Write((int)1);//header
                            for (y = (8 * blocky); y < (8 * (blocky + 1)); y++)
                            {
                                for (x = (8 * blockx); x < (8 * (blockx + 1)); x++)
                                {
                                    var local = CalculateZone(x, y, _stride);

                                    var id = _mapObjects[local].Texture;
                                    var z = _mapObjects[local].Altitude;
                                    if ((id < 0) || (id >= 0x4000))
                                        id = 0;

                                    map0.Write(id);//writes tid
                                    map0.Write(z);//writes Z
                                }
                            }
                        }
                    }
                    map0.Flush();
                }
            }
        }

        #endregion Mul Handers

        #region utility methods

        /// <summary>
        /// it takes a random member in a list of int
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static int RandomFromList(IList<int> list, Random random)
        {
            if (list.Count == 0)
                return 0;
            var number = random.Next(0, list.Count);
            return list[number];
        }

        private int RandomTexture(int index, Random random)
        {
            var textures = TextureAreas.FindByIndex(index);
            return textures == null ? 0 : RandomFromList(textures.List, random);
        }

        /// <summary>
        /// coordinate for x and y in linear matrix
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        /// <returns></returns>
        public static int CalculateZone(int x, int y, int stride)
        {
            return (y * stride) + x;
        }

        internal static sbyte CalculateHeightValue(Color c)
        {
            var tmp = c.B - 128;

            if (tmp > sbyte.MaxValue)
                return sbyte.MaxValue;
            if (tmp < sbyte.MinValue)
                return sbyte.MinValue;

            return (sbyte)tmp;
        }

        /// <summary>
        /// array of precalculated x and y in a linear matrix
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        /// <param name="shiftX"> </param>
        /// <param name="shiftY"> </param>
        /// <returns>array of params</returns>
        private Coordinates MakeIndexesDirections(int x, int y, int shiftX, int shiftY)
        {
            return new Coordinates(shiftX, shiftY, x, y, _stride, _bitmapAreaColor.Length);
        }

        #endregion utility methods

        #endregion Coasts UOL style

        #region Event

        public event EventHandler ProgressText;

        public void OnProgressText(EventArgs e)
        {
            EventHandler handler = ProgressText;
            if (handler != null) handler(this, e);
        }

        #endregion Event

        #endregion Methods
    }

    #region Tool Classes and structures

    internal enum SpecialAboutItems : int
    {
        Nothing = -1,
        ClearAll = -2
    }

    #endregion Tool Classes and structures
}