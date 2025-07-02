using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelExpenseTracker.Api.Data.Entities
{
    public class TripExpense
    {
        [Key]
        public int Id { get; set; }

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; } = null!;

        public int ExpenseCategoryId { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Title { get; set; } = null!;

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Amount { get; set; }

        public DateTime SpentOn { get; set; }
    }
}
