using System.Globalization;

namespace MauiStoreApp.Converters
{
    /// <summary>
    /// Converts a boolean value to a password icon.
    /// </summary>
    public class BoolToPasswordIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "eye_off.svg" : "eye_show.svg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
