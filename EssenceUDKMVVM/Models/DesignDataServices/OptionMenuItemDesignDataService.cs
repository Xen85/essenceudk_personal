using EssenceUDKMVVM.Models.Model.Option;
using System;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class OptionMenuItemDesignDataService : IOptionMenuItem
    {
        public void GetData(Action<object, Exception> callback)
        {
            var optionMenuItem = new OptionTreeMenu { Name = "Options", Parent = null };
            callback(optionMenuItem, null);
        }
    }
}