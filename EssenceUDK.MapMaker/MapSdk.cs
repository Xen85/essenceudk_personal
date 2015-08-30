using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;
using EssenceUDK.MapMaker.Elements.Interfaces;
using EssenceUDK.MapMaker.Elements.Items;
using EssenceUDK.MapMaker.Elements.Textures;
using EssenceUDK.MapMaker.MapMaking;
using EssenceUDK.MapMaker.TextFileReading;
using EssenceUDK.MapMaker.TextFileReading.Factories.Colors;
using EssenceUDK.MapMaker.TextFileReading.Factories.Items;
using EssenceUDK.MapMaker.TextFileReading.Factories.Textures;
using EssenceUDK.Resources.Libraries.MiscUtil.Conversion;
using EssenceUDK.Resources.Libraries.MiscUtil.IO;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EssenceUDK.MapMaker
{
    public class MapSdk : NotificationObject
    {
        public static Dictionary<int, Color> Colors { get; set; }

        public event EventHandler EventMakingMapEnd;

        public event EventHandler EventExtractAltitudeProgress;

        public event EventHandler EventEndExtractAltitudeEnd;

        public void OnEndExtractAltitude(EventArgs e)
        {
            EventHandler handler = EventEndExtractAltitudeEnd;
            if (handler != null) handler(this, e);
        }

        public void OnMakingMap(EventArgs e)
        {
            EventHandler handler = EventMakingMapEnd;
            if (handler != null) handler(this, e);
        }

        #region props

        #region Datas

        public CollectionAreaColor CollectionColorArea { get; set; }

        public CollectionAreaColor CollectionColorCoast { get; set; }

        public CollectionAreaColorMountains CollectionColorMountains { get; set; }

        public CollectionAreaItems CollectionAreaItems { get; set; }

        public CollectionAreaTransitionItemCoast CollectionAreaItemsCoasts { get; set; }

        public CollectionAreaTransitionItems CollectionAreaTransitionItems { get; set; }

        public CollectionAreaTexture CollectionAreaTexture { get; set; }

        public CollectionAreaTransitionTexture CollectionAreaTransitionTexture { get; set; }

        public CollectionAreaTransitionCliffTexture CollectionAreaTransitionCliffTexture { get; set; }

        #endregion Datas

        #region Utilities

        public IEnumerable<int> TextureIds { get { return CollectionAreaTexture.List.Select(o => o.Index); } }

        public IEnumerable<String> TextureName { get { return CollectionColorArea.List.Distinct().Select(o => o.Name); } }

        public IEnumerable<Color> AreaColorColors { get { return CollectionColorArea.List.Select(o => o.Color); } }

        public IEnumerable<int> AreaColorIndexes { get { return CollectionColorArea.List.Select(o => o.Index); } }

        #endregion Utilities

        #region factories

        public List<Factory> Factories { get; set; }

        public FactoryColorArea FactoryColor { get; set; }

        public FactoryCoast FactoryCoast { get; set; }

        public FactoryMountains FactoryMountains { get; set; }

        public FactoryItems FactoryItems { get; set; }

        public FactoryItemCoasts FactoryItemCoasts { get; set; }

        public FactorySmoothItems FactorySmoothItems { get; set; }

        public FactoryTextureArea FactoryTextureArea { get; set; }

        public FactoryTextureSmooth FactoryTextureSmooth { get; set; }

        public FactoryCliff FactoryCliff { get; set; }

        #endregion factories

        #region FolderLocations

        public String FolderLocation { get; set; }

        #endregion FolderLocations

        #region Automatic Height Calculation

        public bool AutomaticMode { get; set; }

        #endregion Automatic Height Calculation

        #region BitmapLocations

        public string BitmapLocationMap { get; set; }

        public string BitmapLocationMapZ { get; set; }

        #endregion BitmapLocations

        #endregion props

        #region Ctor

        public MapSdk(string directory)
        {
            #region initialize props

            CollectionColorArea = new CollectionAreaColor();
            CollectionColorMountains = new CollectionAreaColorMountains();

            CollectionAreaItems = new CollectionAreaItems();
            CollectionAreaItemsCoasts = new CollectionAreaTransitionItemCoast();
            CollectionAreaTransitionItems = new CollectionAreaTransitionItems();

            CollectionAreaTexture = new CollectionAreaTexture();
            CollectionAreaTransitionTexture = new CollectionAreaTransitionTexture();
            CollectionAreaTransitionCliffTexture = new CollectionAreaTransitionCliffTexture();

            #endregion initialize props

            #region initialize Factories

            Factories = new List<Factory>();

            FactoryColor = new FactoryColorArea(Path.Combine(directory, "color_area.txt"));

            Factories.Add(FactoryColor);

            FactoryCoast = new FactoryCoast(Path.Combine(directory, "color_coast.txt"));

            Factories.Add(FactoryCoast);

            FactoryMountains = new FactoryMountains(Path.Combine(directory, "color_mntn.txt"));

            Factories.Add(FactoryMountains);

            FactoryItems = new FactoryItems(Path.Combine(directory, "items.txt"));

            Factories.Add(FactoryItems);

            FactoryItemCoasts = new FactoryItemCoasts(Path.Combine(directory, "ite_tex_coast.txt"));

            Factories.Add(FactoryItemCoasts);

            FactorySmoothItems = new FactorySmoothItems(Path.Combine(directory, "items_smooth.txt"));

            Factories.Add(FactorySmoothItems);

            FactoryTextureArea = new FactoryTextureArea(Path.Combine(directory, "texture_area.txt"));

            Factories.Add(FactoryTextureArea);

            FactoryTextureSmooth = new FactoryTextureSmooth(Path.Combine(directory, "texture_smooth.txt"));

            Factories.Add(FactoryTextureSmooth);

            FactoryCliff = new FactoryCliff(Path.Combine(directory, "texture_cliff.txt"));

            Factories.Add(FactoryCliff);

            #endregion initialize Factories
        }

        public MapSdk()
        {
            #region initialize props

            CollectionColorArea = new CollectionAreaColor();
            CollectionColorMountains = new CollectionAreaColorMountains();
            CollectionColorCoast = new CollectionAreaColor();

            CollectionAreaItems = new CollectionAreaItems();
            CollectionAreaItemsCoasts = new CollectionAreaTransitionItemCoast();
            CollectionAreaTransitionItems = new CollectionAreaTransitionItems();

            CollectionAreaTexture = new CollectionAreaTexture();
            CollectionAreaTransitionTexture = new CollectionAreaTransitionTexture();
            CollectionAreaTransitionCliffTexture = new CollectionAreaTransitionCliffTexture();

            #endregion initialize props
        }

        #endregion Ctor

        #region factories

        public void InitializeFactories(string directory)
        {
            #region initialize Factories

            FolderLocation = directory;

            Factories = new List<Factory>();

            try
            {
                FactoryColor = new FactoryColorArea(Path.Combine(directory, "color_area.txt"));
            }
            catch (Exception)
            {
            }
            if (FactoryColor != null)
                Factories.Add(FactoryColor);

            try
            {
                FactoryCoast = new FactoryCoast(Path.Combine(directory, "color_coast.txt"));
            }
            catch (Exception)
            {
            }
            if (FactoryCoast != null)
                Factories.Add(FactoryCoast);

            try
            {
                FactoryMountains = new FactoryMountains(Path.Combine(directory, "color_mntn.txt"));
            }
            catch (Exception)
            {
            }
            if (FactoryMountains != null)
                Factories.Add(FactoryMountains);

            try
            {
                FactoryItems = new FactoryItems(Path.Combine(directory, "items.txt"));
            }
            catch (Exception)
            {
            }
            if (FactoryItems != null)
                Factories.Add(FactoryItems);

            try
            {
                FactoryItemCoasts = new FactoryItemCoasts(Path.Combine(directory, "ite_tex_coast.txt"));
            }
            catch (Exception)
            {
            }
            if (FactoryItemCoasts != null)
                Factories.Add(FactoryItemCoasts);

            try
            {
                FactorySmoothItems = new FactorySmoothItems(Path.Combine(directory, "items_smooth.txt"));
            }
            catch (Exception)
            {
            }
            if (FactorySmoothItems != null)
                Factories.Add(FactorySmoothItems);

            try
            {
                FactoryTextureArea = new FactoryTextureArea(Path.Combine(directory, "texture_area.txt"));
            }
            catch (Exception)
            {
            }
            if (FactoryTextureArea != null)
                Factories.Add(FactoryTextureArea);

            try
            {
                FactoryTextureSmooth = new FactoryTextureSmooth(Path.Combine(directory, "texture_smooth.txt"));
            }
            catch (Exception)
            {
            }
            if (FactoryTextureSmooth != null)
                Factories.Add(FactoryTextureSmooth);

            try
            {
                FactoryCliff = new FactoryCliff(Path.Combine(directory, "texture_cliff.txt"));
            }
            catch (Exception)
            {
            }
            if (FactoryCliff != null)
                Factories.Add(FactoryCliff);

            #endregion initialize Factories
        }

        public void Populate()
        {
            #region Textures

            if (FactoryTextureArea != null)
            {
                FactoryTextureArea.Read();
                CollectionAreaTexture = FactoryTextureArea.Textures;
            }
            if (FactoryTextureSmooth != null)
            {
                FactoryTextureSmooth.Read();
                CollectionAreaTransitionTexture = FactoryTextureSmooth.Smooth;
            }

            if (FactoryCliff != null)
            {
                FactoryCliff.Read();
                CollectionAreaTransitionCliffTexture = FactoryCliff.CollectionAreaCliffs;
            }

            #endregion Textures

            #region colorread

            if (FactoryColor != null)
            {
                FactoryColor.Read();
                CollectionColorArea = FactoryColor.Areas;
            }

            if (CollectionColorArea != null) CollectionColorArea.InitializeSeaches();

            if (FactoryCoast != null)
            {
                FactoryCoast.Read();
                CollectionColorCoast = FactoryCoast.Area;
            }

            if (FactoryMountains != null)
            {
                FactoryMountains.Read();
                CollectionColorMountains = FactoryMountains.Mountains;
            }

            #endregion colorread

            #region items

            if (FactoryItems != null)
            {
                FactoryItems.Read();
                CollectionAreaItems = FactoryItems.Items;
            }

            if (FactoryItemCoasts != null)
            {
                FactoryItemCoasts.Read();
                CollectionAreaItemsCoasts = FactoryItemCoasts.CoastsAll;
            }

            if (FactorySmoothItems != null)
            {
                FactorySmoothItems.Read();
                CollectionAreaTransitionItems = FactorySmoothItems.SmoothsAll;
            }

            #endregion items

            #region Merging Data

            MergingData();

            #endregion Merging Data
        }

        private void MergingData()
        {
            #region AreaColors

            if (CollectionColorArea == null) return;

            CollectionColorArea.List.CollectionChanged += EventUpdateList;

            foreach (var area in CollectionColorArea.List)
            {
                area.Type = TypeColor.Land;
                area.PropertyChanged += EventUpdateList;
            }

            CollectionColorArea.InitializeSeaches();

            if (CollectionColorMountains != null)
                foreach (var mnt in CollectionColorMountains.List)
                {
                    mnt.Type = TypeColor.Moutains;
                    var area = CollectionColorArea.FindByColor(mnt.Color);
                    if (area == null)
                    {
                        mnt.Index = CollectionColorArea.List.Count;
                        CollectionColorArea.List.Add(mnt);
                    }
                    else
                    {
                        int index = area.Index;
                        CollectionColorArea.List.Remove(area);
                        mnt.Name = area.Name;
                        mnt.Index = index;
                        CollectionColorArea.List.Insert(index, mnt);
                        CollectionColorArea.InitializeSeaches();
                    }
                }

            CollectionColorArea.InitializeSeaches();
            if (CollectionColorCoast != null)
                foreach (var coast in CollectionColorCoast.List)
                {
                    var area = CollectionColorArea.FindByColor(coast.Color);
                    if (area != null)
                    {
                        area.Type = TypeColor.Water;
                        area.Min = coast.Min;
                        area.Max = coast.Max;
                    }
                    else
                    {
                        coast.Index = CollectionColorArea.List.Count;
                        CollectionColorArea.List.Add(coast);
                        coast.Type = TypeColor.Water;
                        CollectionColorArea.InitializeSeaches();
                    }
                }

            #endregion AreaColors

            #region Textures

            if (CollectionAreaTransitionTexture != null)
                foreach (var transition in CollectionAreaTransitionTexture.List)
                {
                    var area = CollectionColorArea.FindByColor(transition.ColorFrom);
                    if (area == null)
                        continue;

                    area.TextureTransitions.Add(transition);
                    var area2 = CollectionColorArea.FindByColor(transition.ColorTo);
                    if (area2 == null) continue;

                    transition.IndexTo = area2.Index;
                }
            if (CollectionAreaTransitionCliffTexture != null)
            {
                var colorclif = new AreaColor { Color = CollectionAreaTransitionCliffTexture.Color, Type = TypeColor.Cliff, Name = "Cliff", Index = CollectionColorArea.List.Count };
                CollectionColorArea.List.Add(colorclif);

                CollectionColorArea.InitializeSeaches();

                foreach (var cliff in CollectionAreaTransitionCliffTexture.List)
                {
                    var area = CollectionColorArea.FindByColor(cliff.ColorFrom);
                    if (area == null) continue;

                    area.TransitionCliffTextures.Add(cliff);

                    var areato = CollectionColorArea.FindByColor(cliff.ColorTo);
                    if (areato == null) continue;
                    cliff.IdTo = areato.Index;
                }
            }

            CollectionAreaTexture.InitializeSeaches();

            #endregion Textures

            #region Items

            if (CollectionAreaItems != null)
                foreach (var items in CollectionAreaItems.List)
                {
                    var area = CollectionColorArea.FindByColor(items.Color);
                    if (area == null)
                        continue;

                    area.Items = items;
                }
            if (CollectionAreaItemsCoasts != null)
                foreach (var coast in CollectionAreaItemsCoasts.List)
                {
                    var area = CollectionColorArea.FindByColor(coast.Ground.Color);

                    if (area == null)
                        continue;

                    area.Coasts = coast;
                }
            if (CollectionAreaTransitionItems != null)
                foreach (var itemtransition in CollectionAreaTransitionItems.List)
                {
                    var area = CollectionColorArea.FindByColor(itemtransition.ColorFrom);

                    if (area == null) continue;

                    area.TransitionItems.Add(itemtransition);
                }

            #endregion Items

            if (CollectionAreaTexture != null)
                CollectionAreaTexture.InitializeSeaches();
            if (CollectionColorArea != null)
                CollectionColorArea.InitializeSeaches();

            EventUpdateList(this, null);

            if (CollectionAreaTexture == null || CollectionAreaTexture.List.First().AreaTransitionTexture.List.Count != 0)
                return;
            foreach (var area in CollectionColorArea.List)
            {
                foreach (var transition in area.TextureTransitions)
                {
                    var texture = CollectionAreaTexture.FindByIndex(area.TextureIndex);
                    var chiappa = CollectionColorArea.FindByColor(transition.ColorTo);
                    if (chiappa != null)
                        transition.TextureIdTo = chiappa.TextureIndex;

                    if (!texture.AreaTransitionTexture.List.Contains(transition))
                        texture.AreaTransitionTexture.List.Add(transition);
                }
            }
        }

        #endregion factories

        #region MapMaking

        public void MapMake(string directory, string bitmaplocation, string bitmapZLocation, int x, int y, int index)
        {
            CollectionColorArea.InitializeSeaches();
            CollectionAreaTexture.InitializeSeaches();
            var list_errors = new List<string>();
            foreach (var variable in CollectionColorArea.List)
            {
                Elements.Textures.TextureArea.AreaTextures area;
                CollectionAreaTexture._fast.TryGetValue(variable.TextureIndex, out area);
                if (variable.Max < variable.Min)
                {
                    var tmp = variable.Max;
                    variable.Max = variable.Min;
                    variable.Min = tmp;
                }

                if (area != null || variable.Type == TypeColor.Cliff) continue;

                var error = variable.Name + @" refers to a non-existent texture '" + variable.TextureIndex + @"' not found";
                list_errors.Add(error);
            }

            if (list_errors.Count > 0)
            {
                string errors = "";
                foreach (var error_in in list_errors)
                {
                    errors += error_in + '\n';
                }

                throw new Exception(errors);
            }

            var task = new Task[2];

            var taskMapBitmap =
                Task<AreaColor[]>.Factory.StartNew(() => BitmapReader.ColorsFromBitmap(CollectionColorArea,
                                                                                       bitmaplocation, x, y));

            var taskMapZ = Task<sbyte[]>.Factory.StartNew(() => BitmapReader.AltitudeFromBitmapVersion2(bitmapZLocation, x, y));

            task[0] = taskMapBitmap;
            task[1] = taskMapZ;
            try
            {
                Task.WaitAll(task);
            }
            catch (Exception e)
            {
                throw e;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var mulmaker = new MapMaking.MapMaker(taskMapZ.Result, taskMapBitmap.Result, x, y, index)
            {
                CollectionAreaColor = CollectionColorArea,
                TextureAreas = CollectionAreaTexture,
                AutomaticZMode = AutomaticMode,
                MulDirectory = directory
            };

            mulmaker.ProgressText += EventProgress;

            try
            {
                mulmaker.Bmp2Map();
                OnMakingMap(EventArgs.Empty);
            }
            catch (Exception e)
            {
                throw e;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExportAltitudes(string directory, int x, int y, IUltimaMapDataProvider manager)
        {
            using (var bitmap = BitmapReader.ExportAltitude(manager, x, y, EventExtractAltitudeProgress))
            {
                bitmap.Save(Path.Combine(directory, "Altitude.BMP"), ImageFormat.Bmp);
            }
            GC.Collect();
            EventEndExtractAltitudeEnd(this, EventArgs.Empty);
        }

        public void MapEditor(string directory, string bitmaplocation, string bitmapZLocation, int x, int y, int index, IUltimaMapDataProvider manager)
        {
            CollectionColorArea.InitializeSeaches();
            CollectionAreaTexture.InitializeSeaches();
            var list_errors = new List<string>();
            foreach (var VARIABLE in CollectionColorArea.List)
            {
                Elements.Textures.TextureArea.AreaTextures area;
                CollectionAreaTexture._fast.TryGetValue(VARIABLE.TextureIndex, out area);
                if (VARIABLE.Max < VARIABLE.Min)
                {
                    var tmp = VARIABLE.Max;
                    VARIABLE.Max = VARIABLE.Min;
                    VARIABLE.Min = tmp;
                }

                if (area != null || VARIABLE.Type == TypeColor.Cliff) continue;

                var error = VARIABLE.Name + @" refers to a non-existent texture '" + VARIABLE.TextureIndex + @"' not found";
                list_errors.Add(error);
            }

            if (list_errors.Count > 0)
            {
                string errors = "";
                foreach (var error_in in list_errors)
                {
                    errors += error_in + '\n';
                }

                throw new Exception(errors);
            }

            var task = new Task[2];

            var taskMapBitmap =
                Task<AreaColor[]>.Factory.StartNew(() => BitmapReader.ColorsFromBitmap(CollectionColorArea,
                                                                                       bitmaplocation, x, y));

            var taskMapZ = Task<sbyte[]>.Factory.StartNew(() => BitmapReader.AltitudeFromBitmapVersion2(bitmapZLocation, x, y));

            task[0] = taskMapBitmap;
            task[1] = taskMapZ;
            try
            {
                Task.WaitAll(task);
            }
            catch (Exception e)
            {
                throw e;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            var mulmaker = new MapMaking.MapMaker(taskMapZ.Result, taskMapBitmap.Result, x, y, index)
            {
                CollectionAreaColor = CollectionColorArea,
                TextureAreas = CollectionAreaTexture,
                AutomaticZMode = AutomaticMode,
                MulDirectory = directory,
                EditingMap = manager,
                EditMul = true,
            };
            mulmaker.EditingMap.MapIndex = index;
            mulmaker.ProgressText += EventProgress;

            try
            {
                mulmaker.MapEditing();
                OnMakingMap(EventArgs.Empty);
            }
            catch (Exception e)
            {
                throw e;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion MapMaking

        #region ACO Making

        public void MakeAco(string file)
        {
            MemoryStream memory = new MemoryStream();
            EndianBinaryWriter bwriterWriter = new EndianBinaryWriter(EndianBitConverter.Big, memory);

            const UInt16 separator = 0;
            UInt16 sectionCounter = 0;

            var colorlist = CollectionColorArea.List.Select(c => c.Color).ToList();

            UInt16 numberOfColors = UInt16.Parse(colorlist.Count.ToString());
            sectionCounter++;

            using (memory)
            {
                using (bwriterWriter)
                {
                    bwriterWriter.Write(sectionCounter);

                    bwriterWriter.Write(numberOfColors); // write the number of colors

                    foreach (var color in colorlist)
                    {
                        ColorStructureWriter(bwriterWriter, color);
                    }
                    sectionCounter++;

                    bwriterWriter.Write(sectionCounter);

                    bwriterWriter.Write(numberOfColors);

                    var encoding = new UnicodeEncoding(true, true, true);

                    foreach (var color in colorlist)
                    {
                        ColorStructureWriter(bwriterWriter, color);

                        var tmpcol = CollectionColorArea.List.FirstOrDefault(c => c.Color == color);
                        var bytes = (encoding.GetBytes(tmpcol.Name));
                        bwriterWriter.Write((ushort)tmpcol.Name.Length + 1);
                        bwriterWriter.Write(bytes);
                        bwriterWriter.Write((ushort)0);
                    }
                    bwriterWriter.Flush();

                    using (FileStream output = new FileStream(file, FileMode.Create))
                    {
                        memory.WriteTo(output);
                    }
                }
            }
        }

        private void ColorStructureWriter(EndianBinaryWriter writer, Color color)
        {
            writer.Write((ushort)0);

            writer.Write(color.R);
            writer.Write(color.R);

            writer.Write(color.G);
            writer.Write(color.G);

            writer.Write(color.B);
            writer.Write(color.B);

            writer.Write(color.A); //alfa
            writer.Write(color.A);
        }

        #endregion ACO Making

        #region UtilityMethods

        public void AddAreaColor(AreaColor add)
        {
            add.PropertyChanged += EventUpdateList;
            CollectionColorArea.List.Add(add);
        }

        #endregion UtilityMethods

        #region Save/Load functions

        public void SaveBinary(string file)
        {
            var objects = new object[]
                              {
                                  CollectionAreaTexture,
                                  CollectionColorArea,
                              };

            var formatter = new BinaryFormatter();
            var ms = new MemoryStream();
            using (ms)
            {
                formatter.Serialize(ms, objects);
                using (var stream = new FileStream(file, FileMode.Create))
                {
                    ms.WriteTo(stream);
                }
            }
        }

        public void LoadBinary(string file)
        {
            //var formatter = new BinaryFormatter();
            var ss = new SurrogateSelector();

            var formatter = new BinaryFormatter { SurrogateSelector = ss };
            object[] objectfrom;
            using (var strema = new FileStream(file, FileMode.Open))
            {
                objectfrom = (object[])formatter.Deserialize(strema);
            }
            CollectionAreaTransitionTexture = (CollectionAreaTransitionTexture)objectfrom[0];
            CollectionColorArea = (CollectionAreaColor)objectfrom[1];
            MergingData();
            UpateEvent();
        }

        public void SaveXML(string file)
        {
            var objects = new NotificationObject[]
                              {
                                  CollectionAreaTexture,
                                  CollectionColorArea
                              };

            var serializer = new SoapFormatter();

            var ms = new MemoryStream();
            using (ms)
            {
                serializer.Serialize(ms, objects);
                using (var stream = new FileStream(file, FileMode.Create))
                {
                    ms.WriteTo(stream);
                }
            }
        }

        public void LoadFromXML(string file)
        {
            var serializer = new SoapFormatter();
            NotificationObject[] collections;
            using (var strema = new FileStream(file, FileMode.Open))
            {
                collections = (NotificationObject[])serializer.Deserialize(strema);
            }
            CollectionAreaTexture = (CollectionAreaTexture)collections[0];
            CollectionColorArea = (CollectionAreaColor)collections[1];

            foreach (var area in CollectionColorArea.List)
            {
                area.PropertyChanged += EventUpdateList;
            }

            if (CollectionAreaTexture != null)
                CollectionAreaTexture.InitializeSeaches();
            if (CollectionColorArea != null)
                CollectionColorArea.InitializeSeaches();

            EventUpdateList(this, null);

            if (CollectionAreaTexture != null && CollectionAreaTexture.List.First().AreaTransitionTexture.List.Count == 0)
                foreach (var area in CollectionColorArea.List)
                {
                    foreach (var transition in area.TextureTransitions)
                    {
                        var texture = CollectionAreaTexture.FindByIndex(area.TextureIndex);
                        var areaTo = CollectionColorArea.FindByColor(transition.ColorTo);
                        if (areaTo != null)
                            transition.TextureIdTo = areaTo.TextureIndex;

                        if (!texture.AreaTransitionTexture.List.Contains(transition))
                            texture.AreaTransitionTexture.List.Add(transition);
                    }
                    area.TextureTransitions.Clear();

                    foreach (var transitem in area.TransitionItems)
                    {
                        var texture = CollectionAreaTexture.FindByIndex(area.TextureIndex);
                        if (texture == null) continue;
                        var areaTo = CollectionColorArea.FindByColor(transitem.ColorTo);
                        if (areaTo != null)
                            transitem.TextureIdTo = areaTo.TextureIndex;
                        if (texture.CollectionAreaItems == null)
                            texture.CollectionAreaItems = new CollectionAreaTransitionItems();
                        if (!texture.CollectionAreaItems.List.Contains(transitem))
                            texture.CollectionAreaItems.List.Add(transitem);
                    }

                    area.TransitionItems.Clear();
                }
            UpateEvent();
        }

        #endregion Save/Load functions

        #region Event Handlers

        public event EventHandler EventMapMakingProgress;

        public void OnEventMapMakingProgress(EventArgs e)
        {
            EventHandler handler = EventMapMakingProgress;
            if (handler != null) handler(this, e);
        }

        public void OnEventExtractAltitudeProgress(EventArgs e)
        {
            EventHandler handler = EventExtractAltitudeProgress;
            if (handler != null) handler(this, e);
        }

        private void EventUpdateList(object sender, EventArgs args)
        {
            Colors = new Dictionary<int, Color>();
            foreach (var area in CollectionColorArea.List)
            {
                try
                {
                    Colors.Add(area.Index, area.Color);
                }
                catch (Exception)
                {
                }
            }
        }

        private void EventProgress(object sender, EventArgs args)
        {
            OnEventMapMakingProgress(args);
        }

        private void EventProgressAltitude(object sender, EventArgs args)
        {
            OnEventExtractAltitudeProgress(args);
        }

        #endregion Event Handlers

        #region clone

        public static object CloneSdkObject(NotificationObject ToClone)
        {
            var soap = new BinaryFormatter();
            var mem = new MemoryStream();
            NotificationObject output = null;
            using (mem)
            {
                soap.Serialize(mem, ToClone);
                mem.Position = 0;
                output = (NotificationObject)soap.Deserialize(mem);
            }
            return output;
        }

        #endregion clone

        private void UpateEvent()
        {
            CollectionColorArea.List.CollectionChanged += (sender, eventarg) =>
            {
                RaisePropertyChanged(() => AreaColorColors);
                RaisePropertyChanged(() => AreaColorIndexes);
            };

            CollectionAreaTexture.List.CollectionChanged += (sender, eventArg) =>
            {
                RaisePropertyChanged(() => TextureName);
                RaisePropertyChanged(() => TextureIds);
            };
        }
    }
}