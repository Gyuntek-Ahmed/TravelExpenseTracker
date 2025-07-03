using Microsoft.AspNetCore.Mvc;
using TravelExpenseTracker.Api.Services;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.Api.Controllers
{
    [Route("api/me")]
    [ApiController]
    public class ProfileController : AppBaseController
    {
        private readonly ProfileService _profileService;
        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("update-name")] // URL: POST /api/me/update-name
        public async Task<ApiResult<string>> UpdateName(SingleValueDto<string> dto)
        {
            var newName = dto.Value;
            if (string.IsNullOrWhiteSpace(newName))
                return ApiResult<string>.Fail("Името не може да бъде празно.");

            return await _profileService.UpdateNameAsync(newName, UserId);
        }

        [HttpPost("change-password")] // URL: POST /api/me/change-password
        public async Task<ApiResult> ChangePassword(SingleValueDto<string> dto)
        {
            var newPass = dto.Value;
            if (string.IsNullOrWhiteSpace(newPass))
                return ApiResult.Fail("Паролата не може да бъде празна.");

            return await _profileService.ChangePasswordAsync(newPass, UserId);
        }
    }
}
