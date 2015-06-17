using System;
using System.Collections.ObjectModel;
using EssenceUDK.MapMaker.Elements.BaseTypes;

namespace EssenceUDK.MapMaker.Elements.Items.ItemText
{
    [Serializable]
    public class CollectionItem : NotificationObject
    {
        private string _name;
        private double _percent;
        private ObservableCollection<SingleItem> _list;
        
        #region Props

        public double Percent { get { return _percent; } set { _percent = value; RaisePropertyChanged(()=>Percent); } }
        
        public string Name { get { return _name; } set { _name = value; RaisePropertyChanged(()=>Name); } }
        
        public ObservableCollection<SingleItem> List { get { return _list; } set { _list = value; RaisePropertyChanged(()=>List); } }
        
        #endregion //Props

        #region Ctor

        public CollectionItem()
        {
            Percent = 0;
            List = new ObservableCollection<SingleItem>();
            Name = "";
        }

        #endregion //Ctor


        #region Methods

        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Percent":
                        {
                            if(Percent >100 || Percent <0)
                            {
                                return "Percent must be between 100 and 0";
                            }
                        }
                        break;
                }
                return null;
            }
        }

        #endregion //Methods

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(()=>Name,info);
            Serialize(()=>Percent,info);
            Serialize(()=>List,info);
        }

        protected CollectionItem(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Name = (string) Deserialize(() => Name, info);
            Percent = (double) Deserialize(() => Percent, info);
            List = new ObservableCollection<SingleItem>(Deserialize(()=>List,info));
        }


    }
}
