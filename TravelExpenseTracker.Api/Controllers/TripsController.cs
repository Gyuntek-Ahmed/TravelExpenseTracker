using Microsoft.AspNetCore.Mvc;
using TravelExpenseTracker.Api.Services;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.Api.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : AppBaseController
    {
        private readonly TripsService _tripsService;

        public TripsController(TripsService tripsService)
        {
            _tripsService = tripsService;
        }

        [HttpGet("categories")] // URL: GET /api/trips/categories
        public async Task<CategoryDto[]> GetCategories()
            => await _tripsService.GetCategoriesAsync();

        [HttpPost("")] // URL: POST /api/trips
        public async Task<ApiResult> SaveTrip(SaveTripDto dto)
            => await _tripsService.SaveTripAsync(dto, UserId);

        [HttpGet("")] // URL: GET /api/trips
        public async Task<TripListDto[]> GetUserTrips(int count = 100)
            => await _tripsService.GetUserTripsAsync(UserId, count);

        [HttpGet("{tripId:int}")] // URL: GET /api/trips/tripId  e.g. /api/trips/5
        public async Task<ApiResult<TripDetailsDto>> GetTripDetails(int tripId, bool includeExpenses = true)
            => await _tripsService.GetTripDetailsAsync(tripId, UserId, includeExpenses);
    }
}
