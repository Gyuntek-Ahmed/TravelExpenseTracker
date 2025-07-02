using Microsoft.EntityFrameworkCore;
using TravelExpenseTracker.Api.Data.Entities;

namespace TravelExpenseTracker.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ExpenseCategory> ExpenseCategories { get; set; } = null!;
        public DbSet<Trip> Trips { get; set; } = null!;
        public DbSet<TripExpense> TripExpenses { get; set; } = null!;
        public DbSet<TripCategory> TripCategories { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TripCategory>()
                .HasData(TripCategory.GetSeedData());

            modelBuilder.Entity<ExpenseCategory>()
                .HasData(ExpenseCategory.GetSeedData());
        }
    }
}
