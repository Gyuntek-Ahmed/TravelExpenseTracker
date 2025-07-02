using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelExpenseTracker.Api.Controllers
{
    [Route("api/trips")]
    [ApiController]
    [Authorize]
    public class TripsController : ControllerBase
    {
        private readonly 
    }
}
