using CommunityToolkit.Maui;
using Core.Helpers;
using Core.ViewModels;
using Core.Views.Desktop;
using Microsoft.Extensions.Logging;

namespace Core
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
#if !WINDOWS
#error "The application supports Windows platform only."
#endif
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Inder-Regular.ttf", "Inder");
                    fonts.AddFont("PixelifySans-Regular.ttf", "Pixel");
                })
                .RegisterHelpers()
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterHelpers(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<AudioManager>();
            builder.Services.AddSingleton<AppStateService>();

            return builder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
#if WINDOWS
            builder.Services.AddTransient<PlayerPage>();
            builder.Services.AddTransient<LibraryPage>();
#endif

            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<AppViewModel>();
            builder.Services.AddTransient<PlayerViewModel>();
            builder.Services.AddTransient<LibraryViewModel>();

            return builder;
        }
    }
}
