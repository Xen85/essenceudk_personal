using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using EssenceUDK.Platform;
using EssenceUDK.Platform.DataTypes;
using EssenceUDKMVVM.Models.Model;
using EssenceUDKMVVM.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace EssenceUDKMVVM.Converters
{
    public class ConveterRenderModelToImage : IValueConverter
    {
        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <returns>
        ///     A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var renderModel = (RenderModel) value;

            if (renderModel == null) return null;


            var datamanager = ServiceLocator.Current.GetInstance<ViewModelLocator>().UODataManager.UoDataManager;

            if (datamanager == null) return null;

            var surf = datamanager.CreateSurface(renderModel.Width, renderModel.Height, PixelFormat.Bpp16X1R5G5B5);

            if (renderModel.Flat)
                datamanager.FacetRender.DrawFlatMap(renderModel.Map, renderModel.SeaLevel, ref surf,
                    renderModel.Range, renderModel.X, renderModel.Y, renderModel.MinZ, renderModel.MaxZ);
            else
            {
                FakeTypes.MicroMapFake fake = new FakeTypes.MicroMapFake();

                datamanager.FacetRender.DrawObliqueMapBlock2(fake, renderModel.SeaLevel, ref surf, 6, 0, 0, 0,
                    30);

                // datamanager.FacetRender.DrawObliqueMap(renderModel.Map, renderModel.SeaLevel, ref surf,
                //     renderModel.Range, renderModel.X, renderModel.Y, renderModel.MinZ, renderModel.MaxZ);
                //
            }

            return surf?.GetSurface().Image;
        }

        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <returns>
        ///     A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterImageSourceItemsLandsFromISourface : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var surface = value as ISurface;
            if (surface == null) return null;
            var image = surface.GetSurface().Image;
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class ConverterImageSourceItemsTextureFromISourface : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var surface = value as ILandTile;
            if (surface != null && surface.Texture != null) return surface.Texture.GetSurface().Image;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterImageSourceTextureFromInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var datamanager = ServiceLocator.Current.GetInstance<ViewModelLocator>().UODataManager.UoDataManager;

            if (datamanager == null) return null;
            var item = datamanager.GetLandTile((int) value);
            if (item == null)
                return null;
            var texture = item.Texture;
            return texture == null ? null : texture.GetSurface().Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterImageSourceItemFromInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var datamanager = ServiceLocator.Current.GetInstance<ViewModelLocator>().UODataManager.UoDataManager;

            var item = datamanager?.GetItemTile((int) value);
            if (item == null)
                return null;
            var image = item.Surface;
            return image?.GetSurface().Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterImageSourceLandFromInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var datamanager = ServiceLocator.Current.GetInstance<ViewModelLocator>().UODataManager.UoDataManager;

            var land = datamanager?.GetLandTile((int) value);
            var image = land?.Surface;
            return image?.GetSurface().Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterInvertVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility) value;

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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}