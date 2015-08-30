using System.Windows;
using System.Windows.Controls;

namespace MapMakerPlugin.Views.ColorManager
{

    /// <summary>
    ///     Interaction logic for CliffUserControl.xaml
    /// </summary>
    public partial class CliffUserControl : UserControl
    {
        public CliffUserControl()
        {
            InitializeComponent();
        }




        public object UoDataManager
        {
            get { return (object)GetValue(UoDataManagerProperty); }
            set { SetValue(UoDataManagerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UoDataManager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UoDataManagerProperty =
            DependencyProperty.Register("UoDataManager", typeof(object), typeof(CliffUserControl), new PropertyMetadata(default(object)));


    }

}