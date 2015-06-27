using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.Models.Model.Option
{
    public class OptionTreeMenu : ObservableObject
    {
        #region fields
        private string _name;
        private ObservableCollection<OptionTreeMenu> _children;
        private OptionTreeMenu _parent;
        #endregion

        #region props
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public ObservableCollection<OptionTreeMenu> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                RaisePropertyChanged(() => Children);
            }
        }

        public OptionTreeMenu Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                RaisePropertyChanged(() => Parent);
            }
        }

        #endregion
    }
}
