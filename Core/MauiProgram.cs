using Core.Platforms.Windows;
using Core.ViewModels;
using Core.Views.Desktop;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

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
                .RegisterViews()
                .ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                    // Configure window chrome appearance
                    events.AddWindows(events =>
                    {
                        events.OnWindowCreated(wnd =>
                        {
                            WindowConfigurator.Configure();
                        });
                    });
#endif  
                });

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
            builder.Services.AddSingleton<PlaylistsPage>();
#endif

            return builder;
        }

        /// <summary>
        /// Register viewmodels in DI container.
        /// </summary>
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<AppViewModel>();
            builder.Services.AddSingleton<PlaylistsViewModel>();

            return builder;
        }
    }
}
