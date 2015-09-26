using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EssenceUDKMVVM.Models.Model.Menu
{
    public class SubMenuModel : ObservableObject
    {
        private ObservableCollection<SubMenuModel> _subModels;

        private ICommand _command;

        private string _header;

        private string _toolTip;

        private bool _isCheckable;

        private bool _isChecked;

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                RaisePropertyChanged(() => Header);
            }
        }

        public ICommand Command
        {
            get { return _command; }
            set
            {
                _command = value;
                RaisePropertyChanged(() => Command);
            }
        }

        public ObservableCollection<SubMenuModel> SubModels
        {
            get { return _subModels; }
            set
            {
                _subModels = value;
                RaisePropertyChanged(() => SubModels);
            }
        }

        public string ToolTip
        {
            get { return _toolTip; }
            set
            {
                _toolTip = value;
                RaisePropertyChanged(() => ToolTip);
            }
        }

        public bool IsCheckable
        {
            get { return _isCheckable; }
            set
            {
                _isCheckable = value;
                RaisePropertyChanged(() => IsCheckable);
            }
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged(() => IsChecked);
            }
        }
    }
}