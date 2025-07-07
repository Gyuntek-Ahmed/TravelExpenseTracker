using Refit;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.APIs
{
    [Headers("Authorization: Bearer ")]
    public interface ITripsApi
    {
        [Get("/api/trips/categories")]
        Task<CategoryDto[]> GetCategoriesAsync();

        [Post("/api/trips")]
        Task<ApiResult> SaveTripAsync(SaveTripDto dto);

        [Get("/api/trips")]
        Task<TripListDto[]> GetUserTripsAsync(int count = 100);

        [Get("/api/trips/{tripId}")]
        Task<ApiResult<TripDetailsDto>> GetTripDetailsAsync(int tripId, bool includeExpenses = true);
    }
}
