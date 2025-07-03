using Refit;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.APIs
{
    [Headers("Authorization: Bearer ")]
    public interface IProfileApi
    {
        [Post("/api/me/update-name")]
        Task<ApiResult<string>> UpdateNameAsync(SingleValueDto<string> dto);

        [Post("/api/me/change-password")]
        Task<ApiResult> ChangePasswordAsync(SingleValueDto<string> dto);
    }
}
