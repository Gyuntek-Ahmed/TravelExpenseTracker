using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }

}
