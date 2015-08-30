using EssenceUDK.Platform;
using EssenceUDKMVVM.Controls.Tiles;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EssenceUDKMVVM.Converters
{
    public class MultiItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
               object parameter, CultureInfo culture)
        {
            if (values.Length < 3) return null;
            int id;
            if (values[0] is int)
                id = (int)values[0];
            else
                return null;
            var manager = values[1] as UODataManager;
            if (manager == null)
                return null;
            if (!(values[2] is ImageType))
                return null;
            var imagetype = (ImageType)values[2];
            ISurface item = null;
            IImageSurface texture = null;
            switch (imagetype)
            {
                case ImageType.Item:
                    {
                        item = manager.GetItemTile(id).Surface;
                        if (item == null) return null;
                        texture = item.GetSurface();
                    }
                    break;

                case ImageType.LandTexture:
                    {
                        item = manager.GetLandTile(id).Texture;
                        if (item == null) return null;
                        texture = item.GetSurface();
                    }
                    break;

                case ImageType.LandTile:
                    {
                        item = manager.GetLandTile(id).Surface;
                        if (item == null) return null;
                        texture = item.GetSurface();
                    }
                    break;
            }

            if (item == null)
                return null;

            return texture == null ? null : texture.Image;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
               object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}