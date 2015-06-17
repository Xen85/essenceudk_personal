using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using EssenceUDK.Controls.Common;
using EssenceUDK.Platform;
using EssenceUDK.Platform.DataTypes;

namespace EssenceUDK.Controls.Ultima
{
    public class TileLandView : TileBaseView
    {
        #region Control Properties

        [Description("Source IEnumerable<ILandTile> of tiles."), Category("EssenceUDK.Controls")]
        public new IEnumerable<ILandTile> ItemsSource {
            get { return (IEnumerable<ILandTile>)GetValue(ItemsSourceProperty); }
            set { if (!DesignerProperties.GetIsInDesignMode(this)) SetValue(ItemsSourceProperty, value); }
        }

        #endregion

        protected override string _SourcePath { get { return "Surface.Image"; } }     
    }
}
