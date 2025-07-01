using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class ManageCategoriesPage : ContentPage
{
	public ManageCategoriesPage(ManageCategoriesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}