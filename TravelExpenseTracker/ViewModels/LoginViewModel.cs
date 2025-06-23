using CommunityToolkit.Mvvm.ComponentModel;

namespace TravelExpenseTracker.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;
    }
}
