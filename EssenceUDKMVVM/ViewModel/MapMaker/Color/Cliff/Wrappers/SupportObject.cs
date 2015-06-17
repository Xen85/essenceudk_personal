using System;
using System.Collections.Generic;
using System.Linq;
using EssenceUDK.MapMaker.Elements.Textures.TexureCliff;
using GalaSoft.MvvmLight;

namespace EssenceUDKMVVM.ViewModel.MapMaker.Color.Cliff.Wrappers
{


    

    public class SupportObject : ObservableObject
    {
        private int _indexTo;
        private System.Windows.Media.Color _color;


        public SupportObject()
        {
            _cliffs = new Dictionary<DirectionCliff, AreaTransitionCliffTexture>();
           

        }



        private readonly Dictionary<DirectionCliff, AreaTransitionCliffTexture> _cliffs;

        /// <summary>
        /// Index of referred color area
        /// </summary>
        public int IndexTo
        {
            get { return _indexTo; }
            set
            {
                System.Windows.Media.Color? color = null;
                _indexTo = value;
                foreach (var element in _cliffs.Values)
                {
                    element.IdTo = value;
                    if (color == null)
                        color = element.ColorTo;
                }
                if (color != null) Color = (System.Windows.Media.Color) color;
                RaisePropertyChanged(() => IndexTo);
            }
        }

        /// <summary>
        /// referred color
        /// </summary>
        public System.Windows.Media.Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                foreach (var element in _cliffs.Values)
                {
                    element.ColorTo = value;
                }
                RaisePropertyChanged(() => Color);
            }
        }

        /// <summary>
        /// this property connects directly to the internal dictionary 
        /// </summary>
        /// <param name="direction"> direction key of the dictionary</param>
        /// <returns>collection item connected in that dictionary</returns>
        public AreaTransitionCliffTexture this[DirectionCliff direction] 
        {
            get
            {
                AreaTransitionCliffTexture result;
                _cliffs.TryGetValue(direction, out result);
                return result;
            }
            set
            {
                AreaTransitionCliffTexture result;
                _cliffs.TryGetValue(direction, out result);
                if (result == null)
                {
                    _cliffs.Add(direction, value);
                }
                else
                {
                    foreach (var item in value.List.Where(item => !result.List.Contains(item)))
                    {
                        result.List.Add(item);
                    }
                }
            }
           
        }

       


        public override string ToString()
        {
            return _indexTo + " " + _color;
        }



    }

}