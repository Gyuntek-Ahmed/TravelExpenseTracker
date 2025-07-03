using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.APIs;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Services;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthApi _authApi;
        private readonly AuthService _authService;

        public LoginViewModel(IAuthApi authApi, AuthService authService)
        {
            _authApi = authApi;
            _authService = authService;
        }

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
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await ErrorAlertAsync("Моля, въведете валиден имейл и парола.");
                return;
            }

            var newLoginDto = new LoginDto
            {
                Email = Email,
                Password = Password
            };

            await MakeApiCall(async () =>
            {
                var result = await _authApi.LoginAsync(newLoginDto);
                if (!result.IsSuccess)
                {
                    await ErrorAlertAsync(result.Error!);
                    return;
                }

                // Store the token
                var token = result.Data;
                _authService.SetToken(token);
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            });
        }
    }
}
