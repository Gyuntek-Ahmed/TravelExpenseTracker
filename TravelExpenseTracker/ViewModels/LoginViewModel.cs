using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.Pages;

namespace TravelExpenseTracker.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        private async Task NavigateToRegisterAsync()
            => await Shell.Current.GoToAsync(nameof(RegisterPage));

        [RelayCommand]
        private async Task LoginAsync()
        {
            // Login Validation
            // Call the API
            // Redirect to Home/Main page

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}
