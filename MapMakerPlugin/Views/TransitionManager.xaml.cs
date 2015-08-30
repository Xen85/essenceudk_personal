using System.Windows;
using System.Windows.Controls;
using EssenceUDK.CommonWPFLibrary.CustomUserControls;

namespace MapMakerPlugin.Views
{

    /// <summary>
    ///     Interaction logic for TransitionManager.xaml
    /// </summary>
    public partial class TransitionManager : UserControl
    {
        /// <summary>
        ///     The <see cref="TileType" /> dependency property's name.
        /// </summary>
        public const string TileTypePropertyName = "TileType";

        /// <summary>
        ///     The <see cref="ImageSize" /> dependency property's name.
        /// </summary>
        public const string ImageSizePropertyName = "ImageSize";

        /// <summary>
        ///     The <see cref="UODataManager" /> dependency property's name.
        /// </summary>
        public const string UODataManagerPropertyName = "UODataManager";

        /// <summary>
        ///     The <see cref="GridSize" /> dependency property's name.
        /// </summary>
        public const string GridSizePropertyName = "GridSize";

        /// <summary>
        ///     The <see cref="TileSize" /> dependency property's name.
        /// </summary>
        public const string TileSizePropertyName = "TileSize";

        /// <summary>
        ///     The <see cref="DragDropTemplate" /> dependency property's name.
        /// </summary>
        public const string DragDropTemplatePropertyName = "DragDropTemplate";

        public TransitionManager()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets or sets the value of the <see cref="TileType" />
        ///     property. This is a dependency property.
        /// </summary>
        public TileType TileType
        {
            get { return (TileType) GetValue(TileTypeProperty); }
            set { SetValue(TileTypeProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value of the <see cref="ImageSize" />
        ///     property. This is a dependency property.
        /// </summary>
        public int ImageSize
        {
            get { return (int) GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value of the <see cref="UODataManager" />
        ///     property. This is a dependency property.
        /// </summary>
        public object UODataManager
        {
            get { return GetValue(UODataManagerProperty); }
            set { SetValue(UODataManagerProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value of the <see cref="GridSize" />
        ///     property. This is a dependency property.
        /// </summary>
        public double GridSize
        {
            get { return (double) GetValue(GridSizeProperty); }
            set { SetValue(GridSizeProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value of the <see cref="TileSize" />
        ///     property. This is a dependency property.
        /// </summary>
        public double TileSize
        {
            get { return (double) GetValue(TileSizeProperty); }
            set { SetValue(TileSizeProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value of the <see cref="DragDropTemplate" />
        ///     property. This is a dependency property.
        /// </summary>
        public DataTemplate DragDropTemplate
        {
            get { return (DataTemplate) GetValue(DragDropTemplateProperty); }
            set { SetValue(DragDropTemplateProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="TileType" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TileTypeProperty = DependencyProperty.Register(
            TileTypePropertyName,
            typeof (TileType),
            typeof (TransitionManager),
            new UIPropertyMetadata(TileType.IntegerToLandTexture));

        /// <summary>
        ///     Identifies the <see cref="ImageSize" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ImageSizeProperty = DependencyProperty.Register(
            ImageSizePropertyName,
            typeof (int),
            typeof (TransitionManager),
            new UIPropertyMetadata(48));

        /// <summary>
        ///     Identifies the <see cref="UODataManager" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty UODataManagerProperty = DependencyProperty.Register(
            UODataManagerPropertyName,
            typeof (object),
            typeof (TransitionManager),
            new UIPropertyMetadata(null));

        /// <summary>
        ///     Identifies the <see cref="GridSize" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty GridSizeProperty = DependencyProperty.Register(
            GridSizePropertyName,
            typeof (double),
            typeof (TransitionManager),
            new UIPropertyMetadata((double) 58));

        /// <summary>
        ///     Identifies the <see cref="TileSize" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TileSizeProperty = DependencyProperty.Register(
            TileSizePropertyName,
            typeof (double),
            typeof (TransitionManager),
            new UIPropertyMetadata((double) 65));

        /// <summary>
        ///     Identifies the <see cref="DragDropTemplate" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DragDropTemplateProperty = DependencyProperty.Register(
            DragDropTemplatePropertyName,
            typeof (DataTemplate),
            typeof (TransitionManager),
            new UIPropertyMetadata(null));
    }

}