namespace TravelExpenseTracker.Models;

public record ExpenseModel(int ID, string Title, string Category, decimal Amount, DateTime SpendOn);