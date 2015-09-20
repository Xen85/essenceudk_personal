#region

using System;
using EssenceUDK.PluginBase.Models.Option;

#endregion

namespace EssenceUDK.PluginBase.Models.DesignDataServices
{

    public class OptionMenuItemDesignDataService : IOptionMenuItem
    {
        public void GetData(Action<object, Exception> callback)
        {
            var optionMenuItem = new OptionTreeMenu {Name = "Options", Parent = null};
            callback(optionMenuItem, null);
        }
    }

}