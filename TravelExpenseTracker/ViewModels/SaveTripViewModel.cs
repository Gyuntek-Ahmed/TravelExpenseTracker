using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.APIs;
using TravelExpenseTracker.Models;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Shared;

namespace TravelExpenseTracker.ViewModels
{
    [QueryProperty(nameof(TripId), nameof(TripId))]
    public partial class SaveTripViewModel : BaseViewModel
    {
        private readonly ITripsApi _tripsApi;

        public SaveTripViewModel(ITripsApi tripsApi)
        {
            _tripsApi = tripsApi;
            FatchCategoriesAsyn();
        }

        [ObservableProperty]
        private int _tripId;

        [ObservableProperty]
        private string _pageTitle = "Добавяне на пътуване";

        async partial void OnTripIdChanged(int value)
        {
            if (value > 0)
            {
                PageTitle = "Редактиране на пътуване";
                await MakeApiCall(async () =>
                {
                    var result = await _tripsApi.GetTripDetailsAsync(value, includeExpenses: false);

                    if (!result.IsSuccess)
                    {
                        await ErrorAlertAsync(result.Error!);
                        return;
                    }

                    var tripDto = result.Data;
                    Model = SaveTripModel.FromDto(tripDto.TripInfo);

                    var selectedCategory = Categories.FirstOrDefault(c => c.Id == Model.CategoryId);
                    if (selectedCategory != null)
                        SetSelectedCategory(selectedCategory);
                });
            }
        }

        [ObservableProperty]
        private CategoryModel[] _categories = [];

        public async Task FatchCategoriesAsyn()
        {
            await MakeApiCall(async () =>
            {
                var categoriesFromApi = await _tripsApi.GetCategoriesAsync();
                Categories = [.. categoriesFromApi.Select(c => new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Image = c.Image,
                    IsSelected = false
                })];
            });
        }

        public string[] Statuses { get; set; } =
        [
            nameof(TripStatus.Планирано),
            nameof(TripStatus.Обработка),
            nameof(TripStatus.Завършено),
            nameof(TripStatus.Отменено)
        ];

        //public SaveTripModel Model { get; set; } = new();
        [ObservableProperty]
        private SaveTripModel _model = new();

        [RelayCommand]
        private void SetSelectedCategory(CategoryModel category)
        {
            if (category.IsSelected)
                return;

            var existingSelectedCategory = Categories.FirstOrDefault(c => c.IsSelected);

            if (existingSelectedCategory != null)
                existingSelectedCategory.IsSelected = false;

            category.IsSelected = true;

            Model.CategoryId = category.Id;
        }

        [RelayCommand]
        private async Task SaveTripAsync()
        {
            var (isValid, error) = Model.Validate();

            if (!isValid)
            {
                await ErrorAlertAsync(error!);
                return;
            }

            await MakeApiCall(async () =>
            {
                var result = await _tripsApi.SaveTripAsync(Model.ToDto());
                if (result.IsSuccess)
                {
                    Model = new();
                    if (TripId == 0)
                    {
                        await ToastAsync("Пътуването е успешно запазено.");
                        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                    }
                    else
                    {
                        await ToastAsync("Пътуването е успешно обновено.");

                        var parameter = new Dictionary<string, object>
                        {
                            { nameof(TripDetailsViewModel.TripUpdated), true }
                        };
                        await Shell.Current.GoToAsync("..", parameter);
                    }
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
