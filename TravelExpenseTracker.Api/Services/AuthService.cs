using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelExpenseTracker.Api.Data;
using TravelExpenseTracker.Api.Data.Entities;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.Api.Services
{
    public class AuthService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtService _jwtService;

        public AuthService(DataContext context, IPasswordHasher<User> passwordHasher, JwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<ApiResult> RegisterAsync(RegisterDto dto)
        {
            if(await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return ApiResult.Fail("Имейлът вече съществува.");
            
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return ApiResult.Success();
        }

        public async Task<ApiResult<string>> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return ApiResult<string>.Fail("Невалиден потребител.");

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                return ApiResult<string>.Fail("Невалидена парола.");

            var token = _jwtService.GenerateJwtToken(user);
            return ApiResult<string>.Success(token);
        }
    }
}
