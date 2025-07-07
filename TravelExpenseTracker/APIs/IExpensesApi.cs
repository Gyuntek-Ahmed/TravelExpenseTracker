using Refit;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.APIs
{
    [Headers("Authorization: Bearer ")]
    public interface IExpensesApi
    {
        [Get("/api/trips/expenses/categories")]
        Task<ExpenseCategoryDto[]> GetExpenseCategoriesAsync();

        [Post("/api/trips/expenses/categories")]
        Task<ApiResult> SaveExpenseCategoryAsync(ExpenseCategoryDto dto);

        [Post("/api/trips/expenses/trip/{tripId}/save")]
        Task<ApiResult> SaveTripExpenseAsync(int tripId, ExpenseDto dto);

        [Get("/api/trips/expenses/of-trip/{tripId}")]
        Task<ExpenseListDto[]> GetTripExpensesAsync(int tripId);
    }
}
