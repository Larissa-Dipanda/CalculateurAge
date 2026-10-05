using Microsoft.Extensions.Logging;
using CalculateurAge.ViewModels;
using CalculateurAge.Views;

namespace CalculateurAge
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<CalculateurViewModel>(); // une seule instance partagée
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ResultatPage>();
            return builder.Build();
        }
    }
}
