using System.Threading.Tasks;
using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly HomeViewModel _viewModel;

        public MainPage(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.FetchTripsAsync();
        }
    }
}
