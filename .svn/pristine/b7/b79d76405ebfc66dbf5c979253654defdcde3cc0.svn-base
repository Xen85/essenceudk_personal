using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.Items.ItemCoast;

namespace EssenceUDK.MapMaker.Elements.Items
{
    [Serializable]
    public class CollectionAreaTransitionItemCoast : NotificationObject, IContainerSet
    {
        private ObservableCollection<AreaTransitionItemCoast> _list;
        #region Props
        public ObservableCollection<AreaTransitionItemCoast> List { get { return _list; } set { _list = value; RaisePropertyChanged(()=>List); } }
        #endregion //Props

        #region Fields
        [NonSerialized] private Dictionary<Color, AreaTransitionItemCoast> _coastses;
        [NonSerialized] private Dictionary<Color, bool> _dictionaryColorCoast;
        #endregion //Fields

        #region Ctor
        public CollectionAreaTransitionItemCoast()
        {
            List = new ObservableCollection<AreaTransitionItemCoast>();
        }
        #endregion //Ctor

        #region search methods

        public AreaTransitionItemCoast FindGroundByColor(Color color )
        {
            AreaTransitionItemCoast coast = null;

            _coastses.TryGetValue(color, out coast);

            return coast;
        }

        public bool FindCoastByColor(Color color)
        {
            bool ret;
            _dictionaryColorCoast.TryGetValue(color, out ret);

            return ret;
        }
        
        public AreaTransitionItemCoast FindByColor(Color color)
        {
            AreaTransitionItemCoast c;
            _coastses.TryGetValue(color, out c);
            return c;
        }

        #endregion //seach Methods

        #region IContainerSet Implementation
        public void InitializeSeaches()
        {
            _coastses = new Dictionary<Color, AreaTransitionItemCoast>();
            _dictionaryColorCoast = new Dictionary<Color, bool>();

            foreach (var itemsCoastse in List)
            {
                try
                {
                    _coastses.Add(itemsCoastse.Ground.Color, itemsCoastse);

                }
                catch (Exception)
                {
                }

                try
                {
                    _dictionaryColorCoast.Add(itemsCoastse.Coast.Color, true);
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion //IContainerSet Implementation

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(()=>List,info);
        }

        protected CollectionAreaTransitionItemCoast(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            List = new ObservableCollection<AreaTransitionItemCoast>(Deserialize(()=>List,info));
        }

    }
}
