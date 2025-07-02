using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker.Api.Data.Entities
{
    public class TripCategory
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(250)]
        public string? Image { get; set; } = null!;

        public static TripCategory Create(int id, string name, string? image) =>
            new()
            {
                Id = id,
                Name = name,
                Image = image
            };

        public static TripCategory[] GetSeedData() =>
            [
                Create( 1, "Плаж", "beach.png" ),
                Create( 2, "Град", "city.jpg" ),
                Create( 3, "Хълмове", "hills.jpg" ),
                Create( 4, "Планини", "mountain.jpg" ),
                Create( 5, "Остров", "island.jpg" ),
                Create( 6, "Религиозен", "religious.jpeg" ),
                Create( 7, "Пътуване с кола", "road.jpg" ),
                Create( 8, "Дива природа", "wild.jpg" ),
                Create( 9, "Други", "logo.png" ),
            ];
    }
}
