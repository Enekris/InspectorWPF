using System.Globalization;
using System.Windows.Data;

namespace Inspector.Helpers
{
    public class HalfPageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is double width ? (width / 2.0) - 60.0 : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
