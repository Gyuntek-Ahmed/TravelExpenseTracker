using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Shared.DTOs
{
    public class RegisterDto
    {
        [Required, MaxLength(30)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required, MaxLength(30)]
        public string Password { get; set; } = null!;
    }
}
