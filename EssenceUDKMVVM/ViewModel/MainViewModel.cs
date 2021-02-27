using EssenceUDKMVVM.Model_Interfaces.Model;
using EssenceUDKMVVM.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace EssenceUDKMVVM.ViewModel
{
	/// <summary>
	///     This class contains properties that the main View can data bind to.
	///     <para>
	///         See http://www.galasoft.ch/mvvm
	///     </para>
	/// </summary>
	public class MainViewModel : ViewModelBase
    {
	    /// <summary>
	    ///     The <see cref="WelcomeTitle" /> property's name.
	    /// </summary>
	    public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private readonly IDataService _dataService;

        private string _welcomeTitle = string.Empty;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        
        [PreferredConstructor]
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                        // Report error here
                        return;

                    WelcomeTitle = ((DataItem) item).Title;
                });
        }

        /// <summary>
        ///     Gets the WelcomeTitle property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string WelcomeTitle
        {
            get => _welcomeTitle;

            set
            {
                if (_welcomeTitle == value) return;

                _welcomeTitle = value;
                RaisePropertyChanged(WelcomeTitlePropertyName);
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}