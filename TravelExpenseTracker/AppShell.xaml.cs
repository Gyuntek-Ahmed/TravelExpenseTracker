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
        }
    }
}
