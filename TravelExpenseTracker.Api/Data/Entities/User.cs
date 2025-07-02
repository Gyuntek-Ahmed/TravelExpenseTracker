using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Api.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MaxLength(500)]
        public string PasswordHash { get; set; } = null!;
    }
}
