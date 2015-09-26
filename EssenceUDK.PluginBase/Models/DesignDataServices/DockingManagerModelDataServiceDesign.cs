#region

using System;

#endregion

namespace EssenceUDK.PluginBase.Models.DesignDataServices
{

    public class DockingManagerModelDataServiceDesign : IDockingManagerModelDataService
    {
        public void GetData(Action<object, Exception> callback)
        {
            //var locator = ServiceLocator.Current.GetInstance<ViewModelLocator>();
            //var item = new DockingManagerModel
            //{
            //    Tools = new ObservableCollection<ViewModelBase>
            //    {
            //        //locator.Land,
            //        //locator.MapRender,
            //        locator.Option
            //    }
            //};
            //callback(item, null);
        }
    }

}