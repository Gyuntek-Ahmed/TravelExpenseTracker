using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.APIs;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly ITripsApi _tripsApi;

        public HomeViewModel(ITripsApi tripsApi)
        {
            _tripsApi = tripsApi;
        }
        [ObservableProperty]
        private TripListDto[] _trips = [];

        public async Task FetchTripsAsync()
        {
            await MakeApiCall(async () =>
            {
                Trips = await _tripsApi.GetUserTripsAsync(count:6);
            });
        }

        [RelayCommand]
        private async Task AddTripAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(SaveTripPage)}");
        }

        [RelayCommand]
        private async Task GoToTripDetailsPageAsync(int tripId)
        {
            var parameter = new Dictionary<string, object>
            {
                { nameof(TripDetailsViewModel.TripId), tripId }
            };

            await Shell.Current.GoToAsync(nameof(TripDetailsPage), parameter);
        }
    }
}
