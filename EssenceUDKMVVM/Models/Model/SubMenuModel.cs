using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.Models.Model
{
    public class SubMenuModel : ObservableObject
    {

        private ObservableCollection<SubMenuModel> _subModels;

        private ICommand _command;

        private string _header;


        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
                RaisePropertyChanged(() => Header);
            }
        }


        public ICommand Command
        {
            get
            {
                return _command;
            }
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

    }


}
