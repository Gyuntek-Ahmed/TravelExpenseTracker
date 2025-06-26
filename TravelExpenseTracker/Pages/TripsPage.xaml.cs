using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class TripsPage : ContentPage
{
	public TripsPage(TripsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}