using System;
using System.Windows.Input;
using EssenceUDK.MapMaker;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.DesignDataServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace EssenceUDKMVVM.ViewModel.MapMaker
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MapMakerSdkViewModel : ViewModelBase
    {

        private MapSdk _sdk;
        /// <summary>
        /// Initializes a new instance of the MapMakerSdkViewModel class.
        /// </summary>
        public MapMakerSdkViewModel()
        {
            //Sdk = new MapSdk();
            //Sdk.LoadFromXML(@"C:\Users\Fabio\Desktop\map\TM.xml");
        }

        [PreferredConstructor]
        public MapMakerSdkViewModel(DataServiceMapMakerSdk dataservice)
        {
            dataservice.GetData(
             (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }
                    
                    Sdk = (MapSdk) item;
                });
           
            //Sdk.LoadFromXML(@"C:\Users\Fabio\Desktop\map\TM.xml");
            //RaisePropertyChanged(() => Sdk);
        }


        public MapSdk Sdk
        {
        get
            {
                return _sdk;
            }
            set
            {
                _sdk = value;
                RaisePropertyChanged(() => Sdk);
                RaisePropertyChanged(null);
            }
        }


       




    }
}