using System.ComponentModel.DataAnnotations;
using TravelExpenseTracker.Shared;

namespace TravelExpenseTracker.Api.Data.Entities
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int CategoryId { get; set; }
        public virtual TripCategory Category { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Location { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; } = nameof(TripStatus.Планирано);

        public virtual ICollection<TripExpense> Expenses { get; set; } = [];
    }
}
