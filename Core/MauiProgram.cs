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
                })
                .RegisterViewModels()
                //.RegisterComponents()
                .RegisterViews()
                .ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                    // Configure window chrome appearance
                    events.AddWindows(events =>
                    {
                        events.OnWindowCreated(w =>
                        {
                            WindowConfigurator.Configure(w);
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
        /// Register common components and controls in DI container.
        /// </summary>
        public static MauiAppBuilder RegisterComponents(this MauiAppBuilder builder)
        {
            // ...
            return builder;
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
