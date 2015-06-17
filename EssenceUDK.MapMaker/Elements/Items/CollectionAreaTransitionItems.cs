using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.BaseTypes;
using EssenceUDK.MapMaker.Elements.Items.ItemsTransition;

namespace EssenceUDK.MapMaker.Elements.Items
{
    [Serializable]
    public class CollectionAreaTransitionItems : NotificationObject, IContainerSet
    {
        private ObservableCollection<AreaTransitionItem> _list;
        private bool init = false;
        #region Props
        public ObservableCollection<AreaTransitionItem> List { get { return _list; } set { _list = value; RaisePropertyChanged(()=>List); } }
        #endregion //Props

        #region Fields
        //[NonSerialized] private Dictionary<Color, AreaTransitionItem> _dictionarySmooth;
        //[NonSerialized] private Dictionary<Color, bool> _dictionaryColorTo;
        [NonSerialized] private Dictionary<int, AreaTransitionItem> _dictionaryFindById; 
        #endregion //Fields

        #region Ctor
        public CollectionAreaTransitionItems()
        {
            List = new ObservableCollection<AreaTransitionItem>();
        }
        #endregion

        #region Search Methods

        //public AreaTransitionItem FindFromByColor(Color color)
        //{
        //    AreaTransitionItem smooth;

        //    _dictionarySmooth.TryGetValue(color, out smooth);

        //    return smooth;
        //}

        
        //public bool ContainsColorTo(Color color)
        //{
        //    bool ret;
        //    _dictionaryColorTo.TryGetValue(color, out ret);
        //    return ret;
        //}

        #endregion

        #region IContainerSet
        public void  InitializeSeaches()
        {
            if (init)
                return;
            //_dictionarySmooth = new Dictionary<Color, AreaTransitionItem>();
            //_dictionaryColorTo = new Dictionary<Color, bool>();
            _dictionaryFindById=new Dictionary<int, AreaTransitionItem>();
            foreach (AreaTransitionItem itemsSmooth in List)
            {
                //try
                //{
                //    _dictionarySmooth.Add(itemsSmooth.ColorFrom, itemsSmooth);
                //}
                //catch (Exception)
                //{
                //}

                //try
                //{
                //    _dictionaryColorTo.Add(itemsSmooth.ColorTo,true);
                //}
                //catch (Exception)
                //{
                //}
                try
                {
                    _dictionaryFindById.Add(itemsSmooth.TextureIdTo,itemsSmooth);
                }
                catch (Exception)
                {
                    
                }
            }
            init = true;
        }
        #endregion //IContainerSet




        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(()=>List,info);
        }

         protected CollectionAreaTransitionItems(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            List = new ObservableCollection<AreaTransitionItem>(Deserialize(()=>List,info));
        }

        public AreaTransitionItem FindById(int index)
        {
            AreaTransitionItem tmp;
            _dictionaryFindById.TryGetValue(index, out tmp);
            return tmp;
        }

        
    }
}
