using System;
using EssenceUDK.MapMaker.Elements.BaseTypes;

namespace EssenceUDK.MapMaker.Elements.ColorArea.ColorMountains
{
    [Serializable]
    public class CircleMountain : NotificationObject
    {
        private int _from, _to;

        public int From { get { return _from; } set { _from = value; RaisePropertyChanged(()=>From); } }
        public int To { get { return _to; } set { _to = value; RaisePropertyChanged(()=>To); } }

        public CircleMountain()
        {
            From = 0;
            To = 0;
        }

        public override string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "From":
                        {
                            if(_from >127 || _from < -128)
                            {
                                return "From MUST be between -128 and 127";
                            }
                        }
                        break;
                    case "To":
                        {
                            if (_to > 127 || _to < -128)
                            {
                                return "To MUST be between -128 and 127";
                            }
                        }
                        break;
                }
                return null;
            }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(()=>From,info);
            Serialize(()=>To,info);
        }

        protected CircleMountain(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            From = Deserialize(() => From, info);
            To = Deserialize(() => To, info);
        }
    }
}
