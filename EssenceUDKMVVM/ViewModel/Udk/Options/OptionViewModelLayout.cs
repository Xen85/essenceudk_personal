using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.Model.Option;
using EssenceUDKMVVM.ViewModel.Utils;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.ViewModel.Udk
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class OptionViewModelLayout : TreeViewItemViewModel
    {
        private IOptionMenuItem _model;
        protected OptionViewModelLayout(TreeViewItemViewModel parent, bool lazyLoadChildren) : base(parent, lazyLoadChildren)
        {
        }

        public OptionViewModelLayout(IOptionMenuItem model)
            :this(null, false)
        {
            
        }
    }
}