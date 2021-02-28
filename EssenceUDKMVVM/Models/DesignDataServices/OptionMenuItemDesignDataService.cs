using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EssenceUDKMVVM.Models.Model.Option;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class OptionMenuItemDesignDataService : IOptionMenuItem
    {
        public void GetData(Action<OptionTreeMenu, Exception> callback)
        {
            var optionMenuItem = new OptionTreeMenu {Name = "Options", Parent = null};
            callback(optionMenuItem, null);
        }
    }
}
