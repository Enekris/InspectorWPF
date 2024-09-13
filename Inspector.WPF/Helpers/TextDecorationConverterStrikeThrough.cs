using System.Globalization;
using System.Windows.Data;

namespace Inspector.Helpers
{
    internal class TextDecorationConverterStrikeThrough : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Length > 1
                ? Binding.DoNothing
                : values[0] is bool destructionMark && destructionMark == true ? TextDecorations.Strikethrough : Binding.DoNothing;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

