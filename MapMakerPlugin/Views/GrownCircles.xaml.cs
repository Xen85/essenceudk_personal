using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EssenceUDKMVVM.Controls.MapMaker
{

    /// <summary>
    ///     Interaction logic for GrownCircles.xaml
    /// </summary>
    public partial class GrownCircles : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource",
            typeof (IList), typeof (GrownCircles), new PropertyMetadata(default(IList)));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem",
            typeof (object), typeof (GrownCircles), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty AddProperty = DependencyProperty.Register("Add", typeof (ICommand),
            typeof (GrownCircles), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty RemoveCommandProperty = DependencyProperty.Register("RemoveCommand",
            typeof (ICommand), typeof (GrownCircles), new PropertyMetadata(default(ICommand)));

        public GrownCircles()
        {
            InitializeComponent();
        }

        public object ItemsSource
        {
            get { return (IList) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public ICommand Add
        {
            get { return (ICommand) GetValue(AddProperty); }
            set { SetValue(AddProperty, value); }
        }

        public ICommand RemoveCommand
        {
            get { return (ICommand) GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }
    }

}