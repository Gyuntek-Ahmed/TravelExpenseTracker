using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Shared.DTOs
{
    public class ExpenseCategoryDto
    {
        public int Id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "Името на категорията не може да е по-дълго от 50 символа.")]
        public string Name { get; set; } = null!;

        public int? UserId { get; set; }
    }
}
