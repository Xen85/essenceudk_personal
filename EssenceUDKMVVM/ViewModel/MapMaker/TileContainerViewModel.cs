﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EssenceUDKMVVM.Controls.Tiles;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
namespace EssenceUDKMVVM.ViewModel.MapMaker
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TileContainerViewModel : ViewModelBase, ITileContainerViewModel
    {
        private int _selectedIndex;
        private ObservableCollection<int> _list;

        private int _selected;
        /// <summary>
        /// Initializes a new instance of the TileContainerViewModel class.
        /// </summary>
        public TileContainerViewModel()
        {
            _selectedIndex = 0;
            Remove = new RelayCommand(() =>
            {
                if (_selected > 0)
                    _list.Remove(_selected);
                else if (_selectedIndex > 0)
                    _list.RemoveAt(_selectedIndex);
            }, () => _selected > 0 && _list.Contains(_selected) || (_list.Count > 0 && _selectedIndex >= 0 && _selectedIndex < _list.Count));

        }

        public bool Reflesh { get; set; }

        public ObservableCollection<int> List
        {
            get { return _list; }
            set
            {
                _list = value;
                if (Reflesh)
                    RaisePropertyChanged(() => List);
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                if (Reflesh)
                    RaisePropertyChanged(() => SelectedIndex);
            }
        }

        public object Selected
        {
            get { return _selected; }
            set
            {
                value = value as int? ?? -1;
                if ((int)value < 0) return;
                _selected = (int)value;
                if (Reflesh)
                    RaisePropertyChanged(() => Selected);
            }
        }

        public ICommand Remove { get; private set; }
    }
}