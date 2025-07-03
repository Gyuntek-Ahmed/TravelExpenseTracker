using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelExpenseTracker.Api.Data;
using TravelExpenseTracker.Api.Data.Entities;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.Api.Services
{
    public class ProfileService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtService _jwtService;

        public ProfileService(DataContext context, IPasswordHasher<User> passwordHasher, JwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<ApiResult<string>> UpdateNameAsync(string name, int userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user is null)
                    return ApiResult<string>.Fail("Потребителят не е намерен.");
                if (string.IsNullOrWhiteSpace(name))
                    return ApiResult<string>.Fail("Името не може да бъде празно.");

                user.Name = name;
                await _context.SaveChangesAsync();
                var token = _jwtService.GenerateJwtToken(user);
                return ApiResult<string>.Success(token);
            }
            catch (Exception ex)
            {
                return ApiResult<string>.Fail(ex.Message);
            }
        }

        public async Task<ApiResult> ChangePasswordAsync(string newPassword, int userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user is null)
                    return ApiResult.Fail("Потребителят не е намерен.");

                user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
                await _context.SaveChangesAsync();
                return ApiResult.Success();
            }
            catch (Exception ex)
            {
                return ApiResult.Fail(ex.Message);
            }
        }
    }
}
