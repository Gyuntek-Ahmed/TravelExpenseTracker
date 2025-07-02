using TravelExpenseTracker.Pages;

namespace TravelExpenseTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(ManageCategoriesPage), typeof(ManageCategoriesPage));
            Routing.RegisterRoute(nameof(TripDetailsPage), typeof(TripDetailsPage));
            Routing.RegisterRoute(nameof(SaveExpensePage), typeof(SaveExpensePage));
        }
    }
}
