using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using Refit;
using TravelExpenseTracker.Pages;

namespace TravelExpenseTracker.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        protected async Task MakeApiCall(Func<Task> apiCall)
        {
            try
            {
                IsBusy = true;
                await apiCall.Invoke();
            }
            catch (ApiException apiEx) when (apiEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await Shell.Current.DisplayAlert("Грешка", "Не сте влезли в системата. Моля, влезте отново.", "Вход");
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            catch (Exception ex)
            {
                await ErrorAlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async Task ErrorAlertAsync(string message)
         => await Shell.Current.DisplayAlert("Грешка", message, "OK");

        protected async Task ToastAsync(string message)
            => await Toast.Make(message).Show();
    }
}
