using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Models;

namespace TravelExpenseTracker.ViewModels
{
    public partial class TripsViewModel : ObservableObject
    {
        public ObservableCollection<TripModel> Trips { get; set; } = [];

        [RelayCommand]
        private void AddTripTemp()
        {
            Trips.Add(new TripModel("logo.png", "Trip to Paris", "Paris, France"));
            Trips.Add(new TripModel("trip_image.png", "Trip to Tokyo", "Tokyo, Japan"));
            Trips.Add(new TripModel("logo.png", "Trip to New York", "New York, USA"));
            Trips.Add(new TripModel("logo.png", "Trip to Sydney", "Sydney, Australia"));
        }
    }
}
