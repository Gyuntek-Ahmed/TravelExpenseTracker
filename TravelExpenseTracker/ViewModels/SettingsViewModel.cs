using CommunityToolkit.Mvvm.ComponentModel;

namespace TravelExpenseTracker.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty, NotifyPropertyChangedFor(nameof(Initial))]
        private string _name = "Gyuntek Ahmed";

        public string Initial => Name[0].ToString().ToUpper();
    }
}
