using System;
using System.Windows.Data;
using EssenceUDK.Platform.DataTypes;

namespace EssenceUDK.CommonWPFLibrary.Converteres
{

    public class ConverterImageSourceItemsTextureFromISourface : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var surface = value as ILandTile;
            return surface?.Texture?.GetSurface().Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}