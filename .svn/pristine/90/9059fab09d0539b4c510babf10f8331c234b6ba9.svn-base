using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EssenceUDK.Controls
{
    public class WpfHelper
    {
        public static bool IsInDesignMode { get; private set; }

        static WpfHelper()
        {
            IsInDesignMode = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }
    }
}
