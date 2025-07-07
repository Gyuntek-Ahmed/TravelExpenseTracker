using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TravelExpenseTracker.APIs;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.Services;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private readonly IProfileApi _profileApi;
        private readonly AuthService _authService;

        public SettingsViewModel(IProfileApi profileApi, AuthService authService)
        {
            _profileApi = profileApi;
            _authService = authService;
            Name = authService.User.Name;
        }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Initial))]
        private string _name = "";

        public string Initial
            =>
            !string.IsNullOrEmpty(Name) ?
            Name[0].ToString().ToUpper() :
            "";

        [RelayCommand]
        private async Task NavigateToManageCategoriesAsync()
            => await Shell.Current.GoToAsync(nameof(ManageCategoriesPage));

        [RelayCommand]
        private async Task UpdateNameAsync()
        {
            var newName = await Shell.Current.DisplayPromptAsync("Промяна на име", "Моля въведете ново име.", "Промени Име", "Откажи");

            if (string.IsNullOrWhiteSpace(newName))
            {
                await ToastAsync("Моля въведете валидно име.");
                return;
            }

            await MakeApiCall(async () =>
            {
                var result = await _profileApi.UpdateNameAsync(new SingleValueDto<string>(newName));
                if (!result.IsSuccess)
                {
                    await ErrorAlertAsync(result.Error!);
                    return;
                }

                var newJwtToken = result.Data;
                _authService.SetToken(newJwtToken);
                Name = _authService.User.Name;
                await ToastAsync("Името е променено успешно.");
            });
        }

        [RelayCommand]
        private async Task ChangePasswordAsync()
        {
            var newPass = await Shell.Current.DisplayPromptAsync("Промяна на парола", "Моля въведете нова парола.", "Промени Парола", "Откажи");

            if (string.IsNullOrWhiteSpace(newPass))
            {
                await ToastAsync("Моля въведете валидна парола");
                return;
            }

            await MakeApiCall(async () =>
            {
                var result = await _profileApi.ChangePasswordAsync(new SingleValueDto<string>(newPass));
                if (!result.IsSuccess)
                {
                    await ErrorAlertAsync(result.Error!);
                    return;
                }

                await ToastAsync("Паролата е променена успешно. Моля, влезте отново с новата парола.");
                await LogOutAsync();
            });
        }

        [RelayCommand]
        private async Task GoToMyGitHub()
            => await Launcher.OpenAsync("https://github.com/Gyuntek-Ahmed");

        [RelayCommand]
        private async Task LogOutAsync()
        {
            _authService.RemoveToken();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
