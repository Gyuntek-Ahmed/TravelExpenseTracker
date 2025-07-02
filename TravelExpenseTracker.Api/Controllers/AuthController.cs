using Microsoft.AspNetCore.Mvc;
using TravelExpenseTracker.Api.Services;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")] // URL /api/auth/register
        public async Task<ApiResult> Register(RegisterDto dto)
            => await _authService.RegisterAsync(dto);

        [HttpPost("login")] // URL /api/auth/login
        public async Task<ApiResult<string>> Login(LoginDto dto)
            => await _authService.LoginAsync(dto);
    }
}
