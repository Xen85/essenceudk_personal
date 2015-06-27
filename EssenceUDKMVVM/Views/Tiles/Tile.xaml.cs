using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EssenceUDKMVVM.Controls.Tiles
{
    /// <summary>
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
        public static readonly DependencyProperty TileTypeProperty = DependencyProperty.Register("TileType", typeof (TileType), typeof (Tile), new PropertyMetadata(TileType.Surface));
        public static readonly DependencyProperty ImageVisibilityProperty = DependencyProperty.Register("ImageVisibility", typeof(Visibility), typeof(Tile), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty ImageWidthAndHeightProperty = DependencyProperty.Register("ImageWidthAndHeight", typeof (double), typeof (Tile), new PropertyMetadata(48.0));
        public static readonly DependencyProperty GridSizeProperty = DependencyProperty.Register("GridSize", typeof (double), typeof (Tile), new PropertyMetadata(48.0));
        public static readonly DependencyProperty UODataManagerProperty = DependencyProperty.Register("UODataManager", typeof (object), typeof (Tile), new PropertyMetadata(default(object)));

        public Tile()
        {
            InitializeComponent();
        }

        public TileType TileType
        {
            get { return (TileType) GetValue(TileTypeProperty); }
            set { SetValue(TileTypeProperty, value); }
        }

        public Visibility ImageVisibility
        {
            get { return (Visibility)GetValue(ImageVisibilityProperty); }
            set { SetValue(ImageVisibilityProperty, value); }
        }

        public double ImageWidthAndHeight
        {
            get { return (double) GetValue(ImageWidthAndHeightProperty); }
            set { SetValue(ImageWidthAndHeightProperty, value); }
        }

        public double GridSize  
        {
            get { return (double) GetValue(GridSizeProperty); }
            set { SetValue(GridSizeProperty, value); }
        }

        public object UODataManager
        {
            get { return (object) GetValue(UODataManagerProperty); }
            set { SetValue(UODataManagerProperty, value); }
        }
    }
}
