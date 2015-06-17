using System;
using System.Windows;
using System.Windows.Data;
using EssenceUDK.Platform;

namespace EssenceUDK.Controls.Converters
{
    public class ConverterImageSourceItemsGumpsFromISurface : IValueConverter
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

    public class ConverterImageSourceItemsLandsFromISurface : IValueConverter
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

    public class ConverterImageSourceTextureISurface : IValueConverter
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
            var manager = parameter as UODataManager;
            if (manager == null) return null;
            var item = manager.GetLandTile((int) value);
            if (item == null)
                return null;
            var texture = item.Texture;
            if (texture == null)
                return null;

            return texture.GetSurface().Image;

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
            var manager = parameter as UODataManager;
            if (manager == null) return null;
            var item = manager.GetItemTile((int)value);
            if (item == null)
                return null;
            var image = item.Surface;
            if (image == null)
                return null;

            return image.GetSurface().Image;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterImageSourceGumpFromInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var manager = parameter as UODataManager;
            if (manager == null) return null;
            var gump = manager.GetGumpSurf((int)value);
            if (gump == null)
                return null;
            var image = gump.Surface;
            if (image == null)
                return null;

            return image.GetSurface().Image;

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
            var manager = parameter as UODataManager;
            if (manager == null) return null;
            var land = manager.GetLandTile((int)value);
            if (land == null)
                return null;
            var image = land.Surface;
            if (image == null)
                return null;

            return image.GetSurface().Image;

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
