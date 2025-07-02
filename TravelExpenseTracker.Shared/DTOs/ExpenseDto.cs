using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Shared.DTOs
{
    public class ExpenseDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100, ErrorMessage = "Заглавието не може да е по-дълго от 100 символа.")]
        public string Title { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Моля изберете категория.")]
        public int CategoryId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Моля въведете валидна сума.")]
        public decimal Amount { get; set; }

        public DateTime SpentOn { get; set; }
    }
}
