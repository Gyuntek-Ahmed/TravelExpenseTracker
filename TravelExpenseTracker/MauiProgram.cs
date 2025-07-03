using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TravelExpenseTracker.Pages;
using TravelExpenseTracker.ViewModels;
using DevExpress.Maui;
using DevExpress.Maui.Core;
using Refit;
using TravelExpenseTracker.APIs;
using Microsoft.Extensions.DependencyInjection;
using TravelExpenseTracker.Services;

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
            builder
                .Services
                .AddSingleton<HomeViewModel>()
                .AddSingleton<MainPage>();
            builder
                .Services
                .AddTransient<TripsViewModel>()
                .AddTransient<TripsPage>();
            builder
                .Services
                .AddTransient<SettingsViewModel>()
                .AddTransient<SettingsPage>();
            builder
                .Services
                .AddTransient<SaveTripViewModel>()
                .AddTransient<SaveTripPage>();
            builder
                .Services
                .AddTransient<ManageCategoriesViewModel>()
                .AddTransient<ManageCategoriesPage>();
            builder
                .Services
                .AddTransient<TripDetailsViewModel>()
                .AddTransient<TripDetailsPage>();
            builder
                .Services
                .AddTransient<SaveExpenseViewModel>()
                .AddTransient<SaveExpensePage>();
            builder
                .Services
                .AddSingleton<AuthService>();

            ConfigureRefit(builder.Services);

            return builder.Build();
        }

        private static void ConfigureRefit(IServiceCollection services)
        {
            const string ApiBaseUrl = "https://8b51t5nc-7091.euw.devtunnels.ms/";

            services
                .AddRefitClient<IAuthApi>()
                .ConfigureHttpClient(SetHttpClient);

            services
                .AddRefitClient<ITripsApi>(GetRefitSettings)
                .ConfigureHttpClient(SetHttpClient);

            services
                .AddRefitClient<IExpensesApi>(GetRefitSettings)
                .ConfigureHttpClient(SetHttpClient);

            services
                .AddRefitClient<IProfileApi>(GetRefitSettings)
                .ConfigureHttpClient(SetHttpClient);

            static RefitSettings GetRefitSettings(IServiceProvider sp)
            {
                var authService = sp.GetRequiredService<AuthService>();
                return new RefitSettings
                {
                    AuthorizationHeaderValueGetter = (_, __) => Task.FromResult(authService.Token ?? "")
                };
            }

            static void SetHttpClient(HttpClient client)
                => client.BaseAddress = new Uri(ApiBaseUrl);
            
        }
    }
}
