using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.ViewModel.Utils;

namespace EssenceUDKMVVM.ViewModel.Udk.Options
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
            : this(null, false)
        {
            Model = _model;
        }

        public IOptionMenuItem Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged(() => Model);
            }
        }
    }
}