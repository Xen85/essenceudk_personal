﻿using EssenceUDK.MapMaker;
using EssenceUDKMVVM.ViewModel.DockableModels;
using GalaSoft.MvvmLight.Ioc;
using DataServiceMapMakerSdk = MapMakerPlugin.Models.DesignData.DataServiceMapMakerSdk;

namespace MapMakerPlugin.ViewModels
{

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MapMakerSdkViewModel : ViewModelDockableBase
    {
        private MapSdk _sdk;

        /// <summary>
        ///     Initializes a new instance of the MapMakerSdkViewModel class.
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
            get { return _sdk; }
            set
            {
                _sdk = value;
                RaisePropertyChanged(() => Sdk);
                RaisePropertyChanged(null);
            }
        }
    }

}