using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Models;

namespace TravelExpenseTracker.ViewModels
{
    public partial class ManageCategoriesViewModel : ObservableObject
    {
        public ObservableCollection<ManageCategoryModel> Categories { get; set; } = [];

        public ManageCategoriesViewModel()
        {
            ManageCategoryModel[] tempExpenseCategories = [
                new (1, "Билети"),
                new (2, "Пазаруване"),
                new (3, "Храни"),
                new (4, "Горива"),
                new (5, "Други")
            ];

            foreach (var category in tempExpenseCategories)
            {
                Categories.Add(category);
            }
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
                var newCategory = new ManageCategoryModel(Categories.Count + 1, newCategoryName);
                Categories.Add(newCategory);
                return;
            }

            await Toast.Make("Моля, въведете валидно име на категория.", ToastDuration.Short).Show();
        }
    }
}
