using System.Globalization;

namespace Ballectics.App.Helper;

public class PhoneNumberFormatter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return string.Empty;

        string phoneNumber = value.ToString();

        return $"{phoneNumber.Substring(0, 4)}-{phoneNumber.Substring(4)}";

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
