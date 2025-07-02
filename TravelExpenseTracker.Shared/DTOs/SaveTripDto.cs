using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Shared.DTOs
{
    public class SaveTripDto
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Моля изберете категория.")]
        public int CategoryId { get; set; }

        [Required, MaxLength(100, ErrorMessage = "Заглавието не може да е по-дълго от 100 символа.")]
        public string Title { get; set; } = null!;

        [Required, MaxLength(250, ErrorMessage = "Местоположението не може да е по-дълго от 250 символа.")]
        public string Location { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required, MaxLength(20, ErrorMessage = "Статуса не може да е по-дълго от 20 символа.")]
        public string Status { get; set; } = null!;
    }
}
