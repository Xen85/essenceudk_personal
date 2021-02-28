using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using CustomWPFControls.DragDrop;
using EssenceUDK.Platform;
using EssenceUDKMVVM.Controls.Tiles;

namespace EssenceUDKMVVM.Internal.AttachedProperties.DragDrop
{
    public class DragDropTileContainerHelper
    {
        // singleton
        private static DragDropTileContainerHelper _instance;

        public static readonly DependencyProperty IsDragSourceProperty =
            DependencyProperty.RegisterAttached("IsDragSource", typeof(bool), typeof(DragDropTileContainerHelper),
                new UIPropertyMetadata(false, IsDragSourceChanged));

        public static readonly DependencyProperty IsDropTargetProperty =
            DependencyProperty.RegisterAttached("IsDropTarget", typeof(bool), typeof(DragDropTileContainerHelper),
                new UIPropertyMetadata(false, IsDropTargetChanged));

        public static readonly DependencyProperty DragDropTemplateProperty =
            DependencyProperty.RegisterAttached("DragDropTemplate", typeof(DataTemplate),
                typeof(DragDropTileContainerHelper), new UIPropertyMetadata(null));

        // source and target
        private readonly DataFormat m_Format = DataFormats.GetDataFormat("DragDropItemsControl");
        private DraggedAdorner m_DraggedAdorner;
        private object m_DraggedData;
        private bool m_HasVerticalOrientation;
        private Vector m_InitialMouseOffset;
        private Point m_InitialMousePosition;
        private InsertionAdorner m_InsertionAdorner;
        private int m_InsertionIndex;
        private bool m_IsInFirstHalf;
        public double ScrollHorizontalOffset { get; set; }
        public double ScrollVerticalOffset { get; set; }

        private FrameworkElement m_SourceItemContainer;

        // source
        private TileContainer m_SourceItemsControl;

        private FrameworkElement m_TargetItemContainer;

        // target
        private TileContainer m_TargetItemsControl;
        public double TargetLeftMargin { get; set; }
        public double TargetTopMargin { get; set; }
        private Window m_TopWindow;

        private static DragDropTileContainerHelper Instance =>
            _instance ?? (_instance = new DragDropTileContainerHelper());

        public static bool GetIsDragSource(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsDragSourceProperty);
        }

