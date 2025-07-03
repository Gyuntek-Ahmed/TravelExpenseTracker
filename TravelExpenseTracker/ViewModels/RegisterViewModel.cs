using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.APIs;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthApi _authApi;

        public RegisterViewModel(IAuthApi authApi)
        {
            _authApi = authApi;
        }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        private async Task NavigateBackAsync()
            => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await ErrorAlertAsync("Моля, попълнете всички полета.");
                return;
            }

            var registerDto = new RegisterDto
            {
                Name = Name,
                Email = Email,
                Password = Password
            };

            await MakeApiCall(async () =>
            {
                var result = await _authApi.RegisterAsync(registerDto);
                if (!result.IsSuccess)
                {
                    await ErrorAlertAsync(result.Error!);
                    return;
                }

                await ToastAsync("Регистрацията е успешна!");

                await NavigateBackAsync();
            });
        }
    }
}
