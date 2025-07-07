using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.APIs;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.ViewModels
{
    public partial class ManageCategoriesViewModel : BaseViewModel
    {
        private readonly IExpensesApi _expensesApi;
        public ObservableCollection<ExpenseCategoryDto> Categories { get; set; } = [];

        public ManageCategoriesViewModel(IExpensesApi expensesApi)
        {
            _expensesApi = expensesApi;
        }

        public async Task FetchCategoriesAsync()
        {
            Categories.Clear();

            await MakeApiCall(async () =>
            {
                var categories = await _expensesApi.GetExpenseCategoriesAsync();

                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            });
        }

        [RelayCommand]
        private async Task AddCategoryAsync()
        {
            var newCategoryName = await Shell.Current.DisplayPromptAsync("Добавяне на категория",
                "Въведете име на категорията",
                accept: "Добави",
                cancel: "Отказ",
                maxLength: 50,
                keyboard: Keyboard.Text
            );

            if (!string.IsNullOrWhiteSpace(newCategoryName))
            {
                await MakeApiCall(async () =>
                {
                    var dto = new ExpenseCategoryDto { Name = newCategoryName };
                    var result = await _expensesApi.SaveExpenseCategoryAsync(dto);

                    if (!result.IsSuccess)
                    {
                        await ErrorAlertAsync(result.Error ?? "Неуспешно добавяне на категория.");
                        return;
                    }

                    Categories.Add(dto);
                    await ToastAsync("Категорията е добавена успешно.");
                });
                return;
            }

            await ToastAsync("Моля, въведете валидно име на категория.");
        }
    }
}
