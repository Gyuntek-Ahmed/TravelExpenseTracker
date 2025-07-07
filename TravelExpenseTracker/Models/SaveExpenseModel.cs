using CommunityToolkit.Mvvm.ComponentModel;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.Models
{
    public partial class SaveExpenseModel : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private decimal _amount;

        [ObservableProperty]
        private DateTime _spentOn = DateTime.UtcNow;

        [ObservableProperty]
        private int _categoryId;

        [ObservableProperty]
        private string _selectedCategoryName;

        public (bool IsValid, string? Error) Validate()
        {
            string? error = null;
            if (CategoryId == 0)
                error = "Моля, изберете категория.";
            else if (string.IsNullOrWhiteSpace(Title))
                error = "Моля, въведете заглавие на пътуването.";
            else if (Amount <= 0)
                error = "Моля, въведете валидна сума.";
            else if (SpentOn < DateTime.UtcNow.AddYears(-100) || SpentOn > DateTime.UtcNow)
                error = "Моля, въведете валидна дата на разхода.";

            return (error == null, error);
        }

        public ExpenseDto ToDto()
            => new ExpenseDto
            {
                Id = Id,
                CategoryId = CategoryId,
                Title = Title,
                Amount = Amount,
                SpentOn = SpentOn
            };
    }
}
