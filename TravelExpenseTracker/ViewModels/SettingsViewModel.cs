using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.Pages;

namespace TravelExpenseTracker.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty, NotifyPropertyChangedFor(nameof(Initial))]
        private string _name = "Gyuntek Ahmed";

        public string Initial => Name[0].ToString().ToUpper();

        [RelayCommand]
        private async Task NavigateToManageCategoriesAsync()
            => await Shell.Current.GoToAsync(nameof(ManageCategoriesPage));
    }
}
