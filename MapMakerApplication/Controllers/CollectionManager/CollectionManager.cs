using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace MapMakerApplication.Controllers.CollectionManager
{
	public class CollectionManager:Control
	{
		#region DependencyProperties

		#region ItemsSource
		/// <summary>
		/// The <see cref="ItemsSource" /> dependency property's name.
		/// </summary>
		public const string ItemsSourcePropertyName = "ItemsSource";

		/// <summary>
		/// Gets or sets the value of the <see cref="ItemsSource" />
		/// property. This is a dependency property.
		/// </summary>
		public IList ItemsSource
		{
			get
			{
				return (IList)GetValue(ItemsSourceProperty);
			}
			set
			{
				SetValue(ItemsSourceProperty, value);
			}
		}

		/// <summary>
		/// Identifies the <see cref="ItemsSource" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
			ItemsSourcePropertyName,
			typeof(IEnumerable), typeof(CollectionManager)
			,
			new UIPropertyMetadata(null));
		#endregion

		#region SelectedIndex
		/// <summary>
		/// The <see cref="SelectedIndex" /> dependency property's name.
		/// </summary>
		public const string SelectedIndexPropertyName = "SelectedIndex";

		/// <summary>
		/// Gets or sets the value of the <see cref="SelectedIndex" />
		/// property. This is a dependency property.
		/// </summary>
		public int SelectedIndex
		{
			get
			{
				return (int)GetValue(SelectedIndexProperty);
			}
			set
			{
				SetValue(SelectedIndexProperty, value);
			}
		}

		/// <summary>
		/// Identifies the <see cref="SelectedIndex" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
			SelectedIndexPropertyName,
			typeof(int),
			typeof(CollectionManager),
			new UIPropertyMetadata(0));
		#endregion

		#region Type

		/// <summary>
		/// The Type attached property's name.
		/// </summary>
		public const string TypePropertyName = "Type";

		/// <summary>
		/// Gets the value of the Type attached property 
		/// for a given dependency object.
		/// </summary>
		/// <param name="obj">The object for which the property value
		/// is read.</param>
		/// <returns>The value of the Type property of the specified object.</returns>
		public static Type GetType(DependencyObject obj)
		{
			return (Type)obj.GetValue(TypeProperty);
		}

		/// <summary>
		/// Sets the value of the Type attached property
		/// for a given dependency object. 
		/// </summary>
		/// <param name="obj">The object to which the property value
		/// is written.</param>
		/// <param name="value">Sets the Type value of the specified object.</param>
		public static void SetType(DependencyObject obj, Type value)
		{
			obj.SetValue(TypeProperty, value);
		}

		/// <summary>
		/// Identifies the Type attached property.
		/// </summary>
		public static readonly DependencyProperty TypeProperty = DependencyProperty.RegisterAttached(
			TypePropertyName,
			typeof(Type),
			typeof(CollectionManager),
			new UIPropertyMetadata(typeof(object)));

		#endregion

		#region CommandAdd
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
			typeof(CollectionManager),
			new UIPropertyMetadata(null));
		#endregion

		#region CommandRemove
		/// <summary>
		/// The <see cref="CommandRemove" /> dependency property's name.
		/// </summary>
		public const string CommandRemovePropertyName = "CommandRemove";

		/// <summary>
		/// Gets or sets the value of the <see cref="CommandRemove" />
		/// property. This is a dependency property.
		/// </summary>
		public ICommand CommandRemove
		{
			get
			{
				return (ICommand)GetValue(CommandRemoveProperty);
			}
			set
			{
				SetValue(CommandRemoveProperty, value);
			}
		}

		/// <summary>
		/// Identifies the <see cref="CommandRemove" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty CommandRemoveProperty = DependencyProperty.Register(
			CommandRemovePropertyName,
			typeof(ICommand),
			typeof(CollectionManager),
			new UIPropertyMetadata(CommandRemoveDefault));
		#endregion

		#region CommandMoveUp
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
			typeof(CollectionManager),
			new UIPropertyMetadata(null));
		#endregion

		#region CommandMoveDown

		/// <summary>
		/// The <see cref="CommandMoveDown" /> dependency property's name.
		/// </summary>
		public const string CommandMoveDownPropertyName = "CommandMoveDown";

		/// <summary>
		/// Gets or sets the value of the <see cref="CommandMoveDown" />
		/// property. This is a dependency property.
		/// </summary>
		public ICommand CommandMoveDown
		{
			get
			{
				return (ICommand)GetValue(CommandMoveDownProperty);
			}
			set
			{
				SetValue(CommandMoveDownProperty, value);
			}
		}

		/// <summary>
		/// Identifies the <see cref="CommandMoveDown" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty CommandMoveDownProperty = DependencyProperty.Register(
			CommandMoveDownPropertyName,
			typeof(ICommand),
			typeof(CollectionManager),
			new UIPropertyMetadata(null));

		#endregion //CommandMoveDown

		#region ShowMoveButtons
		/// <summary>
		/// The <see cref="ShowMoveButtons" /> dependency property's name.
		/// </summary>
		public const string ShowMoveButtonsPropertyName = "ShowMoveButtons";

		/// <summary>
		/// Gets or sets the value of the <see cref="ShowMoveButtons" />
		/// property. This is a dependency property.
		/// </summary>
		public bool ShowMoveButtons
		{
			get
			{
				return (bool)GetValue(ShowMoveButtonsProperty);
			}
			set
			{
				SetValue(ShowMoveButtonsProperty, value);
			}
		}

		/// <summary>
		/// Identifies the <see cref="ShowMoveButtons" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ShowMoveButtonsProperty = DependencyProperty.Register(
			ShowMoveButtonsPropertyName,
			typeof(bool),
			typeof(CollectionManager),
			new UIPropertyMetadata(true));
		#endregion

		#region ListBoxStyle
		/// <summary>
		/// The <see cref="ListBoxStyle" /> dependency property's name.
		/// </summary>
		public const string ListBoxStylePropertyName = "ListBoxStyle";

		/// <summary>
		/// Gets or sets the value of the <see cref="ListBoxStyle" />
		/// property. This is a dependency property.
		/// </summary>
		public Style ListBoxStyle
		{
			get
			{
				return (Style)GetValue(ListBoxStyleProperty);
			}
			set
			{
				SetValue(ListBoxStyleProperty, value);
			}
		}

		/// <summary>
		/// Identifies the <see cref="ListBoxStyle" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ListBoxStyleProperty = DependencyProperty.Register(
			ListBoxStylePropertyName,
			typeof(Style),
			typeof(CollectionManager),
			new UIPropertyMetadata(null));
		#endregion

		#region SelectedItem
		/// <summary>
		/// The <see cref="SelectedItem" /> dependency property's name.
		/// </summary>
		public const string SelectedItemPropertyName = "SelectedItem";

		/// <summary>
		/// Gets or sets the value of the <see cref="SelectedItem" />
		/// property. This is a dependency property.
		/// </summary>
		public object SelectedItem
		{
			get
			{
				return (object)GetValue(SelectedItemProperty);
			}
			set
			{
				SetValue(SelectedItemProperty, value);
			}
		}

		/// <summary>
		/// Identifies the <see cref="SelectedItem" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
			SelectedItemPropertyName,
			typeof(object),
			typeof(CollectionManager),
			new UIPropertyMetadata(0));
		#endregion

		#region ListBox
		/// <summary>
		/// The <see cref="ListBox" /> dependency property's name.
		/// </summary>
		public const string ListBoxPropertyName = "ListBox";

		/// <summary>
		/// Gets or sets the value of the <see cref="ListBox" />
		/// property. This is a dependency property.
		/// </summary>
		public ListBox ListBox
		{
			get
			{
				return (ListBox)GetValue(ListBoxProperty);
			}
			set
			{
				SetValue(ListBoxProperty, value);
			}
		}

		/// <summary>
		/// Identifies the <see cref="ListBox" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ListBoxProperty = DependencyProperty.Register(
			ListBoxPropertyName,
			typeof(ListBox),
			typeof(CollectionManager),
			new UIPropertyMetadata(null));
		#endregion

		#endregion

		#region DefaultCommands

		private static RelayCommand CommandRemoveDefault { get; set; }

		private static RelayCommand CommandMoveUpDefault { get; set; }

		private static RelayCommand CommandMoveDownDefault { get; set; }

		#endregion

		#region DefaultCommandMethods

		private void DefaultCommandRemoveExecuted()
		{
			var selected = SelectedItem;
			//var list = ListBox.Items;
			//list.Remove(selected);
		}

		private bool DefaultCommandRemoveAvable()
		{
			return SelectedItem != null;
		}

		private void DefaultCommandMoveExecuted(int increment)
		{
			var selected = SelectedItem;
			//var list = ListBox.Items;
			//var position = ;
			//list.Remove(selected);
			//list.Insert(position - increment, selected);
		}

		//private bool DefaultCommandMoveAvable()
		//{
		//    //return ListBox.SelectedItem != null;
		//}



		#endregion
	}
}
