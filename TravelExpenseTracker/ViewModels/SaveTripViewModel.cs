using CommunityToolkit.Mvvm.ComponentModel;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Services;

namespace TravelExpenseTracker.ViewModels
{
    public partial class SaveTripViewModel : ObservableObject
    {
        public CategoryModel[] Categories { get; set; } = CategoryService.Categories;

        public string[] Statuses { get; set; } =
        [
            "Планирано",
            "В процес",
            "Завършено",
            "Отменено"
        ];
    }
}
