using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace EssenceUDK.Controls.Common
{
    public static class ExtensionMethods
    {
        private static Action EmptyDelegate = delegate() { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }


        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject parent)
        where T : DependencyObject
        {
            List<T> foundChilds = new List<T>();

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++) {
                var child = VisualTreeHelper.GetChild(parent, i);

                T childType = child as T;
                if (childType == null) {
                    foreach (var other in FindVisualChildren<T>(child))
                        yield return other;
                } else {
                    yield return (T)child;
                }
            }
        }
    }

    public class IfMode : MarkupExtension
    {
        public object Design  { get; set; }
        public object Runtime { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
                return Design;
            return this.Runtime;
        }
    }

    public class IfBuild : MarkupExtension
    {
        public object Debug   { get; set; }
        public object Release { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            #if DEBUG
                return this.Debug;
            #else
                return this.Release;
            #endif
        }
    }

}
