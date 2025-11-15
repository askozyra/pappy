using Microsoft.UI.Xaml.Controls;

namespace Core
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Loaded += AppShell_Loaded;
            HandlerChanged += AppShell_HandlerChanged;
        }

        private void AppShell_Loaded(object? sender, EventArgs e)
        {
            FlyoutIsPresented = false;
        }

        private void AppShell_HandlerChanged(object? sender, EventArgs e)
        {
#if WINDOWS
            if (Handler?.PlatformView is NavigationView navView)
            {
                navView.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                            Windows.UI.Color.FromArgb(255, 42, 42, 42)); // #2A2A2A
            }
#endif
        }
    }
}
