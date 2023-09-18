using System.Globalization;

namespace MauiStoreApp.Converters
{
    public class CategoryToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string categoryName = value as string;

            if (categoryName == null)
            {
                return Color.FromArgb("#A8D5BA");
            }

            // Map category names to specific pastel colors
            return categoryName switch
            {
                "electronics" => Color.FromArgb("#A8D5BA"),
                "jewelery" => Color.FromArgb("#FFC3A0"),
                "men's clothing" => Color.FromArgb("#ACC7E2"),
                "women's clothing" => Color.FromArgb("#D5A6E2"),
                _ => Color.FromArgb("#A8D5BA"), // default
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
