using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class SaveExpensePage : ContentPage
{
    private readonly SaveExpenseViewModel _viewModel;

    public SaveExpensePage(SaveExpenseViewModel viewModel)
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