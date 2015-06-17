using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EssenceUDKMVVM.Models.Model;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class DesignMenuDataService : IMenuDataservice
    {
        public void GetData(Action<object, Exception> callback)
        {
            var item = new SubMenuModel {Header = "Options"};
            callback(item, null);
        }
    }
}
