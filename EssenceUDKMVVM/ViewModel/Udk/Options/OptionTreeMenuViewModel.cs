﻿using System.Collections.Generic;
using EssenceUDKMVVM.Models;
using EssenceUDKMVVM.Models.Model.Option;
using EssenceUDKMVVM.ViewModel.Utils;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.ViewModel.Udk.Options
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class OptionTreeMenuViewModel : TreeViewItemViewModel
    {
        protected static Dictionary<OptionTreeMenu, OptionTreeMenuViewModel> Dictionary =
            new Dictionary<OptionTreeMenu, OptionTreeMenuViewModel>();
        private IOptionMenuItem _dataservice;
        private OptionTreeMenu _model;
        
        protected OptionTreeMenuViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren) : base(parent, lazyLoadChildren)
        {
        }

        public OptionTreeMenuViewModel(IOptionMenuItem dataservice) : base()
        {
            _dataservice = dataservice;
            _dataservice.GetData((item, error) =>
            {
                if (error == null)
                    _model = item as OptionTreeMenu;
            });
            
            

        }



    }
}