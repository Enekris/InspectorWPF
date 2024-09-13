using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Inspector.Helpers
{
    internal class TextSolidColorBrush : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Length > 1
                ? Binding.DoNothing
                : values[0] is bool destructionMark && destructionMark == true
                    ? new SolidColorBrush(Color.FromScRgb(0.5f, 1.0f, 0.5f, 0.05f))
                    : Binding.DoNothing;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

