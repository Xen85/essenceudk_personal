using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using EssenceUDK.Platform;
using EssenceUDK.Platform.DataTypes;

namespace MapMakerApplication.Converters
{
    public class ConverterImageSourceItemsLandsFromISourface : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var surface = value as ISurface;
            if (surface != null)
            {
                var image = surface.GetSurface().Image;
                return image;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterImageSourceTextureFromInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (ApplicationController.manager == null) return null;
            var item = ApplicationController.manager.GetLandTile((int) value);
            if (item == null)
                return null;
            var texture = item.Texture;
            return texture == null ? null : texture.GetSurface().Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterImageSourceItemFromInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (ApplicationController.manager == null) return null;
            var item = ApplicationController.manager.GetItemTile((int)value);
            if (item == null)
                return null;
            var image = item.Surface;
            return image == null ? null : image.GetSurface().Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterImageSourceLandFromInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (ApplicationController.manager == null) return null;
            var land = ApplicationController.manager.GetLandTile((int)value);
            if (land == null)
                return null;
            var image = land.Surface;
            return image == null ? null : image.GetSurface().Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterInvertVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var visibility = (Visibility)value;

            switch (visibility)
            {
                case Visibility.Visible:
                    return Visibility.Hidden;
                case Visibility.Hidden:
                    return Visibility.Visible;
                case Visibility.Collapsed:
                    return Visibility.Visible;
                default:
                    return visibility;
            }
            

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
