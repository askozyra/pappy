using Core.ViewModels;
using Core.Views.Desktop;
using Microsoft.Extensions.Logging;

namespace Core
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
                    fonts.AddFont("Inder-Regular.ttf", "Inder");
                    fonts.AddFont("PixelifySans-Regular.ttf", "Pixel");
                })
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        /// <summary>
        /// Register views in DI container.
        /// </summary>
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
#if WINDOWS
            builder.Services.AddTransient<PlaylistsPage>();
#endif

            return builder;
        }

        /// <summary>
        /// Register viewmodels in DI container.
        /// </summary>
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<AppViewModel>();
            builder.Services.AddTransient<PlaylistsViewModel>();

            return builder;
        }
    }
}
