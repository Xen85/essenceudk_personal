using System;
using System.Windows;
using System.Windows.Interactivity;

namespace EssenceUDKMVVM.AttachedProperties
{
    /// <summary>
    /// Exposes attached behaviors that can be
    /// applied to TreeViewItem objects.
    /// </summary>
    public class ItemControlBehavior : Behavior<System.Windows.Controls.Primitives.Selector>, IDisposable
    {
        #region IsBroughtIntoViewWhenSelected

        public static bool GetIsBroughtIntoViewWhenSelected(System.Windows.Controls.Primitives.Selector treeViewItem)
        {
            return (bool)treeViewItem.GetValue(IsBroughtIntoViewWhenSelectedProperty);
        }

        public static void SetIsBroughtIntoViewWhenSelected(
          System.Windows.Controls.Primitives.Selector treeViewItem, bool value)
        {
            treeViewItem.SetValue(IsBroughtIntoViewWhenSelectedProperty, value);
        }

        public static readonly DependencyProperty IsBroughtIntoViewWhenSelectedProperty =
            DependencyProperty.RegisterAttached(
            "IsBroughtIntoViewWhenSelected",
            typeof(bool),
            typeof(TreeViewItemBehavior),
            new UIPropertyMetadata(false, OnIsBroughtIntoViewWhenSelectedChanged));

        private static void OnIsBroughtIntoViewWhenSelectedChanged(
          DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var item = depObj as System.Windows.Controls.Primitives.Selector;
            if (item == null)
                return;

            if (e.NewValue is bool == false)
                return;

            if ((bool)e.NewValue)
                item.SelectionChanged += OnTreeViewItemSelected;
            else
                item.SelectionChanged -= OnTreeViewItemSelected;
        }

        private static void OnTreeViewItemSelected(object sender, RoutedEventArgs e)
        {
            // Only react to the Selected event raised by the TreeViewItem
            // whose IsSelected property was modified. Ignore all ancestors
            // who are merely reporting that a descendant's Selected fired.
            if (!Object.ReferenceEquals(sender, e.OriginalSource))
                return;

            var item = e.OriginalSource as System.Windows.Controls.Primitives.Selector;
            if (item != null && item.SelectedItem != null)
            {
                var selected = item.SelectedItem as FrameworkElement;
                if (selected == null) return;
                selected.BringIntoView();
            }
        }

        #endregion IsBroughtIntoViewWhenSelected

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}