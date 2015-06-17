using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EssenceUDK.MapMaker.Elements.BaseTypes;

namespace EssenceUDK.MapMaker.Elements.Items.Items
{
    [Serializable]
    public class SingleItem : NotificationObject
    {
        private int _id;
        private int _x;
        private int _y;
        private int _z;
        private string _name;
        private int _hue;


        public int Id { get { return _id; } set { _id = value; RaisePropertyChanged(()=>Id); } }

        public string Name { get { return _name; } set { _name = value; RaisePropertyChanged(() => Name); } }

        public int Hue { get { return _hue; } set { _hue = value; RaisePropertyChanged(() => Hue); } }

        public int X { get { return _x; } set { _x = value; RaisePropertyChanged(()=>X); } }

        public int Y { get { return _y; } set { _y = value; RaisePropertyChanged(()=>Y); } }

        public int Z { get { return _z; } set { _z = value; RaisePropertyChanged(()=>Z); } }


        public SingleItem()
        {
            _id = -1;
            _name = "";
            _hue = 0;
            _x = 0;
            _y = 0;
            _z = 0;
        }

        #region Serialization
        
        
        protected SingleItem(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            _id = Deserialize(() => Id,info);
            _name = Deserialize(() => Name, info);
            _hue = Deserialize(() => Hue, info);
            _x = Deserialize(() => X, info);
            _y = Deserialize(() => Y, info);
            _z = Deserialize(() => Z, info);
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(()=>Id,info);
            Serialize(()=>Name,info);
            Serialize(()=>Hue,info);
            Serialize(()=>X,info);
            Serialize(()=>Y,info);
            Serialize(()=>Z,info);
        }


        #endregion //Serialization
    }
}
