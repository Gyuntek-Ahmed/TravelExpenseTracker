using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class SaveExpensePage : ContentPage
{
	public SaveExpensePage(SaveExpenseViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}