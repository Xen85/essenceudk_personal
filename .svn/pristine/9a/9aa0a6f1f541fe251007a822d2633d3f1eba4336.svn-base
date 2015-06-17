using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapMakerApplication.Controllers.CollectionManager
{
    /// <summary>
    /// Interaction logic for CollectionManagerUserControl.xaml
    /// </summary>
    public partial class CollectionManagerUserControl : UserControl
    {
        #region Declarations


        #endregion //Declarations

        #region Ctor
        public CollectionManagerUserControl()
        {
            InitializeComponent();
        }   
        #endregion

        #region DependencyProperties

        #region Commands

        #region Command Move Down

        /// <summary>
        /// The CommandMoveDown attached property's name.
        /// </summary>
        public const string CommandMoveDownPropertyName = "CommandMoveDown";

        /// <summary>
        /// Gets the value of the CommandMoveDown attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the CommandMoveDown property of the specified object.</returns>
        public static ICommand GetCommandMoveDown(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandMoveDownProperty);
        }

        /// <summary>
        /// Sets the value of the CommandMoveDown attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the CommandMoveDown value of the specified object.</param>
        public static void SetCommandMoveDown(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandMoveDownProperty, value);
        }

        /// <summary>
        /// Identifies the CommandMoveDown attached property.
        /// </summary>
        public static readonly DependencyProperty CommandMoveDownProperty = DependencyProperty.RegisterAttached(
            CommandMoveDownPropertyName,
            typeof(ICommand),
            typeof(CollectionManagerUserControl),
            new UIPropertyMetadata(null));

        #endregion // Command Move Down

        #region Command Move Up

        /// <summary>
        /// The <see cref="CommandMoveUp" /> dependency property's name.
        /// </summary>
        public const string CommandMoveUpPropertyName = "CommandMoveUp";

        /// <summary>
        /// Gets or sets the value of the <see cref="CommandMoveUp" />
        /// property. This is a dependency property.
        /// </summary>
        public ICommand CommandMoveUp
        {
            get
            {
                return (ICommand)GetValue(CommandMoveUpProperty);
            }
            set
            {
                SetValue(CommandMoveUpProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CommandMoveUp" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandMoveUpProperty = DependencyProperty.Register(
            CommandMoveUpPropertyName,
            typeof(ICommand),
            typeof(CollectionManagerUserControl),
            new PropertyMetadata(null));

        #endregion //Command Move Up

        #region Command Remove

        /// <summary>
        /// The CommandRemove attached property's name.
        /// </summary>
        public const string CommandRemovePropertyName = "CommandRemove";

        /// <summary>
        /// Gets the value of the CommandRemove attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the CommandRemove property of the specified object.</returns>
        public static ICommand GetCommandRemove(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandRemoveProperty);
        }

        /// <summary>
        /// Sets the value of the CommandRemove attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the CommandRemove value of the specified object.</param>
        public static void SetCommandRemove(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandRemoveProperty, value);
        }

        /// <summary>
        /// Identifies the CommandRemove attached property.
        /// </summary>
        public static readonly DependencyProperty CommandRemoveProperty = DependencyProperty.RegisterAttached(
            CommandRemovePropertyName,
            typeof(ICommand),
            typeof(CollectionManagerUserControl),
            new UIPropertyMetadata(null));

        #endregion //Command Remove

        #region Command Add

        /// <summary>
        /// The <see cref="CommandAdd" /> dependency property's name.
        /// </summary>
        public const string CommandAddPropertyName = "CommandAdd";

        /// <summary>
        /// Gets or sets the value of the <see cref="CommandAdd" />
        /// property. This is a dependency property.
        /// </summary>
        public ICommand CommandAdd
        {
            get
            {
                return (ICommand)GetValue(CommandAddProperty);
            }
            set
            {
                SetValue(CommandAddProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CommandAdd" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandAddProperty = DependencyProperty.Register(
            CommandAddPropertyName,
            typeof(ICommand),
            typeof(CollectionManagerUserControl),
            new UIPropertyMetadata(null));

        #endregion //CommandAdd

        #endregion //Commands

        #region Button Visibility

        #region Direction

        /// <summary>
        /// The Direction attached property's name.
        /// </summary>
        public const string DirectionPropertyName = "Direction";

        /// <summary>
        /// Gets the value of the Direction attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the Direction property of the specified object.</returns>
        public static Direction GetDirection(DependencyObject obj)
        {
            return (Direction)obj.GetValue(DirectionProperty);
        }

        /// <summary>
        /// Sets the value of the Direction attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the Direction value of the specified object.</param>
        public static void SetDirection(DependencyObject obj, Direction value)
        {
            obj.SetValue(DirectionProperty, value);
        }

        /// <summary>
        /// Identifies the Direction attached property.
        /// </summary>
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.RegisterAttached(
            DirectionPropertyName,
            typeof(Direction),
            typeof(CollectionManagerUserControl),
            new UIPropertyMetadata(Direction.Right));

        #endregion //Direction

        #region Move Button Visibility

        /// <summary>
        /// The MoveButtonVisibility attached property's name.
        /// </summary>
        public const string MoveButtonVisibilityPropertyName = "MoveButtonVisibility";

        /// <summary>
        /// Gets the value of the MoveButtonVisibility attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the MoveButtonVisibility property of the specified object.</returns>
        public static Visibility GetMoveButtonVisibility(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(MoveButtonVisibilityProperty);
        }

        /// <summary>
        /// Sets the value of the MoveButtonVisibility attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the MoveButtonVisibility value of the specified object.</param>
        public static void SetMoveButtonVisibility(DependencyObject obj, Visibility value)
        {
            obj.SetValue(MoveButtonVisibilityProperty, value);
        }

        /// <summary>
        /// Identifies the MoveButtonVisibility attached property.
        /// </summary>
        public static readonly DependencyProperty MoveButtonVisibilityProperty = DependencyProperty.RegisterAttached(
            MoveButtonVisibilityPropertyName,
            typeof(Visibility),
            typeof(CollectionManagerUserControl),
            new UIPropertyMetadata(Visibility.Visible));

        #endregion // Move Button Visibility



        #endregion //endregion

        #endregion //DependencyProperties
    }

    public enum Direction
    {
        Right,
        Left
    }





}
