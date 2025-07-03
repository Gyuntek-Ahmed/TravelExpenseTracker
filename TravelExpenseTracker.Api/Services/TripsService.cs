using Microsoft.EntityFrameworkCore;
using TravelExpenseTracker.Api.Data;
using TravelExpenseTracker.Api.Data.Entities;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.Api.Services
{
    public class TripsService
    {
        private readonly DataContext _context;

        public TripsService(DataContext context)
        {
            _context = context;
        }

        public async Task<CategoryDto[]> GetCategoriesAsync()
            => await _context.TripCategories
                .AsNoTracking()
                .Select(c => new CategoryDto(c.Id, c.Name, c.Image!))
                .ToArrayAsync();

        public async Task<ApiResult> SaveTripAsync(SaveTripDto dto, int userId)
        {
            try
            {
                Trip? trip = null;
                if (dto.Id == 0)
                {
                    trip = new Trip
                    {
                        UserId = userId,
                        CategoryId = dto.CategoryId,
                        Title = dto.Title,
                        Location = dto.Location,
                        StartDate = dto.StartDate,
                        EndDate = dto.EndDate,
                        Status = dto.Status,
                    };

                    _context.Trips.Add(trip);
                }
                else
                {
                    trip = await _context.Trips.FindAsync(dto.Id);

                    if (trip is null)
                        return ApiResult.Fail("Пътуването не съществува.");
                    if (trip.UserId != userId)
                        return ApiResult.Fail("Нямате права да редактирате това пътуване.");

                    trip.CategoryId = dto.CategoryId;
                    trip.Title = dto.Title;
                    trip.Location = dto.Location;
                    trip.StartDate = dto.StartDate;
                    trip.EndDate = dto.EndDate;
                    trip.Status = dto.Status;

                    _context.Trips.Update(trip);
                }

                await _context.SaveChangesAsync();
                return ApiResult.Success();
            }
            catch (Exception ex)
            {
                return ApiResult.Fail(ex.Message);
            }
        }

        public async Task<TripListDto[]> GetUserTripsAsync(int userId, int count = 100)
            => await _context
            .Trips
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .OrderBy(t => t.StartDate)
            .Take(count)
            .Select(t => new TripListDto(
                t.Id,
                t.Title,
                t.Category.Image!,
                t.Status,
                t.Location,
                t.StartDate,
                t.EndDate
            )).ToArrayAsync();

        public async Task<ApiResult<TripDetailsDto>> GetTripDetailsAsync(int tripId, int userId)
        {
            var tripInfo = await _context.Trips
                .AsNoTracking()
                .Where(t => t.Id == tripId && t.UserId == userId)
                .Select(t => new TripListDto(t.Id, t.Title, t.Category.Image!, t.Status, t.Location, t.StartDate, t.EndDate))
                .FirstOrDefaultAsync();

            if (tripInfo is null)
                return ApiResult<TripDetailsDto>.Fail("Пътуването не съществува или нямате права да го видите.");

            var expenses = await _context.TripExpenses
                .AsNoTracking()
                .Where(e => e.TripId == tripId)
                .Select(e => new ExpenseListDto
                (
                    e.Id,
                    e.Title,
                    e.ExpenseCategory.Name,
                    e.Amount,
                    e.SpentOn
                ))
                .ToArrayAsync();

            return ApiResult<TripDetailsDto>.Success(new TripDetailsDto(tripInfo, expenses));
        }
    }
}
