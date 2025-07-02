namespace TravelExpenseTracker.Shared.DTOs
{
    public record TripDetailsDto(TripListDto TripInfo, ExpenseListDto[] Expenses);
}
