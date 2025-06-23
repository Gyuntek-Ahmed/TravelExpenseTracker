using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.ViewModels;
using DevExpress.Maui;
using DevExpress.Maui.Core;

namespace TravelExpenseTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            ThemeManager.UseAndroidSystemColor = false;
            ThemeManager.Theme = new Theme(ThemeSeedColor.TealGreen);

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseDevExpress(useLocalization: false)
                .UseDevExpressCollectionView()
                .UseDevExpressControls()
                .UseDevExpressDataGrid()
                .UseDevExpressEditors()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                })
                .UseMauiCommunityToolkit();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder
                .Services
                .AddTransient<LoginViewModel>()
                .AddTransient<LoginPage>();
            builder
                .Services
                .AddTransient<RegisterViewModel>()
                .AddTransient<RegisterPage>();

            return builder.Build();
        }
    }
}
