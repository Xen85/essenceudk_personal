#region

using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

#endregion

namespace EssenceUDK.PluginBase.ViewModels.Utils
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class TreeViewItemViewModel : ViewModelBase
    {
        #region Data

        private static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();

        private bool _isExpanded;
        private bool _isSelected;

        #endregion Data

        #region Constructors

        protected TreeViewItemViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren)
        {
            Parent = parent;

            Children = new ObservableCollection<TreeViewItemViewModel>();

            if (lazyLoadChildren)
                Children.Add(DummyChild);
        }

        // This is used to create the DummyChild instance.
        private TreeViewItemViewModel()
        {
        }

        #endregion Constructors

        #region Presentation Members

        #region Children

        /// <summary>
        ///     Returns the logical child items of this object.
        /// </summary>
        public ObservableCollection<TreeViewItemViewModel> Children { get; set; }

        #endregion Children

        #region HasLoadedChildren

        /// <summary>
        ///     Returns true if this object's Children have not yet been populated.
        /// </summary>
        public bool HasDummyChild
        {
            get { return Children.Count == 1 && Children[0] == DummyChild; }
        }

        #endregion HasLoadedChildren

        #region IsExpanded

        /// <summary>
        ///     Gets/sets whether the TreeViewItem
        ///     associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    RaisePropertyChanged(() => IsExpanded);
                }

                // Expand all the way up to the root.
                if (_isExpanded && Parent != null)
                    Parent.IsExpanded = true;

                // Lazy load the child items, if necessary.
                if (!HasDummyChild) return;
                Children.Remove(DummyChild);
                LoadChildren();
            }
        }

        #endregion IsExpanded

        #region IsSelected

        /// <summary>
        ///     Gets/sets whether the TreeViewItem
        ///     associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected) return;
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        #endregion IsSelected

        #region LoadChildren

        /// <summary>
        ///     Invoked when the child items need to be loaded on demand.
        ///     Subclasses can override this to populate the Children collection.
        /// </summary>
        protected virtual void LoadChildren()
        {
        }

        #endregion LoadChildren

        #region Parent

        public TreeViewItemViewModel Parent { get; set; }

        #endregion Parent

        #endregion Presentation Members
    }

}