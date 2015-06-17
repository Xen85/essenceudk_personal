using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EssenceUDK.MapMaker.Elements.Textures.TexureCliff;
using EssenceUDKMVVM.ViewModel.MapMaker.Color.AreaColor;
using EssenceUDKMVVM.ViewModel.MapMaker.Color.Cliff.Wrappers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM.ViewModel.MapMaker.Color.Cliff
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CliffListViewModel : ViewModelDockableBase
    {
        private SupportObject _selectedItem;
        private ObservableCollection<SupportObject> _list; 
        /// <summary>
        /// Initializes a new instance of the CliffListViewModel class.
        /// </summary>
        public CliffListViewModel()
        {
            _list = new ObservableCollection<SupportObject>();
            /*
             * event handler about refreshing property
             */
            ServiceLocator.Current.GetInstance<AreaColorViewModel>().PropertyChanged +=
                (s, e) =>
                {
                    if (e.PropertyName != "SelectedAreaColor") return;
                    RaisePropertyChanged(() => List);
                };

            AddCommand = new RelayCommand(() =>
            {
                foreach (DirectionCliff direction in Enum.GetValues(typeof(DirectionCliff)))
                {
                    Area.TransitionCliffTextures.Add(new AreaTransitionCliffTexture(){Directions = direction});
                }
                RaisePropertyChanged(()=>List);
               
            }, () => Area != null);

            RemoveCommand = new RelayCommand(() =>
            {
                foreach (var item in (from DirectionCliff direction in Enum.GetValues(typeof(DirectionCliff)) select SelectedItem[direction]).Where(item => item !=null))
                {
                    Area.TransitionCliffTextures.Remove(item);
                }
                RaisePropertyChanged(() => List);
            }, () => Area != null && SelectedItem != null);
                   

            if (IsInDesignModeStatic)
            {
                SelectedItem = List.First();
            }
        }

        /// <summary>
        /// AreaColor Selected
        /// </summary>
        public EssenceUDK.MapMaker.Elements.ColorArea.ColorArea.AreaColor Area
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AreaColorViewModel>().SelectedAreaColor;
            }
        }

        /// <summary>
        /// This is the list of elements which have been filtered by the main collection of the area color
        /// </summary>
        public ObservableCollection<SupportObject> List
        {
            get
            {
                _list = new ObservableCollection<SupportObject>();
                var removelist = new List<AreaTransitionCliffTexture>();
                var color = new System.Windows.Media.Color();
                SupportObject support = null;
                foreach (var item in Area.TransitionCliffTextures)
                {
                    //reflesh elements check
                    if (support == null || item.ColorTo != color)
                    {
                        color = item.ColorTo;
                        support = new SupportObject() { Color = color };
                        _list.Add(support);
                    }
                    var direction = item.Directions;
                    var obj = support[direction];
                    if (obj != null)
                    {
                        //add to remove list
                        removelist.Add(item);
                    }
                    //add to support and merge
                    support[direction] = item;
                }

                foreach (var obj in removelist)
                    Area.TransitionCliffTextures.Remove((AreaTransitionCliffTexture) obj);

                return _list;
            }
        }


        public SupportObject SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ICommand AddCommand { get; private set; }

        public ICommand RemoveCommand { get; private set; }

    }

}