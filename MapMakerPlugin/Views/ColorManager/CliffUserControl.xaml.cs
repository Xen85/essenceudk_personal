#region

using System.Windows;
using System.Windows.Controls;

#endregion

namespace EssenceUDK.UDKMvvM.Plugins.MapMakerPlugin.Views.ColorManager
{

    /// <summary>
    ///     Interaction logic for CliffUserControl.xaml
    /// </summary>
    public partial class CliffUserControl : UserControl
    {
        // Using a DependencyProperty as the backing store for UoDataManager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UoDataManagerProperty =
            DependencyProperty.Register("UoDataManager", typeof (object), typeof (CliffUserControl),
                new PropertyMetadata(default(object)));

        public CliffUserControl()
        {
            InitializeComponent();
        }

        public object UoDataManager
        {
            get { return GetValue(UoDataManagerProperty); }
            set { SetValue(UoDataManagerProperty, value); }
        }
    }

}