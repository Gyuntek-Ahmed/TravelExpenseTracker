using System.Globalization;
using TravelExpenseTracker.Shared;

namespace TravelExpenseTracker.Converters
{
    public class TripStatusToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is string status)
            {
                var statusColor = status switch
                {
                    nameof(TripStatus.Планирано) => Colors.DarkGray,
                    nameof(TripStatus.Обработка) => Colors.Blue,
                    nameof(TripStatus.Завършено) => Colors.Green,
                    nameof(TripStatus.Отменено) => Colors.Red,
                    _ => Colors.Transparent // Default case for unrecognized status
                };
                return statusColor;
            }
            return Colors.Transparent; // Default color if status is not recognized
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
