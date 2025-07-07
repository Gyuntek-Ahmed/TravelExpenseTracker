using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.APIs;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.ViewModels
{
    [QueryProperty(nameof(TripId), nameof(TripId))]
    [QueryProperty(nameof(ExpenseSaved), nameof(ExpenseSaved))]
    [QueryProperty(nameof(TripUpdated), nameof(TripUpdated))]
    public partial class TripDetailsViewModel : BaseViewModel
    {
        private readonly ITripsApi _tripsApi;
        private readonly IExpensesApi _expensesApi;

        public TripDetailsViewModel(ITripsApi tripsApi, IExpensesApi expensesApi)
        {
            _tripsApi = tripsApi;
            _expensesApi = expensesApi;
        }

        [ObservableProperty]
        private TripListDto _tripInfo = TripListDto.Empty();

        [ObservableProperty]
        private int _tripId;

        [ObservableProperty]
        private bool _expenseSaved;

        [ObservableProperty]
        private bool _tripUpdated;

        async partial void OnTripUpdatedChanged(bool value)
        {
            if(value)
            {
                await FetchTripDetailsAsync();
                TripUpdated = false;
            }
        }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(TotalExpenses))]
        private ExpenseListDto[] _expenses = [];

        public decimal TotalExpenses => Expenses.Sum(e => e.Amount);

        async partial void OnTripIdChanged(int value)
        {
            await FetchTripDetailsAsync();
        }
        
        async partial void OnExpenseSavedChanged(bool value)
        {
            // This method is called when ExpenseSaved is set from the query parameter.
            if (value)
            {
                await MakeApiCall(async () =>
                {
                    Expenses = await _expensesApi.GetTripExpensesAsync(TripId);
                    ExpenseSaved = false; // Reset the flag to false after handling it
                });
            }

        }

        [RelayCommand]
        private async Task AddExpenseAsync()
        {
            var parameter = new Dictionary<string, object>
            {
                { nameof(SaveExpenseViewModel.TripId), TripId }
            };

            await Shell.Current.GoToAsync(nameof(SaveExpensePage), parameter);
        }

        [RelayCommand]
        private async Task EditTripAsync()
        {
            var parameter = new Dictionary<string, object>
            {
                { nameof(SaveTripViewModel.TripId), TripId }
            };
            await Shell.Current.GoToAsync(nameof(SaveTripPage), parameter);
        }

        private async Task FetchTripDetailsAsync()
        {
            // This method is called when TripId is set from the query parameter.
            // You can use this to fetch trip details from an API or database if needed.
            await MakeApiCall(async () =>
            {
                var result = await _tripsApi.GetTripDetailsAsync(TripId);

                if (!result.IsSuccess)
                {
                    await ErrorAlertAsync(result.Error!);
                    return;
                }

                (TripInfo, Expenses) = result.Data;
            });
        }
    }
}
