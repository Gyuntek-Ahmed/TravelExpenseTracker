using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TravelExpenseTracker.ViewModels
{
    public partial class SaveExpenseViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task GoBackAsync()
            => await Shell.Current.GoToAsync("..");
    }
}
