using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.APIs;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.ViewModels
{
    [QueryProperty(nameof(TripId), nameof(TripId))]
    public partial class SaveExpenseViewModel : BaseViewModel
    {
        private IExpensesApi _expensesApi;

        public SaveExpenseViewModel(IExpensesApi expensesApi)
        {
            _expensesApi = expensesApi;
        }

        [ObservableProperty]
        private int _tripId;

        [ObservableProperty]
        private SaveExpenseModel _model = new();

        [ObservableProperty]
        private string[] _categoryNames = [];

        private ExpenseCategoryDto[] _categories = [];

        public async Task FetchCategoriesAsync()
        {
            await MakeApiCall(async () =>
            {
                _categories = await _expensesApi.GetExpenseCategoriesAsync();

                CategoryNames = [.. _categories.Select(c => c.Name)];
            });
        }

        [RelayCommand]
        private async Task GoBackAsync()
            => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        private async Task SaveExpenseAsync()
        {
            Model.CategoryId = _categories.FirstOrDefault(c => c.Name == Model.SelectedCategoryName)?.Id ?? 0;

            var (isValid, error) = Model.Validate();
            if (!isValid)
            {
                await ErrorAlertAsync(error!);
                return;
            }

            await MakeApiCall(async () =>
            {
                var result = await _expensesApi.SaveTripExpenseAsync(TripId, Model.ToDto());
                if (result.IsSuccess)
                {
                    Model = new();
                    await ToastAsync("Разходът е запазен успешно.");

                    var parameter = new Dictionary<string, object>
                    {
                        { nameof(TripDetailsViewModel.ExpenseSaved), true }
                    };
                    await Shell.Current.GoToAsync("..", parameter);
                }
                else
                {
                    await ErrorAlertAsync(result.Error!);
                    return;
                }
            });
        }
    }
}
