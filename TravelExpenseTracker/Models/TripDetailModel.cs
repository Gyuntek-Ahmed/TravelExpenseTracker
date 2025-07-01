namespace TravelExpenseTracker.Models
{
    public record TripDetailModel
        (string Image, string Title, string Location, string CategoryName, string Status, DateTime StartDate, DateTime EndDate)
    {
        public string DisplayDateRange => $"{StartDate:dd-MM-yyyy} - {EndDate:dd-MM-yyyy}";
    }
}
