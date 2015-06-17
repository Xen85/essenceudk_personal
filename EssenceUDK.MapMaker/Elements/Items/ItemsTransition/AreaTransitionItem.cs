using System;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes;
using EssenceUDK.MapMaker.Elements.Interfaces;

namespace EssenceUDK.MapMaker.Elements.Items.ItemsTransition
{
    [Serializable]
    public class AreaTransitionItem : Transition, ITransition
    {
        #region Fields

        private Color _colorFrom, _colorTo;

        private int _idTo;
        private int _textureIdTo;
        
        private string _name;

        #endregion //Fields

        #region Props

        public Color ColorFrom
        {
            get { return _colorFrom; }
            set
            {
                _colorFrom = value;
                RaisePropertyChanged(() => ColorFrom);
            }
        }

        public Color ColorTo
        {
            get { return _colorTo; }
            set
            {
                _colorTo = value;
                RaisePropertyChanged(() => ColorTo);
            }
        }

        public int IndexTo
        {
            get { return _idTo; }
            set
            {
                _idTo = value;
                ColorTo = MapSdk.Colors[value];
                RaisePropertyChanged(() => IndexTo);
            }
        }

        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int TextureIdTo { get { return _textureIdTo; } set { _textureIdTo = value;RaisePropertyChanged(()=>TextureIdTo); } }

        #endregion //Props

        #region Ctor

        public AreaTransitionItem()
            : base()
        {
            ColorFrom = Colors.Black;
            ColorTo = Colors.Black;
        }

        #endregion //Ctor

        #region Methods

        #region Serialization

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info,context);
            Serialize(() => ColorFrom,info);
            Serialize(() => ColorTo,info);
            Serialize(() => Name,info);
            Serialize(() => TextureIdTo,info);
        }

        protected AreaTransitionItem(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            :base(info,context)
        {
            ColorFrom = Deserialize(() => ColorFrom, info);
            ColorTo = Deserialize(() => ColorTo, info);
            Name=(string)Deserialize(() => Name,info);
            try
            {
                TextureIdTo = Deserialize(() => TextureIdTo, info);
            }
            catch (Exception)
            {
                TextureIdTo = -1;
            }
        }

        #endregion //Serialization

        #endregion //Methods
    }
}