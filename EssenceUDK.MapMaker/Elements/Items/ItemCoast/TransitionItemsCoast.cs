using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes;
using System;
using System.Windows.Media;

namespace EssenceUDK.MapMaker.Elements.Items.ItemCoast
{
    [Serializable]
    public class TransitionItemsCoast : Transition
    {
        #region Fields

        private Color _color;

        private int _texture;
        private int _hue;

        #endregion Fields

        #region Props

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                RaisePropertyChanged(() => Color);
            }
        }

        public int Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                RaisePropertyChanged(() => Texture);
            }
        }

        public int Hue { get { return _hue; } set { _hue = value; RaisePropertyChanged(() => Hue); } }

        #endregion Props

        #region Ctor

        public TransitionItemsCoast()
            : base()
        {
            Color = Colors.Black;
            Texture = 0;
        }

        #endregion Ctor

        #region Serialization

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            Serialize(() => Color, info);
            Serialize(() => Texture, info);
            Serialize(() => Hue, info);
        }

        protected TransitionItemsCoast(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            Color = Deserialize(() => Color, info);
            Texture = (int)Deserialize(() => Texture, info);
            try
            {
                Hue = (int)Deserialize(() => Hue, info);
            }
            catch (Exception)
            {
            }
        }

        #endregion Serialization
    }
}