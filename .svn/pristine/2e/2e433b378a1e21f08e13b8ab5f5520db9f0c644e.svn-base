using System.Collections.ObjectModel;
using EssenceUDK.Platform.MiscHelper.Components;
using EssenceUDK.Platform.MiscHelper.Interfaces;

namespace EssenceUDK.Platform.MiscHelper.Factories
{
    public abstract class Factory : IFactory 
    {
        protected UODataManager DataManager;
        protected ObservableCollection<TileCategory> _categories;
        public static readonly char[] Separators = { '\t', ' ' };

        protected Factory(UODataManager location)
        {
            DataManager = location;
            _categories = new ObservableCollection<TileCategory>();
        }

        #region Implementation of IFactory

        public ObservableCollection<TileCategory> Categories
        {
            get { return _categories; }
        }

        public virtual void Populate()
        {
            
        }

        #endregion
    }
}
