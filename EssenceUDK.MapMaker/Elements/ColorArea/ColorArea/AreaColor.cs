using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorMountains;
using EssenceUDK.MapMaker.Elements.Items.ItemCoast;
using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;
using EssenceUDK.MapMaker.Elements.Items.ItemText;
using EssenceUDK.MapMaker.Elements.Textures.TextureTransition;
using EssenceUDK.MapMaker.Elements.Textures.TexureCliff;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace EssenceUDK.MapMaker.Elements.ColorArea.ColorArea
{
    [Serializable]
    public class AreaColor : NotificationObject
    {
        #region Declaration

        private bool _automaticMode;
        private int _textureIndex, _index, _min, _max, _indexTextureTop, _indexColorTop;
        private String _name;
        private TypeColor _typeColor;
        private Color _color, _colorMountain;
        private int _coastAltitude;
        private int _itemsAltitude;
        private bool _cliffCoast;
        private int _minCoastTextureZ;

        private AreaTransitionItemCoast _coast;
        private ObservableCollection<AreaTransitionItem> _transitionItems;
        private AreaItems _items;
        private ObservableCollection<AreaTransitionTexture> _transitionTexture;
        private ObservableCollection<AreaTransitionCliffTexture> _transitionCliff;
        private ObservableCollection<CircleMountain> _list;
        private ObservableCollection<CircleMountain> _smoothCoast;
        private Dictionary<Color, AreaTransitionTexture> _transitionTextureFinding;
        private Dictionary<Color, AreaTransitionItem> _transitionItemsFinding;

        public bool Initialized { get; private set; }

        #endregion Declaration

        #region Props

        #region Item Part

        public int ItemsAltitude { get { return _itemsAltitude; } set { _itemsAltitude = value; RaisePropertyChanged(() => ItemsAltitude); } }

        #endregion Item Part

        #region Coast Info

        [Description("Item Coast Altitude"), Category("Area Color"), DisplayName("Coast Altitude")]
        public int CoastAltitude
        {
            get { return _coastAltitude; }
            set
            {
                _coastAltitude = value;
                RaisePropertyChanged(() => CoastAltitude);
            }
        }

        [Description("Coast Smooth Circles"), Category("Area Color"), DisplayName(@"Coast Smooth Circles")]
        public ObservableCollection<CircleMountain> CoastSmoothCircles
        {
            get { return _smoothCoast; }
            set { _smoothCoast = value; RaisePropertyChanged(() => CoastSmoothCircles); }
        }

        public bool CliffCoast { get { return _cliffCoast; } set { _cliffCoast = value; RaisePropertyChanged(() => CliffCoast); } }

        #endregion Coast Info

        #region ColorArea Generical part

        [Description("Index of Textures"), Category("Area Color"), DisplayName(@"Texture Index")]
        public int TextureIndex { get { return _textureIndex; } set { _textureIndex = value; RaisePropertyChanged(() => TextureIndex); } }

        [Description("Index of Color"), Category("Area Color"), DisplayName(@"Id")]
        public int Index { get { return _index; } set { _index = value; RaisePropertyChanged(() => Index); } }

        [Description("Mimimum of Randon Height in map making"), Category("Map Making References"), DisplayName(@"Minimal Height")]
        public int Min { get { return _min; } set { _min = value; RaisePropertyChanged(() => Min); } }

        [Description("Maximum of Randon Height in map making"), Category("Map Making References"), DisplayName(@"Maximum Height")]
        public int Max { get { return _max; } set { _max = value; RaisePropertyChanged(() => Max); } }

        [Description("Name of the Color Area"), Category("Area Color"), DisplayName(@"Name")]
        public String Name { get { return _name; } set { _name = value; RaisePropertyChanged(() => Name); } }

        [Description("Color of the Area Color"), Category("Area Color"), DisplayName(@"Color")]
        public Color Color { get { return _color; } set { _color = value; RaisePropertyChanged(() => Color); } }

        [Description("It describes what Area Color Represents"), Category("Area Color"), DisplayName(@"Type of Area")]
        public TypeColor Type { get { return _typeColor; } set { _typeColor = value; RaisePropertyChanged(() => Type); } }

        public int MinCoastTextureZ { get { return _minCoastTextureZ; } set { _minCoastTextureZ = value; RaisePropertyChanged(() => MinCoastTextureZ); } }

        #endregion ColorArea Generical part

        #region Mountain Part

        [Description("It's the List of Circles of automatic grown in Map Making"), Category("Rocks"), DisplayName(@"Auto Circles")]
        public ObservableCollection<CircleMountain> List { get { return _list; } set { _list = value; RaisePropertyChanged(() => List); } }

        [Description("It refers to the Area Color at the top of the Rock"), Category("Rocks"), DisplayName(@"Color Top")]
        public Color ColorTopMountain { get { return _colorMountain; } set { _colorMountain = value; RaisePropertyChanged(() => ColorTopMountain); } }

        [Description("It refers to the Area Color at the top of the Rock"), Category("Rocks"), DisplayName(@"Index Top")]
        public int IndexColorTopMountain
        {
            get { return _indexColorTop; }
            set
            {
                _indexColorTop = value;
                ColorTopMountain = MapSdk.Colors[value];
                RaisePropertyChanged(() => IndexColorTopMountain);
            }
        }

        [Description("It refers to the Texture used at the Top of the Rock"), Category("Rocks"), DisplayName(@"Index Texture Top")]
        public int IndexTextureTop
        {
            get { return _indexTextureTop; }
            set
            {
                _indexTextureTop = value;
                RaisePropertyChanged(() => _indexTextureTop);
            }
        }

        [Description("It means if the automatic grown of the mountains is set or not"), Category("Rocks"), DisplayName(@"Automatic Rock Growning")]
        public bool ModeAutomatic { get { return _automaticMode; } set { _automaticMode = value; RaisePropertyChanged(() => ModeAutomatic); } }

        #endregion Mountain Part

        #region Added Collections

        public AreaTransitionItemCoast Coasts { get { return _coast; } set { _coast = value; } }

        public ObservableCollection<AreaTransitionItem> TransitionItems { get { return _transitionItems; } set { _transitionItems = value; } }

        public AreaItems Items { get { return _items; } set { _items = value; } }

        public ObservableCollection<AreaTransitionTexture> TextureTransitions { get { return _transitionTexture; } set { _transitionTexture = value; } }

        public ObservableCollection<AreaTransitionCliffTexture> TransitionCliffTextures { get { return _transitionCliff; } set { _transitionCliff = value; } }

        #endregion Added Collections

        #endregion Props

        #region Ctor

        public AreaColor()
        {
            Color = Colors.Black;
            TextureIndex = 0;
            Index = 0;
            Min = 0;
            Max = 0;
            Name = "";
            _list = new ObservableCollection<CircleMountain>();
            Type = TypeColor.None;
            _items = new AreaItems();
            _transitionTexture = new ObservableCollection<AreaTransitionTexture>();
            _transitionItems = new ObservableCollection<AreaTransitionItem>();
            _coast = new AreaTransitionItemCoast();
            _transitionCliff = new ObservableCollection<AreaTransitionCliffTexture>();
            _smoothCoast = new ObservableCollection<CircleMountain>();
            _coastAltitude = 0;
            _cliffCoast = false;
        }

        #endregion Ctor

        #region Override

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(() => Index, info);
            Serialize(() => Min, info);
            Serialize(() => Max, info);
            //Serialize(() => Type, info);
            info.AddValue("Type", (int)_typeColor);
            Serialize(() => TextureIndex, info);
            Serialize(() => Name, info);
            Serialize(() => Color, info);
            Serialize(() => List, info);
            Serialize(() => ColorTopMountain, info);
            Serialize(() => IndexColorTopMountain, info);
            Serialize(() => IndexTextureTop, info);
            Serialize(() => ModeAutomatic, info);
            Serialize(() => Coasts, info);
            Serialize(() => TransitionItems, info);
            Serialize(() => Items, info);
            Serialize(() => TextureTransitions, info);
            Serialize(() => TransitionCliffTextures, info);
            Serialize(() => CoastAltitude, info);
            Serialize(() => CoastSmoothCircles, info);
            Serialize(() => ItemsAltitude, info);
            Serialize(() => CliffCoast, info);
            Serialize(() => MinCoastTextureZ, info);
        }

        #endregion Override

        #region ISerializable Ctor

        protected AreaColor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Index = Deserialize(() => Index, info);
            Min = Deserialize(() => Min, info);
            Max = Deserialize(() => Max, info);
            //Type = Deserialize(() => Type, info);
            _typeColor = (TypeColor)info.GetInt32("Type");
            TextureIndex = Deserialize(() => TextureIndex, info);
            Name = Deserialize(() => Name, info);
            Color = Deserialize(() => Color, info);
            List = new ObservableCollection<CircleMountain>(Deserialize(() => List, info));
            ColorTopMountain = Deserialize(() => ColorTopMountain, info);
            _indexColorTop = Deserialize(() => IndexColorTopMountain, info);
            IndexTextureTop = Deserialize(() => IndexTextureTop, info);
            ModeAutomatic = Deserialize(() => ModeAutomatic, info);
            Coasts = Deserialize(() => Coasts, info);
            TransitionItems = new ObservableCollection<AreaTransitionItem>(Deserialize(() => TransitionItems, info));
            Items = Deserialize(() => Items, info);
            TextureTransitions = new ObservableCollection<AreaTransitionTexture>(Deserialize(() => TextureTransitions, info));
            TransitionCliffTextures = new ObservableCollection<AreaTransitionCliffTexture>(Deserialize(() => TransitionCliffTextures, info));
            try
            {
                CoastSmoothCircles = new ObservableCollection<CircleMountain>(Deserialize(() => CoastSmoothCircles, info));
            }
            catch (Exception)
            {
                CoastSmoothCircles = new ObservableCollection<CircleMountain>();
            }

            try
            {
                CoastAltitude = Deserialize(() => CoastAltitude, info);
            }
            catch (Exception)
            {
                CoastAltitude = 0;
            }

            try
            {
                ItemsAltitude = Deserialize(() => CoastAltitude, info);
                CliffCoast = Deserialize(() => CliffCoast, info);
            }
            catch (Exception)
            {
                ItemsAltitude = 0;
                CliffCoast = true;
            }

            try
            {
                MinCoastTextureZ = Deserialize(() => MinCoastTextureZ, info);
            }
            catch (Exception)
            {
                MinCoastTextureZ = -15;
            }
        }

        #endregion ISerializable Ctor

        #region Searches

        public void InizializeSearches()
        {
            Initialized = true;
            _transitionTextureFinding = new Dictionary<Color, AreaTransitionTexture>();
            _transitionItemsFinding = new Dictionary<Color, AreaTransitionItem>();
            foreach (var areaTransitionTexture in TextureTransitions)
            {
                try
                {
                    _transitionTextureFinding.Add(areaTransitionTexture.ColorTo, areaTransitionTexture);
                }
                catch (Exception)
                {
                }
            }
            foreach (var areaTransitionItem in TransitionItems)
            {
                try
                {
                    _transitionItemsFinding.Add(areaTransitionItem.ColorTo, areaTransitionItem);
                }
                catch (Exception)
                {
                }
            }
        }

        public AreaTransitionTexture FindTransitionTexture(Color color)
        {
            AreaTransitionTexture found;
            _transitionTextureFinding.TryGetValue(color, out found);
            return found;
        }

        public AreaTransitionItem FindTransationItemByColor(Color color)
        {
            AreaTransitionItem transitionItem;
            _transitionItemsFinding.TryGetValue(color, out transitionItem);
            return transitionItem;
        }

        #endregion Searches
    }

    public enum TypeColor
    {
        None,
        Water,
        Moutains,
        Land,
        Cliff,
        Special,
        WaterCoast,
        TransparentFluid
    }
}