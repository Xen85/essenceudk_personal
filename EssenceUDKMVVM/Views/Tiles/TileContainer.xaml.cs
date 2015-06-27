using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace EssenceUDKMVVM.Controls.Tiles
{
    /// <summary>
    /// Interaction logic for TileContainer.xaml
    /// </summary>
    public partial class TileContainer : UserControl
    {
        public TileContainer()
        {
            InitializeComponent();
        }

        #region TextBlock Properties
        /// <summary>
        /// The <see cref="Title" /> dependency property's name.
        /// </summary>
        public const string TitlePropertyName = "Title";

        /// <summary>
        /// Gets or sets the value of the <see cref="Title" />
        /// property. This is a dependency property.
        /// </summary>
        public String Title
        {
            get
            {
                return (String)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Title" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            TitlePropertyName,
            typeof(String),
            typeof(TileContainer),
            new UIPropertyMetadata(String.Empty));

        #endregion //Text Block Properties


        #region Tile Properties

        #region ListBoxProperties
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
            typeof(TileContainer),
            new UIPropertyMetadata(TileType.IntegerToItem));


        /// <summary>
        /// The <see cref="PanelType" /> dependency property's name.
        /// </summary>
        public const string PanelTypePropertyName = "PanelType";

        /// <summary>
        /// Gets or sets the value of the <see cref="PanelType" />
        /// property. This is a dependency property.
        /// </summary>
        public PanelType PanelType
        {
            get
            {
                return (PanelType)GetValue(PanelTypeProperty);
            }
            set
            {
                SetValue(PanelTypeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="PanelType" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PanelTypeProperty = DependencyProperty.Register(
            PanelTypePropertyName,
            typeof(PanelType),
            typeof(TileContainer),
            new UIPropertyMetadata(PanelType.Wrapper));

        #endregion //ListBox properties


        #region Image Properties

        /// <summary>
        /// The <see cref="Orientation" /> dependency property's name.
        /// </summary>
        public const string OrientationPropertyName = "Orientation";

        /// <summary>
        /// Gets or sets the value of the <see cref="Orientation" />
        /// property. This is a dependency property.
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                return (Orientation)GetValue(OrientationProperty);
            }
            set
            {
                SetValue(OrientationProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Orientation" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            OrientationPropertyName,
            typeof(Orientation),
            typeof(TileContainer),
            new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        /// The <see cref="ImageWidthAndHeight" /> dependency property's name.
        /// </summary>
        public const string ImageWidthAndHeightPropertyName = "ImageWidthAndHeight";

        /// <summary>
        /// Gets or sets the value of the <see cref="ImageWidthAndHeight" />
        /// property. This is a dependency property.
        /// </summary>
        public int ImageWidthAndHeight
        {
            get
            {
                return (int)GetValue(ImageWidthAndHeightProperty);
            }
            set
            {
                SetValue(ImageWidthAndHeightProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ImageWidthAndHeight" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ImageWidthAndHeightProperty = DependencyProperty.Register(
            ImageWidthAndHeightPropertyName,
            typeof(int),
            typeof(TileContainer),
            new UIPropertyMetadata(48));


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
            typeof(TileContainer),
            new UIPropertyMetadata((double)48));


        /// <summary>
        /// The <see cref="TilePanelSize" /> dependency property's name.
        /// </summary>
        public const string TilePanelSizePropertyName = "TilePanelSize";

        /// <summary>
        /// Gets or sets the value of the <see cref="TilePanelSize" />
        /// property. This is a dependency property.
        /// </summary>
        public double TilePanelSize
        {
            get
            {
                return (double)GetValue(TilePanelSizeProperty);
            }
            set
            {
                SetValue(TilePanelSizeProperty, value); 
            }
        }

        /// <summary>
        /// Identifies the <see cref="TilePanelSize" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TilePanelSizeProperty = DependencyProperty.Register(
            TilePanelSizePropertyName,
            typeof(double),
            typeof(TileContainer),
            new UIPropertyMetadata((double)58));

        #endregion // Image Properties

        #region Virtualizing Panel properties
        /// <summary>
        /// The <see cref="CacheLength" /> dependency property's name.
        /// </summary>
        public const string CacheLengthPropertyName = "CacheLength";

        /// <summary>
        /// Gets or sets the value of the <see cref="CacheLength" />
        /// property. This is a dependency property.
        /// </summary>
        public int CacheLength
        {
            get
            {
                return (int)GetValue(CacheLengthProperty);
            }
            set
            {
                SetValue(CacheLengthProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CacheLength" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CacheLengthProperty = DependencyProperty.Register(
            CacheLengthPropertyName,
            typeof(int),
            typeof(TileContainer),
            new UIPropertyMetadata(5000));


        #endregion // Virtualizing panel Properties


        #region UoData
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
            typeof(TileContainer),
            new UIPropertyMetadata(null));

        #endregion //uodata


        #region MVVM properties



      

        #endregion //MVVM props


        #region customizing data properties
        /// <summary>
        /// The <see cref="CustomDataTemplate" /> dependency property's name.
        /// </summary>
        public const string CustomDataTemplatePropertyName = "CustomDataTemplate";

        /// <summary>
        /// Gets or sets the value of the <see cref="CustomDataTemplate" />
        /// property. This is a dependency property.
        /// </summary>
        public DataTemplate CustomDataTemplate
        {
            get
            {
                return (DataTemplate)GetValue(CustomDataTemplateProperty);
            }
            set
            {
                SetValue(CustomDataTemplateProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CustomDataTemplate" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CustomDataTemplateProperty = DependencyProperty.Register(
            CustomDataTemplatePropertyName,
            typeof(DataTemplate),
            typeof(TileContainer),
            new UIPropertyMetadata(null));

        

        

        /// <summary>
        /// The <see cref="ImageVisibility" /> dependency property's name.
        /// </summary>
        public const string ImageVisibilityPropertyName = "ImageVisibility";

        /// <summary>
        /// Gets or sets the value of the <see cref="ImageVisibility" />
        /// property. This is a dependency property.
        /// </summary>
        public Visibility ImageVisibility
        {
            get
            {
                return (Visibility)GetValue(ImageVisibilityProperty);
            }
            set
            {
                SetValue(ImageVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ImageVisibility" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ImageVisibilityProperty = DependencyProperty.Register(
            ImageVisibilityPropertyName,
            typeof(Visibility),
            typeof(TileContainer),
            new UIPropertyMetadata(Visibility.Visible));

        #endregion //customizing data properties

        #endregion // Tile Properties


        #region DRAG&DROP

        


        /// <summary>
        /// The <see cref="IsDropTarget" /> dependency property's name.
        /// </summary>
        public const string IsDropTargetPropertyName = "IsDropTarget";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsDropTarget" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsDropTarget
        {
            get
            {
                return (bool)GetValue(IsDropTargetProperty);
            }
            set
            {
                SetValue(IsDropTargetProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsDropTarget" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsDropTargetProperty = DependencyProperty.Register(
            IsDropTargetPropertyName,
            typeof(bool),
            typeof(TileContainer),
            new UIPropertyMetadata(false));


        /// <summary>
        /// The <see cref="IsDragSource" /> dependency property's name.
        /// </summary>
        public const string IsDragSourcePropertyName = "IsDragSource";

        /// <summary>
        /// Gets or sets the value of the <see cref="IsDragSource" />
        /// property. This is a dependency property.
        /// </summary>
        public bool IsDragSource
        {
            get
            {
                return (bool)GetValue(IsDragSourceProperty);
            }
            set
            {
                SetValue(IsDragSourceProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="IsDragSource" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsDragSourceProperty = DependencyProperty.Register(
            IsDragSourcePropertyName,
            typeof(bool),
            typeof(TileContainer),
            new UIPropertyMetadata(false));

        /// <summary>
        /// The <see cref="Source" /> dependency property's name.
        /// </summary>
        public const string SourcePropertyName = "Source";

        /// <summary>
        /// Gets or sets the value of the <see cref="Source" />
        /// property. This is a dependency property.
        /// </summary>
        public IEnumerable Source
        {
            get
            {
                return (IEnumerable)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Source" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            SourcePropertyName,
            typeof(IEnumerable),
            typeof(TileContainer),
            new UIPropertyMetadata(null));



        /// <summary>
        /// The <see cref="Selected" /> dependency property's name.
        /// </summary>
        public const string SelectedPropertyName = "Selected";

        /// <summary>
        /// Gets or sets the value of the <see cref="Selected" />
        /// property. This is a dependency property.
        /// </summary>
        public object Selected
        {
            get
            {
                return (object)GetValue(SelectedProperty);
            }
            set
            {
                SetValue(SelectedProperty, value);
            }
        }

        public object SelectedItem
        {
            get { return (object) GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public int SelectedIndex    
        {
            get { return (int) GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Selected" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedProperty = DependencyProperty.Register(
            SelectedPropertyName,
            typeof(object),
            typeof(TileContainer),
            new UIPropertyMetadata(default(object)));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof (object), typeof (TileContainer), new PropertyMetadata(default(object)));
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof (int), typeof (TileContainer), new PropertyMetadata(0));

        #endregion




        ///// <summary>
        ///// The <see cref="DropTemplate" /> dependency property's name.
        ///// </summary>
        //public const string DropTemplatePropertyName = "DropTemplate";
        //%
        ///// <summary>
        ///// Gets or sets the value of the <see cref="DropTemplate" />
        ///// property. This is a dependency property.
        ///// </summary>
        //public DataTemplate DropTemplate
        //{
        //    get
        //    {
        //        return (DataTemplate)GetValue(DropTemplateProperty);
        //    }
        //    set
        //    {
        //        SetValue(DropTemplateProperty, value);
        //    }
        //}

        ///// <summary>
        ///// Identifies the <see cref="DropTemplate" /> dependency property.
        ///// </summary>
        //public static readonly DependencyProperty DropTemplateProperty = DependencyProperty.Register(
        //    DropTemplatePropertyName,
        //    typeof(DataTemplate),
        //    typeof(DataTemplate),
        //    new UIPropertyMetadata(default(DataTemplate)));

     

    }

}
