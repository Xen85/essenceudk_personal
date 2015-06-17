using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EssenceUDK.Platform;

namespace EssenceUDK.Controls.Ultima
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:EssenceUDK.Controls.Ultima"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:EssenceUDK.Controls.Ultima;assembly=EssenceUDK.Controls.Ultima"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ImageForTiles/>
    ///
    /// </summary>
    public class ImageForTiles : Control
    {
        static ImageForTiles()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageForTiles), new FrameworkPropertyMetadata(typeof(ImageForTiles)));
        }

        public static readonly DependencyProperty ISurfaceProperty =
            DependencyProperty.Register("ISurface", typeof(ISurface), typeof(ImageForTiles), new PropertyMetadata(default(ISurface)));

        public ISurface ISurface
        {
            get { return (ISurface)GetValue(ISurfaceProperty); }
            set { SetValue(ISurfaceProperty, value); }
        }

        public bool Texture
        {
            get { return (bool)GetValue(TextureProperty); }
            set { SetValue(TextureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Texture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextureProperty =
            DependencyProperty.Register("Texture", typeof(bool), typeof(ImageForTiles), new UIPropertyMetadata(false));

        



        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(ImageForTiles), new UIPropertyMetadata(0));


        public int TileId
        {
            get { return (int)GetValue(TileIdProperty); }
            set { SetValue(TileIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TileId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TileIdProperty =
            DependencyProperty.Register("TileId", typeof(int), typeof(ImageForTiles), new UIPropertyMetadata(0));


        public string TileIdHex
        {
            get { return String.Format("0x{0:X4}", TileId); }
        }


        public bool Miao { get; set; }
    }


    
}