        public static void SetIsDragSource(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragSourceProperty, value);
        }


        public static bool GetIsDropTarget(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsDropTargetProperty);
        }

        public static void SetIsDropTarget(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDropTargetProperty, value);
        }

        public static DataTemplate GetDragDropTemplate(DependencyObject obj)
        {
            return (DataTemplate) obj.GetValue(DragDropTemplateProperty);
        }

        public static void SetDragDropTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(DragDropTemplateProperty, value);
        }

        private static void IsDragSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var dragSource = obj as TileContainer;
            if (dragSource == null) return;
            if (Equals(e.NewValue, true))
            {
                dragSource.PreviewMouseLeftButtonDown += Instance.DragSource_PreviewMouseLeftButtonDown;
                dragSource.PreviewMouseLeftButtonUp += Instance.DragSource_PreviewMouseLeftButtonUp;
                dragSource.PreviewMouseMove += Instance.DragSource_PreviewMouseMove;
            }
            else
            {
                dragSource.PreviewMouseLeftButtonDown -= Instance.DragSource_PreviewMouseLeftButtonDown;
                dragSource.PreviewMouseLeftButtonUp -= Instance.DragSource_PreviewMouseLeftButtonUp;
                dragSource.PreviewMouseMove -= Instance.DragSource_PreviewMouseMove;
            }
        }

        private static void IsDropTargetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var dropTarget = obj as TileContainer;
            if (dropTarget == null) return;

            if (Equals(e.NewValue, true))
            {
                dropTarget.AllowDrop = true;
                dropTarget.PreviewDrop += Instance.DropTarget_PreviewDrop;
                dropTarget.PreviewDragEnter += Instance.DropTarget_PreviewDragEnter;
                dropTarget.PreviewDragOver += Instance.DropTarget_PreviewDragOver;
                dropTarget.PreviewDragLeave += Instance.DropTarget_PreviewDragLeave;
            }
            else
            {
                dropTarget.AllowDrop = false;
                dropTarget.PreviewDrop -= Instance.DropTarget_PreviewDrop;
                dropTarget.PreviewDragEnter -= Instance.DropTarget_PreviewDragEnter;
                dropTarget.PreviewDragOver -= Instance.DropTarget_PreviewDragOver;
                dropTarget.PreviewDragLeave -= Instance.DropTarget_PreviewDragLeave;
            }
        }

        // DragSource

        private void DragSource_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_SourceItemsControl = (TileContainer) sender;
            var visual = e.OriginalSource as Visual;

            m_TopWindow = Window.GetWindow(m_SourceItemsControl);
            m_InitialMousePosition = e.GetPosition(m_TopWindow);

            m_SourceItemContainer = m_SourceItemsControl.ListBox.ContainerFromElement(visual) as FrameworkElement;
            if (m_SourceItemContainer != null) m_DraggedData = m_SourceItemContainer.DataContext;
        }

        // Drag = mouse down + move by a certain amount
        private void DragSource_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (m_DraggedData == null) return;
            // Only drag when user moved the mouse by a reasonable amount.
            if (!IsMovementBigEnough(m_InitialMousePosition, e.GetPosition(m_TopWindow))) return;

            m_InitialMouseOffset =
                m_InitialMousePosition - m_SourceItemContainer.TranslatePoint(new Point(0, 0), m_TopWindow);


            var data = new DataObject(m_Format.Name, m_DraggedData);

            // Adding events to the window to make sure dragged adorner comes up when mouse is not over a drop target.
            var previousAllowDrop = m_TopWindow.AllowDrop;
            m_TopWindow.AllowDrop = true;
            m_TopWindow.DragEnter += TopWindow_DragEnter;
            m_TopWindow.DragOver += TopWindow_DragOver;
            m_TopWindow.DragLeave += TopWindow_DragLeave;

            //if (this._draggedData is UOBaseViewModel)
            //{
            //    _draggedData = (int)((UOBaseViewModel)_draggedData).EntryId;
            //    data = new DataObject(this._format.Name, this._draggedData);
            //}
            System.Windows.DragDrop.DoDragDrop((DependencyObject) sender, data, DragDropEffects.Move);

            // Without this call, there would be a problem in the following scenario: Click on a data item, and drag
            // the mouse very fast outside of the window. When doing this really fast, for some reason I don't get 
            // the Window leave event, and the dragged adorner is left behind.
            // With this call, the dragged adorner will disappear when we release the mouse outside of the window,
            // which is when the DoDragDrop synchronous method returns.
            RemoveDraggedAdorner();

            m_TopWindow.AllowDrop = previousAllowDrop;
            m_TopWindow.DragEnter -= TopWindow_DragEnter;
            m_TopWindow.DragOver -= TopWindow_DragOver;
            m_TopWindow.DragLeave -= TopWindow_DragLeave;

            m_DraggedData = null;
        }

        private void DragSource_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            m_DraggedData = null;
        }

        // DropTarget

        private void DropTarget_PreviewDragEnter(object sender, DragEventArgs e)
        {
            m_TargetItemsControl = (TileContainer) sender;
            //var margin = SumMargins(_targetItemsControl);
            //_targetTopMargin = margin.Top;
            //_targetLeftMargin = margin.Left;
            var draggedItem = e.Data.GetData(m_Format.Name);

            DecideDropTarget(e);
            if (draggedItem != null)
            {
                var position = e.GetPosition(m_TopWindow);
                //ScrollIntoView(this._targetItemsControl, position);
                // Dragged Adorner is created on the first enter only.
                ShowDraggedAdorner(position);
                CreateInsertionAdorner();
            }

            e.Handled = true;
        }

        private void DropTarget_PreviewDragOver(object sender, DragEventArgs e)
        {
            var draggedItem = e.Data.GetData(m_Format.Name);

            DecideDropTarget(e);
            if (draggedItem != null)
            {
                // Dragged Adorner is only updated here - it has already been created in DragEnter.
                var position = e.GetPosition(m_TopWindow);
                //ScrollIntoView(this._targetItemsControl, position);

                ShowDraggedAdorner(position);
                UpdateInsertionAdornerPosition();
            }

            e.Handled = true;
        }

        private void DropTarget_PreviewDrop(object sender, DragEventArgs e)
        {
            var draggedItem = e.Data.GetData(m_Format.Name);
            var indexRemoved = -1;

            if (draggedItem != null)
            {
                if ((e.Effects & DragDropEffects.Move) != 0)
                    indexRemoved = RemoveItemFromItemsControl(m_SourceItemsControl.ListBox, draggedItem);
                // This happens when we drag an item to a later position within the same ItemsControl.
                if (indexRemoved != -1 && Equals(m_SourceItemsControl, m_TargetItemsControl) &&
                    indexRemoved < m_InsertionIndex) m_InsertionIndex--;
                InsertItemInItemsControl(m_TargetItemsControl.ListBox, draggedItem, m_InsertionIndex);

                RemoveDraggedAdorner();
                RemoveInsertionAdorner();
            }

            e.Handled = true;
        }

        private void DropTarget_PreviewDragLeave(object sender, DragEventArgs e)
        {
            // Dragged Adorner is only created once on DragEnter + every time we enter the window. 
            // It's only removed once on the DragDrop, and every time we leave the window. (so no need to remove it here)
            var draggedItem = e.Data.GetData(m_Format.Name);

            if (draggedItem != null) RemoveInsertionAdorner();
            e.Handled = true;
        }

        // If the types of the dragged data and ItemsControl's source are compatible, 
        // there are 3 situations to have into account when deciding the drop target:
        // 1. mouse is over an items container
        // 2. mouse is over the empty part of an ItemsControl, but ItemsControl is not empty
        // 3. mouse is over an empty ItemsControl.
        // The goal of this method is to decide on the values of the following properties: 
        // targetItemContainer, insertionIndex and isInFirstHalf.
        private void DecideDropTarget(DragEventArgs e)
        {
            var targetItemsControlCount = m_TargetItemsControl.ListBox.Items.Count;
            var draggedItem = e.Data.GetData(m_Format.Name);

            if (IsDropDataTypeAllowed(draggedItem))
            {
                if (targetItemsControlCount > 0)
                {
                    m_HasVerticalOrientation = HasVerticalOrientation(
                        m_TargetItemsControl.ListBox.ItemContainerGenerator.ContainerFromIndex(0) as FrameworkElement);
                    m_TargetItemContainer =
                        m_TargetItemsControl.ListBox.ContainerFromElement((DependencyObject) e.OriginalSource) as
                            FrameworkElement;

                    if (m_TargetItemContainer != null)
                    {
                        var positionRelativeToItemContainer = e.GetPosition(m_TargetItemContainer);
                        m_IsInFirstHalf = IsInFirstHalf(m_TargetItemContainer, positionRelativeToItemContainer,
                            m_HasVerticalOrientation);
                        m_InsertionIndex =
                            m_TargetItemsControl.ListBox.ItemContainerGenerator.IndexFromContainer(m_TargetItemContainer);

                        if (!m_IsInFirstHalf) m_InsertionIndex++;
                    }
                    else
                    {
                        m_TargetItemContainer =
                            m_TargetItemsControl.ListBox.ItemContainerGenerator.ContainerFromIndex(
                                targetItemsControlCount - 1) as FrameworkElement;
                        m_IsInFirstHalf = false;
                        m_InsertionIndex = targetItemsControlCount;
                    }
                }
                else
                {
                    m_TargetItemContainer = null;
                    m_InsertionIndex = 0;
                }
            }
            else
            {
                m_TargetItemContainer = null;
                m_InsertionIndex = -1;
                e.Effects = DragDropEffects.None;
            }
        }

        // Can the dragged data be added to the destination collection?
        // It can if destination is bound to IList<allowed type>, IList or not data bound.
        private bool IsDropDataTypeAllowed(object draggedItem)
        {
            if (m_TargetItemsControl.TileType == TileType.IntegerToItem &&
                m_SourceItemsControl.TileType != TileType.Surface &&
                m_SourceItemsControl.TileType != TileType.IntegerToItem)
                return false;

            if (m_TargetItemsControl.TileType == TileType.Surface &&
                m_SourceItemsControl.TileType == TileType.IntegerToItem)
                return true;

            var collectionSource = m_TargetItemsControl.ListBox.ItemsSource;
            if (draggedItem != null)
                if (collectionSource != null)
                {
                    var draggedType = draggedItem.GetType();
                    var collectionType = collectionSource.GetType();

                    var genericIListType = collectionType.GetInterface("IList`1");
                    var genericArguments = genericIListType.GetGenericArguments();
                    if (genericArguments[0] == draggedType) return true;
                    if (draggedType == typeof(ModelLandData) &&
                        (m_TargetItemsControl.TileType == TileType.IntegerToLand ||
                         m_TargetItemsControl.TileType == TileType.IntegerToLandTexture)) return true;
                    if (draggedType == typeof(ModelItemData) && m_TargetItemsControl.TileType == TileType.IntegerToItem)
                        return true;
                }

            return false;
        }

        // Window

        private void TopWindow_DragEnter(object sender, DragEventArgs e)
        {
            ShowDraggedAdorner(e.GetPosition(m_TopWindow));
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void TopWindow_DragOver(object sender, DragEventArgs e)
        {
            ShowDraggedAdorner(e.GetPosition(m_TopWindow));
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void TopWindow_DragLeave(object sender, DragEventArgs e)
        {
            RemoveDraggedAdorner();
            e.Handled = true;
        }

        // Adorners

        // Creates or updates the dragged Adorner. 
        private void ShowDraggedAdorner(Point currentPosition)
        {
            if (m_DraggedAdorner == null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(m_SourceItemsControl);
                m_DraggedAdorner = new DraggedAdorner(m_DraggedData, GetDragDropTemplate(m_SourceItemsControl),
                    m_SourceItemContainer, adornerLayer);
            }

            var left = currentPosition.X - m_InitialMousePosition.X + m_InitialMouseOffset.X;
            Debug.WriteLine("Adorner Left: " + left);
            var top = currentPosition.Y - m_InitialMousePosition.Y + m_InitialMouseOffset.Y;
            Debug.WriteLine("Adorner Top: " + top);
            m_DraggedAdorner.SetPosition(left, top);
        }

        private void RemoveDraggedAdorner()
        {
            if (m_DraggedAdorner != null)
            {
                m_DraggedAdorner.Detach();
                m_DraggedAdorner = null;
            }
        }

        private void CreateInsertionAdorner()
        {
            if (m_TargetItemContainer != null)
            {
                // Here, I need to get adorner layer from targetItemContainer and not targetItemsControl. 
                // This way I get the AdornerLayer within ScrollContentPresenter, and not the one under AdornerDecorator (Snoop is awesome).
                // If I used targetItemsControl, the adorner would hang out of ItemsControl when there's a horizontal scroll bar.
                var adornerLayer = AdornerLayer.GetAdornerLayer(m_TargetItemContainer);
                m_InsertionAdorner = new InsertionAdorner(m_HasVerticalOrientation, m_IsInFirstHalf, m_TargetItemContainer,
                    adornerLayer);
            }
        }

        private void UpdateInsertionAdornerPosition()
        {
            if (m_InsertionAdorner != null)
            {
                m_InsertionAdorner.IsInFirstHalf = m_IsInFirstHalf;
                m_InsertionAdorner.InvalidateVisual();
            }
        }

        private void RemoveInsertionAdorner()
        {
            if (m_InsertionAdorner != null)
            {
                m_InsertionAdorner.Detach();
                m_InsertionAdorner = null;
            }
        }

        // Finds the orientation of the panel of the ItemsControl that contains the itemContainer passed as a parameter.
        // The orientation is needed to figure out where to draw the adorner that indicates where the item will be dropped.
        private static bool HasVerticalOrientation(FrameworkElement itemContainer)
        {
            var hasVerticalOrientation = true;
            if (itemContainer is TileContainer) itemContainer = ((TileContainer) itemContainer).ListBox;
            if (itemContainer == null) return hasVerticalOrientation;
            var panel = VisualTreeHelper.GetParent(itemContainer) as Panel;
            StackPanel stackPanel;
            WrapPanel wrapPanel;

            if ((stackPanel = panel as StackPanel) != null)
                hasVerticalOrientation = stackPanel.Orientation == Orientation.Vertical;
            else if ((wrapPanel = panel as WrapPanel) != null)
                hasVerticalOrientation = wrapPanel.Orientation == Orientation.Vertical;
            // You can add support for more panel types here.
            return hasVerticalOrientation;
        }

        private static void InsertItemInItemsControl(ItemsControl itemsControl, object itemToInsert, int insertionIndex)
        {
            if (itemToInsert == null) return;
            var itemsSource = itemsControl.ItemsSource;
            if (itemToInsert is UOBaseViewModel &&
                itemsControl.ItemsSource.GetType().GetInterface("IList`1").GetGenericArguments()[0] == typeof(int))
                itemToInsert = (int) ((UOBaseViewModel) itemToInsert).EntryId;
            if (itemsSource == null)
            {
                if (!itemsControl.Items.Contains(itemToInsert))
                    itemsControl.Items.Insert(insertionIndex, itemToInsert);
            }
            // Is the ItemsSource IList or IList<T>? If so, insert the dragged item in the list.
            else
            {
                var list = itemsSource as IList;
                if (list != null)
                {
                    if (!list.Contains(itemToInsert))
                        list.Insert(insertionIndex, itemToInsert);
                }
                else
                {
                    var type = itemsSource.GetType();
                    var genericIListType = type.GetInterface("IList`1");
                    if (genericIListType != null)
                        type.GetMethod("Insert").Invoke(itemsSource, new[] {insertionIndex, itemToInsert});
                }
            }
        }

        private static int RemoveItemFromItemsControl(ItemsControl itemsControl, object itemToRemove)
        {
            if (itemToRemove is UOBaseViewModel) return -1;
            var indexToBeRemoved = -1;
            if (itemToRemove != null)
            {
                indexToBeRemoved = itemsControl.Items.IndexOf(itemToRemove);

                if (indexToBeRemoved != -1)
                {
                    var itemsSource = itemsControl.ItemsSource;
                    if (itemsSource == null)
                    {
                        if (indexToBeRemoved >= 0 && indexToBeRemoved < itemsControl.Items.Count)
                            itemsControl.Items.RemoveAt(indexToBeRemoved);
                    }
                    // Is the ItemsSource IList or IList<T>? If so, remove the item from the list.
                    else if (itemsSource is IList)
                    {
                        var list = (IList) itemsSource;
                        if (indexToBeRemoved >= 0 && indexToBeRemoved < list.Count)
                            list.RemoveAt(indexToBeRemoved);
                    }
                    else
                    {
                        var type = itemsSource.GetType();
                        var genericIListType = type.GetInterface("IList`1");
                        if (genericIListType != null)
                            type.GetMethod("RemoveAt").Invoke(itemsSource, new object[] {indexToBeRemoved});
                    }
                }
            }

            return indexToBeRemoved;
        }

        private static bool IsInFirstHalf(FrameworkElement container, Point clickedPoint, bool hasVerticalOrientation)
        {
            if (hasVerticalOrientation) return clickedPoint.Y < container.ActualHeight / 2;
            return clickedPoint.X < container.ActualWidth / 2;
        }

        private static bool IsMovementBigEnough(Point initialMousePosition, Point currentPosition)
        {
            return Math.Abs(currentPosition.X - initialMousePosition.X) >=
                   SystemParameters.MinimumHorizontalDragDistance ||
                   Math.Abs(currentPosition.Y - initialMousePosition.Y) >= SystemParameters.MinimumVerticalDragDistance;
        }
    }
}