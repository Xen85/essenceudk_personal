﻿using System;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes;
using EssenceUDK.MapMaker.Elements.Interfaces;

namespace EssenceUDK.MapMaker.Elements.Textures.TextureTransition
{
    [Serializable]
    public class AreaTransitionTexture : Transition, ITransition
    {
        private Color _colorFrom, _colorTo;
        private string _name;
        private int _indexTo;
        private int _textureIdTo;


        #region Props

        public Color ColorFrom { get => _colorFrom;
            set { _colorFrom = value; RaisePropertyChanged(() => ColorFrom); } }

        public Color ColorTo { get => _colorTo;
            set { _colorTo = value; RaisePropertyChanged(() => ColorTo); } }

        public string Name { get => _name;
            set { _name = value; RaisePropertyChanged(() => Name); } }

        public int IndexTo
        {
            get => _indexTo;
            set
            {
                _indexTo = value;
                ColorTo = MapSdk.Colors[value];
                RaisePropertyChanged(()=>IndexTo);
            }
        }

        public int TextureIdTo { get => _textureIdTo;
            set { _textureIdTo = value; RaisePropertyChanged(() => TextureIdTo); } }

        #endregion //Props

        #region Ctor
        public AreaTransitionTexture()
            : base()
        {
            ColorFrom = Colors.Black;
            ColorTo = Colors.Black;
            Name = "";

        }
        #endregion //Ctor

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info,context);
            Serialize(()=>Name,info);
            Serialize(()=>ColorFrom,info);
            Serialize(()=>ColorTo,info);
            Serialize(()=>IndexTo,info);
            Serialize(()=>TextureIdTo,info);
        }

        protected AreaTransitionTexture(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            :base(info,context)
        {
            Name = Deserialize(() => Name, info);
            ColorFrom = Deserialize(() => ColorFrom, info);
            ColorTo = Deserialize(() => ColorTo, info);
            _indexTo = info.GetInt32("IndexTo");

            try
            {
                TextureIdTo = Deserialize(() => TextureIdTo, info);
            }
            catch (Exception)
            {
                TextureIdTo = 0;
            }
        }

        
        

    }
}
