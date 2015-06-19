using System.Windows;
using System.Windows.Controls;
using EssenceUDKMVVM.ViewModel.DockableModels;

namespace EssenceUDKMVVM.Styles
{
    class PanesStyleSelector : StyleSelector
    {
        public Style ToolStyle
        {
            get;
            set;
        }

        public Style DocStyle
        {
            get;
            set;
        }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is ToolPaneViewModel)
                return ToolStyle;

            if (item is DocPaneViewModel)
                return DocStyle;

            return base.SelectStyle(item, container);
        }
    }
}
