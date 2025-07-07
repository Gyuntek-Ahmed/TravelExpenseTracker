using System.Threading.Tasks;
using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class TripsPage : ContentPage
{
    private readonly TripsViewModel _viewModel;

    public TripsPage(TripsViewModel viewModel)
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