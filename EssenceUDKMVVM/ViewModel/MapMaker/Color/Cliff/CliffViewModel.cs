using System;
using System.Collections.Generic;
using EssenceUDK.MapMaker.Elements.Textures.TexureCliff;
using EssenceUDKMVVM.ViewModel.DockableModels;
using EssenceUDKMVVM.ViewModel.MapMaker.Color.Cliff.Wrappers;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM.ViewModel.MapMaker.Color.Cliff
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class CliffViewModel : ViewModelDockableBase
    {
        private readonly Dictionary<DirectionCliff, TileContainerViewModel> _list;

        /// <summary>
        ///     Initializes a new instance of the CliffViewModel class.
        /// </summary>
        public CliffViewModel()
        {
            _list = new Dictionary<DirectionCliff, TileContainerViewModel>();
            foreach (DirectionCliff direction in Enum.GetValues(typeof(DirectionCliff)))
                _list.Add(direction, new TileContainerViewModel {Reflesh = true});

            var listView = ServiceLocator.Current.GetInstance<CliffListViewModel>();
            if (listView != null)
                listView.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName != "SelectedItem" && e.PropertyName != null) return;

                    if (SelectedItem != null)
                        foreach (DirectionCliff direction in Enum.GetValues(typeof(DirectionCliff)))
                            _list[direction].List = SelectedItem[direction].List;

                    RaisePropertyChanged(() => SelectedItem);
                };
        }

        //NorthSouth = 0,
        public TileContainerViewModel NorthSouth => _list[DirectionCliff.NorthSouth];

        //WestEast = 1,

        public TileContainerViewModel WestEast => _list[DirectionCliff.WestEast];

        //NorthEnd = 2,
        public TileContainerViewModel NorthEnd => _list[DirectionCliff.NorthEnd];

        //EastEnd = 3,
        public TileContainerViewModel EastEnd => _list[DirectionCliff.EastEnd];
        //SouthEnd = 4,

        public TileContainerViewModel SouthEnd => _list[DirectionCliff.SouthEnd];

        //WestEnd = 5,
        public TileContainerViewModel WestEnd => _list[DirectionCliff.WestEnd];
        //NorthWestRounding = 6,

        public TileContainerViewModel NorthWestRounding => _list[DirectionCliff.NorthWestRounding];
        //NorthEastRounding = 7,

        public TileContainerViewModel NorthEastRounding => _list[DirectionCliff.NorthEastRounding];
        //SouthEastRounding = 8,


        public TileContainerViewModel SouthEastRounding => _list[DirectionCliff.SouthEastRounding];
        //SouthWestRounding = 9

        public TileContainerViewModel SouthWestRounding => _list[DirectionCliff.SouthWestRounding];


        public SupportObject SelectedItem => ServiceLocator.Current.GetInstance<CliffListViewModel>().SelectedItem;
    }
}