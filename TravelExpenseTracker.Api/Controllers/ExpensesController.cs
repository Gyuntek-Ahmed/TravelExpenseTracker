using Microsoft.AspNetCore.Mvc;
using TravelExpenseTracker.Api.Services;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.Api.Controllers
{
    [Route("api/trips/expenses")]
    [ApiController]
    public class ExpensesController : AppBaseController
    {
        private readonly TripExpenseService _expensesService;

        public ExpensesController(TripExpenseService expensesService)
        {
            _expensesService = expensesService;
        }

        [HttpGet("categories")] // URL: GET /api/trips/expenses/categories
        public async Task<ExpenseCategoryDto[]> GetExpenseCategories()
            => await _expensesService.GetExpenseCategoriesAsync(UserId);

        [HttpPost("categories")] // URL: POST /api/trips/expenses/categories
        public async Task<ApiResult> SaveExpenseCategory(ExpenseCategoryDto dto)
            => await _expensesService.SaveExpenseCategoryAsync(dto, UserId);

        [HttpPost("trip/{tripId:int}/save")] // URL: POST /api/trips/expenses/of-trip/{tripId}/save
        public async Task<ApiResult> SaveTripExpense(int tripId, ExpenseDto dto)
            => await _expensesService.SaveTripExpenseAsync(tripId, dto, UserId);

        [HttpGet("of-trip/{tripId:int}")] // URL: GET /api/trips/expenses/of-trip/{tripId}
        public async Task<ExpenseListDto[]> GetTripExpenses(int tripId)
            => await _expensesService.GetTripExpensesAsync(tripId, UserId);
    }
}
