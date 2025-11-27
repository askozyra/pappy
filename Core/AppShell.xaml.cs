using Core.ViewModels;
using Microsoft.UI.Xaml.Controls;
using SolidColorBrush = Microsoft.UI.Xaml.Media.SolidColorBrush;

namespace Core
{
    public partial class AppShell : Shell
    {
        private SolidColorBrush _appBackgroundColor =
            new SolidColorBrush(Windows.UI.Color.FromArgb(255, 42, 42, 42)); // #2A2A2A

        public AppShell(AppShellViewModel appShellVM)
        {
            InitializeComponent();

            BindingContext = appShellVM;

            Loaded += AppShell_Loaded;
            HandlerChanged += AppShell_HandlerChanged;
        }

        private void AppShell_Loaded(object? sender, EventArgs e)
        {
            FlyoutIsPresented = false;
        }

        private void AppShell_HandlerChanged(object? sender, EventArgs e)
        {
            if (Handler?.PlatformView is NavigationView navView)
            {
                navView.Background = _appBackgroundColor;
            }
        }
    }
}