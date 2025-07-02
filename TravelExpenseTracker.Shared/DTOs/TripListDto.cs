namespace TravelExpenseTracker.Shared.DTOs
{
    public record TripListDto(
        int Id,
        string Title,
        string Image,
        string Status,
        string Location,
        DateTime StartDate,
        DateTime EndDate
    );
}
