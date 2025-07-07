using TravelExpenseTracker.Services;
using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;

    public LoginPage(LoginViewModel viewModel, AuthService authService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _authService = authService;
    }

    protected override async void OnAppearing()
    {
        if (!string.IsNullOrWhiteSpace(_authService.Token))
        {
            if (!_authService.IsTokenExpired(_authService.Token!))
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                return;
            }
            // Token is expired, remove it
            _authService.RemoveToken();
        }
    }
}