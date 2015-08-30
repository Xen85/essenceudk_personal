using EssenceUDKMVVM.Controls.Tiles;
using System.Windows;
using System.Windows.Controls;

namespace EssenceUDKMVVM.Controls.MapMaker.TextureManager
{
    /// <summary>
    /// Interaction logic for TextureTransition.xaml
    /// </summary>
    public partial class TextureTransition : UserControl
    {
        public TextureTransition()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The <see cref="TileType" /> dependency property's name.
        /// </summary>
        public const string TileTypePropertyName = "TileType";

        /// <summary>
        /// Gets or sets the value of the <see cref="TileType" />
        /// property. This is a dependency property.
        /// </summary>
        public TileType TileType
        {
            get
            {
                return (TileType)GetValue(TileTypeProperty);
            }
            set
            {
                SetValue(TileTypeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="TileType" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TileTypeProperty = DependencyProperty.Register(
            TileTypePropertyName,
            typeof(TileType),
            typeof(TextureTransition),
            new UIPropertyMetadata(TileType.IntegerToLandTexture));

        /// <summary>
        /// The <see cref="ImageSize" /> dependency property's name.
        /// </summary>
        public const string ImageSizePropertyName = "ImageSize";

        /// <summary>
        /// Gets or sets the value of the <see cref="ImageSize" />
        /// property. This is a dependency property.
        /// </summary>
        public int ImageSize
        {
            get
            {
                return (int)GetValue(ImageSizeProperty);
            }
            set
            {
                SetValue(ImageSizeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ImageSize" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ImageSizeProperty = DependencyProperty.Register(
            ImageSizePropertyName,
            typeof(int),
            typeof(TextureTransition),
            new UIPropertyMetadata(48));

        /// <summary>
        /// The <see cref="UODataManager" /> dependency property's name.
        /// </summary>
        public const string UODataManagerPropertyName = "UODataManager";

        /// <summary>
        /// Gets or sets the value of the <see cref="UODataManager" />
        /// property. This is a dependency property.
        /// </summary>
        public object UODataManager
        {
            get
            {
                return (object)GetValue(UODataManagerProperty);
            }
            set
            {
                SetValue(UODataManagerProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="UODataManager" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty UODataManagerProperty = DependencyProperty.Register(
            UODataManagerPropertyName,
            typeof(object),
            typeof(TextureTransition),
            new UIPropertyMetadata(null));

        /// <summary>
        /// The <see cref="GridSize" /> dependency property's name.
        /// </summary>
        public const string GridSizePropertyName = "GridSize";

        /// <summary>
        /// Gets or sets the value of the <see cref="GridSize" />
        /// property. This is a dependency property.
        /// </summary>
        public double GridSize
        {
            get
            {
                return (double)GetValue(GridSizeProperty);
            }
            set
            {
                SetValue(GridSizeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="GridSize" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty GridSizeProperty = DependencyProperty.Register(
            GridSizePropertyName,
            typeof(double),
            typeof(TextureTransition),
            new UIPropertyMetadata((double)58));

        /// <summary>
        /// The <see cref="TileSize" /> dependency property's name.
        /// </summary>
        public const string TileSizePropertyName = "TileSize";

        /// <summary>
        /// Gets or sets the value of the <see cref="TileSize" />
        /// property. This is a dependency property.
        /// </summary>
        public double TileSize
        {
            get
            {
                return (double)GetValue(TileSizeProperty);
            }
            set
            {
                SetValue(TileSizeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="TileSize" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TileSizeProperty = DependencyProperty.Register(
            TileSizePropertyName,
            typeof(double),
            typeof(TextureTransition),
            new UIPropertyMetadata((double)65));
    }
}