using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class TripDetailsPage : ContentPage
{
	public TripDetailsPage(TripDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}