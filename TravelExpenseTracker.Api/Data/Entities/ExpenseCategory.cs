using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Api.Data.Entities
{
    public class ExpenseCategory
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// UserId null or 0 will be for system expense categories
        /// or we can call them global categories
        /// visible to all users.
        /// </summary>
        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        public static ExpenseCategory Create(int id, string name)
            => new() { Id = id, Name = name };

        public static ExpenseCategory[] GetSeedData() =>
            [
                Create(1, "Билети"),
                Create(2, "Пазаруване"),
                Create(3, "Храни"),
                Create(4, "Горива"),
                Create(5, "Други")
            ];
    }
}
