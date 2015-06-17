using System;
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

namespace EssenceUDK.Controls.Common
{
    /// <summary>
    /// Interaction logic for DataTypeImage.xaml
    /// </summary>
    public partial class DataTypeImage : UserControl
    {
        public DataTypeImage()
        {
            InitializeComponent();
        }



        public UODataManager Manager
        {
            get { return (UODataManager)GetValue(ManagerProperty); }
            set { SetValue(ManagerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ManagerProperty =
            DependencyProperty.Register("Manager", typeof(UODataManager), typeof(DataTypeImage), new UIPropertyMetadata(0));

        
    }
}
