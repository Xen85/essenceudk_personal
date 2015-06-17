using System;
using EssenceUDK.MapMaker.Elements.BaseTypes;

namespace EssenceUDK.MapMaker.Elements.Items.ItemCoast
{
    [Serializable]
    public class AreaTransitionItemCoast : NotificationObject
    {
        private string _name;
        private TransitionItemsCoast _coast;
        private TransitionItemsCoast _ground;

        public String Name { get { return _name; } set { _name = value; RaisePropertyChanged(()=>Name); } }
        public TransitionItemsCoast Coast { get { return _coast; } set { _coast = value; RaisePropertyChanged(()=>Coast); } }
        public TransitionItemsCoast Ground { get { return _ground; } set { _ground = value; RaisePropertyChanged(() => Ground); } }

        public AreaTransitionItemCoast()
        {
            Coast = new TransitionItemsCoast();
            Ground = new TransitionItemsCoast();
            Name = "";
        }


        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Serialize(()=>Name,info);
            Serialize(()=>Coast,info);
            Serialize(()=>Ground,info);
        }

        protected AreaTransitionItemCoast(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Name = (string) Deserialize(() => Name,info);
            Coast = (TransitionItemsCoast) Deserialize(() => Coast, info);
            Ground = (TransitionItemsCoast) Deserialize(() => Ground, info);
        }
    }
}
