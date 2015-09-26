using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.Model.Option;
using EssenceUDKMVVM.ViewModel.Utils;
using System.Collections.Generic;

namespace EssenceUDKMVVM.ViewModel.Udk.Options
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class OptionTreeMenuViewModel : TreeViewItemViewModel
    {
        protected static Dictionary<OptionTreeMenu, OptionTreeMenuViewModel> Dictionary =
            new Dictionary<OptionTreeMenu, OptionTreeMenuViewModel>();

        private IOptionMenuItem _dataservice;
        private OptionTreeMenu _model;

        protected OptionTreeMenuViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren) : base(parent, lazyLoadChildren)
        {
        }


    }
}