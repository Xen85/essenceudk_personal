#region

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;

#endregion

namespace EssenceUDK.PluginBase.ViewModels.DockableModels
{

    public abstract class ViewModelDockableBase : ViewModelBase
    {
        private ICommand _closeCommand;
        private string _contentId;
        private ImageSource _iconSource;
        private bool _isActive;
        private bool _isSelected;
        private string _title;
        private string _toolTip;
        private Visibility _visibility;

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