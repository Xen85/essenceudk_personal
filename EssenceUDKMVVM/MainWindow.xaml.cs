using System.Windows;
using EssenceUDKMVVM.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM
	{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
		{
		/// <summary>
		/// Initializes a new instance of the MainWindow class.
		/// </summary>
		public MainWindow()
			{
			InitializeComponent();
			Closing += ( s, e ) => ServiceLocator.Current.GetInstance<ViewModelLocator>().UODataManager.Cleanup();
			}
		}
	}