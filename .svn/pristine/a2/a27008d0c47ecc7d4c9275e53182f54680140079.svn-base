using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace EssenceUDK.Controls.Common
{
    public abstract class Converter<T> : MarkupExtension, IValueConverter 
    where T : class, new()
    {
        /// <summary>
        /// Must be implemented in inheritor.
        /// </summary>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Override if needed.
        /// </summary>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #region MarkupExtension members

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
                _converter = new T();
            return _converter;
        }

        private static T _converter = null;

        #endregion
    }

    // ---------------------------------------------------------------------------------------------------------
    // Some common converters ...

    public class DateTimeToString : Converter<DateTimeToString>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class EnumToBoolean : Converter<EnumToBoolean>, IValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }

    public class BoolToEnum : Converter<BoolToEnum>
    {
        private object Parse(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = parameter.ToString();
            var etype = param.Substring(0, param.LastIndexOf('.'));
            var esval = param.Substring(param.LastIndexOf('.')+1);
            switch (etype) {
                case "ChecksumType"     : etype = "EssenceUDK.Platform.UtilHelpers.ChecksumType, EssenceUDK.Platform";       break;
                case "CompressorType"   : etype = "EssenceUDK.Platform.UtilHelpers.CompressorType, EssenceUDK.Platform";     break;
            }

            System.Diagnostics.Debug.WriteLine("@@@@@ BoolToEnum : " + param);
            return Enum.Parse(Type.GetType(etype), esval);
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var paramval = Parse(value, targetType, parameter, culture);
            return value.Equals(paramval);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Parse(value, targetType, parameter, culture);
        }
    }

}
