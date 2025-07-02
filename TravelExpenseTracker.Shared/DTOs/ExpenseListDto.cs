namespace TravelExpenseTracker.Shared.DTOs
{
    public record ExpenseListDto(
        long Id,
        string Title,
        string Category,
        decimal Amount,
        DateTime SpentOn
    );
}
