namespace TravelExpenseTracker.Shared.DTOs
{
    public record TripListDto(
        int Id,
        string Title,
        string Image,
        string Status,
        string Location,
        DateTime StartDate,
        DateTime EndDate,
        int CategoryId
    )
    {
        public string DisplayDateRange => $"{StartDate:dd/MM/yyyy} - {EndDate:dd/MM/yyyy}";

        public static TripListDto Empty()
            => new(default, string.Empty, string.Empty, string.Empty, string.Empty, default, default, default);
    }
}
