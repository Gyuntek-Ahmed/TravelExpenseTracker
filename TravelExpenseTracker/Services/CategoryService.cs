using TravelExpenseTracker.Models;

namespace TravelExpenseTracker.Services
{
    public class CategoryService
    {
        public static CategoryModel[] Categories { get; private set; } = [];

        //static CategoryService()
        //{
        //    Categories =
        //    [

        //        new( 1, "Плаж", "beach.png" ),
        //        new( 2, "Град", "city.jpg" ),
        //        new( 3, "Хълмове", "hills.jpg" ),
        //        new( 4, "Планини", "mountain.jpg" ),
        //        new( 5, "Остров", "island.jpg" ),
        //        new( 6, "Религиозен", "religious.jpeg" ),
        //        new( 7, "Пътуване с кола", "road.jpg" ),
        //        new( 8, "Дива природа", "wild.jpg" ),
        //        new( 9, "Други", "logo.png" ),
        //    ];
        //}
    }
}
