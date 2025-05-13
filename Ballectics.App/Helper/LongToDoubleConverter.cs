using System.Globalization;

namespace Ballectics.App.Helper;

public class LongToDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        System.Convert.ToDouble(value);

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        System.Convert.ToInt64(value);
}
