using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EssenceUDKMVVM.ViewModel.DockableModels
{
    public abstract class ViewModelDockableBase : ViewModelBase
    {
        private Visibility _visibility;
        private string _contentId;
        private string _title;
        private bool _isSelected;
        private string _toolTip;
        private bool _isActive;
        private ICommand _closeCommand;
        private ImageSource _iconSource;

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                RaisePropertyChanged(() => Visibility);
            }
        }

        public string ContentId
        {
            get { return _contentId; }
            set
            {
                _contentId = value;
                RaisePropertyChanged(() => ContentId);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
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

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                RaisePropertyChanged(() => IsActive);
            }
        }

        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set
            {
                _closeCommand = value;
                RaisePropertyChanged(() => CloseCommand);
            }
        }

        public ImageSource IconSource
        {
            get { return _iconSource; }
            set
            {
                _iconSource = value;
                RaisePropertyChanged(() => IconSource);
            }
        }
    }
}