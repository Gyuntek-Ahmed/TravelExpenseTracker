using TravelExpenseTracker.ViewModels;

namespace TravelExpenseTracker.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}