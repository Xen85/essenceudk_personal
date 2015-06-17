using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapMakerApplication.Controllers
	{
	/// <summary>
	/// Interaction logic for MultiTransitionManager.xaml
	/// </summary>
	public partial class MultiTransitionManager : UserControl
		{
		public MultiTransitionManager()
			{
			InitializeComponent();
			}

	/// <summary>
		/// The <see cref="DataTemplate" /> dependency property's name.
		/// </summary>
		public const string DataTemplatePropertyName = "DataTemplate";

		/// <summary>
		/// Gets or sets the value of the <see cref="DataTemplate" />
		/// property. This is a dependency property.
		/// </summary>
		public DataTemplate DataTemplate
			{
			get
				{
				return ( DataTemplate ) GetValue(DataTemplateProperty);
				}
			set
				{
				SetValue(DataTemplateProperty, value);
				}
			}

		/// <summary>
		/// Identifies the <see cref="DataTemplate" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty DataTemplateProperty = DependencyProperty.Register(
			DataTemplatePropertyName,
			typeof(DataTemplate),
			typeof(MultiTransitionManager),
			new UIPropertyMetadata(null));


		/// <summary>
		/// The <see cref="Source" /> dependency property's name.
		/// </summary>
		public const string SourcePropertyName = "Source";

		/// <summary>
		/// Gets or sets the value of the <see cref="Source" />
		/// property. This is a dependency property.
		/// </summary>
		public object Source
			{
			get
				{
				return ( object ) GetValue(SourceProperty);
				}
			set
				{
				SetValue(SourceProperty, value);
				}
			}

		/// <summary>
		/// Identifies the <see cref="Source" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
			SourcePropertyName,
			typeof(object),
			typeof(MultiTransitionManager),
			new UIPropertyMetadata(null));



		}
		


	}
