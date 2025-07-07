using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class ManageCategoriesPage : ContentPage
{
    private readonly ManageCategoriesViewModel _viewModel;

    public ManageCategoriesPage(ManageCategoriesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.FetchCategoriesAsync();
    }
}