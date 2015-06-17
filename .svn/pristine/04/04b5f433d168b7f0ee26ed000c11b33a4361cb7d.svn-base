using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.Items.ItemText;

namespace EssenceUDK.MapMaker.Elements.Items
{
    [Serializable]
    public class CollectionAreaItems : NotificationObject , IContainerSet
    {
        private ObservableCollection<AreaItems> _list;
        
        #region Props
        public ObservableCollection<AreaItems> List { get { return _list; } set { _list = value; RaisePropertyChanged(()=>List); } }
        #endregion // Props

        #region Fields
        [NonSerialized] private Dictionary<Color, AreaItems> _items; 
        #endregion //Fields

        #region Ctor
        public CollectionAreaItems()
        {
            List = new ObservableCollection<AreaItems>();
            _items = null;
        }
        #endregion //ctor

        #region Search Methods

        public AreaItems SearchByColor(Color color)
        {
            AreaItems i;
            _items.TryGetValue(color, out i);
            return i;
        }

        #endregion //Search Methods

        #region IContainerSet Implementation
        public void InitializeSeaches()
        {
            _items = new Dictionary<Color, AreaItems>();

            foreach (var itemse in List)
            {
                try
                {
                    _items.Add(itemse.Color, itemse);
                }
                catch(Exception)
                {
                    
                }
            }
        }
        #endregion //IContainerSet Implementation


        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(()=>List,info);
        }

        protected CollectionAreaItems(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            List = new ObservableCollection<AreaItems>(Deserialize(()=>List,info));
        }
    }
}
