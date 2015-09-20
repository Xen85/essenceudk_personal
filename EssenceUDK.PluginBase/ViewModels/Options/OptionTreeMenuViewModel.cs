#region

using System.Collections.Generic;
using EssenceUDK.PluginBase.Models;
using EssenceUDK.PluginBase.Models.Option;
using EssenceUDK.PluginBase.ViewModels.Utils;

#endregion

namespace EssenceUDK.PluginBase.ViewModels.Options
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class OptionTreeMenuViewModel : TreeViewItemViewModel
    {
        protected static Dictionary<OptionTreeMenu, OptionTreeMenuViewModel> Dictionary =
            new Dictionary<OptionTreeMenu, OptionTreeMenuViewModel>();

        private IOptionMenuItem _dataservice;
        private OptionTreeMenu _model;

        protected OptionTreeMenuViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren)
            : base(parent, lazyLoadChildren)
        {
        }
    }

}